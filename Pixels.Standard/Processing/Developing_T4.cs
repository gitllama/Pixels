
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Pixels.Standard
{
    //

    public static partial class PixelDeveloper
    {
        /**/

        /**/
        public static unsafe void Demosaic(Pixel<Double> src, IntPtr pin, int stride, bool parallel)
        {
            int channel = 3;
            int residue = stride - channel * src.Width;
            int width = src.Width;
            int height = src.Height;
            if (parallel)
            {
                Parallel.For(0, src.Height, y =>
                {
                    byte* p = (byte*)(pin);
                    p += y * stride;
                    var span = src.pix.AsSpan().Slice(y * width, width);
                    for (int x = 0; x < span.Length; x++)
                    {
                        var M = span[x];
                        byte N = (byte)(M > Byte.MaxValue ? Byte.MaxValue : M < Byte.MinValue ? 0 : M);
                        //*p++ = ((N << 16) | (N << 8) | N);
                        *p++ = N;
                        *p++ = N;
                        *p++ = N;
                    }
                });
            }
            else
            {
                var p = (byte*)pin;
                for (int y = 0; y < height; ++y)
                {
                    for (int x = y * width; x < y * width + width; ++x)
                    {
                        var M = src[x];
                        byte N = (byte)(M > Byte.MaxValue ? Byte.MaxValue : M < Byte.MinValue ? 0 : M);

                        *(p++) = N;
                        *(p++) = N;
                        *(p++) = N;
                    }
                    p += residue;
                }
            }
        }
        /**/

        /**/
        public static unsafe void Demosaic(Pixel<Single> src, IntPtr pin, int stride, bool parallel)
        {
            int channel = 3;
            int residue = stride - channel * src.Width;
            int width = src.Width;
            int height = src.Height;
            if (parallel)
            {
                Parallel.For(0, src.Height, y =>
                {
                    byte* p = (byte*)(pin);
                    p += y * stride;
                    var span = src.pix.AsSpan().Slice(y * width, width);
                    for (int x = 0; x < span.Length; x++)
                    {
                        var M = span[x];
                        byte N = (byte)(M > Byte.MaxValue ? Byte.MaxValue : M < Byte.MinValue ? 0 : M);
                        //*p++ = ((N << 16) | (N << 8) | N);
                        *p++ = N;
                        *p++ = N;
                        *p++ = N;
                    }
                });
            }
            else
            {
                var p = (byte*)pin;
                for (int y = 0; y < height; ++y)
                {
                    for (int x = y * width; x < y * width + width; ++x)
                    {
                        var M = src[x];
                        byte N = (byte)(M > Byte.MaxValue ? Byte.MaxValue : M < Byte.MinValue ? 0 : M);

                        *(p++) = N;
                        *(p++) = N;
                        *(p++) = N;
                    }
                    p += residue;
                }
            }
        }
        /**/
    }
}

