# UnitTest and Benchmark for Pixels

## Benchmark

## UnitTest

### UnitTest

- MsTest
- xUnitTest

#### MsTest

```cs
[TestClass]
public class HogeTest
{
	//Privateもとれる
    
	[TestMethod]
    public void Test()
    {
        var hoge = new Hoge();
       
        Assert.AreEqual("hoge", hoge.AsDynamic().GetString());
		Assert.AreNotEqual();
		Assert.AreSame();						//同一のオブジェクトであるか
		Assert.AreNotSame();
		Assert.IsInstanceOfType();				//指定の型であるか
		Assert.IsNotInstanceOfType();
		Assert.IsTrue();
		Assert.IsFlase();
		Assert.IsNull();
		Assert.IsNotNull();

		CollectionAssert.AreEqual();			//同一 : 同じ値の要素が同じ順番で同じ数
		CollectionAssert.AreNotEqual();		
		CollectionAssert.AreEquivalent();		//等価 : 同じ値の要素が同じ数
		CollectionAssert.AreNotEquivalent();
		CollectionAssert.Contains();			//指定の要素を含む
		CollectionAssert.DoesNotContain();
		CollectionAssert.IsSubsetOf();			//別のコレクションのサブセット (部分集合) 
		CollectionAssert.IsNotSubsetOf();

		AllItemsAreInstancesOfType();	//すべての要素が指定の型
		AllItemsAreNotNull();			//すべての要素がnullでない
		AllItemsAreUnique();			//すべての要素が一意

		StringAssert.Matches();			//指定の正規表現に一致する
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

#### xUnitTest

```cs
public class HogeTest
{
	private readonly ITestOutputHelper output;

	[Fact(DisplayName = "hogehoge")]

    public void Test()
    {
		Assert.Equal("test1", test);
		test.Is("test1");

		output.WriteLine("test end");	//Debug.WriteLine();動かないのでITestOutputHelper使う
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
		Add(x, y).Is(ans);
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
		str.Is("hogehoge");
		    
		var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => { return ReadAllTextAsync(null); });
	}

}
```