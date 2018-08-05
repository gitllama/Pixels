using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Pixels.Extensions
{

    /* ForEach : 配列の個々の要素を処理 
     *   forEach() は呼び出された配列を変化させません。
     *   parallel : tureの際は外部変数をlockする必要があります
     * 
     *   1 : ForEach(Pixel, Action<int,int,Pixel>) : x, yを取らないといけないので普通のループ
     *   2 : ForEach(Pixel, Action<T>)             : index参照しないのでunsafeで最適化
     * 
     * Map : 配列の個々の要素を加工 / 写像
     * 
     *   1 : Map(Pixel, Pixel, Action<int,int,Pixel,Pixel>) : x, yを取らないといけないので普通のループ
     *   2 : Map(Pixel, Pixel, Func<T, U>)                  : index参照しないのでunsafeで最適化
     * 
     * currentValue, index, array
     * 
     * Fill : 固定値で設定します
     *
     * 
     * 他言語にはあるけど実装しないもの
     * 
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

    // reduce : 配列の要素を左から右へ処理する / previousValue, currentValue, index, array
    //           accumulator

    public static class HigherOrder
    {


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
                        Parallel.For(sts.top, sts.top + (int)Math.Ceiling((double)sts.height / sts.incY), n =>
                        {
                            var y = n * sts.incY;
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



        public static void Map<T, U>(this Pixel<T> src, Pixel<U> dst, Action<int, int, Pixel<T>, Pixel<U>> action, string subPlane = null, string cfa = null, bool parallel = false) where T : unmanaged where U : unmanaged
        {
            throw new NotImplementedException();
        }

        public static void Map<T, U>(this Pixel<T> src, Pixel<U> dst, Func<int, int, Pixel<T>, U> func, string subPlane = null, string cfa = null, bool parallel = false) where T : unmanaged where U : unmanaged
        {
            throw new NotImplementedException();
            /* action(x, y, src); */
        }

        public static void Map<T, U>(this Pixel<T> src, Pixel<U> dst, Func<T, U> func, string subPlane = null, string cfa = null, bool parallel = false) where T : unmanaged where U : unmanaged
        {
            throw new NotImplementedException();
            /* action(x, y, src); */
        }



        public static Pixel<U> Map<T, U>(this Pixel<T> src, Action<int, int, Pixel<T>, Pixel<U>> action, string subPlane = null, string cfa = null, bool parallel = false) where T : unmanaged where U : unmanaged
        {
            var dst = src.Clone<T, U>();
            Map(src, dst, action, subPlane, cfa, parallel);
            return dst;
        }

        public static Pixel<U> Map<T, U>(this Pixel<T> src, Func<int, int, Pixel<T>, U> func, string subPlane = null, string cfa = null, bool parallel = false) where T : unmanaged where U : unmanaged
        {
            var dst = src.Clone<T, U>();
            Map(src, dst, func, subPlane, cfa, parallel);
            return dst;
        }

        public static Pixel<U> Map<T, U>(this Pixel<T> src, Func<T, U> func, string subPlane = null, string cfa = null, bool parallel = false) where T : unmanaged where U : unmanaged
        {
            var dst = src.Clone<T, U>();
            Map(src, dst, func, subPlane, cfa, parallel);
            return dst;
        }


    }

}
