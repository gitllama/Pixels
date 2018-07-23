using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pixels
{
    public static partial class PixelExtention
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="U"></typeparam>
        public unsafe static void ConvertTo<T>(T src, byte* dst, int width, int height, int stride) where T : unmanaged
        {
            var p = src;
            var d = dst;
            byte* pin = dst;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte* p1 = pin + (y * stride) + (x * 3);
                    p1[0] = 255;
                    p1[1] = 64;
                    p1[2] = 0;
                }
            }
        }

        public unsafe static void ForEach(this Pixel<int> src, Func<int, int> func, bool parallel = false)
        {
            var pln = src.SubPlane["Full"];
            int stride = src.Width;
            if (parallel)
            {
                Parallel.For(pln.top, pln.top + pln.height, y =>
                {
                    var span = src.pix.AsSpan().Slice(y * stride + pln.left, pln.width);
                    fixed (int* pin = span)
                    {
                        var p = pin;
                        while (p < pin + span.Length)
                        {
                            *p = *p + 10;
                            p++;
                        }
                    }
                });
            }
            else
            {
                for (int y = pln.top * stride; y < (pln.top + pln.height) * stride; y += stride)
                {
                    var span = src.pix.AsSpan().Slice(y + pln.left, pln.width);
                    fixed (int* pin = span)
                    {
                        var p = pin;
                        var last = p + span.Length;
                        while (p < last)
                        {
                            *p = *p + 10;
                            p++;
                        }
                    }
                }
            }

        }


        public unsafe static void ForEach<T>(this Pixel<T> src, Func<T, T> func, bool parallel = false) where T : struct
        {
            var pln = src.SubPlane["Full"];
            int stride = src.Width;
            for (int y = 0; y < src.Height; y++)
            {
                for (int x = 0; x < src.Width; x++)
                {
                    src[x, y] = func(src[x, y]);
                }
            }
        }

        public static void ForEach<T>(this Pixel<T> src, Action<T> action) where T : struct
        {
            for (int y = 0; y < src.Height; y++)
            {
                for (int x = 0; x < src.Width; x++)
                {
                    action(src[x, y]);
                }
            }
        }

        public static void ForEach<T, U>(this Pixel<T> src1, Pixel<U> src2, Action<T, U> action) where T : struct where U : struct
        {
            for (int y = 0; y < src1.Height; y++)
            {
                for (int x = 0; x < src1.Width; x++)
                {
                    action(src1[x, y], src2[x, y]);
                }
            }
        }


        public static void Map<T, U>(this Pixel<T> src, Pixel<U> dst, Func<T, U> func) where T : struct where U : struct
        {
            for (int y = 0; y < src.Height; y++)
            {
                for (int x = 0; x < src.Width; x++)
                {
                    dst[x, y] = func(src[x, y]);
                }
            }
        }

        public static void Map<T, U>(this Pixel<T> src, Pixel<U> dst, Action<int, int, Pixel<T>, Pixel<U>> action) where T : struct where U : struct
        {
            for (int y = 0; y < src.Height; y++)
            {
                for (int x = 0; x < src.Width; x++)
                {
                    action(x, y, src, dst);
                }
            }
        }

        public static void Map<T, TT, U>(Pixel<T> src1, Pixel<TT> src2, Pixel<U> dst, Func<T, TT, U> func) where T : struct where TT : struct where U : struct
        {
            for (int y = 0; y < src1.Height; y++)
            {
                for (int x = 0; x < src1.Width; x++)
                {
                    dst[x, y] = func(src1[x, y], src2[x, y]);
                }
            }
        }

        public static void Map<T, TT, U>(Pixel<T> src1, Pixel<TT> src2, Pixel<U> dst, Action<int, int, Pixel<T>, Pixel<TT>, Pixel<U>> action) where T : struct where TT : struct where U : struct
        {
            for (int y = 0; y < src1.Height; y++)
            {
                for (int x = 0; x < src1.Width; x++)
                {
                    action(x, y, src1, src2, dst);
                }
            }
        }



    }
}