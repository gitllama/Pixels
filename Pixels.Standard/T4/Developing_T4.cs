/*
*/
/* Code generated using the t4 templates */
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Pixels.Standard.Processing
{
    #region A
    /**/
    #endregion

    #region T4

    public static partial class PixelDeveloper
    {
        #region MyRegion
        /**/
        #endregion



        #region T4

        //created 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void DemosaicMono(Pixel<Double> src, IntPtr pin, int stride, Options option)
        {
            int channel = 3;
            int residue = stride - channel * src.Width;
            int width = src.Width;
            int height = src.Height;
            if (option.parallel)
            {
                Parallel.For(0, src.Height, y =>
                {
                    byte* p = (byte*)(pin);
                    p += y * stride;
                    var span = src.pix.AsSpan().Slice(y * width, width);
                    for (int x = 0; x < span.Length; x++)
                    {
                        var M = option.LUT((int)span[x]);
                        // byte N = (byte)(M > Byte.MaxValue ? Byte.MaxValue : M < Byte.MinValue ? 0 : M);
                        // *p++ = ((N << 16) | (N << 8) | N);
                        *p++ = M;
                        *p++ = M;
                        *p++ = M;
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
                        var M = option.LUT((int)src.pix[x]);
                        *(p++) = M;
                        *(p++) = M;
                        *(p++) = M;
                    }
                    p += residue;
                }
            }
        }
        

        //created 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void DemosaicMono(Pixel<Single> src, IntPtr pin, int stride, Options option)
        {
            int channel = 3;
            int residue = stride - channel * src.Width;
            int width = src.Width;
            int height = src.Height;
            if (option.parallel)
            {
                Parallel.For(0, src.Height, y =>
                {
                    byte* p = (byte*)(pin);
                    p += y * stride;
                    var span = src.pix.AsSpan().Slice(y * width, width);
                    for (int x = 0; x < span.Length; x++)
                    {
                        var M = option.LUT((int)span[x]);
                        // byte N = (byte)(M > Byte.MaxValue ? Byte.MaxValue : M < Byte.MinValue ? 0 : M);
                        // *p++ = ((N << 16) | (N << 8) | N);
                        *p++ = M;
                        *p++ = M;
                        *p++ = M;
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
                        var M = option.LUT((int)src.pix[x]);
                        *(p++) = M;
                        *(p++) = M;
                        *(p++) = M;
                    }
                    p += residue;
                }
            }
        }
        

        //created  
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void DemosaicColor(Pixel<Double> src, IntPtr pin, int stride, Options option)
        {
            int channel = 3;
            int residue = stride - channel * src.Width;
            int width = src.Width;
            int height = src.Height;
            if (option.parallel)
            {
                DemosaicColorParallel(src, pin, stride, option);
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void DemosaicColorParallel(Pixel<Double> src, IntPtr pin, int stride, Options option)
        {
            int channel = 3;
            int residue = stride - channel * src.Width;
            int width = src.Width;
            int height = src.Height;
            var origins = option.GetOrigins(src);
            (int left, int top, int width, int height) origin;

            origin = origins[0]; //Gr
            Parallel.For(0, origin.height / 2, y =>
            {
                byte* p = (byte*)(pin);
                p += ((origin.top + y * 2) * stride + origin.left * 3);
                int indexY = (origin.top + y * 2) * width;
                var top = src.pix.AsSpan().Slice(indexY - width + option.stagger.BG);
                var mid = src.pix.AsSpan().Slice(indexY + option.stagger.GR);
                var bot = src.pix.AsSpan().Slice(indexY + width + option.stagger.BG);
                for (int x = origin.left; x < origin.left + origin.width; x += 2)
                {
                    float G = (float)mid[x] / 2.0f + ((float)top[x - 1] + (float)top[x + 1] + (float)bot[x - 1] + (float)bot[x + 1]) / 8f;
                    float R = ((float)mid[x - 1] + (float)mid[x + 1]) / 2f;
                    float B = ((float)top[x] + (float)bot[x]) / 2f;

                    Set(R, G, B, ref p, option);
                }
            });
            origin = origins[1]; //R
            Parallel.For(0, origin.height / 2, y =>
            {
                byte* p = (byte*)(pin);
                p += ((origin.top + y * 2) * stride + origin.left * 3);
                int indexY = (origin.top + y * 2) * width;
                var top = src.pix.AsSpan().Slice(indexY - width + option.stagger.BG);
                var mid = src.pix.AsSpan().Slice(indexY + option.stagger.GR);
                var bot = src.pix.AsSpan().Slice(indexY + width + option.stagger.BG);
                for (int x = origin.left; x < origin.left + origin.width; x += 2)
                {
                    float R = (float)mid[x];
                    float G = ((float)top[x] + (float)mid[x - 1] + (float)mid[x + 1] + (float)bot[x]) / 4f;
                    float B = ((float)top[x - 1] + (float)top[x + 1] + (float)bot[x - 1] + (float)bot[x + 1]) / 4f;

                    Set(R, G, B, ref p, option);
                }
            });
            origin = origins[2]; //B
            Parallel.For(0, origin.height / 2, y =>
            {
                byte* p = (byte*)(pin);
                p += ((origin.top + y * 2) * stride + origin.left * 3);
                int indexY = (origin.top + y * 2) * width;
                var top = src.pix.AsSpan().Slice(indexY - width + option.stagger.GR);
                var mid = src.pix.AsSpan().Slice(indexY + option.stagger.BG);
                var bot = src.pix.AsSpan().Slice(indexY + width + option.stagger.GR);
                for (int x = origin.left; x < origin.left + origin.width; x += 2)
                {
                    float B = (float)mid[x];
                    float G = ((float)top[x] + (float)mid[x - 1] + (float)mid[x + 1] + (float)bot[x]) / 4f;
                    float R = ((float)top[x - 1] + (float)top[x + 1] + (float)bot[x - 1] + (float)bot[x + 1]) / 4f;

                    Set(R, G, B, ref p, option);
                }
            });
            origin = origins[3]; //Gb
            Parallel.For(0, origin.height / 2, y =>
            {
                byte* p = (byte*)(pin);

                p += ((origin.top + y * 2) * stride + origin.left * 3);
                int indexY = (origin.top + y * 2) * width;

                var top = src.pix.AsSpan().Slice(indexY - width + option.stagger.GR);
                var mid = src.pix.AsSpan().Slice(indexY + option.stagger.BG);
                var bot = src.pix.AsSpan().Slice(indexY + width + option.stagger.GR);
                for (int x = origin.left; x < origin.left + origin.width; x += 2)
                {
                    float G = (float)mid[x] / 2f + ((float)top[x - 1] + (float)top[x + 1] + (float)bot[x - 1] + (float)bot[x + 1]) / 8f;

                    float B = ((float)mid[x - 1] + (float)mid[x + 1]) / 2f;
                    float R = ((float)top[x] + (float)bot[x]) / 2f;

                    Set(R, G, B, ref p, option);
                }
            });
        }
        

        //created  
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void DemosaicColor(Pixel<Single> src, IntPtr pin, int stride, Options option)
        {
            int channel = 3;
            int residue = stride - channel * src.Width;
            int width = src.Width;
            int height = src.Height;
            if (option.parallel)
            {
                DemosaicColorParallel(src, pin, stride, option);
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void DemosaicColorParallel(Pixel<Single> src, IntPtr pin, int stride, Options option)
        {
            int channel = 3;
            int residue = stride - channel * src.Width;
            int width = src.Width;
            int height = src.Height;
            var origins = option.GetOrigins(src);
            (int left, int top, int width, int height) origin;

            origin = origins[0]; //Gr
            Parallel.For(0, origin.height / 2, y =>
            {
                byte* p = (byte*)(pin);
                p += ((origin.top + y * 2) * stride + origin.left * 3);
                int indexY = (origin.top + y * 2) * width;
                var top = src.pix.AsSpan().Slice(indexY - width + option.stagger.BG);
                var mid = src.pix.AsSpan().Slice(indexY + option.stagger.GR);
                var bot = src.pix.AsSpan().Slice(indexY + width + option.stagger.BG);
                for (int x = origin.left; x < origin.left + origin.width; x += 2)
                {
                    float G = (float)mid[x] / 2.0f + ((float)top[x - 1] + (float)top[x + 1] + (float)bot[x - 1] + (float)bot[x + 1]) / 8f;
                    float R = ((float)mid[x - 1] + (float)mid[x + 1]) / 2f;
                    float B = ((float)top[x] + (float)bot[x]) / 2f;

                    Set(R, G, B, ref p, option);
                }
            });
            origin = origins[1]; //R
            Parallel.For(0, origin.height / 2, y =>
            {
                byte* p = (byte*)(pin);
                p += ((origin.top + y * 2) * stride + origin.left * 3);
                int indexY = (origin.top + y * 2) * width;
                var top = src.pix.AsSpan().Slice(indexY - width + option.stagger.BG);
                var mid = src.pix.AsSpan().Slice(indexY + option.stagger.GR);
                var bot = src.pix.AsSpan().Slice(indexY + width + option.stagger.BG);
                for (int x = origin.left; x < origin.left + origin.width; x += 2)
                {
                    float R = (float)mid[x];
                    float G = ((float)top[x] + (float)mid[x - 1] + (float)mid[x + 1] + (float)bot[x]) / 4f;
                    float B = ((float)top[x - 1] + (float)top[x + 1] + (float)bot[x - 1] + (float)bot[x + 1]) / 4f;

                    Set(R, G, B, ref p, option);
                }
            });
            origin = origins[2]; //B
            Parallel.For(0, origin.height / 2, y =>
            {
                byte* p = (byte*)(pin);
                p += ((origin.top + y * 2) * stride + origin.left * 3);
                int indexY = (origin.top + y * 2) * width;
                var top = src.pix.AsSpan().Slice(indexY - width + option.stagger.GR);
                var mid = src.pix.AsSpan().Slice(indexY + option.stagger.BG);
                var bot = src.pix.AsSpan().Slice(indexY + width + option.stagger.GR);
                for (int x = origin.left; x < origin.left + origin.width; x += 2)
                {
                    float B = (float)mid[x];
                    float G = ((float)top[x] + (float)mid[x - 1] + (float)mid[x + 1] + (float)bot[x]) / 4f;
                    float R = ((float)top[x - 1] + (float)top[x + 1] + (float)bot[x - 1] + (float)bot[x + 1]) / 4f;

                    Set(R, G, B, ref p, option);
                }
            });
            origin = origins[3]; //Gb
            Parallel.For(0, origin.height / 2, y =>
            {
                byte* p = (byte*)(pin);

                p += ((origin.top + y * 2) * stride + origin.left * 3);
                int indexY = (origin.top + y * 2) * width;

                var top = src.pix.AsSpan().Slice(indexY - width + option.stagger.GR);
                var mid = src.pix.AsSpan().Slice(indexY + option.stagger.BG);
                var bot = src.pix.AsSpan().Slice(indexY + width + option.stagger.GR);
                for (int x = origin.left; x < origin.left + origin.width; x += 2)
                {
                    float G = (float)mid[x] / 2f + ((float)top[x - 1] + (float)top[x + 1] + (float)bot[x - 1] + (float)bot[x + 1]) / 8f;

                    float B = ((float)mid[x - 1] + (float)mid[x + 1]) / 2f;
                    float R = ((float)top[x] + (float)bot[x]) / 2f;

                    Set(R, G, B, ref p, option);
                }
            });
        }
        

        #endregion






    }

    #endregion

}

// エリア限定する際
//static void DemosaicMono(int[] src, byte[] dst, (int x, int y) start, (int x, int y) length, int width)
//{
//    Parallel.For(0, length.y, y =>
//    {
//        int indexY = (start.y + y) * width;
//        var mid = src.AsSpan().Slice(indexY);
//        for (int x = start.x; x < start.x + length.x; x++)
//        {
//            int M = mid[x];
//            byte N = (byte)(M > Byte.MaxValue ? Byte.MaxValue : M < Byte.MinValue ? 0 : M);
//            dst[(indexY + x) * 3] = N;
//            dst[(indexY + x) * 3 + 1] = N;
//            dst[(indexY + x) * 3 + 2] = N;
//        }
//    });
//}
