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

            Demosaic(src.pix, bitmap.BackBuffer, bitmap.PixelWidth, bitmap.PixelHeight, bitmap.BackBufferStride);

            bitmap.AddDirtyRect(new Int32Rect(0, 0, bitmap.PixelWidth, bitmap.PixelHeight));
            bitmap.Unlock();
        }

        
        public unsafe static void Demosaic(int[] src, IntPtr pin, int width, int height, int stride)
        {
            int channel = 3;
            int residue = stride - channel * width;

            Parallel.For(0, height, y =>
            {
                byte* p = (byte*)(pin);
                p += y * stride;
                var span = src.AsSpan().Slice(y * width, width);
                for (int x = 0; x < span.Length; x++)
                {
                    int M = span[x];
                    byte N = (byte)(M > Byte.MaxValue ? Byte.MaxValue : M < Byte.MinValue ? 0 : M);
                    //*p++ = ((N << 16) | (N << 8) | N);
                    *p++ = N;
                    *p++ = N;
                    *p++ = N;
                }
            });

            //for (int y = 0; y < height; ++y)
            //{
            //    for (int x = 0; x < width; ++x)
            //    {
            //        R = 0;
            //        B = 0;
            //        G = 0;

            //        *((int*)p) = ((R << 16) | (B << 8) | G);

            //        p += channel;
            //    }
            //    p += residue;
            //}
        }
    }
}
