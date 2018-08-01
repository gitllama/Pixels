using Pixels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTest.Pixels
{
    public class UnitTestExtended
    {

        [Fact]
        public void IOTest_Map()
        {
            var src = new Pixel<int>(2000,1000);
            var dst = new Pixel<int>(2000, 1000);

            src.Map(dst, (x, y, s, d) => { d[x, y] = s[x, y]; });

           
        }

        [Fact]
        public void IOTest_Foreach()
        {

        }

    }
}
