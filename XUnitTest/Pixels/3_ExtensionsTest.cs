using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

using Pixels;
using Pixels.Extensions;
using System.IO;
using Xunit.Abstractions;

namespace XUnitTest
{

    public class Test3_Extensions : IDisposable
    {
        readonly ITestOutputHelper output;
        string path;

        public Test3_Extensions(ITestOutputHelper _output)
        {
            path = Directory.GetCurrentDirectory() + @"\Sample\sample.json";
            output = _output;
        }
        public void Dispose()
        {
            /* teardown */
            path = null;
        }



        [Theory]
        [InlineData("", "")]
        [InlineData("Normal", "")]
        [InlineData("Normal", "R")]
        public void Test_Fill(string palne, string cfa)
        {
            var expected = PixelBuilder.Create<Int32>(path);
            var actual1 = PixelBuilder.Create<Int32>(path);
            var actual2 = PixelBuilder.Create<Int32>(path);

            expected.MapSelf(i => 10, palne, cfa);
            actual1.Fill(10, palne, cfa);
            actual2.Fill(10, palne, cfa, true);

            Assert.Equal(expected.ToArray(), actual1.ToArray());
            Assert.Equal(expected.ToArray(), actual2.ToArray());
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("Normal", "")]
        [InlineData("Normal", "R")]
        public void Test_ForeachA(string palne, string cfa)
        {
            var src = PixelBuilder.Create<Int32>(path);
            Statistics.Random.Uniform(src);

            //32bit 環境だとlong や double の Atomic なアクセスは言語機能としてはカバー
            //してないので読み込みもInterlocked.Read必要

            double expected = 0;
            double actual = 0;
            object lockobj = new object();
            src.ForEach(n => expected += n, palne, cfa);
            src.ForEach(n =>
            {
                lock (lockobj)
                {
                    actual += n;
                }
                //Interlocked.Add(ref total, x);
            }, palne, cfa, true);

            Assert.Equal(expected, actual);
            output.WriteLine($"{expected} : {actual}" );
        }

        [Theory]
        [InlineData("Normal", "")]
        [InlineData("Normal", "R")]
        public void Test_ForeachB(string palne, string cfa)
        {
            var src = PixelBuilder.Create<Int32>(path);
            Statistics.Random.Uniform(src);

            int expected = 0;
            int actual = 0;
            object lockobj = new object();
            src.ForEach((x, y, p) =>
            {
                var hoge = (p[x - 1, y] + p[x + 1, y] + p[x, y - 1] + p[x, y + 1])/4;
                if(p[x, y] > hoge) expected++;
            }, palne, cfa);
            src.ForEach((x, y, p) =>
            {
                lock (lockobj)
                {
                    var hoge = (p[x - 1, y] + p[x + 1, y] + p[x, y - 1] + p[x, y + 1]) / 4;
                    if (p[x, y] > hoge) actual++;
                }
            }, palne, cfa, parallel: true);

            Assert.Equal(expected, actual);
            output.WriteLine($"{expected} : {actual}");
        }


        [Theory]
        [InlineData("", "")]
        [InlineData("Normal", "")]
        [InlineData("Normal", "Gr")]
        [InlineData("Normal", "R")]
        [InlineData("Normal", "B")]
        [InlineData("Normal", "Gb")]
        public void Test_Map(string palne, string cfa)
        {
            var src1 = PixelBuilder.Create<Int32>(path);
            var src2 = PixelBuilder.Create<Int32>(path);
            var dst1 = PixelBuilder.Create<Int32>(path);
            var dst2 = PixelBuilder.Create<Int32>(path);
            Statistics.Random.Uniform(src1);

            src1.Map(src2, (i) => i);

            src1.Map(src2, (i) => i + 1, palne, cfa);
            Assert.NotEqual(src1.ToArray(), src2.ToArray());
            src2.Map(src2, (i) => i - 1, palne, cfa, true);
            Assert.Equal(src1.ToArray(), src2.ToArray());

            if(palne == "")
            {
                var ex1 = Assert.ThrowsAny<Exception>(() => { src1.Map(dst1, (x, y, p) => (p[x + 1, y] + p[x - 1, y] + p[x, y + 1] + p[x, y - 1] + p[x, y]) / 5, palne, cfa); });
                var ex2 = Assert.ThrowsAny<Exception>(() => { src1.Map(dst2, (x, y, p) => (p[x + 1, y] + p[x - 1, y] + p[x, y + 1] + p[x, y - 1] + p[x, y]) / 5, palne, cfa, true); });
                output.WriteLine(ex1.ToString());
                output.WriteLine(ex2.ToString());
            }
            else
            {
                src1.Map(dst1, (x, y, p) => (p[x + 1, y] + p[x - 1, y] + p[x, y + 1] + p[x, y - 1] + p[x, y]) / 5, palne, cfa);
                src1.Map(dst2, (x, y, p) => (p[x + 1, y] + p[x - 1, y] + p[x, y + 1] + p[x, y - 1] + p[x, y]) / 5, palne, cfa, true);
                Assert.Equal(dst1.ToArray(), dst2.ToArray());
            }

        }

    }

    [LegacyJitX86Job, RyuJitX64Job]
    [ClrJob, CoreJob]
    public class ExtensionsBench
    {
        int Width = 2256;
        int Height = 1178;
        Random random = new Random();
        Pixel<Int32> src;

        public ExtensionsBench()
        {
            //src = new Pixel<Int32>(Width, Height);

            //byte[] rdm = new byte[8];
            //for (var i = 0; i < src.Size; i++)
            //{
            //    random.NextBytes(rdm);
            //    src[i] = PixelBitConverter.To<Int32>(rdm, 0);
            //}
        }

        //[Benchmark] public void Foreach0() => src.MapTest(src, (x,y,s,d) => d[x,y] = s[x,y] + 1 );
        //[Benchmark] public void Foreach1() => src.MapTest2(src, (x, y, s, d) => d[x, y] = s[x, y] + 1);

        //[Benchmark] public void Foreach0() => src.ForEach((n) => n + 1);
        //[Benchmark] public void Foreach1() => src.ForEach((n) => n + 1, parallel: true);
        //[Benchmark] public void Foreach2() => src.ForEach2((n) => n + 1);
        //[Benchmark] public void Foreach3() => src.ForEach2((n) => n + 1, parallel: true);
    }

}
