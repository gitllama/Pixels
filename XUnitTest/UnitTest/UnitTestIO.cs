using System;
using Xunit;
using Pixels;
using Pixels.IO;

/*<#@ include file="Base.t4" #>*/
namespace XUnitTest.Pixels
{
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
        [InlineData(2256, 1178, 0)]
        [InlineData(2256, 1178, 16)]
        public void IOTest_Int32(int w, int h, int offset)
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
            dst.Load(tempPath);

            Assert.Equal(src.pix, dst.pix);
        }
        /*","Int32");#>*/

    }
}


//var a = src.pix.AsMemory().Pin();
//var b = a.Pointer;
//src.pix.AsMemory().Length;



//fixed (Int32* pin = src.pix.AsSpan<byte>())
//{
//    for(var i = 0;i<sizeof())
//}
//foreach(ref var i in src.pix.AsSpan())
//{

//    i = (Int32)rnd.Next(Int32.MinValue, Int32.MaxValue);
//}