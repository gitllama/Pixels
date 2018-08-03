using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DebugConsole.Benchmark
{

    public class Bench
    {
        int w = 2256;
        int h = 1178;
        byte[] dst;
        int[] src;

        //[Benchmark] public int B0_0() => F0(-300);
        //[Benchmark] public int B0_1() => F1(-300);
        //[Benchmark] public int B0_2() => F2(-300);
        //[Benchmark] public int B0_3() => F3(-300);

        //[Benchmark] public int B1_0() => F0(100);
        //[Benchmark] public int B1_1() => F1(100);
        //[Benchmark] public int B1_2() => F2(100);
        //[Benchmark] public int B1_3() => F3(100);

        //[Benchmark] public int B2_0() => F0(300);
        //[Benchmark] public int B2_1() => F1(300);
        //[Benchmark] public int B2_2() => F2(300);
        //[Benchmark] public int B2_3() => F3(300);

        [Benchmark]
        public unsafe void ToByte_0()
        {
            fixed (int* s = src)
            fixed (byte* d = dst)
            {
                ToByte(s, d, src.Length);
            }
        }
        [Benchmark]
        public unsafe void ToByte_1()
        {
            fixed (int* s = src)
            fixed (byte* d = dst)
            {
                ToByte1(s, (Color*)d, src.Length);
            }
        }
        [Benchmark]
        public unsafe void ToByte_2()
        {
            fixed (int* s = src)
            fixed (byte* d = dst)
            {
                ToByte2(s, d, src.Length);
            }
        }

        public Bench()
        {
            dst = new byte[w * h * 4];
            src = new int[w * h];
        }


        #region Clamp

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Clamp(float val) => val > Byte.MaxValue ? Byte.MaxValue : val < Byte.MinValue ? Byte.MinValue : (byte)val;
        public static byte Clamp2(float val) => (byte)Math.Min(255f, Math.Max(0f, val));
        public static byte Clamp1(float val)
        {
            if (val > 255)
            {
                return (byte)255;
            }
            else if (val < 0)
            {
                return (byte)0;
            }
            return (byte)val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clamp3(ref Color color, in (float B, float G, float R) val)
        {
            color.B = Clamp(val.B); //別に書くよりキャッシュしてる？
            color.G = Clamp(val.G);
            color.R = Clamp(val.R);
            color.A = 0; 
        }

        #endregion


        #region Matrix

        public struct Matrix
        {
            public float M11, M12, M13, M21, M22, M23, M31, M32, M33;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct Color
        {
            public byte B, G, R, A;
        }

        Matrix _matrix = new Matrix() { M11 = 1, M12 = 0, M13 = 0, M21 = 0, M22 = 1, M23 = 0, M31 = 0, M32 = 0, M33 = 1 };


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void _Matrix(ref (float B, float G, float R) bayer, ref Matrix matrix)
        {
            var _B = matrix.M11 * bayer.B + matrix.M12 * bayer.G + matrix.M13 * bayer.R;
            var _G = matrix.M21 * bayer.B + matrix.M22 * bayer.G + matrix.M23 * bayer.R;
            var _R = matrix.M31 * bayer.B + matrix.M32 * bayer.G + matrix.M33 * bayer.R;
            bayer = (_B, _G, _R);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void _Matrix(ref Vector4 bayer, in Matrix matrix)
        {
            bayer.X = matrix.M11 * bayer.X + matrix.M12 * bayer.Y + matrix.M13 * bayer.Z;
            bayer.Y = matrix.M21 * bayer.X + matrix.M22 * bayer.Y + matrix.M23 * bayer.Z;
            bayer.Z = matrix.M31 * bayer.X + matrix.M32 * bayer.Y + matrix.M33 * bayer.Z;
            //bayer = (_B, _G, _R);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (float B, float G, float R) _Matrix((float B, float G, float R) bayer, Matrix matrix)
        {
            var _B = matrix.M11 * bayer.B + matrix.M12 * bayer.G + matrix.M13 * bayer.R;
            var _G = matrix.M21 * bayer.B + matrix.M22 * bayer.G + matrix.M23 * bayer.R;
            var _R = matrix.M31 * bayer.B + matrix.M32 * bayer.G + matrix.M33 * bayer.R;
            return (_B, _G, _R);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void ToByte(int* src, byte* dst, int length)
        {
            for (var i = 0; i < length; i++)
            {
                var buf = (float)*src++;
                (float B, float G, float R) bayer = (buf, buf, buf);　//これはやい？外に追い出そうがかわらん
                _Matrix(ref bayer, ref _matrix); //ローカルに代入しないほがはやい

                *dst++ = Clamp(bayer.B);
                *dst++ = Clamp(bayer.G);
                *dst++ = Clamp(bayer.R);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void ToByte1(int* src, Color* dst, int length)
        {
            for (var i = 0; i < length; i++)
            {
                var buf = (float)*src++;
                (float B, float G, float R) bayer = (buf, buf, buf);
                _Matrix(ref bayer, ref _matrix);

                //color.B = Clamp(bayer.B);
                //color.G = Clamp(bayer.G);
                //color.R = Clamp(bayer.R);
                //*dst++ = color;
                //*dst++ = Clamp(bayer.B) << 16 | Clamp(bayer.G) << 8 | Clamp(bayer.R);

                Clamp3(ref *dst, in bayer); // 戻り値よりはやい
                dst++;
            }
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void ToByte2(int* src, byte* dst, int width)
        {
            Vector4 v = new Vector4(0);
            Vector4 min = new Vector4(0);
            Vector4 max = new Vector4(255);

            for (var i = 0; i < width; i++)
            {
                var buf = (float)*src++;
                v.X = buf;
                v.Y = buf;
                v.Z = buf;
                _Matrix(ref v, in _matrix);
                Vector4.Clamp(v, min, max); //x64でないときかん
                
                *dst++ = (byte)v.X;
                *dst++ = (byte)v.Y;
                *dst++ = (byte)v.Z;

            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void ToByte0(int* src, byte* dst, int width)
        {
            for (var i = 0; i < width; i++)
            {
                var buf = (float)*src++;
                var R = buf;
                var G = buf;
                var B = buf;
                var _B = _matrix.M11 * B + _matrix.M12 * G + _matrix.M13 * R;
                var _G = _matrix.M21 * B + _matrix.M22 * G + _matrix.M23 * R;
                var _R = _matrix.M31 * B + _matrix.M32 * G + _matrix.M33 * R;
                *dst++ = _B > Byte.MaxValue ? Byte.MaxValue : _B < Byte.MinValue ? Byte.MinValue : (byte)_B;
                *dst++ = _G > Byte.MaxValue ? Byte.MaxValue : _G < Byte.MinValue ? Byte.MinValue : (byte)_G;
                *dst++ = _R > Byte.MaxValue ? Byte.MaxValue : _R < Byte.MinValue ? Byte.MinValue : (byte)_R;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void ToByte3(int* src, byte* dst, int length)
        {
            for (var i = 0; i < length; i++)
            {
                var buf = (float)*src++;
                var bayer = _Matrix((buf, buf, buf), _matrix);
                *dst++ = Clamp(bayer.B);
                *dst++ = Clamp(bayer.G);
                *dst++ = Clamp(bayer.R);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void ToByte4(int* src, int* dst, int length)
        {
            for (var i = 0; i < length; i++)
            {
                var buf = (float)*src++;
                var bayer = _Matrix((buf, buf, buf), _matrix);
                *dst++ = Clamp(bayer.B) << 16 | Clamp(bayer.G) << 8 | Clamp(bayer.R);
            }
        }

        #endregion


        #region MyRegion

        //[Benchmark]//235.30 ms
        public void Default()
        {
            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                {
                    int index = x + y * w;
                    byte buf = (byte)src[index];
                    dst[index * 3 + 0] = buf;
                    dst[index * 3 + 1] = buf;
                    dst[index * 3 + 2] = buf;
                }
        }


        //163.55 ms
        public void SingleLoop()
        {
            for (int i = 0; i < h * w; i++)
            {
                byte buf = (byte)src[i];
                dst[i * 3 + 0] = buf;
                dst[i * 3 + 1] = buf;
                dst[i * 3 + 2] = buf;
            }
        }

        //92.39 ms
        public void PinLoop()
        {
            unsafe
            {
                fixed (byte* pindst = dst)
                fixed (int* pinsrc = src)
                {
                    var pdst = pindst;
                    var psrc = pinsrc;
                    for (int i = 0; i < h * w; i++)
                    {
                        byte buf = (byte)psrc++;

                        *pdst++ = buf;
                        *pdst++ = buf;
                        *pdst++ = buf;
                    }

                }
            }
        }

        //[Benchmark]　//62.52 ms
        public void PinLoop2()
        {
            unsafe
            {
                fixed (byte* pindst = dst)
                fixed (int* pinsrc = src)
                {
                    var pdst = pindst;
                    var psrc = pinsrc;
                    var last = psrc + (h * w);
                    while (psrc < last)
                    {
                        var buf = (byte)psrc++;

                        *pdst++ = buf;
                        *pdst++ = buf;
                        *pdst++ = buf;
                    }

                }
            }
        }

        //おそくなる　96.70
        public void PinLoop32()
        {
            unsafe
            {
                fixed (byte* pindst = dst)
                fixed (int* pinsrc = src)
                {
                    var pdst = (int*)pindst;
                    var psrc = pinsrc;
                    var end = h * w;
                    for (int i = 0; i < h * w; i++)
                    {
                        byte buf = (byte)psrc++;
                        *pdst++ = (buf << 16) | (buf << 8) | (buf);
                    }

                }
            }
        }

        //81.34 ms
        public void AsSpan()
        {
            unsafe
            {
                var _src = src.AsSpan().Slice(0, w * h);
                var _dst = dst.AsSpan().Slice(0, w * h * 3);
                fixed (byte* pindst = _dst)
                fixed (int* pinsrc = _src)
                {
                    var pdst = pindst;
                    var psrc = pinsrc;
                    var end = h * w;
                    for (int i = 0; i < end; i++)
                    {
                        byte buf = (byte)psrc++;

                        *pdst++ = buf;
                        *pdst++ = buf;
                        *pdst++ = buf;
                    }

                }
            }
        }


        //98.61
        public void AsSpan1()
        {
            unsafe
            {
                var _src = src.AsSpan().Slice(0, w * h);
                var _dst = dst.AsSpan().Slice(0, w * h * 3);
                fixed (byte* pindst = _dst)
                {
                    var pdst = pindst;
                    var end = h * w;
                    foreach (var n in _src)
                    {
                        byte buf = (byte)n;

                        *pdst++ = buf;
                        *pdst++ = buf;
                        *pdst++ = buf;
                    }

                }
            }
        }

        //[Benchmark]
        public void AsSpan5()
        {
            unsafe
            {
                int fullsize = h * w;
                int stride = w * 3;
                for (var line = 0; line < fullsize; line += w)
                {
                    var _src = src.AsSpan().Slice(line, w);
                    var _dst = dst.AsSpan().Slice(line * 3, stride);
                    fixed (byte* pindst = _dst)
                    fixed (int* pinsrc = _src)
                    {
                        var psrc = pinsrc;
                        var last = pinsrc + _src.Length;
                        var pdst = pindst;
                        while (psrc < last)
                        {
                            byte buf = (byte)*psrc;
                            psrc++;

                            *pdst = buf;
                            pdst++;
                            *pdst = buf;
                            pdst++;
                            *pdst = buf;
                            pdst++;
                        }
                    }
                }
            }
        }

        //[Benchmark]
        public void AsSpan6()
        {
            unsafe
            {
                var mat = new float[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                int fullsize = h * w;
                int stride = w * 4;
                for (var line = 0; line < fullsize; line += w)
                {
                    var _src = src.AsSpan().Slice(line, w);
                    var _dst = dst.AsSpan().Slice(line * 4, stride);
                    fixed (byte* pindst = _dst)
                    fixed (int* pinsrc = _src)
                    {
                        var psrc = pinsrc;
                        var last = pinsrc + _src.Length;
                        var pdst = pindst;
                        while (psrc < last)
                        {
                            float buf = (float)*psrc;
                            psrc++;
                            var B = (mat[0] * buf + mat[1] * buf + mat[2] * buf);
                            var G = (mat[0] * buf + mat[1] * buf + mat[2] * buf);
                            var R = (mat[0] * buf + mat[1] * buf + mat[2] * buf);
                            B = B > 255 ? 255 : B < 0 ? 0 : B;
                            G = G > 255 ? 255 : G < 0 ? 0 : G;
                            R = R > 255 ? 255 : R < 0 ? 0 : R;
                            *pdst++ = (byte)B;
                            *pdst++ = (byte)G;
                            *pdst++ = (byte)R;
                        }
                    }
                }
            }
        }

        //[Benchmark]
        public void AsSpan7()
        {
            var matrix = new Matrix4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0);

            unsafe
            {
                int fullsize = h * w;
                int stride = w * 4;
                Vector3 v = new Vector3();
                Vector3 min = new Vector3(0);
                Vector3 max = new Vector3(255);

                for (var line = 0; line < fullsize; line += w)
                {
                    var _src = src.AsSpan().Slice(line, w);
                    var _dst = dst.AsSpan().Slice(line * 4, stride);
                    fixed (byte* pindst = _dst)
                    fixed (int* pinsrc = _src)
                    {
                        var psrc = pinsrc;
                        var last = pinsrc + _src.Length;
                        var pdst = pindst;
                        while (psrc < last)
                        {
                            var buf = (float)*psrc++;
                            v.X = buf;
                            v.Y = buf;
                            v.Z = buf;
                            Vector3.Transform(v, matrix);
                            Vector3.Clamp(v, min, max);
                            *pdst++ = (byte)v.X;
                            *pdst++ = (byte)v.Y;
                            *pdst++ = (byte)v.Z;
                        }
                    }
                }
            }
        }

        #endregion



        Loop<int> _Def = new Loop<int>();
        LoopA _A = new LoopA();

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

        //[Benchmark]
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
        //[Benchmark]
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

        //[Benchmark]
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


//public class ToByte
//{
//    int w = 8000;
//    int h = 4000;
//    byte[] dst;
//    int[] src;





//    public ToByte()
//    {
//        dst = new byte[w * h * 3];
//        src = new int[w * h];
//    }

//    [Benchmark]
//    public unsafe void A()
//    {
//        Matrix4x4 matrix = new Matrix4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0);
//        Vector3 min = new Vector3(0);
//        Vector3 max = new Vector3(255);

//        fixed (byte* pindst = dst)
//        fixed (int* pinsrc = src)
//        {
//            var p = pinsrc;
//            var b = pindst;
//            Vector3 v = new Vector3();
//            for (var i = 0; i < src.Length; i++)
//            {
//                var buf = (float)*p++;
//                v.X = buf;
//                v.Y = buf+1;
//                v.Z = buf+2;
//                Vector3.Transform(v, matrix);
//                Vector3.Clamp(v, min, max);
//                *b++ = (byte)v.X;
//                *b++ = (byte)v.Y;
//                *b++ = (byte)v.Z;
//            }
//        }
//    }

//    [Benchmark]
//    public unsafe void B()
//    {
//        float[] matrix = new float[] { 1, 0, 0, 0, 1, 0, 0, 0, 1 };

//        fixed (byte* pindst = dst)
//        fixed (int* pinsrc = src)
//        {
//            var p = pinsrc;
//            var b = pindst;
//            for (var i = 0; i < src.Length; i++)
//            {
//                var buf = (float)*p++;
//                var R = buf;
//                var G = buf+1;
//                var B = buf+2;
//                R = matrix[0] * R + matrix[1] * G + matrix[2] * B;
//                B = matrix[3] * R + matrix[4] * G + matrix[5] * B;
//                G = matrix[6] * R + matrix[7] * G + matrix[8] * B;
//                *b++ = (byte)(R > 255 ? 255 : R < 0 ? 0 : R);
//                *b++ = (byte)(G > 255 ? 255 : G < 0 ? 0 : G);
//                *b++ = (byte)(B > 255 ? 255 : B < 0 ? 0 : B);
//            }
//        }
//    }

//    [Benchmark]
//    public unsafe void C()
//    {
//        Matrix matrix = new Matrix() { M11 = 1, M12 = 0, M13 = 0, M21 = 0, M22 = 1, M23 = 0, M31 = 0, M32 = 0, M33 = 1 };

//        fixed (byte* pindst = dst)
//        fixed (int* pinsrc = src)
//        {
//            var p = pinsrc;
//            var b = pindst;
//            for (var i = 0; i < src.Length; i++)
//            {
//                var buf = (float)*p++;
//                var R = buf;
//                var G = buf + 1;
//                var B = buf + 2;
//                R = matrix.M11 * R + matrix.M12 * G + matrix.M13 * B;
//                B = matrix.M21 * R + matrix.M22 * G + matrix.M23 * B;
//                G = matrix.M31 * R + matrix.M32 * G + matrix.M33 * B;
//                *b++ = (byte)(R > 255 ? 255 : R < 0 ? 0 : R);
//                *b++ = (byte)(G > 255 ? 255 : G < 0 ? 0 : G);
//                *b++ = (byte)(B > 255 ? 255 : B < 0 ? 0 : B);
//            }
//        }
//    }

//    [Benchmark]
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public unsafe void D()
//    {
//        Matrix matrix = new Matrix() { M11 = 1, M12 = 0, M13 = 0, M21 = 0, M22 = 1, M23 = 0, M31 = 0, M32 = 0, M33 = 1 };

//        fixed (byte* pindst = dst)
//        fixed (int* pinsrc = src)
//        {
//            var p = pinsrc;
//            var b = pindst;
//            for (var i = 0; i < src.Length; i++)
//            {
//                var buf = (float)*p++;
//                matrix.R = buf;
//                matrix.G = buf + 1;
//                matrix.B = buf + 2;
//                *b++ = matrix.GetR();
//                *b++ = matrix.GetB();
//                *b++ = matrix.GetG();
//            }
//        }
//    }
//}
