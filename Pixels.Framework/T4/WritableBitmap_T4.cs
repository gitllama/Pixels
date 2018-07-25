/*








*/

/* Code generated using the t4 templates */

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

namespace Pixels.Framework
{

    public static partial class WritableBitmapExtentions
    {

        #region Base
        /**/
        #endregion 


        #region T4

        // 
        public static void ToWriteableBitmap24(this Pixel<Double> src, WriteableBitmap bitmap, Options option = null)
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
         

        // 
        public static void ToWriteableBitmap24(this Pixel<Single> src, WriteableBitmap bitmap, Options option = null)
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
        

        #endregion





    }
}




