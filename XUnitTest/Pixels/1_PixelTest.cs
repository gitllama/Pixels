using System;
using Xunit;
using Pixels;
using Pixels.IO;
using Xunit.Abstractions;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using System.Runtime.CompilerServices;
using Pixels.DDL;
using System.IO;

namespace XUnitTest
{
    public class Test1_Pixel
    {
        Random random = new Random();


        [Theory]
        [InlineData(@"\Sample\sample.json")]
        public void TestFactory(string path)
        {
                    }

        [Fact]
        public unsafe void TestLockPixel()
        {
            //Pixel<int> a = new Pixel<int>(100, 100);
            //a[0] = 100;
            //a[1] = -100;
            //a[2] = 3;

            //using (var b = new LockPixel<int>(a))
            //{
            //    var i = (int*)b.ToPointer();
            //    Assert.Equal(a[0], *i++);
            //    Assert.Equal(a[1], *i++);
            //    Assert.Equal(a[2], *i++);
            //}
        }

    }

    [LegacyJitX86Job, RyuJitX64Job]
    [ClrJob, CoreJob]
    public class PixelBench
    {
        //byte[] bytes = new byte[8];
        //Pixel<Int32> src1 = new Pixel<int>(100,100);
        //PixelByte<Int32> src2 = new PixelByte<int>(100, 100);
        //Pixel<Int24> src3 = new Pixel<Int24>(100, 100);
        //PixelByte<Int24> src4 = new PixelByte<Int24>(100, 100);

        //public PixelBench()
        //{
        //    Random random = new Random();
        //    random.NextBytes(bytes);
        //}

        //[Benchmark] public int ReadPixel() => src1[0];
        //[Benchmark] public int ReadPixelByte() => src2[0];

        //[Benchmark] public void WritePixel() => src1[0] = 10;
        //[Benchmark] public void WritePixelByte() => src2[0] = 10;

        //[Benchmark] public int ReadPixel24() => src3[0];
        //[Benchmark] public int ReadPixelByte24() => src4[0];

        //[Benchmark] public int WritePixel24() => src3[0];
        //[Benchmark] public int WritePixelByte24() => src4[0];
    }

}

