# Development Process

- コードの開発
  - Visual Studio Community 2017
  - T4 Template
- IL/JIT Asmの確認
  - [SharpLab](https://sharplab.io/)
  - [ILSpy](https://github.com/icsharpcode/ILSpy)
- 単体テスト / ベンチマーク
  - xUnitTest
  - BenchmarkDotNet
- コードカバレッジ
  - 未定
- バージョン管理
  - Github

## 1. コードの開発

アジャイル（素早く反復を繰り返す）な開発を行う手法

- TDD (Test-Driven Development)
  - RED/GREEN/REFACTOR
  - テストファースト
- BDD (Behavior Driven Development)
  - Given/When/Then
- FDD (Feature-Driven Development)
- TiDD (Ticket-Driven Development)
  - コーディングの品質とは無関係
- MDD (Model-Driven Development)

別に仕様があるわけではない(インターフェースがない)のでTDD(テストワースト)やりたいわけではないが、Test⇔REFACTORのループはまわしましょう。

- 関心の分離

データ構造 / ビジネスロジック（データ操作） / IO をきれいに分離したい

- (DI)依存性の注入

とりあえずjsonでなんでもいれるようにしてみる

### T4 Template

```ファイルのプロパテ > カスタムツール```に```TextTemplatingFileGenerator```で自動生成可能

参考[RoslynをT4テンプレート内で使う](http://www.misuzilla.org/Blog/2015/12/04/UsingRoslynInT4Template)

## 2. IL/JIT Asmの確認

### ILSpy

.NET Standard / Core DLLは標準で```/deterministic```でのコンパイルになるらしく、そのままではILSpyでの確認ができない。  
ただ対応していないのは```.pdb```のみで、```.pdb```ファイルを消した場合読み込み可。

## 3. 単体テスト / ベンチマーク

プロジェクトを無暗に増やしたくなかったので、以下のような構成でBenchmarkdotnetとxUnitを混在させている。

別プロジェクトにした方が安定してそう。

### .csproj設定

#### BenchmarkDotNetのVer混在

[faq](https://benchmarkdotnet.org/articles/faq.html)によると

.Net Coreコンソールプロジェクトの```.csproj```を
```cs
<PropertyGroup>
  <OutputType>Exe</OutputType>
  <TargetFramework>netcoreapp2.1</TargetFramework>
</PropertyGroup>
```
↓
```cs
<PropertyGroup>
  <OutputType>Exe</OutputType>
  <TargetFrameworks>netcoreapp2.1;net47</TargetFrameworks>
  <PlatformTarget>AnyCPU</PlatformTarget>
  <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
</PropertyGroup>
```

とすることで、BenchmarkDotNetの```[ClrJob, CoreJob]```が有効となる

#### xUnitとの混在

加えて、xUnitとBenchmarkDotNetを混在させるため、xUnitテストプロジェクト(.Net Core)でプロジェクト作成し

```cs
<PropertyGroup>
  <OutputType>Exe</OutputType>
  <TargetFrameworks>netcoreapp2.1;net47</TargetFrameworks>
  <PlatformTarget>AnyCPU</PlatformTarget>
  <LangVersion>latest</LangVersion>
  <StartupObject>XUnitTest.Program</StartupObject>
  <DebugType>full</DebugType>
  <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
</PropertyGroup>
```

とすることで、
- C# 7.3
- Release RunでのBenchmarkRunnerのRun
- DebugType : カバレッジの有効化
- Usafeコードの有効化

を行っている

### UnitTest

- MsTest
- xUnitTest

等があるが、xUnitTestを使用し品質の管理を行う。  
Private Methodの期待値判定はリフレクションで呼ぶ必要あり。

#### xUnitTest記述例

```cs
public class HogeTest
{
    private readonly ITestOutputHelper output;

    [Fact(DisplayName = "hogehoge")]
    public void Test()
    {
        Assert.Equal("test1", test);
        output.WriteLine("test end");   //Debug.WriteLine();動かないのでITestOutputHelper使う
    }

    [Fact(DisplayName = "実行しない", Skip = "実行しない")]
    [Trait("Category", "Arithmetic")]
    [Trait("Priority", "2")]
    public void Test()
    {
        // skip : 実行skip
        // trait : タグ付け
    }

    [Theory]
    [InlineData(1, 1, 2)]
    [InlineData(2, 3, 5)]
    public void AddTest(int x, int y, int ans)
    {
        Assert.Equal(Add(x, y), ans);
    }

    [Fact]
    public void Test()
    {
        // 例外のチェック
        var ex = Assert.Throws<ArgumentNullException>(() => { hoge(); });
        ex.Message.Contains("null").IsTrue();

        var ex2 = Assert.ThrowsAny<ArgumentException>(() => { hoge(); });
    }

    [Fact]
    public async Task ReadAllTextReturnsFileContent()
    {
        var str = await ReadAllTextAsync("test");
        Assert.Equal(str, "hogehoge");
        var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => ReadAllTextAsync(null));
    }
}
```

#### (参考)MsTest

```CS
[TestClass]
public class HogeTest
{
    [TestMethod]
    public void Test()
    {
        var hoge = new Hoge();

        Assert.AreEqual("hoge", hoge.AsDynamic().GetString());
        Assert.AreNotEqual();
        Assert.AreSame();                       //同一のオブジェクトであるか
        Assert.AreNotSame();
        Assert.IsInstanceOfType();              //指定の型であるか
        Assert.IsNotInstanceOfType();
        Assert.IsTrue();
        Assert.IsFlase();
        Assert.IsNull();
        Assert.IsNotNull();

        CollectionAssert.AreEqual();            //同一 : 同じ値の要素が同じ順番で同じ数
        CollectionAssert.AreNotEqual();
        CollectionAssert.AreEquivalent();       //等価 : 同じ値の要素が同じ数
        CollectionAssert.AreNotEquivalent();
        CollectionAssert.Contains();            //指定の要素を含む
        CollectionAssert.DoesNotContain();
        CollectionAssert.IsSubsetOf();          //別のコレクションのサブセット (部分集合)
        CollectionAssert.IsNotSubsetOf();

        AllItemsAreInstancesOfType();   //すべての要素が指定の型
        AllItemsAreNotNull();           //すべての要素がnullでない
        AllItemsAreUnique();            //すべての要素が一意

        StringAssert.Matches();         //指定の正規表現に一致する
        StringAssert.DoesNotMatch();

        Contains();
        StartsWith();
        EndsWith();
    }

    [TestMethod]
    [ExpectedException(typeof(System.Exception))]
    public void TestMethod1()
    {
        //例外が発生すると成功
    }

    [TestMethod]
    [Ignore]
    public void TestMethod2()
    {
        //無効化
    }
}
```

### カバレッジ

[参考1](http://kouki-hoshi.hatenablog.com/entry/2017/05/20/060526)  
[参考2](https://budougumi0617.github.io/2017/07/13/opencover-to-vs2017/)
