# がんばってチューニングしてみるさー

C#7(2017/3)～C#7.3(2018/未定)を中心に

***

## 1. Unsafeは減らしたい

（safetyなC#なんだから）Unsafeはなるべく使いたいたくないが

- unmanagedポインタのメリット
  - 参照渡しでコピーの発生を減らせる
  - 境界チェックがない為オーバーヘッドが減らせる
  - 型キャストが軽い
- 配列to配列はコンパイル最適化で境界チェック外しずらい
- objectやdynamicのキャストは重い
- 低レベルアクセスとの仲介しないといけないので無理して我慢することない。

***

### 1-1. 大きな値は参照渡（ref演算子）

int程度のサイズの型だと（ポインタアドレスのサイズと変わらないので）メリットはないが、大きな構造体だとコピーのオーバーヘッドを減らせる。

（たとえ値渡しでもインライン展開で値コピーが消えることがあるので、インライン展開可能かどうかで確認が必要）

```cs
struct Complex
{
  static void Main()
  {
    ref var b = ref a;    // ローカルでの参照渡し C#7
    x > y ? ref x : ref y // 条件演算子ref C# 7.2  
    b = ref c;            // refの再代入 C#7.3

    Swap(ref a, ref b);
    ref var y = ref Ref(ref x); // 参照戻り C# 7
    F(in a);                    // 読み取り専用参照渡し C# 7.2

    for (ref int i = ref x; i < array.Length; i++){}  // C# 7.3
    foreach (ref var x in array.AsRef()){}            // C# 7.3

    int.TryParse(s, out int x) ? x : default(int?);   // 出力変数宣言　C# 7
  }

    //演算子オーバーロードでの参照渡し C#7.2
    public static Complex operator +(in Complex a, in Complex b)
      => new Complex(a.X + b.X, a.Y + b.Y);
}
public static class Extensions
{
    // 参照渡しの拡張メソッド C#7.2
    public static void Conjugate(ref this A q){}
    public static A Rotate(in this A p, in A q){}
}
```

#### 例
indexを減らす
```cs
static ref int RefMax(int[] array)
{
    if (array.Length == 0) throw new InvalidOperationException();
    // var maxIndex = 0;
    ref var max = ref array[0];

    // if (array[maxIndex] < array[i])
    // {
    //     array[maxIndex] = array[i];
    //     maxIndex = i;
    // }
    for (int i = 1; i < array.Length; i++)
    {
        ref var x = ref array[i];
        if (max < x) max = ref x;
    }

    return ref max;
}
```
```cs
using System;

struct Point
{
    public int X, Y, Z;

    public ref int At(int index)
    {
        switch (index)
        {
            // インスタンス メソッド(プロパティ、インデクサー)では以下の ref が認められていない(コンパイル エラー)
            case 0: return ref X;
            case 1: return ref Y;
            case 2: return ref Z;
            default: throw new IndexOutOfRangeException();
        }
    }
}

static class PointExtensions
{
    public static ref int At(ref this Point p, int index)
    {
        switch (index)
        {
            // インスタンス メソッド版とやっていることは同じでも、こちらは OK
            case 0: return ref p.X;
            case 1: return ref p.Y;
            case 2: return ref p.Z;
            default: throw new IndexOutOfRangeException();
        }
    }
}
```

#### 注：refでパフォーマンスが上がらない例

- int程度のサイズの型だと（ポインタアドレスのサイズと変わらない）
- コンパイラがインライン展開可能な場合は（そもそもコピーが発生しないので）そちらの方がパフォーマンスが出る
- redonlyがいる構造体（参照を作らない保証をしないといけない/readonly structなら関係ない）
- スタックにしか参照は作れない = クロージャ/イテレータ/非同期で使用できない

