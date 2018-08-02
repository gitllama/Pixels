using System;
using Xunit;
using Pixels;
using Pixels.IO;
using BenchmarkDotNet.Attributes;

/*











*/

namespace XUnitTest
{
    /**/

    public partial class UnitTest_IO
    {
        #region MyRegion
        /**/
        #endregion


        /**/
        [Theory]
        [InlineData(2256, 1178, 0 ,0)]
        [InlineData(2256, 1178, 16,0)]
        [InlineData(2256, 1178, 2256,0)]
        public void IOTest_Byte(int w, int h, int bufsize, int offset)
        {
            var src = new Pixel<Byte>(w, h);
            var dst = new Pixel<Byte>(w, h);

            byte[] rdm = new byte[8];
            for (var i = 0; i < src.pix.Length; i++)
            {
                random.NextBytes(rdm);
                src.pix[i] = PixelBitConverter.ToByte(rdm, 0);
            }

            src.Save(tempPath); //ヘッダーの入力つくりたい
            dst.Load(tempPath, bufsize);

            Assert.Equal(src.pix, dst.pix);
        }
        /**/
        [Theory]
        [InlineData(2256, 1178, 0 ,0)]
        [InlineData(2256, 1178, 16,0)]
        [InlineData(2256, 1178, 2256,0)]
        public void IOTest_Int16(int w, int h, int bufsize, int offset)
        {
            var src = new Pixel<Int16>(w, h);
            var dst = new Pixel<Int16>(w, h);

            byte[] rdm = new byte[8];
            for (var i = 0; i < src.pix.Length; i++)
            {
                random.NextBytes(rdm);
                src.pix[i] = PixelBitConverter.ToInt16(rdm, 0);
            }

            src.Save(tempPath); //ヘッダーの入力つくりたい
            dst.Load(tempPath, bufsize);

            Assert.Equal(src.pix, dst.pix);
        }
        /**/
        [Theory]
        [InlineData(2256, 1178, 0 ,0)]
        [InlineData(2256, 1178, 16,0)]
        [InlineData(2256, 1178, 2256,0)]
        public void IOTest_Int24(int w, int h, int bufsize, int offset)
        {
            var src = new Pixel<Int24>(w, h);
            var dst = new Pixel<Int24>(w, h);

            byte[] rdm = new byte[8];
            for (var i = 0; i < src.pix.Length; i++)
            {
                random.NextBytes(rdm);
                src.pix[i] = PixelBitConverter.ToInt24(rdm, 0);
            }

            src.Save(tempPath); //ヘッダーの入力つくりたい
            dst.Load(tempPath, bufsize);

            Assert.Equal(src.pix, dst.pix);
        }
        /**/

        /**/
    }
}



