using System;
using Xunit;
using Pixels;
using Pixels.IO;

/*









*/
namespace XUnitTest.Pixels
{
    public partial class UnitTest_IO
    {
        #region MyRegion
        /**/
        #endregion


        /**/
        [Fact]
        public void IOTest_Byte()
        {
            var src = new Pixel<Byte>(Width, Height);
            var dst = new Pixel<Byte>(Width, Height);

            byte[] rdm = new byte[8];
            for (var i = 0; i < src.pix.Length; i++)
            {
                random.NextBytes(rdm);
                src.pix[i] = PixelBitConverter.ToByte(rdm, 0);
            }

            src.Save(tempPath);
            dst.Load(tempPath);

            Assert.Equal(src.pix, dst.pix);
        }
        /**/
        [Fact]
        public void IOTest_Int16()
        {
            var src = new Pixel<Int16>(Width, Height);
            var dst = new Pixel<Int16>(Width, Height);

            byte[] rdm = new byte[8];
            for (var i = 0; i < src.pix.Length; i++)
            {
                random.NextBytes(rdm);
                src.pix[i] = PixelBitConverter.ToInt16(rdm, 0);
            }

            src.Save(tempPath);
            dst.Load(tempPath);

            Assert.Equal(src.pix, dst.pix);
        }
        /**/
        [Fact]
        public void IOTest_Int24()
        {
            var src = new Pixel<Int24>(Width, Height);
            var dst = new Pixel<Int24>(Width, Height);

            byte[] rdm = new byte[8];
            for (var i = 0; i < src.pix.Length; i++)
            {
                random.NextBytes(rdm);
                src.pix[i] = PixelBitConverter.ToInt24(rdm, 0);
            }

            src.Save(tempPath);
            dst.Load(tempPath);

            Assert.Equal(src.pix, dst.pix);
        }
        /**/


        /**/
    }
}

