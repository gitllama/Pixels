using System;
using Xunit;
using Pixels;
using Pixels.IO;
using Xunit.Abstractions;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace XUnitTest
{

    public class OtherTest
    {
        private readonly ITestOutputHelper output;
        Random random = new Random();

        [Fact]
        public void Test()
        {
            var bytes = new byte[8];
            random.NextBytes(bytes);
           
            Assert.Equal(BitConverter.ToInt16(bytes, 0), Unsafe.As<byte, Int16>(ref bytes[0]));
            Assert.Equal(PixelBitConverter.ToInt16E(bytes, 0), Unsafe.As<byte, Int16E>(ref bytes[0]));

        }

    }

    [LegacyJitX86Job, RyuJitX64Job]
    [ClrJob, CoreJob]
    public class OtherBench
    {
        byte[] bytes = new byte[8];

        public OtherBench()
        {
            Random random = new Random();
            random.NextBytes(bytes);
        }
        [Benchmark] public Int16 Default0() => BitConverter.ToInt16(bytes, 0);
        [Benchmark] public Int16 Default1() => Unsafe.As<byte, Int16>(ref bytes[0]);

    }
}

