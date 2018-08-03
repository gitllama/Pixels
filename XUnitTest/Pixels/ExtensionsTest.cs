﻿using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

using Pixels;
using Pixels.Extensions;

using Pixels.Extensions.Benchmark;

namespace XUnitTest
{

    public class ExtensionsTest
    {

        [Theory]
        [InlineData(2256, 1178)]
        public void TestMap(int w, int h)
        {
            var src = new Pixel<int>(w, h)
            {
                Bayer = new Dictionary<string, (int x, int y, int width, int height)>
                {
                    ["Gr"] = (0, 0, 2, 2),
                    ["R"] = (1, 0, 2, 2),
                    ["B"] = (0, 1, 2, 2),
                    ["Gb"] = (1, 1, 2, 2)
                }
            };
            var dst1 = new Pixel<int>(w, h);
            var dst2 = new Pixel<int>(w, h);


            src.Map(dst1, (x, y, s, d) => { d[x, y] = s[x, y] + 1; });
            src.Map(dst2, (x, y, s, d) => { d[x, y] = s[x, y] + 1; }, parallel: true);

            Assert.Equal(dst1.ToArray(), dst2.ToArray());

            src.Map(dst1, (x, y, s, d) => { d[x, y] += s[x, y] + 1; }, bayer: "Gr");
            src.Map(dst2, (x, y, s, d) => { d[x, y] += s[x, y] + 1; }, bayer: "Gr" ,parallel: true);

            Assert.Equal(dst1.ToArray(), dst2.ToArray());
        }

        [Theory]
        [InlineData(2256, 1178)]
        public void Test_Foreach(int w, int h)
        {
            var src1 = new Pixel<int>(w, h);
            var src2 = new Pixel<int>(w, h);

            //byte[] rdm = new byte[8];
            //for (var i = 0; i < src1.pix.Length; i++)
            //{
            //    random.NextBytes(rdm);
            //    src1.pix[i] = PixelBitConverter.ToInt32(rdm, 0);
            //    src2.pix[i] = PixelBitConverter.ToInt32(rdm, 0);
            //}

            //src1.ForEach((n) => n + 1);
            //src2.ForEach((n) => n + 1, parallel: true);

            //Assert.Equal(src1.pix, src2.pix);
        }

    }

    [LegacyJitX86Job, RyuJitX64Job]
    [ClrJob, CoreJob]
    public class ExtensionsBench
    {
        int Width = 2256;
        int Height = 1178;
        Random random = new Random();
        Pixel<Int32> src;

        public ExtensionsBench()
        {
            src = new Pixel<Int32>(Width, Height);

            byte[] rdm = new byte[8];
            for (var i = 0; i < src.Size; i++)
            {
                random.NextBytes(rdm);
                src[i] = PixelBitConverter.ToInt32(rdm, 0);
            }
        }

        [Benchmark] public void Foreach0() => src.MapTest(src, (x,y,s,d) => d[x,y] = s[x,y] + 1 );
        //[Benchmark] public void Foreach1() => src.MapTest2(src, (x, y, s, d) => d[x, y] = s[x, y] + 1);

        //[Benchmark] public void Foreach0() => src.ForEach((n) => n + 1);
        //[Benchmark] public void Foreach1() => src.ForEach((n) => n + 1, parallel: true);
        //[Benchmark] public void Foreach2() => src.ForEach2((n) => n + 1);
        //[Benchmark] public void Foreach3() => src.ForEach2((n) => n + 1, parallel: true);
    }

}