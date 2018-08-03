using BenchmarkDotNet.Attributes;
using OpenCvSharp;
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
        Matrix3x3 mat = new Matrix3x3() { M11 = 1, M12 = 0, M13 = 0, M21 = 0, M22 = 1, M23 = 0, M31 = 0, M32 = 0, M33 = 1 };






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

        [Benchmark] public void OpenCV() => OpenCV(src, dst, w,h, mat);
        [Benchmark] public void Default() => LineDefault((1,1,2254,1176));
        [Benchmark] public void Test() => LineTest((1, 1, 2254, 1176));

        //[Benchmark] public void ToByte_0() => ToByte(src, dst);
        //[Benchmark] public void ToByte_1() => ToByte1(src, dst);


        //[Benchmark]
        //public unsafe void ToByte_2()
        //{
        //    fixed (int* s = src)
        //    fixed (byte* d = dst)
        //    {
        //        ToByte2(s, d, src.Length);
        //    }
        //}

        public Bench()
        {
            dst = new byte[w * h * 4];
            src = new int[w * h];
        }

        #region OpenCV

        public static void OpenCV(int[] src, byte[] dst, int w,int h,Matrix3x3 matrix)
        {
            var ary = Array.ConvertAll(src, (i) => (UInt16)i);
            var CV_16UC3 = new UInt16[h * w * 3];
            float[] mat = new float[] {
                matrix.M11, matrix.M12, matrix.M13,
                matrix.M21, matrix.M22, matrix.M23,
                matrix.M31, matrix.M32, matrix.M33
            };

            using (var mat16UC1 = new Mat(h, w, MatType.CV_16U, ary))
            using (var mat16UC3 = new Mat(h, w, MatType.CV_16UC3, CV_16UC3))
            using (var matMatrix = new Mat(3, 3, MatType.CV_32FC1, mat))
            using (var matDst = new Mat(h, w, MatType.CV_8UC3, dst))
            {
                Cv2.CvtColor(mat16UC1, mat16UC3, ColorConversionCodes.BayerBG2BGR);
                Cv2.Transform(mat16UC3, mat16UC3, matMatrix);
                //for(var i = 0; i < CV_16UC3.Length; i++)
                //{
                //    var val = CV_16UC3[i];
                //    dst[i] = val > Byte.MaxValue ? Byte.MaxValue : val < Byte.MinValue ? Byte.MinValue : (byte)val;
                //}

            }

        }

        #endregion


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

        public struct Matrix3x3
        {
            public float M11, M12, M13, M21, M22, M23, M31, M32, M33;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Color
        {
            public byte B, G, R, A;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct Color3
        {
            public byte B, G, R;
        }
        public struct Vec
        {
            public float X, Y, Z;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Matrix(ref (float B, float G, float R) bayer, in Matrix3x3 matrix)
        {
            var _B = matrix.M11 * bayer.B + matrix.M12 * bayer.G + matrix.M13 * bayer.R;
            var _G = matrix.M21 * bayer.B + matrix.M22 * bayer.G + matrix.M23 * bayer.R;
            var _R = matrix.M31 * bayer.B + matrix.M32 * bayer.G + matrix.M33 * bayer.R;
            bayer = (_B, _G, _R);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void _Matrix(ref Vector4 bayer, in Matrix3x3 matrix)
        {
            bayer.X = matrix.M11 * bayer.X + matrix.M12 * bayer.Y + matrix.M13 * bayer.Z;
            bayer.Y = matrix.M21 * bayer.X + matrix.M22 * bayer.Y + matrix.M23 * bayer.Z;
            bayer.Z = matrix.M31 * bayer.X + matrix.M32 * bayer.Y + matrix.M33 * bayer.Z;
            //bayer = (_B, _G, _R);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (float B, float G, float R) _Matrix((float B, float G, float R) bayer, Matrix3x3 matrix)
        {
            var _B = matrix.M11 * bayer.B + matrix.M12 * bayer.G + matrix.M13 * bayer.R;
            var _G = matrix.M21 * bayer.B + matrix.M22 * bayer.G + matrix.M23 * bayer.R;
            var _R = matrix.M31 * bayer.B + matrix.M32 * bayer.G + matrix.M33 * bayer.R;
            return (_B, _G, _R);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte vectorDot2(in Vec src, in Vec src2)
        {
            var val = src2.X * src.X + src2.Y * src.Y + src2.Z * src.Z;
            return val > Byte.MaxValue ? Byte.MaxValue : val < Byte.MinValue ? Byte.MinValue : (byte)val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte vectorDot(Vector3 src, Vector3 src2)
        {
            var val = src2.X * src.X + src2.Y * src.Y + src2.Z * src.Z;
            return val > Byte.MaxValue ? Byte.MaxValue : val < Byte.MinValue ? Byte.MinValue : (byte)val;
        }

        public void LineDefault((int left,int top,int width,int height) area)
        {
            for (var line = area.top; line < area.top + area.height; line++)
            {
                var linehead = (line * w) + area.left - 1;
                var top = src.AsSpan().Slice(linehead - w, area.width + 2);
                var mid = src.AsSpan().Slice(linehead + 0, area.width + 2);
                var bot = src.AsSpan().Slice(linehead + w, area.width + 2);

                var spanDst = dst.AsSpan().Slice(linehead * 3, (area.width + 2) * 3);

                ColDefault(top, mid, bot, spanDst, mat);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ColDefault(Span<int> top, Span<int> mid, Span<int> bot, Span<byte> dst, Matrix3x3 matrix)
        {
            for (var col = 1; col < mid.Length - 1; col++)
            {
                float R = (float)mid[col];
                float G = ((float)top[col] + (float)mid[col - 1] + (float)mid[col + 1] + (float)bot[col]) / 4f;
                float B = ((float)top[col - 1] + (float)top[col + 1] + (float)bot[col - 1] + (float)bot[col + 1]) / 4f;

                var _B = matrix.M11 * B + matrix.M12 * G + matrix.M13 * R;
                var _G = matrix.M21 * B + matrix.M22 * G + matrix.M23 * R;
                var _R = matrix.M31 * B + matrix.M32 * G + matrix.M33 * R;

                dst[col * 3 + 0] = _B > Byte.MaxValue ? Byte.MaxValue : _B < Byte.MinValue ? Byte.MinValue : (byte)_B;
                dst[col * 3 + 1] = _G > Byte.MaxValue ? Byte.MaxValue : _G < Byte.MinValue ? Byte.MinValue : (byte)_G;
                dst[col * 3 + 2] = _R > Byte.MaxValue ? Byte.MaxValue : _R < Byte.MinValue ? Byte.MinValue : (byte)_R;
            }
        }


        public void LineTest((int left, int top, int width, int height) area)
        {
            for (var line = area.top; line < area.top + area.height; line++)
            {
                var linehead = (line * w) + area.left - 1;
                var top = src.AsSpan().Slice(linehead - w, area.width + 2);
                var mid = src.AsSpan().Slice(linehead + 0, area.width + 2);
                var bot = src.AsSpan().Slice(linehead + w, area.width + 2);

                var spanDst = dst.AsSpan().Slice(linehead * 3, (area.width + 2) * 3);

                ColTest(top, mid, bot, spanDst, mat);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void ColTest(Span<int> top, Span<int> mid, Span<int> bot, Span<byte> dst, Matrix3x3 matrix)
        {
            fixed (int* pinTop = top)
            fixed (int* pinMid = mid)
            fixed (int* pinBot = bot)
            fixed (byte* pindst = dst)
            {
                var M11 = pinTop + 0;
                var M12 = pinTop + 1;
                var M13 = pinTop + 2;
                var M21 = pinMid + 0;
                var M22 = pinMid + 1;
                var M23 = pinMid + 2;
                var M31 = pinBot + 0;
                var M32 = pinBot + 1;
                var M33 = pinBot + 2;

                var d = (Color3*)pindst;

                Vec v1 = new Vec() { X = 1, Y = 0, Z = 0 };
                Vec v2 = new Vec() { X = 0, Y = 1, Z = 0 };
                Vec v3 = new Vec() { X = 0, Y = 0, Z = 1 };

                for (var col = 1; col < mid.Length - 1; col++)
                {
                    float R = (float)*M22++;
                    float G = ((float)*M12++ + (float)*M21++ + (float)*M23++ + (float)*M32++) / 4f;
                    float B = ((float)*M11++ + (float)*M13++ + (float)*M31++ + (float)*M33++) / 4f;

                    var _B = matrix.M11 * B + matrix.M12 * G + matrix.M13 * R;
                    var _G = matrix.M21 * B + matrix.M22 * G + matrix.M23 * R;
                    var _R = matrix.M31 * B + matrix.M32 * G + matrix.M33 * R;

                    //Vec bayer = new Vec() { X = buf, Y = buf, Z = buf };

                    *d++ = new Color3() { B = Clamp(_B), G= Clamp(_G),R=Clamp(_R) };
                }

            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ColTest(Span<int> src, Span<byte> dst, Matrix3x3 matrix)
        {
            var v1 = new Vector3() { X = matrix.M11, Y = matrix.M12, Z = matrix.M13 };
            var v2 = new Vector3() { X = matrix.M21, Y = matrix.M22, Z = matrix.M23 };
            var v3 = new Vector3() { X = matrix.M31, Y = matrix.M32, Z = matrix.M33 };
            var bayer = new Vector3() { X = matrix.M31, Y = matrix.M32, Z = matrix.M33 };
            for (var i = 1; i < src.Length - 1; i++)
            {
                var buf = (float)src[i];
                bayer.X = buf;
                bayer.Y = buf;
                bayer.Z = buf;

                dst[i * 3 + 0] = Clamp(Vector3.Dot(bayer,v1));
                dst[i * 3 + 1] = Clamp(Vector3.Dot(bayer, v1));
                dst[i * 3 + 2] = Clamp(Vector3.Dot(bayer, v1));
            }
        }






        //RyuJitで早くてx86でも早い両立
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void ToByte(Span<int> src, Span<byte> dst, Matrix3x3 matrix)
        {
            Vec v1 = new Vec() { X = 1, Y = 0, Z = 0 };
            Vec v2 = new Vec() { X = 0, Y = 1, Z = 0 };
            Vec v3 = new Vec() { X = 0, Y = 0, Z = 1 };

            fixed (int* pinSrc = src)
            fixed (byte* pinDst = dst)
            {
                var s = pinSrc;
                var d = pinDst;
                var length = src.Length;
                

                for (var i = 0; i < length; i++)
                {
                    var buf = (float)*s++;
                    Vec bayer = new Vec() { X = buf, Y = buf, Z = buf };

                    *d++ = vectorDot2(in bayer, in v1);
                    *d++ = vectorDot2(in bayer, in v2);
                    *d++ = vectorDot2(in bayer, in v3);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void ToByte1(Span<int> src, Span<byte> dst, Matrix3x3 matrix)
        {
            fixed (int* pinSrc = src)
            fixed (byte* pinDst = dst)
            {
                var s = pinSrc;
                var d = (Color*)pinDst;
                var length = src.Length;

                for (var i = 0; i < length; i++)
                {
                    var buf = (float)*s++;
                    (float B, float G, float R) bayer = (buf, buf, buf);
                    //Matrix(ref bayer, ref matrix);

                    Clamp3(ref *d, in bayer); // 戻り値よりはやい
                    d++;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void ToByte2(int* src, byte* dst, int width)
        {
            Vector4 v = new Vector4(0);
            Vector4 min = new Vector4(0);
            Vector4 max = new Vector4(255);

            Vector4 m1 = new Vector4(1, 0, 0, 0);
            Vector4 m2 = new Vector4(0, 1, 0, 0);
            Vector4 m3 = new Vector4(0, 0, 1, 0);

            for (var i = 0; i < width; i++)
            {
                var buf = (float)*src++;
                v.X = buf;
                v.Y = buf;
                v.Z = buf;
                v = new Vector4(Vector4.Dot(v, m1), Vector4.Dot(v, m2), Vector4.Dot(v, m3), 0);
                Vector4.Clamp(v, min, max);
                
                *dst++ = (byte)v.X;
                *dst++ = (byte)v.Y;
                *dst++ = (byte)v.Z;

            }
        }





        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void ToByte3(int* src, byte* dst, int length)
        {
            Matrix3x3 _matrix = new Matrix3x3() { M11 = 1, M12 = 0, M13 = 0, M21 = 0, M22 = 1, M23 = 0, M31 = 0, M32 = 0, M33 = 1 };

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
            Matrix3x3 _matrix = new Matrix3x3() { M11 = 1, M12 = 0, M13 = 0, M21 = 0, M22 = 1, M23 = 0, M31 = 0, M32 = 0, M33 = 1 };

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
        //public void Default()
        //{
        //    for (int y = 0; y < h; y++)
        //        for (int x = 0; x < w; x++)
        //        {
        //            int index = x + y * w;
        //            byte buf = (byte)src[index];
        //            dst[index * 3 + 0] = buf;
        //            dst[index * 3 + 1] = buf;
        //            dst[index * 3 + 2] = buf;
        //        }
        //}


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


        #region MyRegion

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
        #endregion


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
