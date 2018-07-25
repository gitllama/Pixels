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
    //     
    #endregion

    #region T4

    public static partial class PixelDeveloper
    {

        // 
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
        

        #endregion



        // 

    }

    #endregion

}


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
