using System;
using Xunit;
using Pixels;
using Pixels.IO;
using BenchmarkDotNet.Attributes;

/*
<#@ output extension=".generated.cs" #>
<#@ include file="Base.t4" #>
*/

namespace XUnitTest
{
    /*<#/*/
    [LegacyJitX86Job, RyuJitX64Job]
    [ClrJob, CoreJob]
    public class Bench_IO
    {
        int Width = 2256;
        int Height = 1178;
        Random random = new Random();
        string tempPath = "temp.bin";
        Pixel<Int32> dst;
        public Bench_IO()
        {
            var src = new Pixel<Int32>(Width, Height);
            dst = new Pixel<Int32>(Width, Height);
            byte[] rdm = new byte[8];
            for (var i = 0; i < src.pix.Length; i++)
            {
                random.NextBytes(rdm);
                src.pix[i] = PixelBitConverter.ToInt32(rdm, 0);
            }
            src.Save(tempPath);
        }

        [Benchmark] public void Load0() => dst.Load(tempPath, 0);
        [Benchmark] public void Load128() => dst.Load(tempPath, 128);
        [Benchmark] public void Load2256() => dst.Load(tempPath, 2256);

        [Benchmark] public void LoadFunc0() => dst.Load<int,int>(tempPath, i=>i, 0);
        [Benchmark] public void LoadFunc128() => dst.Load<int, int>(tempPath, i => i, 128);
        [Benchmark] public void LoadFunc2256() => dst.Load<int, int>(tempPath, i => i, 2256);
    }
    /*/#>*/

    public partial class UnitTest_IO
    {
        #region MyRegion
        /*<#/*/
        int Width = 2256;
        int Height = 1178;
        Random random = new Random();
        string tempPath = "temp.bin";

        public UnitTest_IO()
        {
            // test setup
        }

        public void Dispose()
        {
            // teardown
        }
        /*/#>*/
        #endregion


        /*<# Method(@"*/
        [Theory]
        [InlineData(2256, 1178, 0 ,0)]
        [InlineData(2256, 1178, 16,0)]
        [InlineData(2256, 1178, 2256,0)]
        public void IOTest_Int32(int w, int h, int bufsize, int offset)
        {
            var src = new Pixel<Int32>(w, h);
            var dst = new Pixel<Int32>(w, h);

            byte[] rdm = new byte[8];
            for (var i = 0; i < src.pix.Length; i++)
            {
                random.NextBytes(rdm);
                src.pix[i] = PixelBitConverter.ToInt32(rdm, 0);
            }

            src.Save(tempPath); //ヘッダーの入力つくりたい
            dst.Load(tempPath, bufsize);

            Assert.Equal(src.pix, dst.pix);
        }
        /*","Int32");#>*/

        /*<#/*/
        [Theory]
        [InlineData(2256, 1178, 0)]
        [InlineData(2256, 1178, 16)]
        [InlineData(2256, 1178, 2256)]
        public void IOTest_Func(int w, int h, int bufsize)
        {
            var src = new Pixel<Int32>(w, h);
            var dst = new Pixel<Int32>(w, h);

            byte[] rdm = new byte[8];
            for (var i = 0; i < src.pix.Length; i++)
            {
                random.NextBytes(rdm);
                src.pix[i] = PixelBitConverter.ToInt32(rdm, 0);
            }

            src.Save(tempPath);
            dst.Load<int,int>(tempPath, (i)=> i, bufsize);

            Assert.Equal(src.pix, dst.pix);
        }





        /*/#>*/
    }
}



