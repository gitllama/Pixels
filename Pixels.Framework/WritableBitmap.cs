using Pixels.Standard;
using Pixels.Standard.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

/*
<#@ include file="T4Base.t4" once="true" #>
*/

namespace Pixels.Framework
{
    public static partial class WritableBitmapExtentions
    {
        #region field

        /*<#/*/

        private const string mayname = "Goro";

        /*/#>*/

        #endregion

        #region [T4]ToWriteableBitmap24 

        /*<# Method(@"*/
        public static void ToWriteableBitmap24(this Pixel<Int32> src, WriteableBitmap bitmap, Options option = null)
        {
            bitmap.Lock();

            option = option ?? new Options();

            switch (option.bayer)
            {
                case Bayer.Mono:
                    PixelDeveloper.DemosaicMono(src, bitmap.BackBuffer, bitmap.BackBufferStride, option);
                    break;
                default:
                    PixelDeveloper.DemosaicColorParallel(src, bitmap.BackBuffer, bitmap.BackBufferStride, option);
                    break;
            }

            bitmap.AddDirtyRect(new Int32Rect(0, 0, bitmap.PixelWidth, bitmap.PixelHeight));
            bitmap.Unlock();
        }
        /*", types, (i,j)=> i.Replace("<Int32>",$"<{j}>"));#>*/

        #endregion
    }
}




