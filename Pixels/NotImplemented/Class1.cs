using System;
using System.Collections.Generic;
using System.Text;

namespace Pixels.NotImplemented
{
    class Class1
    {
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


/*

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

 */
