using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;


namespace Pixels.IO
{
    public static partial class FileStream
    {
        #region Load
        
        public unsafe static void LoadByte(this Byte[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Byte));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Byte*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = *p++;
                }
            }
        }

        
        public unsafe static void LoadInt16(this Int16[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int16));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int16*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = *p++;
                }
            }
        }

        
        public unsafe static void LoadInt24(this Int24[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int24));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int24*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = *p++;
                }
            }
        }

        
        public unsafe static void LoadInt32(this Int32[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int32));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int32*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = *p++;
                }
            }
        }

        
        public unsafe static void LoadInt64(this Int64[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int64));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int64*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = *p++;
                }
            }
        }

        
        public unsafe static void LoadUInt16(this UInt16[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt16));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt16*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = *p++;
                }
            }
        }

        
        public unsafe static void LoadUInt24(this UInt24[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt24));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt24*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = *p++;
                }
            }
        }

        
        public unsafe static void LoadUInt32(this UInt32[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt32));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt32*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = *p++;
                }
            }
        }

        
        public unsafe static void LoadUInt64(this UInt64[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt64));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt64*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = *p++;
                }
            }
        }

        
        public unsafe static void LoadSingle(this Single[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Single));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Single*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = *p++;
                }
            }
        }

        
        public unsafe static void LoadDouble(this Double[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Double));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Double*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = *p++;
                }
            }
        }

        
        public unsafe static void LoadInt16E(this Int16E[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int16E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int16E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = *p++;
                }
            }
        }

        
        public unsafe static void LoadInt24E(this Int24E[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int24E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int24E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = *p++;
                }
            }
        }

        
        public unsafe static void LoadInt32E(this Int32E[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int32E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int32E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = *p++;
                }
            }
        }

        
        public unsafe static void LoadInt64E(this Int64E[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int64E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int64E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = *p++;
                }
            }
        }

        
        public unsafe static void LoadUInt16E(this UInt16E[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt16E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt16E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = *p++;
                }
            }
        }

        
        public unsafe static void LoadUInt24E(this UInt24E[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt24E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt24E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = *p++;
                }
            }
        }

        
        public unsafe static void LoadUInt32E(this UInt32E[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt32E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt32E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = *p++;
                }
            }
        }

        
        public unsafe static void LoadUInt64E(this UInt64E[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt64E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt64E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = *p++;
                }
            }
        }

        
        public unsafe static void LoadSingleE(this SingleE[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(SingleE));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (SingleE*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = *p++;
                }
            }
        }

        
        public unsafe static void LoadDoubleE(this DoubleE[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(DoubleE));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (DoubleE*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = *p++;
                }
            }
        }

                #endregion

        #region Load w Action
        
        public unsafe static void LoadByte<T>(this T[] dst, Stream fs, Func<Byte, T> func) where T : struct
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Byte));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Byte*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = func(*p++);
                }
            }
        }

        
        public unsafe static void LoadInt16<T>(this T[] dst, Stream fs, Func<Int16, T> func) where T : struct
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int16));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int16*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = func(*p++);
                }
            }
        }

        
        public unsafe static void LoadInt24<T>(this T[] dst, Stream fs, Func<Int24, T> func) where T : struct
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int24));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int24*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = func(*p++);
                }
            }
        }

        
        public unsafe static void LoadInt32<T>(this T[] dst, Stream fs, Func<Int32, T> func) where T : struct
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int32));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int32*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = func(*p++);
                }
            }
        }

        
        public unsafe static void LoadInt64<T>(this T[] dst, Stream fs, Func<Int64, T> func) where T : struct
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int64));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int64*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = func(*p++);
                }
            }
        }

        
        public unsafe static void LoadUInt16<T>(this T[] dst, Stream fs, Func<UInt16, T> func) where T : struct
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt16));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt16*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = func(*p++);
                }
            }
        }

        
        public unsafe static void LoadUInt24<T>(this T[] dst, Stream fs, Func<UInt24, T> func) where T : struct
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt24));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt24*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = func(*p++);
                }
            }
        }

        
        public unsafe static void LoadUInt32<T>(this T[] dst, Stream fs, Func<UInt32, T> func) where T : struct
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt32));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt32*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = func(*p++);
                }
            }
        }

        
        public unsafe static void LoadUInt64<T>(this T[] dst, Stream fs, Func<UInt64, T> func) where T : struct
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt64));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt64*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = func(*p++);
                }
            }
        }

        
        public unsafe static void LoadSingle<T>(this T[] dst, Stream fs, Func<Single, T> func) where T : struct
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Single));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Single*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = func(*p++);
                }
            }
        }

        
        public unsafe static void LoadDouble<T>(this T[] dst, Stream fs, Func<Double, T> func) where T : struct
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Double));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Double*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = func(*p++);
                }
            }
        }

        
        public unsafe static void LoadInt16E<T>(this T[] dst, Stream fs, Func<Int16E, T> func) where T : struct
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int16E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int16E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = func(*p++);
                }
            }
        }

        
        public unsafe static void LoadInt24E<T>(this T[] dst, Stream fs, Func<Int24E, T> func) where T : struct
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int24E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int24E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = func(*p++);
                }
            }
        }

        
        public unsafe static void LoadInt32E<T>(this T[] dst, Stream fs, Func<Int32E, T> func) where T : struct
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int32E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int32E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = func(*p++);
                }
            }
        }

        
        public unsafe static void LoadInt64E<T>(this T[] dst, Stream fs, Func<Int64E, T> func) where T : struct
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int64E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int64E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = func(*p++);
                }
            }
        }

        
        public unsafe static void LoadUInt16E<T>(this T[] dst, Stream fs, Func<UInt16E, T> func) where T : struct
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt16E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt16E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = func(*p++);
                }
            }
        }

        
        public unsafe static void LoadUInt24E<T>(this T[] dst, Stream fs, Func<UInt24E, T> func) where T : struct
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt24E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt24E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = func(*p++);
                }
            }
        }

        
        public unsafe static void LoadUInt32E<T>(this T[] dst, Stream fs, Func<UInt32E, T> func) where T : struct
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt32E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt32E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = func(*p++);
                }
            }
        }

        
        public unsafe static void LoadUInt64E<T>(this T[] dst, Stream fs, Func<UInt64E, T> func) where T : struct
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt64E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt64E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = func(*p++);
                }
            }
        }

        
        public unsafe static void LoadSingleE<T>(this T[] dst, Stream fs, Func<SingleE, T> func) where T : struct
        {
            int size = dst.Length * Marshal.SizeOf(typeof(SingleE));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (SingleE*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = func(*p++);
                }
            }
        }

        
        public unsafe static void LoadDoubleE<T>(this T[] dst, Stream fs, Func<DoubleE, T> func) where T : struct
        {
            int size = dst.Length * Marshal.SizeOf(typeof(DoubleE));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (DoubleE*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = func(*p++);
                }
            }
        }

                #endregion

        #region Load to Int32
        
        public unsafe static void LoadByte(this int[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Byte));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Byte*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (int)*p++;
                }
            }
        }

        
        public unsafe static void LoadInt16(this int[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int16));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int16*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (int)*p++;
                }
            }
        }

        
        public unsafe static void LoadInt24(this int[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int24));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int24*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (int)*p++;
                }
            }
        }

        
        public unsafe static void LoadInt64(this int[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int64));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int64*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (int)*p++;
                }
            }
        }

        
        public unsafe static void LoadUInt16(this int[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt16));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt16*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (int)*p++;
                }
            }
        }

        
        public unsafe static void LoadUInt24(this int[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt24));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt24*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (int)*p++;
                }
            }
        }

        
        public unsafe static void LoadUInt32(this int[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt32));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt32*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (int)*p++;
                }
            }
        }

        
        public unsafe static void LoadUInt64(this int[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt64));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt64*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (int)*p++;
                }
            }
        }

        
        public unsafe static void LoadSingle(this int[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Single));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Single*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (int)*p++;
                }
            }
        }

        
        public unsafe static void LoadDouble(this int[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Double));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Double*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (int)*p++;
                }
            }
        }

        
        public unsafe static void LoadInt16E(this int[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int16E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int16E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (int)*p++;
                }
            }
        }

        
        public unsafe static void LoadInt24E(this int[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int24E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int24E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (int)*p++;
                }
            }
        }

        
        public unsafe static void LoadInt32E(this int[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int32E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int32E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (int)*p++;
                }
            }
        }

        
        public unsafe static void LoadInt64E(this int[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int64E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int64E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (int)*p++;
                }
            }
        }

        
        public unsafe static void LoadUInt16E(this int[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt16E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt16E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (int)*p++;
                }
            }
        }

        
        public unsafe static void LoadUInt24E(this int[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt24E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt24E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (int)*p++;
                }
            }
        }

        
        public unsafe static void LoadUInt32E(this int[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt32E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt32E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (int)*p++;
                }
            }
        }

        
        public unsafe static void LoadUInt64E(this int[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt64E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt64E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (int)*p++;
                }
            }
        }

        
        public unsafe static void LoadSingleE(this int[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(SingleE));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (SingleE*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (int)*p++;
                }
            }
        }

        
        public unsafe static void LoadDoubleE(this int[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(DoubleE));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (DoubleE*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (int)*p++;
                }
            }
        }

                #endregion

        #region Load to Single
        
        public unsafe static void LoadByte(this float[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Byte));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Byte*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (float)*p++;
                }
            }
        }

        
        public unsafe static void LoadInt16(this float[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int16));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int16*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (float)*p++;
                }
            }
        }

        
        public unsafe static void LoadInt24(this float[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int24));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int24*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (float)*p++;
                }
            }
        }

        
        public unsafe static void LoadInt32(this float[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int32));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int32*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (float)*p++;
                }
            }
        }

        
        public unsafe static void LoadInt64(this float[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int64));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int64*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (float)*p++;
                }
            }
        }

        
        public unsafe static void LoadUInt16(this float[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt16));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt16*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (float)*p++;
                }
            }
        }

        
        public unsafe static void LoadUInt24(this float[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt24));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt24*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (float)*p++;
                }
            }
        }

        
        public unsafe static void LoadUInt32(this float[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt32));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt32*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (float)*p++;
                }
            }
        }

        
        public unsafe static void LoadUInt64(this float[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt64));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt64*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (float)*p++;
                }
            }
        }

        
        public unsafe static void LoadDouble(this float[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Double));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Double*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (float)*p++;
                }
            }
        }

        
        public unsafe static void LoadInt16E(this float[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int16E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int16E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (float)*p++;
                }
            }
        }

        
        public unsafe static void LoadInt24E(this float[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int24E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int24E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (float)*p++;
                }
            }
        }

        
        public unsafe static void LoadInt32E(this float[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int32E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int32E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (float)*p++;
                }
            }
        }

        
        public unsafe static void LoadInt64E(this float[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int64E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int64E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (float)*p++;
                }
            }
        }

        
        public unsafe static void LoadUInt16E(this float[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt16E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt16E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (float)*p++;
                }
            }
        }

        
        public unsafe static void LoadUInt24E(this float[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt24E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt24E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (float)*p++;
                }
            }
        }

        
        public unsafe static void LoadUInt32E(this float[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt32E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt32E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (float)*p++;
                }
            }
        }

        
        public unsafe static void LoadUInt64E(this float[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt64E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt64E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (float)*p++;
                }
            }
        }

        
        public unsafe static void LoadSingleE(this float[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(SingleE));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (SingleE*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (float)*p++;
                }
            }
        }

        
        public unsafe static void LoadDoubleE(this float[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(DoubleE));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (DoubleE*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (float)*p++;
                }
            }
        }

                #endregion

        #region Load to Double
        
        public unsafe static void LoadByte(this double[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Byte));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Byte*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (double)*p++;
                }
            }
        }

        
        public unsafe static void LoadInt16(this double[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int16));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int16*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (double)*p++;
                }
            }
        }

        
        public unsafe static void LoadInt24(this double[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int24));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int24*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (double)*p++;
                }
            }
        }

        
        public unsafe static void LoadInt32(this double[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int32));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int32*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (double)*p++;
                }
            }
        }

        
        public unsafe static void LoadInt64(this double[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int64));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int64*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (double)*p++;
                }
            }
        }

        
        public unsafe static void LoadUInt16(this double[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt16));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt16*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (double)*p++;
                }
            }
        }

        
        public unsafe static void LoadUInt24(this double[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt24));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt24*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (double)*p++;
                }
            }
        }

        
        public unsafe static void LoadUInt32(this double[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt32));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt32*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (double)*p++;
                }
            }
        }

        
        public unsafe static void LoadUInt64(this double[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt64));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt64*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (double)*p++;
                }
            }
        }

        
        public unsafe static void LoadSingle(this double[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Single));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Single*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (double)*p++;
                }
            }
        }

        
        public unsafe static void LoadInt16E(this double[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int16E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int16E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (double)*p++;
                }
            }
        }

        
        public unsafe static void LoadInt24E(this double[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int24E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int24E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (double)*p++;
                }
            }
        }

        
        public unsafe static void LoadInt32E(this double[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int32E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int32E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (double)*p++;
                }
            }
        }

        
        public unsafe static void LoadInt64E(this double[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int64E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (Int64E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (double)*p++;
                }
            }
        }

        
        public unsafe static void LoadUInt16E(this double[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt16E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt16E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (double)*p++;
                }
            }
        }

        
        public unsafe static void LoadUInt24E(this double[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt24E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt24E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (double)*p++;
                }
            }
        }

        
        public unsafe static void LoadUInt32E(this double[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt32E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt32E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (double)*p++;
                }
            }
        }

        
        public unsafe static void LoadUInt64E(this double[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt64E));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (UInt64E*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (double)*p++;
                }
            }
        }

        
        public unsafe static void LoadSingleE(this double[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(SingleE));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (SingleE*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (double)*p++;
                }
            }
        }

        
        public unsafe static void LoadDoubleE(this double[] dst, Stream fs)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(DoubleE));
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            fixed (byte* pin = buffer)
            {
                var p = (DoubleE*)pin;
                for (var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (double)*p++;
                }
            }
        }

                #endregion

    }
}
