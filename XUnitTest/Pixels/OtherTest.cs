using System;
using Xunit;
using Pixels;
using Pixels.IO;
using Xunit.Abstractions;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using System.Numerics;
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
            Assert.Equal(PixelBitConverter.To<Int16E>(bytes, 0), Unsafe.As<byte, Int16E>(ref bytes[0]));

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
        //[Benchmark] public Int16 Default0() => BitConverter.ToInt16(bytes, 0);
        //[Benchmark] public Int16 Default1() => Unsafe.As<byte, Int16>(ref bytes[0]);


        [Benchmark]
        public void LoopFloat()
        {
            var src = new float[2256 * 1178];
            var bytes = new byte[2256 * 1178];

            for (var i = 0; i < src.Length; i++)
            {
                src[i] = (src[i] + 10) * 200 / 255;
                bytes[i] = (byte)(src[i] > 255 ? 255 : src[i] < 0 ? 0 : src[i]); 
            }

        }
        [Benchmark]
        public void LoopVector4()
        {
            var src = new Vector4[2256 * 1178 / 4];
            var bytes = new byte[2256 * 1178];
            var offset = new Vector4(10);
            var depth = new Vector4(200 / 255);
            var max = new Vector4(255);
            var min = new Vector4(0);
            for (var i = 0; i < src.Length; i++)
            {
                src[i] = Vector4.Clamp(Vector4.Multiply(Vector4.Add(src[i], offset), depth), min, max);
                bytes[i * 4 + 0] = (byte)src[i].X;
                bytes[i * 4 + 1] = (byte)src[i].Y;
                bytes[i * 4 + 2] = (byte)src[i].Z;
                bytes[i * 4 + 3] = (byte)src[i].W;
                
            }
        }
    }

}

