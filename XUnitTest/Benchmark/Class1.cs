using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XUnitTest.Benchmark
{
    class test
    {
        void read()
        {
            var bufferSize = 128;
            using (var s = File.Open("a", FileMode.Open))
            {
                Span<byte> buffer = bufferSize <= 128 ? stackalloc byte[bufferSize] : new byte[bufferSize];
                s.Read(buffer); //Core 2.1のみ
            }
        }

    }
}
