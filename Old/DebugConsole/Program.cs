using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;

using DebugConsole.Benchmark;
using BenchmarkDotNet.Running;

namespace DebugConsole
{
    class Program
    {
        static unsafe void Main(string[] args)
        {
            //RyuJITは64bitのみ
#if DEBUG
            Bench bench = new Bench();
            bench.ToByte_2();
            //    var bench = new ToByte();
            //    bench.A();
            //    bench.B();
            //    bench.C();
            
            //var a = (float)int.MaxValue+2;
            //    var b = (float)int.MinValue-2;
            //    unchecked
            //    {
            //        Console.WriteLine((int)(a));
            //        Console.WriteLine((int)(b));
            //    }
            //    //0b_SCCC_CCCC_CDDD_DDDD_DDDD_DDDD_DDDD_DDDD
#else
            BenchmarkRunner.Run<Bench>();
#endif
        }
    }
}

