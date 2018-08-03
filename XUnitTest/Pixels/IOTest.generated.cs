using System;
using Xunit;
using Pixels;
using Pixels.IO;
using BenchmarkDotNet.Attributes;
using System.Runtime.InteropServices;
using Pixels.Extensions;

/*












*/

namespace XUnitTest
{
    /**/

    public partial class IOTest
    {
        #region MyRegion
        /**/
        #endregion


        /**/
        [Theory]
        [InlineData(2256, 1178, 0 , 0)]
        [InlineData(2256, 1178, 16, 0)]
        [InlineData(2256, 1178, 2256, 0)]
        public void IOTest_Byte(int w, int h, int bufsize, int offset)
        {
            var src = new Pixel<Byte>(w, h);
            var dst = new Pixel<Byte>(w, h);

            Statistics.Random.Uniform(src);

            src.Save(tempPath);
            dst.Load(tempPath, bufsize);

            Assert.Equal(src.ToArray(), dst.ToArray());
        }
        /**/
        [Theory]
        [InlineData(2256, 1178, 0 , 0)]
        [InlineData(2256, 1178, 16, 0)]
        [InlineData(2256, 1178, 2256, 0)]
        public void IOTest_Int16(int w, int h, int bufsize, int offset)
        {
            var src = new Pixel<Int16>(w, h);
            var dst = new Pixel<Int16>(w, h);

            Statistics.Random.Uniform(src);

            src.Save(tempPath);
            dst.Load(tempPath, bufsize);

            Assert.Equal(src.ToArray(), dst.ToArray());
        }
        /**/
        [Theory]
        [InlineData(2256, 1178, 0 , 0)]
        [InlineData(2256, 1178, 16, 0)]
        [InlineData(2256, 1178, 2256, 0)]
        public void IOTest_Int24(int w, int h, int bufsize, int offset)
        {
            var src = new Pixel<Int24>(w, h);
            var dst = new Pixel<Int24>(w, h);

            Statistics.Random.Uniform(src);

            src.Save(tempPath);
            dst.Load(tempPath, bufsize);

            Assert.Equal(src.ToArray(), dst.ToArray());
        }
        /**/

        /**/
    }
}



