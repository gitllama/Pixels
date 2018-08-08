using System;
using BenchmarkDotNet.Running;
using Pixels;

namespace XUnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG

#else
            BenchmarkRunner.Run<OtherBench>();
            //BenchmarkRunner.Run<StructBench>();
            //BenchmarkRunner.Run<PixelBench>();

            //BenchmarkRunner.Run<ExtensionsBench>();
            //BenchmarkRunner.Run<LoopBlock>();
            //BenchmarkRunner.Run<Bench_IO>();

#endif
        }
    }
}
