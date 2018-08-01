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
        [Fact]
        public void IOTest_Int32()
        {
            var src = new Pixel<Int32>(Width, Height);
            var dst = new Pixel<Int32>(Width, Height);

            byte[] rdm = new byte[8];
            for (var i = 0; i < src.pix.Length; i++)
            {
                random.NextBytes(rdm);
                src.pix[i] = PixelBitConverter.ToInt32(rdm, 0);
            }

            src.Save(tempPath);
            dst.Load(tempPath);

            Assert.Equal(src.pix, dst.pix);
        }
        /*","Int32");#>*/


        /*<#/*/
        [Theory(DisplayName = "IO / header skip", Skip = "未実装")]
        [InlineData(1, 1, 2)]
        [InlineData(2, 3, 5)]
        public void AddTest(int x, int y, int ans)
        {
            //Add(x, y).Is(ans);
        }
        /*/#>*/
    }
}