***
[参照渡し](https://ufcpp.net/study/csharp/sp_ref.html)
***

### 1-2. Span<T>

配列の参照範囲を切りなおすことでコンパイラ側の最適化をかかりやすくできる。

複数配列間の境界チェックは無理だし、.Net Core2.1以降でないとslow Span（最大限の恩恵を受けられない）ので現状最適化の面ではそこまでの恩恵を受けることができない

stackallocが使用できるのでスタックの利用が楽になる

```cs
Span<int> x1 = stackalloc int[3] { 0xEF, 0xBB, 0xBF }; //C#7.3

// ref 構造体を持てるのは ref 構造体だけ
ref struct RefStruct
{
    private Span<int> _span; //OK
}

//Core2.1ではストリームからの読み出しを直接Spanにとれる
rest -= stream.Read(buffer);
```

***
[Span構造体](https://ufcpp.net/study/csharp/resource/span/)
***

### 1-3. fixed

```cs
readonly struct Array<T>
{
    private readonly T[] _array;
    public ref T GetPinnableReference() => ref _array[0]; //C#7.3
}
static unsafe void Main(string[] args)
{
    var a = new Array<int>(5);
    // fixed (int* p = &a.GetPinnableReference()) に展開される。
    fixed (int* p = a){ }
}
```
### 1-4. where T : unmanaged

もともと、ジェネリックなクラス/関数内で```T*```は取得することができなかったが、C#7.3よりunmanaged制約を付けることができるようになっている。これにより、ジェネリック関数内でポインターの使用が可能となった。

基本的にはC++のtemplateでは全てインライン展開され、C#でも値型であれば（ボックス化を避ける為）展開されるため実行効率を犠牲にすることはない。

また、C# リファレンスによると

> unmanaged 制約は、class や struct 制約と組み合わせることはできません。 unmanaged 制約は struct にする必要がある型を適用します

とのことであるが、ILSpyを使用したところ、unmanaged制約はstruct制約として展開されていることが確認できる。

```cs
// struct制約
public static void A<T>() where T : struct { }
// unmanaged制約
public static void B<[IsUnmanaged] T>() where T : struct, ValueType { }
// struct制約 IL
.method public hidebysig static void A<valuetype .ctor ([netstandard]System.ValueType) T> () cil managed
// unmanaged制約 IL
.method public hidebysig static void B<valuetype .ctor ([netstandard]System.ValueType modreq([netstandard]System.Runtime.InteropServices.UnmanagedType) ) T> () cil managed

```

### 1-5. System.Runtime.CompilerServices

```cs
using System.Runtime.CompilerServices;

unsafe void Main(){
  void* pointer = Unsafe.AsPointer(ref x);  
  ref int r = ref Unsafe.AsRef<int>(pointer);  
}
```

[参照渡しとポインターの相互変換](https://ufcpp.net/study/csharp/sp_ref.html?p=4)

## 2. ジェネリックを活用する

dynamicやobjectだとオーバヘッドが大きい。

### 2-1.

###

型分岐 C#7
ジェネリック型に対するパターン マッチング(型スイッチ) C#7.1



### インライン展開

```cs
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
```
ローカル関数かつクロージャ

### 構造体とマーシャリング

- [構造体からポインタ(バイト配列)への変換](﻿﻿http://schima.hatenablog.com/entry/20090512/1242139542)
- [ポインタ(バイト配列)から構造体への変換](http://schima.hatenablog.com/entry/20090514/1242305433)
- [C#でレガシーな事をする方向けのまとめ](https://qiita.com/hmuronaka/items/619f8889e36c7b5db92d)

### 固定サイズバッファ

- [固定サイズバッファによる高速化効果](http://xptn.dtiblog.com/blog-entry-94.html)
- [固定サイズバッファでの使用時のfixed](http://xptn.dtiblog.com/blog-entry-85.html)

### JITベースでの最適化の参考

- [さぁ fixed を捨てて Unsafe だ](https://azyobuzin.hatenablog.com/entry/2017/09/30/023703)
- [C#はunsafeの方が速いという幻想](http://aokomoriuta.hateblo.jp/entry/2016/05/05/145810)

###

- [ZeroFormatterに見るC#で最速のシリアライザを作成する100億の方法](https://www.slideshare.net/neuecc/zeroformatterc100)
- [C#/.Netでパフォーマンスのために意識すべきこと](https://amaya382.hatenablog.jp/entry/2017/01/30/194834)

### ビット演算

- [明日使えないすごいビット演算](https://www.slideshare.net/KMC_JP/slide-www)
- [Cのビット演算系のtips](https://gist.github.com/tchn/9d95658c37f6d251ed76)

### 並列処理

- [排他ロック](http://article.higlabo.com/ja/thread_locking.html)

## Note



// unsafe
    // {
    //     fixed (byte* pin = value.AsSpan().Slice(startindex, 3))
    //     {
    //        return *(Int24*)pin;
    //     }
    // }
    // だと1/4のパフォーマンス
