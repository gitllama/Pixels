using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DebugConsole.Benchmark
{
    public class SomeTest
    {
        public int[] src;
        public int[] dst;
        public int w;
        public int h;

        public SomeTest()
        {
            w = 8000;
            h = 4000;
            src = new int[w * h];
            dst = new int[w * h];

            //Random r = new Random();
            //for (int i = 0; i < w*h; i++)
            //{
            //    //0以上10未満のランダム整数を返す
            //    src[i] = r.Next(int.MaxValue,int.MinValue);
            //}
        }

        [Benchmark]
        public void LoopBlock()
        {
            for (var y = 0; y < h; y++)
                for (var x = 0; x < w; x++)
                {
                    dst[x + y * w] = src[x + y * w];
                }
        }

        [Benchmark]
        public void LoopBlock4()
        {
            for (var y = 0; y < h; y++)
            {
                for (var x = y * w; x < (y + 1) * w; x++)
                {
                    dst[x] = src[x];
                }
            }
        }

        [Benchmark]
        public void LoopBlock5()
        {
            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
                {
                    dst[x + y * w] = ff(x, y, w, dst);
                }
            }
        }

        [Benchmark]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void LoopBlock6()
        {
            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
                {
                    dst[x + y * w] = ff(x, y, w, dst);
                }
            }
        }

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void LoopBlock7()
        {
            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
                {
                    dst[x + y * w] = ff(x, y, w, dst);
                }
            }
        }

        public static int ff(int a, int b, int w, int[] src)
        {
            return src[a + b * w];
        }

    }
}
