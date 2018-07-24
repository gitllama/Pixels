using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Pixels.Framework.sandbox
{
    public static class _PixelDeveloper
    {
        public static unsafe void Demosaic(int[] src, IntPtr pin, int width, int height, int stride)
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

        public static void Demosaic2(int[] src, IntPtr pin, int width, int height, int stride)
        {
            int channel = 3;
            int residue = stride - channel * width;

            var buffer = new byte[stride];
            for (int y = 0; y < height; ++y)
            {
                int index = 0;
                for (int x = y * width; x < y * width + width; ++x)
                {
                    int M = src[x];
                    byte N = (byte)(M > Byte.MaxValue ? Byte.MaxValue : M < Byte.MinValue ? 0 : M);
                    buffer[index++] = N;
                    buffer[index++] = N;
                    buffer[index++] = N;
                }
                Marshal.Copy(buffer, 0, pin, buffer.Length);
                pin += stride;
            }
        }

        public static void Demosaic3(int[] src, IntPtr pin, int width, int height, int stride)
        {
            int channel = 3;
            int residue = stride - channel * width;

            var buffer = new byte[stride];
            for (int y = 0; y < height; ++y)
            {
                int index = 0;
                var span = src.AsSpan().Slice(y * width, width);
                for (int x = 0; x < span.Length; ++x)
                {
                    int M = span[x];
                    byte N = (byte)(M > Byte.MaxValue ? Byte.MaxValue : M < Byte.MinValue ? 0 : M);
                    buffer[index++] = N;
                    buffer[index++] = N;
                    buffer[index++] = N;
                }
                Marshal.Copy(buffer, 0, pin, buffer.Length);
                pin += stride;
            }
        }

        public static unsafe void Demosaic4(int[] src, IntPtr pin, int width, int height, int stride)
        {
            int channel = 3;
            int residue = stride - channel * width;
            var p = (byte*)pin;
            for (int y = 0; y < height; ++y)
            {
                for (int x = y * width; x < y * width + width; ++x)
                {
                    int M = src[x];
                    byte N = (byte)(M > Byte.MaxValue ? Byte.MaxValue : M < Byte.MinValue ? 0 : M);

                    *(p++) = N;
                    *(p++) = N;
                    *(p++) = N;
                }
                p += residue;
            }
        }

        public static unsafe void Demosaic5(int[] src, IntPtr pin, int width, int height, int stride)
        {
            int channel = 3;
            int residue = stride - channel * width;
            var p = (byte*)pin;
            fixed (int* pin_s = src)
            {
                var p_s = pin_s;
                for (int y = 0; y < height; ++y)
                {

                    for (int x = 0; x < width; ++x)
                    {
                        int M = *p_s++;
                        byte N = (byte)(M > Byte.MaxValue ? Byte.MaxValue : M < Byte.MinValue ? 0 : M);
                        *(p++) = N;
                        *(p++) = N;
                        *(p++) = N;
                    }
                    p += residue;
                }
            }
        }
    }
}
