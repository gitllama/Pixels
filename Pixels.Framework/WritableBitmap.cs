using Pixels.Standard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Pixels.Framework
{
    public static class WritableBitmapExtentions
    {
        public static void ToWriteableBitmap24(this Pixel<int> src, WriteableBitmap bitmap)
        {
            bitmap.Lock();

            PixelDeveloper.Demosaic(src.pix, bitmap.BackBuffer, bitmap.PixelWidth, bitmap.PixelHeight, bitmap.BackBufferStride);

            bitmap.AddDirtyRect(new Int32Rect(0, 0, bitmap.PixelWidth, bitmap.PixelHeight));
            bitmap.Unlock();
        }
    }
}
