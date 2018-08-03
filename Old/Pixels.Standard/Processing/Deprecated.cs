using System;
using System.Collections.Generic;
using System.Text;

namespace Pixels.Standard.Processing
{
    class Class1
    {
    }
}

// エリア限定する際
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