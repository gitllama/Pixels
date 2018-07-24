using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Pixels.Standard
{

    #region enum

    public enum FileType : uint
    {
        None = 0,

        Byte    = 0x_0000_0001,

        Int16   = 0x_0000_0002,  //for IGXL 0
        Int24   = 0x_0000_0003,
        Int32   = 0x_0000_0004,  //for IGXL 1
        Int64   = 0x_0000_0006,
        UInt16  = 0x_0000_0102,
        UInt24  = 0x_0000_0103,
        UInt32  = 0x_0000_0104,
        UInt64  = 0x_0000_0106,
        Single  = 0x_0000_0204, //for IGXL 2
        Double  = 0x_0000_0208,

        Int16E  = 0x_0000_1302,
        Int24E  = 0x_0000_1303,
        Int32E  = 0x_0000_1304,
        Int64E  = 0x_0000_1308,
        UInt16E = 0x_0000_1402,
        UInt24E = 0x_0000_1403,
        UInt32E = 0x_0000_1404,
        UInt64E = 0x_0000_1408,
        SingleE = 0x_0000_1504,
        DoubleE = 0x_0000_1508,
    }

    public enum Bayer
    {
        Mono,
        BG,
        GB,
        RG,
        GR
    }

    #endregion


    #region Pixel<T>

    public class Pixel<T> where T : struct
    {
        public T[] pix;

        public readonly int Width;
        public readonly int Height;

        public Dictionary<string, (int left, int top, int width, int height)> SubPlane;


        public T this[int x, int y]
        {
            get { return pix[x + y * Width]; }
            set { pix[x + y * Width] = value; }
        }

        public T this[int index]
        {
            get { return pix[index]; }
            set { pix[index] = value; }
        }

        public int GetIndex(int x, int y) => x + y * Width;

        public (int x, int y) GetPoint(int index) => (index % Width, index / Width);

        public (int left, int top, int width, int height) GetPlane(string name)
        {
            return SubPlane.ContainsKey(name) ? SubPlane[name] : (0, 0, Width, Height);
        }

        public Pixel(int width, int height)
        {
            Width = width;
            Height = height;
            pix = new T[width * height];
            SubPlane = new Dictionary<string, (int left, int top, int width, int height)>();
        }
  
    }

    //public class PixelByte<T> where T : struct
    //{
    //    public readonly int Width;
    //    public readonly int Height;
    //    public byte[] pix;

    //    private Func<byte, T> Converter = null;

    //    public T this[int x, int y]
    //    {
    //        get { return Converter(x + y * Width); }
    //        set { pix[x + y * Width] = value; }
    //    }

    //    public T this[int index]
    //    {
    //        get { return pix[index]; }
    //        set { pix[index] = value; }
    //    }

    //    public Dictionary<string, (int left, int top, int width, int height)> SubPlane;

    //    public Pixel(int width, int height)
    //    {
    //        Width = width;
    //        Height = height;
    //        pix = new T[width * height];
    //        SubPlane = new Dictionary<string, (int left, int top, int width, int height)>();
    //        SubPlane.Add("Full", (0, 0, width, height));
    //    }
    //}

    #endregion


    #region Struct

    //[FieldOffset(0)] // A と一緒

    //public unsafe Byte* Byte0 { get { return &_b0; } }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct Int24
    {
        [FieldOffset(0)] private byte _b0;
        [FieldOffset(1)] private byte _b1;
        [FieldOffset(2)] private byte _b2;

        public static implicit operator int(Int24 val) => (val._b2 << 24 | (val._b1 << 16) | (val._b0 << 8)) >> 8;
        public static implicit operator float(Int24 val) => ((int)val);
        public static implicit operator double(Int24 val) => ((int)val);

        public static Int24 FromByte(byte[] bytes, int startindex = 0)
        {
            return new Int24()
            {
                _b0 = bytes[startindex],
                _b1 = bytes[startindex + 1],
                _b2 = bytes[startindex + 2]
            };
        }

        public const int MaxValue = 0X_007F_FFFF;
        public const int MinValue = ~0X_007F_FFFF;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct UInt24
    {
        [FieldOffset(0)] private byte _b0;
        [FieldOffset(1)] private byte _b1;
        [FieldOffset(2)] private byte _b2;

        public static implicit operator uint(UInt24 val) => (((uint)val._b2 << 24 | ((uint)val._b1 << 16) | ((uint)val._b0 << 8)) >> 8);
        public static explicit operator int(UInt24 val) => (int)((uint)val);
        public static implicit operator float(UInt24 val) => ((uint)val);
        public static implicit operator double(UInt24 val) => ((uint)val);

        public static UInt24 FromByte(byte[] bytes, int startindex = 0)
        {
            return new UInt24()
            {
                _b0 = bytes[startindex],
                _b1 = bytes[startindex + 1],
                _b2 = bytes[startindex + 2]
            };
        }

        public const uint MaxValue = 0X_00FF_FFFF;
        public const uint MinValue = 0X_0000_0000;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct Int16E
    {
        [FieldOffset(0)] private byte _b0;
        [FieldOffset(1)] private byte _b1;

        public static implicit operator Int16(Int16E val) => (Int16)((val._b0 << 8) | val._b1);
        public static implicit operator int(Int16E val) => ((Int16)val);
        public static implicit operator float(Int16E val) => ((Int16)val);
        public static implicit operator double(Int16E val) => ((Int16)val);

        public static Int16E FromByte(byte[] bytes, int startindex = 0)
        {
            return new Int16E()
            {
                _b0 = bytes[startindex],
                _b1 = bytes[startindex + 1]
            };
        }

        public const Int16 MaxValue = Int16.MaxValue;
        public const Int16 MinValue = Int16.MinValue;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct Int24E
    {
        [FieldOffset(0)] private byte _b0;
        [FieldOffset(1)] private byte _b1;
        [FieldOffset(2)] private byte _b2;

        public static implicit operator int(Int24E val) => ((val._b0 << 24) | (val._b1 << 16) | (val._b2 << 8)) >> 8;
        public static implicit operator float(Int24E val) => ((Int24E)val);
        public static implicit operator double(Int24E val) => ((Int24E)val);

        public static Int24E FromByte(byte[] bytes, int startindex = 0)
        {
            return new Int24E()
            {
                _b0 = bytes[startindex],
                _b1 = bytes[startindex + 1],
                _b2 = bytes[startindex + 2]
            };
        }

        public const int MaxValue = Int24.MaxValue;
        public const int MinValue = Int24.MinValue;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct Int32E
    {
        [FieldOffset(0)] private byte _b0;
        [FieldOffset(1)] private byte _b1;
        [FieldOffset(2)] private byte _b2;
        [FieldOffset(3)] private byte _b3;

        public static implicit operator int(Int32E val) => ((val._b0 << 24) | (val._b1 << 16) | (val._b2 << 8) | val._b3);
        public static implicit operator float(Int32E val) => ((Int32E)val);
        public static implicit operator double(Int32E val) => ((Int32E)val);

        public static Int32E FromByte(byte[] bytes, int startindex = 0)
        {
            return new Int32E()
            {
                _b0 = bytes[startindex],
                _b1 = bytes[startindex + 1],
                _b2 = bytes[startindex + 2],
                _b3 = bytes[startindex + 3]
            };
        }

        public const int MaxValue = Int32.MaxValue;
        public const int MinValue = Int32.MinValue;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct Int64E
    {
        [FieldOffset(0)] private byte _b0;
        [FieldOffset(1)] private byte _b1;
        [FieldOffset(2)] private byte _b2;
        [FieldOffset(3)] private byte _b3;
        [FieldOffset(4)] private byte _b4;
        [FieldOffset(5)] private byte _b5;
        [FieldOffset(6)] private byte _b6;
        [FieldOffset(7)] private byte _b7;

        public static implicit operator Int64(Int64E val) => (
            ((Int64)val._b0 << 56) | ((Int64)val._b1 << 48) | ((Int64)val._b2 << 40) | ((Int64)val._b3 << 32) |
            ((Int64)val._b4 << 24) | ((Int64)val._b5 << 16) | ((Int64)val._b6 << 8) | (Int64)val._b7
        );
        public static explicit operator int(Int64E val) => (int)((Int64E)val);
        public static implicit operator float(Int64E val) => ((Int64E)val);
        public static implicit operator double(Int64E val) => ((Int64E)val);

        public static Int64E FromByte(byte[] bytes, int startindex = 0)
        {
            return new Int64E()
            {
                _b0 = bytes[startindex],
                _b1 = bytes[startindex + 1],
                _b2 = bytes[startindex + 2],
                _b3 = bytes[startindex + 3],
                _b4 = bytes[startindex + 4],
                _b5 = bytes[startindex + 5],
                _b6 = bytes[startindex + 6],
                _b7 = bytes[startindex + 7],
            };
        }

        public const Int64 MaxValue = Int64.MaxValue;
        public const Int64 MinValue = Int64.MinValue;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct UInt16E
    {
        [FieldOffset(0)] private byte _b0;
        [FieldOffset(1)] private byte _b1;

        public static implicit operator UInt16(UInt16E val) => (UInt16)((val._b0 << 8) | val._b1);
        public static implicit operator int(UInt16E val) => ((UInt16E)val);
        public static implicit operator float(UInt16E val) => ((UInt16E)val);
        public static implicit operator double(UInt16E val) => ((UInt16E)val);

        public static UInt16E FromByte(byte[] bytes, int startindex = 0)
        {
            return new UInt16E()
            {
                _b0 = bytes[startindex],
                _b1 = bytes[startindex + 1]
            };
        }

        public const UInt16 MaxValue = UInt16.MaxValue;
        public const UInt16 MinValue = UInt16.MinValue;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct UInt24E
    {
        [FieldOffset(0)] private byte _b0;
        [FieldOffset(1)] private byte _b1;
        [FieldOffset(2)] private byte _b2;

        public static implicit operator uint(UInt24E val) => (((uint)val._b0 << 24) | ((uint)val._b1 << 16) | ((uint)val._b2 << 8)) >> 8;
        public static explicit operator int(UInt24E val) => (int)((uint)val);
        public static implicit operator float(UInt24E val) => ((UInt24E)val);
        public static implicit operator double(UInt24E val) => ((UInt24E)val);

        public static UInt24E FromByte(byte[] bytes, int startindex = 0)
        {
            return new UInt24E()
            {
                _b0 = bytes[startindex],
                _b1 = bytes[startindex + 1],
                _b2 = bytes[startindex + 2]
            };
        }

        public const int MaxValue = Int24.MaxValue;
        public const int MinValue = Int24.MinValue;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct UInt32E
    {
        [FieldOffset(0)] private byte _b0;
        [FieldOffset(1)] private byte _b1;
        [FieldOffset(2)] private byte _b2;
        [FieldOffset(3)] private byte _b3;

        public static implicit operator UInt32(UInt32E val) => (UInt32)((val._b0 << 24) | (val._b1 << 16) | (val._b2 << 8) | val._b3);
        public static explicit operator int(UInt32E val) => (int)((UInt32E)val);
        public static implicit operator float(UInt32E val) => ((UInt32E)val);
        public static implicit operator double(UInt32E val) => ((UInt32E)val);

        public static UInt32E FromByte(byte[] bytes, int startindex = 0)
        {
            return new UInt32E()
            {
                _b0 = bytes[startindex],
                _b1 = bytes[startindex + 1],
                _b2 = bytes[startindex + 2],
                _b3 = bytes[startindex + 3]
            };
        }

        public const int MaxValue = Int32.MaxValue;
        public const int MinValue = Int32.MinValue;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct UInt64E
    {
        [FieldOffset(0)] private byte _b0;
        [FieldOffset(1)] private byte _b1;
        [FieldOffset(2)] private byte _b2;
        [FieldOffset(3)] private byte _b3;
        [FieldOffset(4)] private byte _b4;
        [FieldOffset(5)] private byte _b5;
        [FieldOffset(6)] private byte _b6;
        [FieldOffset(7)] private byte _b7;

        public static implicit operator UInt64(UInt64E val) => (
            ((UInt64)val._b0 << 56) | ((UInt64)val._b1 << 48) | ((UInt64)val._b2 << 40) | ((UInt64)val._b3 << 32) |
            ((UInt64)val._b4 << 24) | ((UInt64)val._b5 << 16) | ((UInt64)val._b6 << 8) | (UInt64)val._b7
        );
        public static explicit operator int(UInt64E val) => (int)((UInt64)val);
        public static implicit operator float(UInt64E val) => ((UInt64E)val);
        public static implicit operator double(UInt64E val) => ((UInt64E)val);

        public static UInt64E FromByte(byte[] bytes, int startindex = 0)
        {
            return new UInt64E()
            {
                _b0 = bytes[startindex],
                _b1 = bytes[startindex + 1],
                _b2 = bytes[startindex + 2],
                _b3 = bytes[startindex + 3],
                _b4 = bytes[startindex + 4],
                _b5 = bytes[startindex + 5],
                _b6 = bytes[startindex + 6],
                _b7 = bytes[startindex + 7],
            };
        }

        public const UInt64 MaxValue = UInt64.MaxValue;
        public const UInt64 MinValue = UInt64.MinValue;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct SingleE
    {
        [FieldOffset(0)] private byte _b0;
        [FieldOffset(1)] private byte _b1;
        [FieldOffset(2)] private byte _b2;
        [FieldOffset(3)] private byte _b3;

        public static implicit operator Single(SingleE val) => BitConverter.ToSingle(new byte[] { val._b3, val._b2, val._b1, val._b0 }, 0);
        public static explicit operator int(SingleE val) => (int)((SingleE)val);
        public static implicit operator double(SingleE val) => ((SingleE)val);

        public static SingleE FromByte(byte[] bytes, int startindex = 0)
        {
            return new SingleE()
            {
                _b0 = bytes[startindex],
                _b1 = bytes[startindex + 1],
                _b2 = bytes[startindex + 2],
                _b3 = bytes[startindex + 3]
            };
        }

        public const float MaxValue = Single.MaxValue;
        public const float MinValue = Single.MinValue;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct DoubleE
    {
        [FieldOffset(0)] private byte _b0;
        [FieldOffset(1)] private byte _b1;
        [FieldOffset(2)] private byte _b2;
        [FieldOffset(3)] private byte _b3;
        [FieldOffset(4)] private byte _b4;
        [FieldOffset(5)] private byte _b5;
        [FieldOffset(6)] private byte _b6;
        [FieldOffset(7)] private byte _b7;

        public static implicit operator Double(DoubleE val) => BitConverter.ToDouble(new byte[] {
            val._b7, val._b6, val._b5, val._b4, val._b3, val._b2, val._b1, val._b0}, 0);
        public static explicit operator int(DoubleE val) => (int)((DoubleE)val);
        public static explicit operator float(DoubleE val) => (float)((DoubleE)val);

        public static DoubleE FromByte(byte[] bytes, int startindex = 0)
        {
            return new DoubleE()
            {
                _b0 = bytes[startindex],
                _b1 = bytes[startindex + 1],
                _b2 = bytes[startindex + 2],
                _b3 = bytes[startindex + 3],
                _b4 = bytes[startindex + 4],
                _b5 = bytes[startindex + 5],
                _b6 = bytes[startindex + 6],
                _b7 = bytes[startindex + 7],
            };
        }

        public const double MaxValue = Double.MaxValue;
        public const double MinValue = Double.MinValue;
    }

    #endregion


    #region PixelBitConverter

    // unsafe
    // {
    //     fixed (byte* pin = value.AsSpan().Slice(startindex, 3))
    //     {
    //        return *(Int24*)pin;
    //     }
    // }
    // だと1/4のパフォーマンス

    public static class PixelBitConverter
    {

        public static Int16 ToInt16(byte[] value, int startindex) => BitConverter.ToInt16(value, startindex);
        public static Int24 ToInt24(byte[] value, int startindex) => Int24.FromByte(value, startindex);
        public static Int32 ToInt32(byte[] value, int startindex) => BitConverter.ToInt32(value, startindex);
        public static Int64 ToInt64(byte[] value, int startindex) => BitConverter.ToInt64(value, startindex);

        public static UInt16 ToUInt16(byte[] value, int startindex) => BitConverter.ToUInt16(value, startindex);
        public static UInt24 ToUInt24(byte[] value, int startindex) => UInt24.FromByte(value, startindex);
        public static UInt32 ToUInt32(byte[] value, int startindex) => BitConverter.ToUInt32(value, startindex);
        public static UInt64 ToUInt64(byte[] value, int startindex) => BitConverter.ToUInt64(value, startindex);

        public static Single ToSingle(byte[] value, int startindex) => BitConverter.ToSingle(value, startindex);
        public static Double ToDouble(byte[] value, int startindex) => BitConverter.ToDouble(value, startindex);

        public static Int16E ToInt1E6(byte[] value, int startindex) => Int16E.FromByte(value, startindex);
        public static Int24E ToInt24E(byte[] value, int startindex) => Int24E.FromByte(value, startindex);
        public static Int32E ToInt32E(byte[] value, int startindex) => Int32E.FromByte(value, startindex);
        public static Int64E ToInt64E(byte[] value, int startindex) => Int64E.FromByte(value, startindex);

        public static UInt16E ToUInt16E(byte[] value, int startindex) => UInt16E.FromByte(value, startindex);
        public static UInt24E ToUInt24E(byte[] value, int startindex) => UInt24E.FromByte(value, startindex);
        public static UInt32E ToUInt32E(byte[] value, int startindex) => UInt32E.FromByte(value, startindex);
        public static UInt64E ToUInt64E(byte[] value, int startindex) => UInt64E.FromByte(value, startindex);

        public static SingleE ToSingleE(byte[] value, int startindex) => SingleE.FromByte(value, startindex);
        public static DoubleE ToDoubleE(byte[] value, int startindex) => DoubleE.FromByte(value, startindex);

    }

    #endregion


    public static class PixelExtented
    {
        public static PixelDeveloping Build(this Pixel<int> src)
        {
            return new PixelDeveloping(src.Width, src.Height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="func"></param>
        /// <param name="parallel">funcの外部変数は排他制御必要</param>
        public unsafe static void ForEach(this Pixel<int> src, Func<int, int> func, string subPlane = "Full", bool parallel = false)
        {
            (int left, int top, int width, int height) plane = src.GetPlane(subPlane);
            if (parallel)
            {
                //Lockしないので外部の変数参照はよくない
                //60->5何が最適化してる?
                //Parallel.For(0, src.pix.Length, i =>
                //{
                //    src.pix[i] = func(src.pix[i]);
                //});

                //152->0ms
                Parallel.For(plane.top, plane.top + plane.height, y =>
                {
                    int yy = y * src.Width;
                    var span = src.pix.AsSpan().Slice(yy + plane.left, plane.width);
                    fixed (int* pin = span)
                    {
                        var p = pin;
                        while (p < pin + span.Length)
                        {
                            *p = func(*p);
                            p++;
                        }
                    }
                });

            }
            else
            {
                //25-5
                for (int y = plane.top; y < plane.top + plane.height; y++)
                {
                    int yy = y * src.Width;
                    var span = src.pix.AsSpan().Slice(yy + plane.left, plane.width);
                    fixed (int* pin = span)
                    {
                        var p = pin;
                        while (p < pin + span.Length)
                        {
                            *p = func(*p);
                            p++;
                        }
                    }
                }
            }
        }

        public unsafe static void Map(this Pixel<int> src, Pixel<int> dst,Action<int, int, Pixel<int>, Pixel<int>> action, string subPlane = "Full", bool parallel = false)
        {
            (int left, int top, int width, int height) plane = src.SubPlane[subPlane];
            if (parallel)
            {

            }
            else
            {
                //25-5
                for (int y = plane.top; y < plane.top + plane.height; y++)
                {
                    for (int x = plane.left; x < plane.left + plane.width; x++)
                    {
                        action(x, y, src, dst);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="upper"></param>
        /// <param name="lower"></param>
        /// <param name="parallel">並列の方が10倍遅い</param>
        /// <returns></returns>
        public unsafe static (int count, List<(int x, int y, int value)>) Count(this Pixel<int> src, string subPlane = "Full", int upper = int.MaxValue, int lower = int.MinValue, bool parallel = false)
        {
            int counter = 0;
            (int left, int top, int width, int height) plane = src.SubPlane[subPlane];
            var list = new List<(int x, int y, int value)>();
            if (parallel)
            {
                Parallel.For(plane.top, plane.top + plane.height, y =>
                {
                    int yy = y * src.Width;
                    var span = src.pix.AsSpan().Slice(yy + plane.left, plane.width);
                    fixed (int* pin = span)
                    {
                        var p = pin;
                        while (p < pin + span.Length)
                        {
                            if (*p > upper || *p < lower)
                            {
                                Interlocked.Increment(ref counter);
                            }
                            p++;
                        }
                    }
                });
            }
            else
            {
                for (int y = plane.top; y < plane.top + plane.height; y++)
                {
                    int yy = y * src.Width;
                    var span = src.pix.AsSpan().Slice(yy + plane.left, plane.width);
                    fixed (int* pin = span)
                    {
                        var p = pin;
                        while (p < pin + span.Length)
                        {
                            if (*p > upper || *p < lower)
                            {
                                counter++;
                                if (counter < 100)
                                    list.Add(((int)(p - pin) + plane.left, y, *p));
                            }
                            p++;
                        }
                    }
                }
            }
            return (counter, list);
        }

        #region Filter

        public unsafe static void Median(Pixel<int> src, Pixel<int> dst, (int w, int h) window, bool parallel = false)
        {
            int[] coord = new int[window.w * window.h];
            int center = (coord.Length - 1) / 2;
            int w = (window.w - 1) / 2;
            int h = (window.h - 1) / 2;

            int c = 0;
            for (int yy = -h; yy <= h; yy++)
            {
                for (int xx = -w; xx <= w; xx++)
                {
                    coord[c++] = xx + yy * src.Width;
                }
            }

            if (parallel)
            {
                Parallel.For(h, dst.Height - h, y =>
                {
                    int[] box = new int[window.w * window.h];
                    for (int x = w; x < dst.Width - w; x++)
                    {
                        int index = x + y * dst.Width;
                        c = 0;
                        foreach (var i in coord)
                            box[c++] = src.pix[index + i];
                        Array.Sort(box); //90%ここ
                        dst.pix[index] = box[center];

                    }
                });
            }
            else
            {
                int[] box = new int[window.w * window.h];
                for (int y = h; y < dst.Height - h; y++)
                {
                    for (int x = w; x < dst.Width - w; x++)
                    {
                        int index = x + y * dst.Width;
                        c = 0;
                        foreach (var i in coord)
                            box[c++] = src.pix[index + i];
                        Array.Sort(box);
                        dst.pix[index] = box[center];
                    }
                }
            }

        }

        public unsafe static void Median2(in Pixel<int> src, ref Pixel<int> dst, (int w, int h) window)
        {
            int[] box = new int[window.w * window.h];
            int[] coord = new int[window.w * window.h];
            //Span<int> box = stackalloc int[window.w * window.h];
            //Span<int> coord = stackalloc int[window.w * window.h];

            int center = (box.Length - 1) / 2;
            int w = (window.w - 1) / 2;
            int h = (window.h - 1) / 2;
            int c = 0;

            for (int yy = -h; yy <= h; yy++)
            {
                for (int xx = -w; xx <= w; xx++)
                {
                    coord[c++] = xx + yy * src.Width;
                }
            }

            fixed (int* pinSrc = src.pix)
            fixed (int* pinDst = dst.pix)
            fixed (int* pinbox = box)
            {
                for (int y = h; y < dst.Height - h; y++)
                {
                    for (int x = w; x < dst.Width - w; x++)
                    {
                        int index = x + y * dst.Width;
                        c = 0;
                        foreach (var i in coord)
                            box[c++] = src.pix[index + i];
                        Array.Sort(box); //90%ここ
                        dst.pix[index] = box[center];
                    }
                }
            }
        }


        #endregion

        #region Op

        public unsafe static void Diff(Pixel<int> src1, Pixel<int> src2, Pixel<int> dst)
        {
            //231
            //unsafe
            //{
            //    fixed (int* pin1 = src1.pix)
            //    fixed (int* pin2 = src2.pix)
            //    fixed (int* pin3 = dst.pix)
            //    {
            //        var p1 = pin1;
            //        var p2 = pin2;
            //        var p3 = pin3;
            //        var length = pin3 + dst.pix.Length;
            //        while (p3 < length)
            //        {
            //            *p3++ = *p1++ - *p2++;
            //        }
            //    }
            //}

            //459
            //for (var i = 0; i < dst.pix.Length; i++)
            //    dst.pix[i] = src1.pix[i] - src2.pix[i];

            ////170
            //Parallel.For(0, dst.Height, y =>
            //{
            //    for (int x = 0; x < dst.Width; x++)
            //    {
            //        int index = x + y * dst.Width; ;
            //        dst.pix[index] = src1.pix[index] - src2.pix[index];
            //    }
            //});

            //157
            //Parallel.For(0, dst.Height, y =>
            //{
            //    for (int x = y * dst.Width; x < y * dst.Width + dst.Width; x++)
            //    {
            //        dst.pix[x] = src1.pix[x] - src2.pix[x];
            //    }
            //});

        }

        #endregion

        public static void RadixSort(int[] a)
        {
            List<int>[] bucket = new List<int>[256];
            for (int d = 0, logR = 0; d < 4; ++d, logR += 8)
            {
                for (int i = 0; i < a.Length; ++i)
                {
                    int key = (a[i] >> logR) & 255; // a[i] を256進 d 桁目だけを取り出す。
                    if (bucket[key] == null) bucket[key] = new List<int>();
                    bucket[key].Add(a[i]);
                }

                for (int j = 0, i = 0; j < bucket.Length; ++j)
                    if (bucket[j] != null)
                        foreach (int val in bucket[j])
                            a[i++] = val;

                for (int j = 0; j < bucket.Length; ++j)
                    bucket[j] = null;
            }
        }

        public static int SortTest(int[] a)
        {
            int[] bucket = new int[1024 * 2 + 1];
            for (int i = 0; i < a.Length; ++i)
            {
                var hoge = a[i] > 1024 ? 1024 + 1024 : a[i] < -1024 ? 0 : a[i] + 1024;
                bucket[hoge]++;
            }

            int c = 0;
            for (int i = 0; i < bucket.Length; i++)
            {
                c += bucket[i];
                if (c >= a.Length / 2) return i - 1024;
            }
            return -1;
        }

    }

    public class PixelIntPtr<T> where T : struct
    {
        public IntPtr pix;
     
        public readonly int Width;
        public readonly int Height;

        public Dictionary<string, (int left, int top, int width, int height)> SubPlane;


        //public T this[int x, int y]
        //{
        //    get { return pix[x + y * Width]; }
        //    set { pix[x + y * Width] = value; }
        //}

        //public T this[int index]
        //{
        //    get {
        //        IntPtr.Zero;

        //        return pix[index]; }
        //    set { pix[index] = value; }
        //}

        public int GetIndex(int x, int y) => x + y * Width;

        public (int x, int y) GetPoint(int index) => (index % Width, index / Width);

        public (int left, int top, int width, int height) GetPlane(string name)
        {
            return SubPlane.ContainsKey(name) ? SubPlane[name] : (0, 0, Width, Height);
        }

        public PixelIntPtr(int width, int height)
        {
            Width = width;
            Height = height;
            pix = Marshal.AllocHGlobal(width * height * Marshal.SizeOf(typeof(T)));
            SubPlane = new Dictionary<string, (int left, int top, int width, int height)>();
        }

        ~PixelIntPtr()
        {
            Marshal.FreeHGlobal(pix);
        }

//    var a = new byte[w];
//for (int y = 0; y<h; y++)
//{
// for (int x = 0; x<a.Length; x++)
// {
//  var val = x ^ y;
//    var v = (byte)val;
//    a[x] = v;
// }
//Marshal.Copy(a, 0, dst, a.Length);
// IntPtr.Add(dst, sizeof(byte) * stride);
//}

    }

}

/*
ReaderWriterLockSlim locker = new ReaderWriterLockSlim();
{
    try
    {
        locker.EnterWriteLock(); //locker.EnterReadLock();
        //hogehoge
    }
    finally
    {
        locker.ExitWriteLock(); //locker.ExitReadLock();
    }
}
var lockObject = new object();
lock (lockObject)
{
}
*/

//ref struct型は「スタック上に確保することが強制される」データ型
//http://article.higlabo.com/ja/thread_locking.html
//http://xptn.dtiblog.com/blog-entry-94.html
//http://xptn.dtiblog.com/blog-entry-85.html




//class Pixel
//{
//    public int[] pix;
//    public void Add(int val)
//    {
//        var a = (10, 10);

//        var span = pix.AsSpan().Slice(2, 3);
//        for (int i = 0; i < span.Length; i++)
//        {
//            span[i] += val;
//        }
//    }


//    // Core2.1
//    static void Clear(Span<int> span)
//    {
//        unsafe
//        {
//            // 結局内部的には unsafe にしてポインターを使った方が速い場合あり
//            fixed (int* pin = &span.GetPinnableReference())
//            // 注: C# 7.3 からは以下の書き方ができる
//            // fixed (byte* pin = span)
//            {
//                var p = pin;
//                var last = p + span.Length;
//                while (p + 8 < last)
//                {
//                    *(ulong*)p = 0;
//                    p += 8;
//                }
//                if (p + 4 < last)
//                {
//                    *(uint*)p = 0;
//                    p += 4;
//                }
//                while (p < last)
//                {
//                    *p = 0;
//                    ++p;
//                }
//            }
//        }
//    }

//    static void Read()
//    {
//        //https://ufcpp.net/study/csharp/resource/span/
//        const int BufferSize = 128;
//        using (var f = File.OpenRead("test.data"))
//        {
//            var rest = (int)f.Length;
//            // Span<byte> で受け取ることで、new (配列)を stackalloc (スタック確保)に変更できる
//            Span<byte> buffer = stackalloc byte[BufferSize];

//            while (true)
//            {
//                // Read(Span<byte>) が追加された
//                //var read = f.Read(buffer);
//                //rest -= read;
//                if (rest == 0) break;

//                // buffer に対して何か処理する
//            }
//        }
//    }

//    //読み取り専用の参照渡し
//    public static Pixel operator *(in Pixel a, in Pixel b)
//    => new Pixel();

//    static ref readonly int Max(in int x, in int y)
//    {
//        ref readonly var t = ref x;
//        ref readonly var u = ref y;

//        if (t < u) return ref u;
//        else return ref t;
//    }
//}

//public struct PixelInt
//{
//    public int[] pix;
//    readonly int width;
//    readonly int height;

//    public PixelInt(int width, int height)
//    {
//        pix = new int[width * height];
//        this.width = width;
//        this.height = height;
//    }

//    public Span<int> Slice(int start, int length)
//    {
//        var span = pix.AsSpan().Slice(2, 3);
//        //for (int i = 0; i < span.Length; i++)
//        //{
//        //    span[i] += val;
//        //}
//        return span;
//    }

//    public void Slice1(int x_start, int x_length, int y_start, int y_length)
//    {
//        for (int y = y_start; y < y_length + y_start; y++)
//        {
//            var span = pix.AsSpan().Slice(y * width + x_start, x_length);
//            for (int x = 0; x < span.Length; x++)
//            {
//                span[x] += 10;
//            }
//        }
//    }

//    public void Slice2(int x_start, int x_length, int y_start, int y_length)
//    {
//        for (int y = y_start; y < y_length + y_start; y++)
//        {
//            for (int x = y * width + x_start; x < y * width + x_length + x_start; x++)
//            {
//                pix[x] += 10;
//            }
//        }
//    }

//    public unsafe void Slice3(int x_start, int x_length, int y_start, int y_length)
//    {
//        for (int y = y_start; y < y_length + y_start; y++)
//        {
//            var span = pix.AsSpan().Slice(y * width + x_start, x_length);
//            fixed (int* pin = span)
//            {
//                var p = pin;
//                var last = p + span.Length;
//                while (p < last)
//                {
//                    *p += 11;
//                    p++;
//                }
//            }
//            //foreach (ref int x in span)
//            //{
//            //    x += 10;
//            //}
//        }
//    }

//    public unsafe void Slice4(int x_start, int x_length, int y_start, int y_length)
//    {
//        for (int y = y_start; y < y_length + y_start; y++)
//        {
//            var span = pix.AsSpan().Slice(y * width + x_start, x_length);

//            var bytes = MemoryMarshal.AsBytes(span);


//            fixed (int* pin = span)
//            {
//                var p = pin;
//                var last = p + span.Length;
//                while (p < last)
//                {
//                    *p += 11;
//                    p++;
//                }
//            }
//            //foreach (ref int x in span)
//            //{
//            //    x += 10;
//            //}
//        }
//    }
//}