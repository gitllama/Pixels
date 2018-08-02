using BenchmarkDotNet.Attributes;
using Pixels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTest
{
    [LegacyJitX86Job, RyuJitX64Job]
    [ClrJob, CoreJob]
    public class BenchExtended
    {
        int Width = 2256;
        int Height = 1178;
        Random random = new Random();
        Pixel<Int32> src;

        public BenchExtended()
        {
            src = new Pixel<Int32>(Width, Height);

            byte[] rdm = new byte[8];
            for (var i = 0; i < src.Size; i++)
            {
                random.NextBytes(rdm);
                src[i] = PixelBitConverter.ToInt32(rdm, 0);
            }
        }

        //[Benchmark] public void Foreach0() => src.ForEach((n) => n + 1);
        //[Benchmark] public void Foreach1() => src.ForEach((n) => n + 1, parallel: true);
        //[Benchmark] public void Foreach2() => src.ForEach2((n) => n + 1);
        //[Benchmark] public void Foreach3() => src.ForEach2((n) => n + 1, parallel: true);
    }

    public class UnitTestExtended
    {
 
        [Fact]
        public void IOTest_Map()
        {
            var src = new Pixel<int>(2000,1000);
            var dst1 = new Pixel<int>(2000, 1000);
            var dst2 = new Pixel<int>(2000, 1000);

            //byte[] rdm = new byte[8];
            //for (var i = 0; i < src.pix.Length; i++)
            //{
            //    random.NextBytes(rdm);
            //    src.pix[i] = PixelBitConverter.ToInt32(rdm, 0);
            //}

            //src.Map(dst1, (x, y, s, d) => { d[x, y] = s[x, y] + 1; });
            //src.Map(dst2, (x, y, s, d) => { d[x, y] = s[x, y] + 1; }, parallel: true);

            //Assert.Equal(dst1.pix, dst2.pix);
        }

        [Theory]
        [InlineData(2256, 1178)]
        public void Test_Foreach(int w, int h)
        {
            var src1 = new Pixel<int>(w, h);
            var src2 = new Pixel<int>(w, h);

            //byte[] rdm = new byte[8];
            //for (var i = 0; i < src1.pix.Length; i++)
            //{
            //    random.NextBytes(rdm);
            //    src1.pix[i] = PixelBitConverter.ToInt32(rdm, 0);
            //    src2.pix[i] = PixelBitConverter.ToInt32(rdm, 0);
            //}

            //src1.ForEach((n) => n + 1);
            //src2.ForEach((n) => n + 1, parallel: true);

            //Assert.Equal(src1.pix, src2.pix);
        }

    }
}
