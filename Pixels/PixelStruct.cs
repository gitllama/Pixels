using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Pixels
{
    //[FieldOffset(0)] // A と一緒
    //public unsafe Byte* Byte0 { get { return &_b0; } }
    //ref struct型は「スタック上に確保することが強制される」データ型

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct Int24
    {
        [FieldOffset(0)] private byte _b0;
        [FieldOffset(1)] private byte _b1;
        [FieldOffset(2)] private byte _b2;

        public static implicit operator int(Int24 val) => (val._b2 << 24 | (val._b1 << 16) | (val._b0 << 8)) >> 8;
        public static implicit operator float(Int24 val) => ((int)val);
        public static implicit operator double(Int24 val) => ((int)val);

        public static explicit operator Int24(int val) => new Int24() { _b0 = (byte)(val), _b1 = (byte)(val >> 8), _b2 = (byte)(val >> 16) };    
       
        public Int24(byte b0, byte b1, byte b2)
        {
            _b0 = b0;
            _b1 = b1;
            _b2 = b2;
        }
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

        public static explicit operator UInt24(int val) => new UInt24() { _b2 = (byte)(val), _b1 = (byte)(val >> 8), _b0 = (byte)(val >> 16) };

        public UInt24(byte b0, byte b1, byte b2)
        {
            _b0 = b0;
            _b1 = b1;
            _b2 = b2;
        }
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

        public Int16E(byte b0, byte b1)
        {
            _b0 = b0;
            _b1 = b1;
        }
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

        public Int24E(byte b0, byte b1, byte b2)
        {
            _b0 = b0;
            _b1 = b1;
            _b2 = b2;
        }
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

        public Int32E(byte b0, byte b1, byte b2, byte b3)
        {
            _b0 = b0;
            _b1 = b1;
            _b2 = b2;
            _b3 = b3;
        }
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

        public Int64E(byte b0, byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7)
        {
            _b0 = b0;
            _b1 = b1;
            _b2 = b2;
            _b3 = b3;
            _b4 = b4;
            _b5 = b5;
            _b6 = b6;
            _b7 = b7;
        }
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

        public UInt16E(byte b0, byte b1)
        {
            _b0 = b0;
            _b1 = b1;
        }
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

        public UInt24E(byte b0, byte b1, byte b2)
        {
            _b0 = b0;
            _b1 = b1;
            _b2 = b2;
        }
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

        public UInt32E(byte b0, byte b1, byte b2, byte b3)
        {
            _b0 = b0;
            _b1 = b1;
            _b2 = b2;
            _b3 = b3;
        }
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

        public UInt64E(byte b0, byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7)
        {
            _b0 = b0;
            _b1 = b1;
            _b2 = b2;
            _b3 = b3;
            _b4 = b4;
            _b5 = b5;
            _b6 = b6;
            _b7 = b7;
        }
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

        public SingleE(byte b0, byte b1, byte b2, byte b3)
        {
            _b0 = b0;
            _b1 = b1;
            _b2 = b2;
            _b3 = b3;
        }
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

        public DoubleE(byte b0, byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7)
        {
            _b0 = b0;
            _b1 = b1;
            _b2 = b2;
            _b3 = b3;
            _b4 = b4;
            _b5 = b5;
            _b6 = b6;
            _b7 = b7;
        }
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
        public static Byte ToByte(byte[] value, int startindex) => value[0];

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

}