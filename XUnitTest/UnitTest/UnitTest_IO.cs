using System;
using Xunit;
using Pixels;
using Pixels.IO;

namespace XUnitTest
{
    public class UnitTest_IO
    {
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


        [Fact(DisplayName = "IO / Save and Load")]
        public void IOTest()
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


        [Theory(DisplayName = "IO / header skip", Skip = "–¢ŽÀ‘•")]
        [InlineData(1, 1, 2)]
        [InlineData(2, 3, 5)]
        public void AddTest(int x, int y, int ans)
        {
            //Add(x, y).Is(ans);
        }







        //MemberData

        //[ClassData(typeof(TestDataClass))]

    }
}
