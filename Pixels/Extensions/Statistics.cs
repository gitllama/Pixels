using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace Pixels.Extensions
{

    public static class Statistics
    {

        #region Random

        public static class Random
        {
            public static void Uniform<T>(Pixel<T> src) where T : struct
            {
                var rand = new System.Random();
                byte[] bytes = new byte[Marshal.SizeOf(typeof(T))];

                for (var i = 0; i < src.Size; i++)
                {
                    rand.NextBytes(bytes);
                    src[i] = Unsafe.As<byte, T>(ref bytes[0]);
                }
            }


            public static void Normal<T>(Pixel<T> src) where T : struct
            {
                var rand = new Normal(-1, 1);
                for (var i = 0; i < src.Size; i++)
                {
                    //src[i] = (dynamic)rand.Sample();
                    throw new NotImplementedException();
                }
            }
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

