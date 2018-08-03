using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTest
{
    public static class Utils
    {
        [Obsolete("BenchmarkDotNetを使用してください")]
        public static void Timer(Action action)
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Restart();
            action();
            sw.Stop();
            Console.WriteLine($"　{sw.ElapsedMilliseconds}ms");
        }

    }
}
