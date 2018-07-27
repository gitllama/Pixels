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

            BenchmarkRunner.Run<Bench>();
        }
    }
}

