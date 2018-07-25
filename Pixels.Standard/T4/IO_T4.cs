/*
*/
/* Code generated using the t4 templates */
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Pixels.Standard.IO
{
    //強制 int double float 実装
    //, FileType type = FileType.Noneを使用した読み込み

    /**/


    public static partial class FileStream
    {
        /**/

        public static partial class Load
        {

            /**/


            // 
            public static unsafe void FromInt16(Stream stream, Int16[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int16));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int16*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = *p++;
                        }
                    }
                }
            }
            
            public static unsafe void FromInt24(Stream stream, Int24[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int24));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int24*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = *p++;
                        }
                    }
                }
            }
            
            public static unsafe void FromInt32(Stream stream, Int32[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int32));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int32*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = *p++;
                        }
                    }
                }
            }
            
            public static unsafe void FromInt64(Stream stream, Int64[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int64));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int64*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = *p++;
                        }
                    }
                }
            }
            
            public static unsafe void FromUInt16(Stream stream, UInt16[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt16));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt16*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = *p++;
                        }
                    }
                }
            }
            
            public static unsafe void FromUInt24(Stream stream, UInt24[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt24));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt24*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = *p++;
                        }
                    }
                }
            }
            
            public static unsafe void FromUInt32(Stream stream, UInt32[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt32));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt32*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = *p++;
                        }
                    }
                }
            }
            
            public static unsafe void FromUInt64(Stream stream, UInt64[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt64));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt64*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = *p++;
                        }
                    }
                }
            }
            
            public static unsafe void FromSingle(Stream stream, Single[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Single));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Single*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = *p++;
                        }
                    }
                }
            }
            
            public static unsafe void FromDouble(Stream stream, Double[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Double));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Double*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = *p++;
                        }
                    }
                }
            }
            
            public static unsafe void FromInt16E(Stream stream, Int16E[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int16E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int16E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = *p++;
                        }
                    }
                }
            }
            
            public static unsafe void FromInt24E(Stream stream, Int24E[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int24E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int24E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = *p++;
                        }
                    }
                }
            }
            
            public static unsafe void FromInt32E(Stream stream, Int32E[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int32E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int32E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = *p++;
                        }
                    }
                }
            }
            
            public static unsafe void FromInt64E(Stream stream, Int64E[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int64E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int64E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = *p++;
                        }
                    }
                }
            }
            
            public static unsafe void FromUInt16E(Stream stream, UInt16E[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt16E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt16E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = *p++;
                        }
                    }
                }
            }
            
            public static unsafe void FromUInt24E(Stream stream, UInt24E[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt24E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt24E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = *p++;
                        }
                    }
                }
            }
            
            public static unsafe void FromUInt32E(Stream stream, UInt32E[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt32E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt32E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = *p++;
                        }
                    }
                }
            }
            
            public static unsafe void FromUInt64E(Stream stream, UInt64E[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt64E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt64E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = *p++;
                        }
                    }
                }
            }
            
            public static unsafe void FromSingleE(Stream stream, SingleE[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(SingleE));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (SingleE*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = *p++;
                        }
                    }
                }
            }
            
            public static unsafe void FromDoubleE(Stream stream, DoubleE[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(DoubleE));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (DoubleE*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = *p++;
                        }
                    }
                }
            }
            

            // 
            public unsafe static void FromInt16To<T>(Stream stream, T[] dst, Func<Int16, T> func, int buffersize) where T : struct
            {
                int size = Marshal.SizeOf(typeof(Int16));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int16*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = func(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromInt24To<T>(Stream stream, T[] dst, Func<Int24, T> func, int buffersize) where T : struct
            {
                int size = Marshal.SizeOf(typeof(Int24));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int24*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = func(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromInt32To<T>(Stream stream, T[] dst, Func<Int32, T> func, int buffersize) where T : struct
            {
                int size = Marshal.SizeOf(typeof(Int32));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int32*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = func(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromInt64To<T>(Stream stream, T[] dst, Func<Int64, T> func, int buffersize) where T : struct
            {
                int size = Marshal.SizeOf(typeof(Int64));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int64*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = func(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromUInt16To<T>(Stream stream, T[] dst, Func<UInt16, T> func, int buffersize) where T : struct
            {
                int size = Marshal.SizeOf(typeof(UInt16));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt16*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = func(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromUInt24To<T>(Stream stream, T[] dst, Func<UInt24, T> func, int buffersize) where T : struct
            {
                int size = Marshal.SizeOf(typeof(UInt24));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt24*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = func(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromUInt32To<T>(Stream stream, T[] dst, Func<UInt32, T> func, int buffersize) where T : struct
            {
                int size = Marshal.SizeOf(typeof(UInt32));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt32*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = func(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromUInt64To<T>(Stream stream, T[] dst, Func<UInt64, T> func, int buffersize) where T : struct
            {
                int size = Marshal.SizeOf(typeof(UInt64));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt64*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = func(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromSingleTo<T>(Stream stream, T[] dst, Func<Single, T> func, int buffersize) where T : struct
            {
                int size = Marshal.SizeOf(typeof(Single));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Single*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = func(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromDoubleTo<T>(Stream stream, T[] dst, Func<Double, T> func, int buffersize) where T : struct
            {
                int size = Marshal.SizeOf(typeof(Double));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Double*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = func(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromInt16ETo<T>(Stream stream, T[] dst, Func<Int16E, T> func, int buffersize) where T : struct
            {
                int size = Marshal.SizeOf(typeof(Int16E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int16E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = func(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromInt24ETo<T>(Stream stream, T[] dst, Func<Int24E, T> func, int buffersize) where T : struct
            {
                int size = Marshal.SizeOf(typeof(Int24E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int24E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = func(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromInt32ETo<T>(Stream stream, T[] dst, Func<Int32E, T> func, int buffersize) where T : struct
            {
                int size = Marshal.SizeOf(typeof(Int32E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int32E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = func(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromInt64ETo<T>(Stream stream, T[] dst, Func<Int64E, T> func, int buffersize) where T : struct
            {
                int size = Marshal.SizeOf(typeof(Int64E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int64E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = func(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromUInt16ETo<T>(Stream stream, T[] dst, Func<UInt16E, T> func, int buffersize) where T : struct
            {
                int size = Marshal.SizeOf(typeof(UInt16E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt16E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = func(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromUInt24ETo<T>(Stream stream, T[] dst, Func<UInt24E, T> func, int buffersize) where T : struct
            {
                int size = Marshal.SizeOf(typeof(UInt24E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt24E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = func(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromUInt32ETo<T>(Stream stream, T[] dst, Func<UInt32E, T> func, int buffersize) where T : struct
            {
                int size = Marshal.SizeOf(typeof(UInt32E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt32E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = func(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromUInt64ETo<T>(Stream stream, T[] dst, Func<UInt64E, T> func, int buffersize) where T : struct
            {
                int size = Marshal.SizeOf(typeof(UInt64E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt64E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = func(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromSingleETo<T>(Stream stream, T[] dst, Func<SingleE, T> func, int buffersize) where T : struct
            {
                int size = Marshal.SizeOf(typeof(SingleE));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (SingleE*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = func(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromDoubleETo<T>(Stream stream, T[] dst, Func<DoubleE, T> func, int buffersize) where T : struct
            {
                int size = Marshal.SizeOf(typeof(DoubleE));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (DoubleE*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = func(*p++);
                        }
                    }
                }
            }
            


        }


        public static partial class LoadInt32
        {
            // 
            // 
            public static unsafe void FromInt16(Stream stream, Int32[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int16));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int16*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
                    }
                }
            }
            
            public static unsafe void FromInt24(Stream stream, Int32[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int24));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int24*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
                    }
                }
            }
            
            public static unsafe void FromInt32(Stream stream, Int32[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int32));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int32*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
                    }
                }
            }
            
            public static unsafe void FromInt64(Stream stream, Int32[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int64));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int64*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
                    }
                }
            }
            
            public static unsafe void FromUInt16(Stream stream, Int32[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt16));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt16*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
                    }
                }
            }
            
            public static unsafe void FromUInt24(Stream stream, Int32[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt24));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt24*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
                    }
                }
            }
            
            public static unsafe void FromUInt32(Stream stream, Int32[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt32));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt32*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
                    }
                }
            }
            
            public static unsafe void FromUInt64(Stream stream, Int32[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt64));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt64*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
                    }
                }
            }
            
            public static unsafe void FromSingle(Stream stream, Int32[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Single));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Single*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
                    }
                }
            }
            
            public static unsafe void FromDouble(Stream stream, Int32[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Double));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Double*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
                    }
                }
            }
            
            public static unsafe void FromInt16E(Stream stream, Int32[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int16E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int16E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
                    }
                }
            }
            
            public static unsafe void FromInt24E(Stream stream, Int32[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int24E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int24E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
                    }
                }
            }
            
            public static unsafe void FromInt32E(Stream stream, Int32[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int32E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int32E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
                    }
                }
            }
            
            public static unsafe void FromInt64E(Stream stream, Int32[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int64E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int64E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
                    }
                }
            }
            
            public static unsafe void FromUInt16E(Stream stream, Int32[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt16E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt16E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
                    }
                }
            }
            
            public static unsafe void FromUInt24E(Stream stream, Int32[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt24E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt24E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
                    }
                }
            }
            
            public static unsafe void FromUInt32E(Stream stream, Int32[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt32E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt32E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
                    }
                }
            }
            
            public static unsafe void FromUInt64E(Stream stream, Int32[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt64E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt64E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
                    }
                }
            }
            
            public static unsafe void FromSingleE(Stream stream, Int32[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(SingleE));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (SingleE*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
                    }
                }
            }
            
            public static unsafe void FromDoubleE(Stream stream, Int32[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(DoubleE));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (DoubleE*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
                    }
                }
            }
            
        }


        public static partial class LoadSingle
        {

            // 
            // 
            public unsafe static void FromInt16(Stream stream, Single[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int16));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int16*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (float)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromInt24(Stream stream, Single[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int24));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int24*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (float)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromInt32(Stream stream, Single[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int32));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int32*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (float)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromInt64(Stream stream, Single[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int64));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int64*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (float)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromUInt16(Stream stream, Single[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt16));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt16*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (float)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromUInt24(Stream stream, Single[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt24));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt24*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (float)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromUInt32(Stream stream, Single[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt32));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt32*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (float)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromUInt64(Stream stream, Single[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt64));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt64*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (float)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromSingle(Stream stream, Single[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Single));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Single*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (float)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromDouble(Stream stream, Single[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Double));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Double*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (float)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromInt16E(Stream stream, Single[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int16E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int16E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (float)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromInt24E(Stream stream, Single[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int24E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int24E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (float)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromInt32E(Stream stream, Single[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int32E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int32E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (float)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromInt64E(Stream stream, Single[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int64E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int64E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (float)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromUInt16E(Stream stream, Single[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt16E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt16E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (float)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromUInt24E(Stream stream, Single[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt24E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt24E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (float)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromUInt32E(Stream stream, Single[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt32E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt32E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (float)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromUInt64E(Stream stream, Single[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt64E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt64E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (float)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromSingleE(Stream stream, Single[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(SingleE));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (SingleE*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (float)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromDoubleE(Stream stream, Single[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(DoubleE));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (DoubleE*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (float)(*p++);
                        }
                    }
                }
            }
            
        }


        public static partial class LoadDouble
        {
            // 

            // 
            public unsafe static void FromInt16(Stream stream, Double[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int16));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int16*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (double)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromInt24(Stream stream, Double[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int24));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int24*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (double)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromInt32(Stream stream, Double[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int32));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int32*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (double)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromInt64(Stream stream, Double[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int64));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int64*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (double)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromUInt16(Stream stream, Double[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt16));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt16*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (double)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromUInt24(Stream stream, Double[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt24));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt24*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (double)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromUInt32(Stream stream, Double[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt32));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt32*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (double)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromUInt64(Stream stream, Double[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt64));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt64*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (double)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromSingle(Stream stream, Double[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Single));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Single*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (double)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromDouble(Stream stream, Double[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Double));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Double*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (double)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromInt16E(Stream stream, Double[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int16E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int16E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (double)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromInt24E(Stream stream, Double[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int24E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int24E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (double)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromInt32E(Stream stream, Double[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int32E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int32E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (double)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromInt64E(Stream stream, Double[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Int64E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int64E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (double)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromUInt16E(Stream stream, Double[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt16E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt16E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (double)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromUInt24E(Stream stream, Double[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt24E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt24E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (double)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromUInt32E(Stream stream, Double[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt32E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt32E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (double)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromUInt64E(Stream stream, Double[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(UInt64E));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (UInt64E*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (double)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromSingleE(Stream stream, Double[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(SingleE));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (SingleE*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (double)(*p++);
                        }
                    }
                }
            }
            
            public unsafe static void FromDoubleE(Stream stream, Double[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(DoubleE));
                var buffer = new byte[buffersize * size];

                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (DoubleE*)pin;
                        var last = index + buffersize;
                        while (index < last)
                        {
                            dst[index++] = (double)(*p++);
                        }
                    }
                }
            }
            
        }

        #region MyRegion
        /**/
        #endregion


    }
}
