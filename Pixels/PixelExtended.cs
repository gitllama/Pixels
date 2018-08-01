using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pixels
{
    public static class PixelExtented
    {
        /*
        * 
        * funcの外部変数は排他制御必要
        * Lockしないので外部の変数参照はよくない
        * 
        *
        */

        //60->5何が最適化してる?
        //Parallel.For(0, src.pix.Length, i =>
        //{
        //    src.pix[i] = func(src.pix[i]);
        //});

        //bayerに対応したinc.x yの実装

        #region ForEach

        public static void ForEach<T>(this Pixel<T> src, Func<T, T> func, string subPlane = null, bool parallel = false) where T : struct
        {
            if (parallel)
                _ForEachParallel(src, src.GetPlane(subPlane), func);
            else
                _ForEachSeries(src, src.GetPlane(subPlane), func);
        }

        private static unsafe void _ForEachParallel<T>(Pixel<T> src, (int left, int top, int width, int height) plane, Func<T, T> func) where T : struct
        {
            Parallel.For(plane.top, plane.top + plane.height, y =>
            {
                int yy = y * src.Width;
                var span = src.pix.AsSpan(yy + plane.left, plane.width);
                //fixed (T* pin = span)
                //{
                //    var p = pin;
                //    while (p < pin + span.Length)
                //    {
                //        *p = func(*p);
                //        p++;
                //    }
                //}
            });
        }

        private static unsafe void _ForEachSeries<T>(Pixel<T> src, (int left, int top, int width, int height) plane, Func<T, T> func) where T : struct
        {
            (int x, int y) inc = (1, 1);
            for (int y = plane.top * src.Width; y < (plane.top + plane.height) * src.Width; y += inc.y * src.Width)
            {
                var span = src.pix.AsSpan(y, plane.width);
                //fixed (int* pin = span)
                //{
                //    var p = pin;
                //    var last = pin + span.Length;
                //    while (p < pin + span.Length)
                //    {
                //        *p = func(*p);
                //        p++;
                //    }
                //}
                int index = 0;
                var last = span.Length;
                while (index < last)
                {
                    span[index] = func(span[index]);
                    index++;
                }
            }
        }

        #endregion


        #region Map

        public static void Map<T, U>(this Pixel<T> src, Pixel<U> dst, Action<int, int, Pixel<T>, Pixel<U>> action, string subPlane = null, bool parallel = false) where T : struct where U : struct
        {
            if (parallel)
                _MapParallel(src, dst, src.GetPlane(subPlane), action);
            else
                _MapParallel(src, dst, src.GetPlane(subPlane), action);
        }

        private static unsafe void _MapParallel<T, U>(Pixel<T> src, Pixel<U> dst, (int left, int top, int width, int height) plane, Action<int, int, Pixel<T>, Pixel<U>> action) where T : struct where U : struct
        {
            //Parallel.For(plane.top, plane.top + plane.height, y =>
            //{
            //    int yy = y * src.Width;
            //    var span = src.pix.AsSpan().Slice(yy + plane.left, plane.width);
            //    fixed (int* pin = span)
            //    {
            //        var p = pin;
            //        while (p < pin + span.Length)
            //        {
            //            *p = func(*p);
            //            p++;
            //        }
            //    }
            //});
        }

        private static unsafe void _MapSeries<T, U>(Pixel<T> src, Pixel<U> dst, (int left, int top, int width, int height) plane, Action<int, int, Pixel<T>, Pixel<U>> action) where T : struct where U : struct
        {
            //for (int y = plane.top; y < plane.top + plane.height; y++)
            //{
            //    int yy = y * src.Width;
            //    var span = src.pix.AsSpan().Slice(yy + plane.left, plane.width);
            //    fixed (int* pin = span)
            //    {
            //        var p = pin;
            //        while (p < pin + span.Length)
            //        {
            //            *p = func(*p);
            //            p++;
            //        }
            //    }
            //}
        }

        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="upper"></param>
        /// <param name="lower"></param>
        /// <param name="parallel">並列の方が10倍遅い</param>
        /// <returns></returns>
        public unsafe static (int count, List<(int x, int y, int value)>) Count(this Pixel<int> src, string subPlane = "Full", int upper = int.MaxValue, int lower = int.MinValue, bool parallel = false)
        {
            int counter = 0;
            (int left, int top, int width, int height) plane = src.SubPlane[subPlane];
            var list = new List<(int x, int y, int value)>();
            if (parallel)
            {
                Parallel.For(plane.top, plane.top + plane.height, y =>
                {
                    int yy = y * src.Width;
                    var span = src.pix.AsSpan().Slice(yy + plane.left, plane.width);
                    fixed (int* pin = span)
                    {
                        var p = pin;
                        while (p < pin + span.Length)
                        {
                            if (*p > upper || *p < lower)
                            {
                                //Interlocked.Increment(ref counter);
                            }
                            p++;
                        }
                    }
                });
            }
            else
            {
                for (int y = plane.top; y < plane.top + plane.height; y++)
                {
                    int yy = y * src.Width;
                    var span = src.pix.AsSpan().Slice(yy + plane.left, plane.width);
                    fixed (int* pin = span)
                    {
                        var p = pin;
                        while (p < pin + span.Length)
                        {
                            if (*p > upper || *p < lower)
                            {
                                counter++;
                                if (counter < 100)
                                    list.Add(((int)(p - pin) + plane.left, y, *p));
                            }
                            p++;
                        }
                    }
                }
            }
            return (counter, list);
        }

        #region Filter

        public unsafe static void Median(Pixel<int> src, Pixel<int> dst, (int w, int h) window, bool parallel = false)
        {
            int[] coord = new int[window.w * window.h];
            int center = (coord.Length - 1) / 2;
            int w = (window.w - 1) / 2;
            int h = (window.h - 1) / 2;

            int c = 0;
            for (int yy = -h; yy <= h; yy++)
            {
                for (int xx = -w; xx <= w; xx++)
                {
                    coord[c++] = xx + yy * src.Width;
                }
            }

            if (parallel)
            {
                Parallel.For(h, dst.Height - h, y =>
                {
                    int[] box = new int[window.w * window.h];
                    for (int x = w; x < dst.Width - w; x++)
                    {
                        int index = x + y * dst.Width;
                        c = 0;
                        foreach (var i in coord)
                            box[c++] = src.pix[index + i];
                        Array.Sort(box); //90%ここ
                        dst.pix[index] = box[center];

                    }
                });
            }
            else
            {
                int[] box = new int[window.w * window.h];
                for (int y = h; y < dst.Height - h; y++)
                {
                    for (int x = w; x < dst.Width - w; x++)
                    {
                        int index = x + y * dst.Width;
                        c = 0;
                        foreach (var i in coord)
                            box[c++] = src.pix[index + i];
                        Array.Sort(box);
                        dst.pix[index] = box[center];
                    }
                }
            }

        }

        public unsafe static void Median2(in Pixel<int> src, ref Pixel<int> dst, (int w, int h) window)
        {
            int[] box = new int[window.w * window.h];
            int[] coord = new int[window.w * window.h];
            //Span<int> box = stackalloc int[window.w * window.h];
            //Span<int> coord = stackalloc int[window.w * window.h];

            int center = (box.Length - 1) / 2;
            int w = (window.w - 1) / 2;
            int h = (window.h - 1) / 2;
            int c = 0;

            for (int yy = -h; yy <= h; yy++)
            {
                for (int xx = -w; xx <= w; xx++)
                {
                    coord[c++] = xx + yy * src.Width;
                }
            }

            fixed (int* pinSrc = src.pix)
            fixed (int* pinDst = dst.pix)
            fixed (int* pinbox = box)
            {
                for (int y = h; y < dst.Height - h; y++)
                {
                    for (int x = w; x < dst.Width - w; x++)
                    {
                        int index = x + y * dst.Width;
                        c = 0;
                        foreach (var i in coord)
                            box[c++] = src.pix[index + i];
                        Array.Sort(box); //90%ここ
                        dst.pix[index] = box[center];
                    }
                }
            }
        }


        #endregion

        #region Op

        public unsafe static void Diff(Pixel<int> src1, Pixel<int> src2, Pixel<int> dst)
        {
            //231
            //unsafe
            //{
            //    fixed (int* pin1 = src1.pix)
            //    fixed (int* pin2 = src2.pix)
            //    fixed (int* pin3 = dst.pix)
            //    {
            //        var p1 = pin1;
            //        var p2 = pin2;
            //        var p3 = pin3;
            //        var length = pin3 + dst.pix.Length;
            //        while (p3 < length)
            //        {
            //            *p3++ = *p1++ - *p2++;
            //        }
            //    }
            //}

            //459
            //for (var i = 0; i < dst.pix.Length; i++)
            //    dst.pix[i] = src1.pix[i] - src2.pix[i];

            ////170
            //Parallel.For(0, dst.Height, y =>
            //{
            //    for (int x = 0; x < dst.Width; x++)
            //    {
            //        int index = x + y * dst.Width; ;
            //        dst.pix[index] = src1.pix[index] - src2.pix[index];
            //    }
            //});

            //157
            //Parallel.For(0, dst.Height, y =>
            //{
            //    for (int x = y * dst.Width; x < y * dst.Width + dst.Width; x++)
            //    {
            //        dst.pix[x] = src1.pix[x] - src2.pix[x];
            //    }
            //});

        }

        #endregion



    }


}

