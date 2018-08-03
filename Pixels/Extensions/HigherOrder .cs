using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Pixels.Extensions
{
    public static class HigherOrder
    {
        /* forEach : 配列の個々の要素を処理
         * 
         * Map : 配列の個々の要素を加工 / 写像
         * 
         *  1 : Map(Pixel, Pixel, Action<int,int,Pixel,Pixel>) : x, yを取らないといけないので普通のループ
         *  2 : Map(Pixel, Pixel, Func<T, U>)                  : index参照しないのでunsafeで最適化
         * 
         * 
         * 
         * 
         */
        // 
        // 

        // reduce : 配列の要素を左から右へ処理する / previousValue, currentValue, index, array

        // filter : 配列の個々の要素を判定
        // sort : 配列の要素を並び替える

        // every : 条件を全ての値が満たすか / boolean
        // some : 条件を満たすものがあるか / boolean
        // find, findIndex



        /*
        * 
        * funcの外部変数は排他制御必要
        * Lockしないので外部の変数参照はよくない
        * 
        *
        */

        #region Map 1
        /*<#/*/

        public static void Map<T, U>( this Pixel<T> src, Pixel<U> dst, 
            Action<int, int, Pixel<T>, Pixel<U>> action, 
            string subPlane = null, string bayer = null, bool parallel = false)
            where T :struct where U : struct
        {
            var sts = src.LoopState(subPlane, bayer);
            if (parallel)
                _MapParallel(src, dst, action, sts);
            else
                _MapSeries(src, dst, action, sts);
        }

        private static void _MapParallel<T, U>( Pixel<T> src, Pixel<U> dst, 
            Action<int, int, Pixel<T>, Pixel<U>> action, 
            (int left, int top, int width, int height, int incX, int incY) sts) 
            where T : struct where U : struct
        {
            Parallel.For(sts.top, (sts.top + sts.height) / sts.incX, y =>
            {
                for (int x = sts.left; x < sts.left + sts.width; x += sts.incX)
                {
                    action(x, y * sts.incX, src, dst);
                }
            });
        }

        private static void _MapSeries<T, U>( Pixel<T> src, Pixel<U> dst, 
            Action<int, int, Pixel<T>, Pixel<U>> action, 
            (int left, int top, int width, int height, int incX, int incY) sts) 
            where T : struct where U : struct
        {
            for (int y = sts.top; y < sts.top + sts.height; y += sts.incY)
                for (int x = sts.left; x < sts.left + sts.width; x += sts.incX)
                {
                    action(x, y, src, dst);
                }
        }

        /*/#>*/
        #endregion

        #region Map2

        public static void Map<T, U>(this Pixel<T> src, Pixel<U> dst,
            Func<T, U> func, 
            string subPlane = null, string bayer = null, bool parallel = false)
            where T : struct where U : struct
        {
            var sts = src.LoopState(subPlane, bayer);
            if (parallel)
                _MapParallel(src, dst, func, sts);
            else
                _MapSeries(src, dst, func, sts);                
        }

        private static void _MapParallel<T, U>(Pixel<T> src, Pixel<U> dst,
            Func<T, U> func,
            (int left, int top, int width, int height, int incX, int incY) sts)
            where T : struct where U : struct
        {
            Parallel.For(sts.top, (sts.top + sts.height) / sts.incX, y =>
            {
                for (int x = sts.left; x < sts.left + sts.width; x += sts.incX)
                {
                    int index = src.GetIndex(x, y * sts.incY);
                    dst[index] = func(src[index]);
                }
            });
        }

        private static void _MapSeries<T, U>(Pixel<T> src, Pixel<U> dst,
            Func<T, U> func,
            (int left, int top, int width, int height, int incX, int incY) sts)
            where T : struct where U : struct
        {
            unsafe
            {
                using (var _src = new LockPixel<T>(src))
                using (var _dst = new LockPixel<U>(dst))
                {
                    var pin_src = (byte*)_src.ToPointer();
                    var pin_dst = (byte*)_dst.ToPointer();

                    for (int y = sts.top; y < sts.top + sts.height; y += sts.incY)
                    {
                        for (int x = sts.left; x < sts.left + sts.width; x += sts.incX)
                        {
                            var p_src = pin_src + (y * src.Width + x) * _src.Size;
                            var p_dst = pin_dst + (y * src.Width + x) * _dst.Size;
                            Unsafe.As<byte, U>(ref *p_dst) = func(Unsafe.As<byte,T>(ref *p_src));
                        }
                    }
                }
            }
        }

        #endregion



        #region ForEach




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
                int index = 0;
                var last = span.Length;
                while (index < last)
                {
                    span[index] = func(span[index]);
                    index++;
                }
            });
        }

        private static void _ForEachSeries<T>(Pixel<T> src, (int left, int top, int width, int height) plane, Func<T, T> func) where T : struct
        {
            (int x, int y) inc = (1, 1);
            for (int y = plane.top; y < plane.top + plane.height; y += inc.y)
            {
                int yy = y * src.Width;
                var span = src.pix.AsSpan(yy + plane.left, plane.width);
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
                    index+= inc.x;
                }
            }
        }


        public static void ForEach2(this Pixel<int> src, Func<int, int> func, string subPlane = null, bool parallel = false)
        {
            if (parallel)
                __ForEachParallel(src, src.GetPlane(subPlane), func);
            else
                __ForEachSeries(src, src.GetPlane(subPlane), func);
        }

        private static unsafe void __ForEachParallel(Pixel<int> src, (int left, int top, int width, int height) plane, Func<int, int> func)
        {
            Parallel.For(plane.top, plane.top + plane.height, y =>
            {
                int yy = y * src.Width;
                var span = src.pix.AsSpan(yy + plane.left, plane.width);
                fixed (int* pin = span)
                {
                    var p = pin;
                    while (p < pin + span.Length)
                    {
                        *p = func(*p);
                        p++;
                    }
                }
            });
        }

        private static unsafe void __ForEachSeries(Pixel<int> src, (int left, int top, int width, int height) plane, Func<int, int> func)
        {
            (int x, int y) inc = (1, 1);
            for (int y = plane.top; y < plane.top + plane.height; y += inc.y)
            {
                int yy = y * src.Width;
                var span = src.pix.AsSpan(yy + plane.left, plane.width);
                fixed (int* pin = span)
                {
                    var p = pin;
                    var last = pin + span.Length;
                    while (p < pin + span.Length)
                    {
                        *p = func(*p);
                        p++;
                    }
                }
            }
        }

        private static unsafe void __ForEachSeries2(Pixel<int> src, (int left, int top, int width, int height) plane, Func<int, int> func)
        {
            (int x, int y) inc = (1, 1);
            for (int y = plane.top; y < plane.top + plane.height; y += inc.y)
            {
                int yy = y * src.Width;
                var span = src.pix.AsSpan(yy + plane.left, plane.width);

                fixed (int* pin = span)
                {
                    var p = pin;
                    var last = pin + span.Length;
                    while (p < pin + span.Length)
                    {
                        *p = func(*p);
                        p++;
                    }
                }
            }
        }

        #endregion


        #region Map




        private static unsafe void __MapParallel<T, U>(Pixel<T> src, Pixel<U> dst, Action<int, int, Pixel<T>, Pixel<U>> action, (int left, int top, int width, int height, int incX, int incY) sts) where T : struct where U : struct
        {
            throw new NotImplementedException();
            //using (var _src = new PixelLoop)
            //using (var _dst = new PixelLoop)
            //    Parallel.For(sts.top, sts.top + sts.height, y =>
            //    {
            //        int yy = y * src.Width;
            //        var span = src.pix.AsSpan().Slice(yy + plane.left, plane.width);
            //        fixed (int* pin = span)
            //        {
            //            var p = pin;
            //            while (p < pin + span.Length)
            //            {
            //                action();
            //                p++;
            //            }
            //        }
            //    });
        }



        #endregion
    }
}

namespace Pixels.Extensions.Benchmark
{
    public static class HigherOrder
    {
        public static void MapTest(this Pixel<int> src, Pixel<int> dst, Action<int, int, Pixel<int>, Pixel<int>> action, string subPlane = null, string bayer = null, bool parallel = false)
        {
            var sts = src.LoopState(subPlane, bayer);
            unsafe
            {
                using (var _src = new LockPixel<int>(src))
                using (var _dst = new LockPixel<int>(dst))
                {
                    var p_src = (int*)_src.ToPointer();
                    var p_dst = (int*)_dst.ToPointer();

                    for (int y = sts.top; y < sts.top + sts.height; y += sts.incY)
                    {
                        for (int x = sts.left; x < sts.left + sts.width; x += sts.incX)
                        {
                            action(x, y, src, dst);
                        }
                    }
                }
            }
        }


    }
}
