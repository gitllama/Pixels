using System;
using Xunit;
using Pixels;
using Pixels.IO;
using Xunit.Abstractions;
using System.Collections.Generic;

namespace XUnitTest
{
    public class StructTest
    {
        private readonly ITestOutputHelper output;
        Random random = new Random();

        [Fact]
        public void Test_Int24()
        {
            for(var i = Int24.MinValue; i<=Int24.MaxValue; i++)
            {
                var bytes = BitConverter.GetBytes(i);

                Assert.Equal(i, (int)(new Int24(bytes[0], bytes[1], bytes[2])));
                Assert.Equal(i, (int)(Int24.FromByte(bytes, 0)));
            }
        }

        [Fact]
        public void Test_UInt24()
        {
            for (uint i = UInt24.MinValue; i <= UInt24.MaxValue; i++)
            {
                var bytes = BitConverter.GetBytes(i);

                Assert.Equal(i, (uint)(new UInt24(bytes[0], bytes[1], bytes[2])));
                Assert.Equal(i, (uint)(UInt24.FromByte(bytes, 0)));
            }
        }


        [Theory]
        [InlineData(256)]
        public void Test_Int64E(int n)
        {
            var expected = new List<long>();
            expected.Add(long.MinValue);
            expected.Add(long.MaxValue);
            var buf = new byte[8];
            Array.ForEach(new int[n], _ => {
                random.NextBytes(buf);
                expected.Add(BitConverter.ToInt64(buf, 0));
            });

            foreach (var i in expected)
            {
                var bytes = BitConverter.GetBytes(i);
                Array.Reverse(bytes);
                Assert.Equal(i, (long)(new Int64E(bytes[0], bytes[1], bytes[2], bytes[3], bytes[4], bytes[5], bytes[6], bytes[7])));
                Assert.Equal(i, (long)(Int64E.FromByte(bytes, 0)));
            }
        }

        [Theory]
        [InlineData(256)]
        public void Test_Int32E(int n)
        {
            var expected = new List<int>();
            expected.Add(Int32E.MinValue);
            expected.Add(Int32E.MaxValue);
            Array.ForEach(new int[n], _ => expected.Add((int)random.Next(Int32E.MinValue, Int32E.MaxValue)));

            foreach (var i in expected)
            {
                var bytes = BitConverter.GetBytes(i);
                Array.Reverse(bytes);
                Assert.Equal(i, (int)(new Int32E(bytes[0], bytes[1], bytes[2], bytes[3])));
                Assert.Equal(i, (int)(Int32E.FromByte(bytes, 0)));
            }
        }

        [Theory]
        [InlineData(256)]
        public void Test_Int24E(int n)
        {
            var expected = new List<int>();
            expected.Add(Int24E.MinValue);
            expected.Add(Int24E.MaxValue);
            Array.ForEach(new int[n], _ => expected.Add((int)random.Next(Int24E.MinValue, Int24E.MaxValue)));

            foreach (var i in expected)
            {
                var bytes = BitConverter.GetBytes(i);
                Array.Reverse(bytes);
                Assert.Equal(i, (int)(new Int24E(bytes[1], bytes[2], bytes[3])));
                Assert.Equal(i, (int)(Int24E.FromByte(bytes, 1)));
            }
        }

        [Theory]
        [InlineData(256)]
        public void Test_Int16E(int n)
        {
            var expected = new List<Int16>();
            expected.Add(Int16E.MinValue);
            expected.Add(Int16E.MaxValue);
            Array.ForEach(new int[n], _ => expected.Add((Int16)random.Next(Int16.MinValue, Int16.MaxValue)));

            foreach (var i in expected)
            {
                var bytes = BitConverter.GetBytes(i);
                Array.Reverse(bytes);
                Assert.Equal(i, (int)(new Int16E(bytes[0], bytes[1])));
                Assert.Equal(i, (int)(Int16E.FromByte(bytes, 0)));
            }

        }
    }
}

