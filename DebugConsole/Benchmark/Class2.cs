using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
/*
<#@include file="T4Base.t4" once="true" #>
<# 
    var t = new string[]{"A", "B"}; 


    var _indent = "";
    void loop(int indent)
    {
        _indent = new string('\t', indent);
        WriteLine("*" + "/");
        PushIndent(_indent);
        ClearIndent();
    }
    void endloop()
    {
        PushIndent(_indent);
        Write("/" + "*");
        ClearIndent();   
    }

    void commentout(string code,int indent)
    {
        _indent = new string('\t', indent);
        PushIndent(_indent + "// ");
        foreach(var i in code.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
        {
            WriteLine(i.Trim());
        }
        ClearIndent();
    }

    void newmethod(string name)
    {
        WriteLine("*"+"/new/"+"*" + name);
    }
#>

 */
namespace ConsoleApp4
{
    public partial class Hoge
    {
        // 一度の記載のみの共通コードは生成時にコメントアウトする必要がある
        // 文字列はなるべくフィールドにまとめておいた方が記述しやすい

        #region フィールド

        /*<#/*/

        private string name = "nobody";
        private const string mayname = "Goro";

        /*/#>*/

        //<# commentout(@"

        private int month;
        private int date;

        //", 2); #>

        #endregion フィールド


        // プロパティーなどは生成側にすべて記載する
        // /**/でのコメントアウトは使用できないが""は使用できる

        #region プロパティ

        /*<# loop(2);
        var list1 = new List<(string key, string val)>();
        list1.Add(("A", "Aのプロパティ"));
        list1.Add(("B", "Bのプロパティ"));
        foreach(var i in list1){ #>

        /// <summary>
        /// <#= i.val #>
        /// </summary>
        private string _<#= i.key #>;
        public string <#= i.key #> { get; set; }

        <# } endloop(); #>*/

        #endregion プロパティ


        // メソッドなどT4生成前にデバッグのRunやIntelliSenseを機能させたい場合
        // 生成時にベースとなったコードのコメントアウト化を考慮する必要がある。

        #region メソッド

        /*<# var add = @"*/
        public int Add(int i) => (int)(i + i);
        /*"; #>*/

        /*<#= add.Replace("int", "double") #>*/

        /*<#= string.Join("", (new string[] { "byte", "uint" }).Select(x => add.Replace("int", x))) #>*/


        #endregion メソッド

        // ロジックの共通ブロックだけ抜出は面倒臭い
        // 生成を継承にしてオーバーライドの方が素直そう

        #region メソッド2

        /*<#/*/
        public int Count(int i)
        {
            int dst = 0;
            /*/#><# var loopblock = @"*/
            for (var n = i; n < 10; n++)
            {
                dst++;
            }
            /*"; #><#/*/
            return dst;
        }
        /*/#>*/

        public /*<# newmethod(@"*/double Count(double i)/*");#> */
        {
            double dst = 0;
            /*<#= loopblock #>*/
            return dst;
        }


        #endregion メソッド2
    }

}
