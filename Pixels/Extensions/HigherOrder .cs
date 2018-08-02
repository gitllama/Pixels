using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Pixels.Extensions
{
    public static class HigherOrder
    {
        // forEach : 配列の個々の要素を処理
        // map : 配列の個々の要素を加工 / 写像
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


    }


}

