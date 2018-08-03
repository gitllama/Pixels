
### Unsafeは減らしたい

が、低レベルアクセスとの仲介しないといけないので、無理して我慢することない


### インライン展開

```cs
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
```

### 構造体とマーシャリング

[構造体からポインタ(バイト配列)への変換](﻿﻿http://schima.hatenablog.com/entry/20090512/1242139542)
[ポインタ(バイト配列)から構造体への変換](http://schima.hatenablog.com/entry/20090514/1242305433)
[C#でレガシーな事をする方向けのまとめ](https://qiita.com/hmuronaka/items/619f8889e36c7b5db92d)

### 固定サイズバッファ

[固定サイズバッファによる高速化効果](http://xptn.dtiblog.com/blog-entry-94.html)
[固定サイズバッファでの使用時のfixed](http://xptn.dtiblog.com/blog-entry-85.html)

### JITベースでの最適化の参考

[さぁ fixed を捨てて Unsafe だ](https://azyobuzin.hatenablog.com/entry/2017/09/30/023703)
[C#はunsafeの方が速いという幻想](http://aokomoriuta.hateblo.jp/entry/2016/05/05/145810)

### 並列処理

[排他ロック](http://article.higlabo.com/ja/thread_locking.html)

## Note

//ref int val =  dst.pix[index++]思ったより速度でない
// Span<byte> buffer = stackalloc byte[BufferSize]; //Core 2.1
// rest -= stream.Read(buffer); //Core 2.1

// unsafe
    // {
    //     fixed (byte* pin = value.AsSpan().Slice(startindex, 3))
    //     {
    //        return *(Int24*)pin;
    //     }
    // }
    // だと1/4のパフォーマンス
