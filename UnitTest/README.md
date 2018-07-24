
#

```C#
[TestClass]
public class HogeTest
{
    [TestMethod]
    public void テキストを取得する()
    {
        var hoge = new Hoge();
        //Privateもとれる
        Assert.AreEqual("hoge", hoge.AsDynamic().GetString());
    }
}
```