using System;
using Xunit;
using Pixels;
using Pixels.IO;
using BenchmarkDotNet.Attributes;
using System.Runtime.InteropServices;
using Pixels.Extensions;
using System.IO;
using System.Collections.Generic;

namespace XUnitTest
{

    public partial class Test2_IO : IDisposable
    {
        int Width = 2256;
        int Height = 1178;
        
        string tempPath = "temp.bin";

        public Test2_IO() { /* test setup */ }

        public void Dispose() { /* teardown */ }





        //public static IEnumerable<object[]> GetSource()
        //{
        //    yield return new object[] { new Pixel<Int16>(2256, 1178) };
        //    yield return new object[] { new Pixel<Int32>(2256, 1178) };
        //    yield return new object[] { new Pixel<Single>(2256, 1178) };
        //    yield return new object[] { new Pixel<Double>(2256, 1178) };
        //}

        //[Theory, MemberData(nameof(GetSource))]

        [Theory]
        [InlineData(2256, 1178, 0)]
        [InlineData(2256, 1178, 16)]
        [InlineData(2256, 1178, 2256)]
        public void TestIOInt32(int w, int h, int bufsize)
        {
            var src = PixelBuilder.Create<Int32>(w, h);
            var dst = src.Clone();

            Statistics.Random.Uniform(src);

            src.Save(tempPath);
            dst.Load(tempPath, bufsize);

            Assert.Equal(src.ToArray(), dst.ToArray());
        }


        [Theory]
        [InlineData(2256, 1178, 0)]
        [InlineData(2256, 1178, 16)]
        [InlineData(2256, 1178, 2256)]
        public void IOTest_Func(int w, int h, int bufsize)
        {
            var src = PixelBuilder.Create<Int32>(w, h);
            var dst = PixelBuilder.Create<Int32>(w, h);

            Statistics.Random.Uniform(src);

            src.Save(tempPath);
            dst.Load<int,int>(tempPath, (i)=> i, bufsize);

            Assert.Equal(src.ToArray(), dst.ToArray());
        }


        [Theory]
        [InlineData(2256,1178,@"\Sample\sample.json")]
        public void Test_IOTemplate(int w, int h,string path)
        {
            var src = PixelBuilder.Create<Int32>(w, h);
            Statistics.Random.Uniform(src);

            src.Save(tempPath);

            var dst = PixelBuilder.Create<int>(Directory.GetCurrentDirectory() + path);
            dst.Load<int>(tempPath);

            Assert.Equal(src.ToArray(), dst.ToArray());
        }


    }


    [LegacyJitX86Job, RyuJitX64Job]
    [ClrJob, CoreJob]
    public class IOBench
    {
        int Width = 2256;
        int Height = 1178;
        Random random = new Random();
        string tempPath = "temp.bin";
        Pixel<Int32> dst;
        public IOBench()
        {
            //var src = new Pixel<Int32>(Width, Height);
            //dst = new Pixel<Int32>(Width, Height);
            //byte[] rdm = new byte[8];
            //for (var i = 0; i < src.Size; i++)
            //{
            //    random.NextBytes(rdm);
            //    src[i] = PixelBitConverter.To<Int32>(rdm, 0);
            //}
            //src.Save(tempPath);
        }

        [Benchmark] public void Load0() => dst.Load(tempPath, 0);
        [Benchmark] public void Load128() => dst.Load(tempPath, 128);
        [Benchmark] public void Load2256() => dst.Load(tempPath, 2256);

        [Benchmark] public void LoadFunc0() => dst.Load<int, int>(tempPath, i => i, 0);
        [Benchmark] public void LoadFunc128() => dst.Load<int, int>(tempPath, i => i, 128);
        [Benchmark] public void LoadFunc2256() => dst.Load<int, int>(tempPath, i => i, 2256);
    }

}



