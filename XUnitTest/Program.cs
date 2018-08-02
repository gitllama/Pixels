using System;
using BenchmarkDotNet.Running;
using Pixels;

namespace XUnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var src = new PixelByte<int>(100, 100);

            src.pix[0] = 1;
            src.pix[1] = 3;
            src.pix[2] = 5;
            src.pix[3] = 8;

            Console.WriteLine(src[0].ToString());

            Console.WriteLine(BitConverter.ToInt32(new byte[] { 1, 3, 5, 8 }, 0).ToString());

            //BenchmarkRunner.Run<LoopBlock>();
            //BenchmarkRunner.Run<Bench_IO>();
            //BenchmarkRunner.Run<BenchExtended>();
        }
    }
}
