using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Pixels.Extensions
{

    /* Fill : 固定値で設定します
     * 
     *   1 : Fill(Pixel, val)
     * 
     * ForEach : 配列の個々の要素を処理 
     *   forEach() は呼び出された配列を変化させません。
     *   parallel=tureの際は外部変数をlockする必要があります
     * 
     *   1 : ForEach(Pixel, Action<int,int,Pixel>) : x, yを取らないといけないので普通のループ
     *   2 : ForEach(Pixel, Action<T>)             : index参照しないのでunsafeで最適化
     * 
     * Map : 配列の個々の要素を加工 / 写像
     *   メソッドチェーンを使用するためにdstを戻り値としてます。dstにsrcを参照すると破壊的な動作する
     *   parallel=tureの際はdstに自己を参照した場合計算順の保証はできません
     * 
     *   1 : Map(Pixel, Pixel, Action<int,int,Pixel,Pixel>) : x, yを取らないといけないので普通のループ
     *   2 : Map(Pixel, Pixel, Func<T, U>)                  : index参照しないのでunsafeで最適化
     * 
     * currentValue, index, array
     * 
     * 
     *
     * 
     * 他言語にはあるけど実装しないもの
     * 
     * 　reduce : 配列の要素を左から右へ処理する / previousValue, currentValue, index, array
     * 　  -> accumulator系は考え中
     *   every : 条件を全ての値が満たすか / boolean
     *     -> そのようなシチュエーションが想定できない
     *   some : 条件を満たすものがあるか / boolean
     *     -> Countで代用する
     *   sort : 配列の要素を並び替える
     *     -> 現実的ではない
     *   find : 一致する最初の要素を抜き出す
     *     -> 途中切り上げのCountで代用
     *   filter : 配列の個々の要素を判定し新しい配列を生成
     *     -> 生成しない
     *   includes : 特定の要素が配列に含まれているかどうか / boolean
     *     -> 決まった値とのマッチは使用が少ない、Countで代用可能
     *   indexOf : 同じ内容を持つ配列要素の内、最初のものの添字を返しま
     */

    public static class HigherOrder
    {


        public static void Fill<T>(this Pixel<T> src, T val, string subPlane = null, string cfa = null, bool parallel = false) where T : unmanaged
        {
            unsafe
            {
                fixed (T* _pin = src)
                {
                    OrderOptions option = new OrderOptions(src.GetPlane(subPlane), src.GetCFA(cfa));
                    var org = _pin + option.left + option.top * src.Width;
                    if (parallel)
                        Parallel.For(0, (int)Math.Ceiling((double)option.height / option.incY), y =>
                        {
                            var p = org + (y * option.incY * src.Width);
                            var last = p + option.width;
                            while (p < last)
                            {
                                *p = val;
                                p += option.incX;
                            }
                        });
                    else
                        for (var y = 0; y < option.height; y += option.incY)
                        {
                            var p = org + (y * src.Width);
                            var last = p + option.width;
                            while (p < last)
                            {
                                *p = val;
                                p += option.incX;
                            }
                        }
                }
            }
        }



        public static void ForEach<T>(this Pixel<T> src, Action<T> action, string subPlane = null, string cfa = null, bool parallel = false) where T : unmanaged
        {
            unsafe
            {
                fixed (T* _pin = src)
                {
                    OrderOptions option = new OrderOptions(src.GetPlane(subPlane), src.GetCFA(cfa));
                    var org = _pin + option.left + option.top * src.Width;
                    if (parallel)
                        Parallel.For(0, (int)Math.Ceiling((double)option.height / option.incY), y =>
                        {
                            var p = org + (y * option.incY * src.Width);
                            var last = p + option.width;
                            while (p < last)
                            {
                                action(*p);
                                p += option.incX;
                            }
                        });
                    else
                        for (var y = 0; y < option.height; y += option.incY)
                        {
                            var p = org + (y * src.Width);
                            var last = p + option.width;
                            while (p < last)
                            {
                                action(*p);
                                p += option.incX;
                            }
                        }
                }
            }
        }

        public static void ForEach<T>(this Pixel<T> src, Action<int, int, Pixel<T>> action, string subPlane = null, string cfa = null, bool parallel = false) where T : unmanaged
        {
            unsafe
            {
                fixed (T* pin = src)
                {
                    OrderOptions sts = new OrderOptions(src.GetPlane(subPlane), src.GetCFA(cfa));
                    if (parallel)
                        Parallel.For(0, (int)Math.Ceiling((double)sts.height / sts.incY), n =>
                        {
                            var y = sts.top + n * sts.incY;
                            for (var x = sts.left; x < sts.left + sts.width; x += sts.incX)
                            {
                                action(x, y, src);
                            }
                        });
                    else
                        for (var y = sts.top; y < sts.top + sts.height; y += sts.incY)
                            for (var x = sts.left; x < sts.left + sts.width; x += sts.incX)
                            {
                                action(x, y, src);
                            }
                }
            }
        }



        public static Pixel<U> Map<T, U>(this Pixel<T> src, Pixel<U> dst, Func<T, U> func, string subPlane = null, string cfa = null, bool parallel = false) where T : unmanaged where U : unmanaged
        {
            unsafe
            {
                fixed (T* _pinSrc = src)
                fixed (U* _pinDst = dst)
                {
                    OrderOptions option = new OrderOptions(src.GetPlane(subPlane), src.GetCFA(cfa));
                    var orgSrc = _pinSrc + option.left + option.top * src.Width;
                    var orgDst = _pinDst + option.left + option.top * src.Width;
                    if (parallel)
                        Parallel.For(0, (int)Math.Ceiling((double)option.height / option.incY), y =>
                        {
                            var p = orgSrc + (y * option.incY * src.Width);
                            var q = orgDst + (y * option.incY * src.Width);
                            var last = p + option.width;
                            while (p < last)
                            {
                                *q = func(*p);
                                p += option.incX;
                                q += option.incX;
                            }
                        });
                    else
                        for (var y = 0; y < option.height; y += option.incY)
                        {
                            var p = orgSrc + (y * src.Width);
                            var q = orgDst + (y * src.Width);
                            var last = p + option.width;
                            while (p < last)
                            {
                                *q = func(*p);
                                p += option.incX;
                                q += option.incX;
                            }
                        }
                }
            }
            return dst;
        }

        public static Pixel<U> Map<T, U>(this Pixel<T> src, Pixel<U> dst, Func<int, int, Pixel<T>, U> func, string subPlane = null, string cfa = null, bool parallel = false) where T : unmanaged where U : unmanaged
        {
            unsafe
            {
                fixed (T* pinSrc = src)
                fixed (U* pinDst = dst)
                {
                    OrderOptions opt = new OrderOptions(src.GetPlane(subPlane), src.GetCFA(cfa));
                    if (parallel)
                        Parallel.For(0, (int)Math.Ceiling((double)opt.height / opt.incY), n =>
                        {
                            var y = opt.top + n * opt.incY;
                            for (var x = opt.left; x < opt.left + opt.width; x += opt.incX)
                            {
                                dst[x,y] = func(x, y, src);
                            }
                        });
                    else
                        for (var y = opt.top; y < opt.top + opt.height; y += opt.incY)
                            for (var x = opt.left; x < opt.left + opt.width; x += opt.incX)
                            {
                                dst[x, y] = func(x, y, src);
                            }
                }
            }
            return dst;
        }

        public static Pixel<U> Map<T, U>(this Pixel<T> src, Pixel<U> dst, Action<int, int, Pixel<T>, Pixel<U>> action, string subPlane = null, string cfa = null, bool parallel = false) where T : unmanaged where U : unmanaged
        {
            unsafe
            {
                fixed (T* pinSrc = src)
                fixed (U* pinDst = dst)
                {
                    OrderOptions sts = new OrderOptions(src.GetPlane(subPlane), src.GetCFA(cfa));
                    if (parallel)
                        Parallel.For(0, (int)Math.Ceiling((double)sts.height / sts.incY), n =>
                        {
                            var y = sts.top + n * sts.incY;
                            for (var x = sts.left; x < sts.left + sts.width; x += sts.incX)
                            {
                                action(x, y, src, dst);
                            }
                        });
                    else
                        for (var y = sts.top; y < sts.top + sts.height; y += sts.incY)
                            for (var x = sts.left; x < sts.left + sts.width; x += sts.incX)
                            {
                                action(x, y, src, dst);
                            }
                }
            }
            return dst;
        }


        public static Pixel<U> Map<T, U>(this Pixel<T> src, Action<int, int, Pixel<T>, Pixel<U>> action, string subPlane = null, string cfa = null, bool parallel = false) where T : unmanaged where U : unmanaged
            => Map(src, src.Clone<T, U>(), action, subPlane, cfa, parallel);

        public static Pixel<U> Map<T, U>(this Pixel<T> src, Func<int, int, Pixel<T>, U> func, string subPlane = null, string cfa = null, bool parallel = false) where T : unmanaged where U : unmanaged
            => Map(src, src.Clone<T, U>(), func, subPlane, cfa, parallel);

        public static Pixel<U> Map<T, U>(this Pixel<T> src, Func<T, U> func, string subPlane = null, string cfa = null, bool parallel = false) where T : unmanaged where U : unmanaged
            => Map(src, src.Clone<T, U>(), func, subPlane, cfa, parallel);


        public static Pixel<T> MapSelf<T>(this Pixel<T> src, Action<int, int, Pixel<T>, Pixel<T>> action, string subPlane = null, string cfa = null, bool parallel = false) where T : unmanaged
            => Map(src, src, action, subPlane, cfa, parallel);

        public static Pixel<T> MapSelf<T>(this Pixel<T> src, Func<int, int, Pixel<T>, T> func, string subPlane = null, string cfa = null, bool parallel = false) where T : unmanaged
            => Map(src, src, func, subPlane, cfa, parallel);

        public static Pixel<T> MapSelf<T>(this Pixel<T> src, Func<T, T> func, string subPlane = null, string cfa = null, bool parallel = false) where T : unmanaged
            => Map(src, src, func, subPlane, cfa, parallel);


    }

}
