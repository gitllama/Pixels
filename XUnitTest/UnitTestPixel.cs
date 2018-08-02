using Pixels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace XUnitTest
{
    public class UnitTestPixel
    {
        readonly ITestOutputHelper output;

        [Fact]
        public void TestPixel()
        {
            var src = new PixelByte<int>(100, 100);

            src.pix[0] = 1;
            src.pix[1] = 3;
            src.pix[2] = 5;
            src.pix[3] = 8;

            output.WriteLine(src[0].ToString());

            //Assert.Equal(dst1.pix,dst2.pix);
        }

    }
}
