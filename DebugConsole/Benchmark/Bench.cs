using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DebugConsole.Benchmark
{
    public class Bench
    {
        Loop<int> _Def = new Loop<int>();
        LoopA _A = new LoopA();
        int w = 8000;
        int h = 4000;
        byte[] dst;
        int[] src;

        public Bench()
        {
            dst = new byte[w * h * 4];
            src = new int[w * h];
        }


        //[Benchmark]
        static unsafe void Copy(byte[] src, byte[] dst)
        {
            int len = src.Length;
            if (len != dst.Length)

                throw new ArgumentException();

            if (len <= 64)

            {

                for (int i = 0; i < len; i++)

                    dst[i] = src[i];

            }
            else
            {

                fixed (byte* p = src)

                {

                    Marshal.Copy(new IntPtr(p), dst, 0, len);

                }

            }

        }

        //[Benchmark]
        public void Def()
        {
            _Def.p(w, h, src, dst);
        }

        //[Benchmark]
        public void A()
        {
            _A.p(w, h, src, dst);
        }

        //[Benchmark]
        public void Def_Q()
        {
            _Def.q(w, h, src, dst);
        }

        //[Benchmark]
        public void A_Q()
        {
            _A.q(w, h, src, dst);
        }

        //[Benchmark]
        public void A_S()
        {
            _A.s(w, h, src, dst);
        }

        [Benchmark]
        public unsafe void s()
        {
            Parallel.For(0, h, y =>
            {
                int offset = y * w;
                var span = src.AsSpan().Slice(offset, w);
                var span2 = dst.AsSpan().Slice(offset * 3, w * 3);
                fixed (int* pin = span)
                fixed (byte* pin2 = span2)
                {
                    var p = pin;
                    var p2 = pin2;
                    for (int index = offset; index < offset + span.Length; index++)
                    {
                        var i = (byte)(1 + *p++);
                        *p2++ = i;
                        *p2++ = i;
                        *p2++ = i;
                    }
                }
            });
        }
        //[Benchmark]
        public unsafe void s1()
        {
            Parallel.For(0, h, y =>
            {
                int offset = y * w;
                var span = src.AsSpan().Slice(offset, w);
                var span2 = dst.AsSpan().Slice(offset * 3, w * 3);
                int c = 0;
                for (int x = 0; x < span.Length; x++)
                {
                    var i = (byte)(1 + span[x]);
                    span2[c++] = i;
                    span2[c++] = i;
                    span2[c++] = i;
                }
            });
        }
        [Benchmark]
        public unsafe void s2()
        {
            Parallel.For(0, h, y =>
            {
                fixed (int* pin = src)
                fixed (byte* pin2 = dst)
                {
                    var p = pin;
                    var p2 = pin2;
                    int offset = y * w;
                    p += offset;
                    p2 += (offset + offset + offset);

                    for (int index = 0; index < w; index++)
                    {
                        var i = (byte)(1 + *p++);
                        *p2++ = i;
                        *p2++ = i;
                        *p2++ = i;
                    }
                }
            });
        }

        [Benchmark]
        public unsafe void s4()
        {
            int stride = w * 3;
            Parallel.For(0, h, y =>
            {
                fixed (int* pin = src)
                fixed (byte* pin2 = dst)
                {
                    var p = pin;
                    var p2 = pin2;
                    p += (y * w);
                    p2 += (y * stride);

                    var last = (p + w);
                    while (p < last)
                    { 
                        var i = (byte)(1 + *p++);
                        *p2++ = i;
                        *p2++ = i;
                        *p2++ = i;
                    }
                }
            });
        }

        //[Benchmark]
        public unsafe void s3()
        {

            Parallel.For(0, h, y =>
            {
                fixed (int* pin = src)
                fixed (byte* pin2 = dst)
                {
                    var p = pin;
                    var p2 = (int*)pin2;
                    int offset = y * w;
                    p += offset;
                    p2 += offset;

                    for (int index = 0; index < w; index++)
                    {
                        var i = (byte)(1 + *p++);
                        *p2++ = (i << 24) | (i << 16) | (i << 8) | (i);
                    }
                }
            });

        }


        //[Benchmark]
        public unsafe void ss()
        {
            Parallel.For(0, h, y =>
            {
                int offset = y * w;
                var span = src.AsSpan().Slice(offset, w);
                var span2 = dst.AsSpan().Slice(offset, w);
                fixed (int* pin = span)
                fixed (byte* pin2 = span2)
                {
                    var p = pin;
                    var p2 = (int*)pin2;
                    for (int index = offset; index < offset + span.Length; index++)
                    {
                        var i = (byte)(1 + *p++);
                        *p2++ = (i << 16) | (i << 8) | (i);
                    }
                }
            });
        }

        //[Benchmark]
        public unsafe void sss()
        {
            Parallel.For(0, h, y =>
            {
                int offset = y * w;
                byte[] buffer = new byte[w * 3];
                var span = src.AsSpan().Slice(offset, w);

                fixed (int* pin = span)
                fixed (byte* pin2 = buffer)
                {
                    var p = pin;
                    var p2 = pin2;
                    for (int index = offset; index < offset + span.Length; index++)
                    {
                        var i = (byte)(1 + *p++);
                        *p2++ = i;
                        *p2++ = i;
                        *p2++ = i;
                    }
                    Marshal.Copy(new IntPtr(pin2), dst, 0, buffer.Length);
                }
            });
        }
    }

    public class LoopA : Loop<int>
    {
        public override void Set(int i, int[] src, byte[] dst)
        {
            dst[i] = (byte)(src[i] + 1);
        }
        public override void Set(int i, Span<int> src, byte[] dst)
        {
            dst[i] = (byte)(src[i] + 1);
        }
        public override unsafe void Set(int i, int* src, byte[] dst)
        {
            dst[i] = (byte)(*src + 1);
        }
        public override unsafe void Set(ref int* src, ref byte* dst)
        {
            *dst++ = (byte)(1 + *src++);
        }
        public override unsafe void Set(int* src, byte* dst)
        {
            *dst++ = (byte)(1 + *src++);
        }

    }

    public class Loop<T> where T : struct
    {
        public virtual void Set(int i, T[] src, byte[] dst)
        {
            //dst[i] = src[i];
        }
        public virtual void Set(int i, Span<T> src, byte[] dst)
        {
            //dst[i] = src[i];
        }
        public virtual unsafe void Set(int i, int* src, byte[] dst)
        {
            //dst[i] = src[i];
        }
        public virtual unsafe void Set(ref int* src, ref byte* dst)
        {
            //dst[i] = src[i];
        }
        public virtual unsafe void Set(int* src, byte* dst)
        {
            //dst[i] = src[i];
        }

        public void p(int w, int h, T[] src, byte[] dst)
        {
            Parallel.For(0, h, y =>
            {
                for (int x = 0; x < w; x++)
                {
                    Set(x + y * w, src, dst);
                }
            });
        }

        public void q(int w, int h, T[] src, byte[] dst)
        {
            Parallel.For(0, h, y =>
            {
                int offset = y * w;
                for (int index = offset; index < offset + w; index++)
                {
                    Set(index, src, dst);
                }
            });
        }

        public void r(int w, int h, T[] src, byte[] dst)
        {
            Parallel.For(0, h, y =>
            {
                int offset = y * w;
                var span = src.AsSpan().Slice(offset, w);
                for (int index = offset; index < offset + span.Length; index++)
                {
                    Set(index, span, dst);
                }
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void s(int w, int h, int[] src, byte[] dst)
        {
            Parallel.For(0, h, y =>
            {
                int offset = y * w;
                var span = src.AsSpan().Slice(offset, w);
                var span2 = dst.AsSpan().Slice(offset, w);
                fixed (int* pin = span)
                fixed (byte* pin2 = span2)
                {
                    var p = pin;
                    var p2 = pin2;
                    for (int index = offset; index < offset + span.Length; index++)
                    {
                        Set(p, p2);
                    }
                }
            });
        }
    }

    public class SomeTest
    {
        public int[] src;
        public int[] dst;
        public int w;
        public int h;

        public SomeTest()
        {
            w = 8000;
            h = 4000;
            src = new int[w * h];
            dst = new int[w * h];

            //Random r = new Random();
            //for (int i = 0; i < w*h; i++)
            //{
            //    //0以上10未満のランダム整数を返す
            //    src[i] = r.Next(int.MaxValue,int.MinValue);
            //}
        }

        [Benchmark]
        public void LoopBlock()
        {
            for (var y = 0; y < h; y++)
                for (var x = 0; x < w; x++)
                {
                    dst[x + y * w] = src[x + y * w];
                }
        }

        [Benchmark]
        public void LoopBlock4()
        {
            for (var y = 0; y < h; y++)
            {
                for (var x = y * w; x < (y + 1) * w; x++)
                {
                    dst[x] = src[x];
                }
            }
        }

        [Benchmark]
        public void LoopBlock5()
        {
            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
                {
                    dst[x + y * w] = ff(x, y, w, dst);
                }
            }
        }

        [Benchmark]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void LoopBlock6()
        {
            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
                {
                    dst[x + y * w] = ff(x, y, w, dst);
                }
            }
        }

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void LoopBlock7()
        {
            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
                {
                    dst[x + y * w] = ff(x, y, w, dst);
                }
            }
        }

        public static int ff(int a, int b, int w, int[] src)
        {
            return src[a + b * w];
        }

    }
}
