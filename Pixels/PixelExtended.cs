using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pixels
{

    public static class PixelExtented
    {









        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="func"></param>
        /// <param name="parallel">funcの外部変数は排他制御必要</param>
        public unsafe static void ForEach(this Pixel<int> src, Func<int, int> func, string subPlane = "Full", bool parallel = false)
        {
            (int left, int top, int width, int height) plane = src.GetPlane(subPlane);
            if (parallel)
            {
                //Lockしないので外部の変数参照はよくない
                //60->5何が最適化してる?
                //Parallel.For(0, src.pix.Length, i =>
                //{
                //    src.pix[i] = func(src.pix[i]);
                //});

                //152->0ms
                Parallel.For(plane.top, plane.top + plane.height, y =>
                {
                    int yy = y * src.Width;
                    var span = src.pix.AsSpan().Slice(yy + plane.left, plane.width);
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
            else
            {
                //25-5
                for (int y = plane.top; y < plane.top + plane.height; y++)
                {
                    int yy = y * src.Width;
                    var span = src.pix.AsSpan().Slice(yy + plane.left, plane.width);
                    fixed (int* pin = span)
                    {
                        var p = pin;
                        while (p < pin + span.Length)
                        {
                            *p = func(*p);
                            p++;
                        }
                    }
                }
            }
        }

        public unsafe static void Map(this Pixel<int> src, Pixel<int> dst, Action<int, int, Pixel<int>, Pixel<int>> action, string subPlane = "Full", bool parallel = false)
        {
            (int left, int top, int width, int height) plane = src.SubPlane[subPlane];
            if (parallel)
            {

            }
            else
            {
                //25-5
                for (int y = plane.top; y < plane.top + plane.height; y++)
                {
                    for (int x = plane.left; x < plane.left + plane.width; x++)
                    {
                        action(x, y, src, dst);
                    }
                }
            }
        }

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
                                Interlocked.Increment(ref counter);
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

        public static void RadixSort(int[] a)
        {
            List<int>[] bucket = new List<int>[256];
            for (int d = 0, logR = 0; d < 4; ++d, logR += 8)
            {
                for (int i = 0; i < a.Length; ++i)
                {
                    int key = (a[i] >> logR) & 255; // a[i] を256進 d 桁目だけを取り出す。
                    if (bucket[key] == null) bucket[key] = new List<int>();
                    bucket[key].Add(a[i]);
                }

                for (int j = 0, i = 0; j < bucket.Length; ++j)
                    if (bucket[j] != null)
                        foreach (int val in bucket[j])
                            a[i++] = val;

                for (int j = 0; j < bucket.Length; ++j)
                    bucket[j] = null;
            }
        }

        public static int SortTest(int[] a)
        {
            int[] bucket = new int[1024 * 2 + 1];
            for (int i = 0; i < a.Length; ++i)
            {
                var hoge = a[i] > 1024 ? 1024 + 1024 : a[i] < -1024 ? 0 : a[i] + 1024;
                bucket[hoge]++;
            }

            int c = 0;
            for (int i = 0; i < bucket.Length; i++)
            {
                c += bucket[i];
                if (c >= a.Length / 2) return i - 1024;
            }
            return -1;
        }

    }


}



/*
ReaderWriterLockSlim locker = new ReaderWriterLockSlim();
{
    try
    {
        locker.EnterWriteLock(); //locker.EnterReadLock();
        //hogehoge
    }
    finally
    {
        locker.ExitWriteLock(); //locker.ExitReadLock();
    }
}
var lockObject = new object();
lock (lockObject)
{
}
*/


//http://article.higlabo.com/ja/thread_locking.html
//http://xptn.dtiblog.com/blog-entry-94.html
//http://xptn.dtiblog.com/blog-entry-85.html




//class Pixel
//{
//    public int[] pix;
//    public void Add(int val)
//    {
//        var a = (10, 10);

//        var span = pix.AsSpan().Slice(2, 3);
//        for (int i = 0; i < span.Length; i++)
//        {
//            span[i] += val;
//        }
//    }


//    // Core2.1
//    static void Clear(Span<int> span)
//    {
//        unsafe
//        {
//            // 結局内部的には unsafe にしてポインターを使った方が速い場合あり
//            fixed (int* pin = &span.GetPinnableReference())
//            // 注: C# 7.3 からは以下の書き方ができる
//            // fixed (byte* pin = span)
//            {
//                var p = pin;
//                var last = p + span.Length;
//                while (p + 8 < last)
//                {
//                    *(ulong*)p = 0;
//                    p += 8;
//                }
//                if (p + 4 < last)
//                {
//                    *(uint*)p = 0;
//                    p += 4;
//                }
//                while (p < last)
//                {
//                    *p = 0;
//                    ++p;
//                }
//            }
//        }
//    }

//    static void Read()
//    {
//        //https://ufcpp.net/study/csharp/resource/span/
//        const int BufferSize = 128;
//        using (var f = File.OpenRead("test.data"))
//        {
//            var rest = (int)f.Length;
//            // Span<byte> で受け取ることで、new (配列)を stackalloc (スタック確保)に変更できる
//            Span<byte> buffer = stackalloc byte[BufferSize];

//            while (true)
//            {
//                // Read(Span<byte>) が追加された
//                //var read = f.Read(buffer);
//                //rest -= read;
//                if (rest == 0) break;

//                // buffer に対して何か処理する
//            }
//        }
//    }

//    //読み取り専用の参照渡し
//    public static Pixel operator *(in Pixel a, in Pixel b)
//    => new Pixel();

//    static ref readonly int Max(in int x, in int y)
//    {
//        ref readonly var t = ref x;
//        ref readonly var u = ref y;

//        if (t < u) return ref u;
//        else return ref t;
//    }
//}

//public struct PixelInt
//{
//    public int[] pix;
//    readonly int width;
//    readonly int height;

//    public PixelInt(int width, int height)
//    {
//        pix = new int[width * height];
//        this.width = width;
//        this.height = height;
//    }

//    public Span<int> Slice(int start, int length)
//    {
//        var span = pix.AsSpan().Slice(2, 3);
//        //for (int i = 0; i < span.Length; i++)
//        //{
//        //    span[i] += val;
//        //}
//        return span;
//    }

//    public void Slice1(int x_start, int x_length, int y_start, int y_length)
//    {
//        for (int y = y_start; y < y_length + y_start; y++)
//        {
//            var span = pix.AsSpan().Slice(y * width + x_start, x_length);
//            for (int x = 0; x < span.Length; x++)
//            {
//                span[x] += 10;
//            }
//        }
//    }

//    public void Slice2(int x_start, int x_length, int y_start, int y_length)
//    {
//        for (int y = y_start; y < y_length + y_start; y++)
//        {
//            for (int x = y * width + x_start; x < y * width + x_length + x_start; x++)
//            {
//                pix[x] += 10;
//            }
//        }
//    }

//    public unsafe void Slice3(int x_start, int x_length, int y_start, int y_length)
//    {
//        for (int y = y_start; y < y_length + y_start; y++)
//        {
//            var span = pix.AsSpan().Slice(y * width + x_start, x_length);
//            fixed (int* pin = span)
//            {
//                var p = pin;
//                var last = p + span.Length;
//                while (p < last)
//                {
//                    *p += 11;
//                    p++;
//                }
//            }
//            //foreach (ref int x in span)
//            //{
//            //    x += 10;
//            //}
//        }
//    }

//    public unsafe void Slice4(int x_start, int x_length, int y_start, int y_length)
//    {
//        for (int y = y_start; y < y_length + y_start; y++)
//        {
//            var span = pix.AsSpan().Slice(y * width + x_start, x_length);

//            var bytes = MemoryMarshal.AsBytes(span);


//            fixed (int* pin = span)
//            {
//                var p = pin;
//                var last = p + span.Length;
//                while (p < last)
//                {
//                    *p += 11;
//                    p++;
//                }
//            }
//            //foreach (ref int x in span)
//            //{
//            //    x += 10;
//            //}
//        }
//    }
//}