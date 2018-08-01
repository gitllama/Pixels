using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Horology;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Benchmark
{
    [LegacyJitX86Job, RyuJitX64Job]
    [ClrJob, CoreJob]
    public class LoopBlock : ManualConfig
    {
        int Width = 2000;
        int Height = 1000;

        public LoopBlock()
        {

        }

        [Benchmark] public void Default()=> _Default(new int[Width * Height], new int[Width * Height], Width, Height);
        //[Benchmark] public void Default2() => _Default2(new int[Width * Height], new int[Width * Height], Width, Height);
        //[Benchmark] public void SafeA() => _SafeA(new int[Width * Height], new int[Width * Height], Width, Height);
        //[Benchmark] public void UnafeA() => _UnsafeA(new int[Width * Height], new int[Width * Height], Width, Height);


        public void _Default(int[] src, int[] dst, int width, int height)
        {
            for (var y = 1; y < height-1; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var index = x + y * width;
                    var top = src[index - width];
                    var mid = src[index];
                    var bot = src[index + width];

                    dst[index] = (top + mid + bot) / 3;
                }
            }
        }

        public void _Default2(int[] src, int[] dst, int width, int height)
        {
            for (var index = width; index < ( src.Length- width); index++)
            {
                var top = src[index - width];
                var mid = src[index];
                var bot = src[index + width];

                dst[index] = (top + mid + bot) / 3;
            }
        }

        public void _SafeA(int[] src, int[] dst, int width, int height)
        {
            for (var line = width; line < src.Length- width; line+= width)
            {
                for (var index = line; index < line + width; index++)
                {
                    var top = src[index - width];
                    var mid = src[index];
                    var bot = src[index + width];

                    dst[index] = (top + mid + bot) / 3;
                }
            }
        }

        //public unsafe void _UnsafeA(int[] src, int[] dst, int width, int height)
        //{
        //    fixed(int* pinSrc = src)
        //    fixed (int* pinDst = dst)
        //    {
        //        for (var line = width; line < src.Length - width; line += width)
        //        {
        //            var pSrc = pinSrc + line;
        //            var pDst = pinDst + line;

        //            for (var index = 0; index < width; index++)
        //            {
        //                var top = *(pSrc - width);
        //                var mid = *pSrc;
        //                var bot = *(pSrc + width);

        //                *pDst++ = (top + mid + bot) / 3;
        //                pSrc++;
        //            }
        //        }
        //    }
        //}
    }
}
