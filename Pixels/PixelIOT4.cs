using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;


namespace Pixels.IO
{
    public static partial class FileStream
    {
        public static class Load
        {
            #region Load
            
            public unsafe static void FromByte(Stream stream, Byte[] dst, int buffersize)
            {
                int size = dst.Length * Marshal.SizeOf(typeof(Byte));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Byte*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = *p++;
                    }
                }
            }

            
            public unsafe static void FromInt16(Stream stream, Int16[] dst, int buffersize)
            {
                int size = dst.Length * Marshal.SizeOf(typeof(Int16));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int16*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = *p++;
                    }
                }
            }

            
            public unsafe static void FromInt24(Stream stream, Int24[] dst, int buffersize)
            {
                int size = dst.Length * Marshal.SizeOf(typeof(Int24));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int24*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = *p++;
                    }
                }
            }

            
            public unsafe static void FromInt32(Stream stream, Int32[] dst, int buffersize)
            {
                int size = dst.Length * Marshal.SizeOf(typeof(Int32));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int32*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = *p++;
                    }
                }
            }

            
            public unsafe static void FromInt64(Stream stream, Int64[] dst, int buffersize)
            {
                int size = dst.Length * Marshal.SizeOf(typeof(Int64));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int64*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = *p++;
                    }
                }
            }

            
            public unsafe static void FromUInt16(Stream stream, UInt16[] dst, int buffersize)
            {
                int size = dst.Length * Marshal.SizeOf(typeof(UInt16));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt16*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = *p++;
                    }
                }
            }

            
            public unsafe static void FromUInt24(Stream stream, UInt24[] dst, int buffersize)
            {
                int size = dst.Length * Marshal.SizeOf(typeof(UInt24));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt24*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = *p++;
                    }
                }
            }

            
            public unsafe static void FromUInt32(Stream stream, UInt32[] dst, int buffersize)
            {
                int size = dst.Length * Marshal.SizeOf(typeof(UInt32));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt32*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = *p++;
                    }
                }
            }

            
            public unsafe static void FromUInt64(Stream stream, UInt64[] dst, int buffersize)
            {
                int size = dst.Length * Marshal.SizeOf(typeof(UInt64));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt64*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = *p++;
                    }
                }
            }

            
            public unsafe static void FromSingle(Stream stream, Single[] dst, int buffersize)
            {
                int size = dst.Length * Marshal.SizeOf(typeof(Single));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Single*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = *p++;
                    }
                }
            }

            
            public unsafe static void FromDouble(Stream stream, Double[] dst, int buffersize)
            {
                int size = dst.Length * Marshal.SizeOf(typeof(Double));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Double*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = *p++;
                    }
                }
            }

            
            public unsafe static void FromInt16E(Stream stream, Int16E[] dst, int buffersize)
            {
                int size = dst.Length * Marshal.SizeOf(typeof(Int16E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int16E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = *p++;
                    }
                }
            }

            
            public unsafe static void FromInt24E(Stream stream, Int24E[] dst, int buffersize)
            {
                int size = dst.Length * Marshal.SizeOf(typeof(Int24E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int24E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = *p++;
                    }
                }
            }

            
            public unsafe static void FromInt32E(Stream stream, Int32E[] dst, int buffersize)
            {
                int size = dst.Length * Marshal.SizeOf(typeof(Int32E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int32E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = *p++;
                    }
                }
            }

            
            public unsafe static void FromInt64E(Stream stream, Int64E[] dst, int buffersize)
            {
                int size = dst.Length * Marshal.SizeOf(typeof(Int64E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int64E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = *p++;
                    }
                }
            }

            
            public unsafe static void FromUInt16E(Stream stream, UInt16E[] dst, int buffersize)
            {
                int size = dst.Length * Marshal.SizeOf(typeof(UInt16E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt16E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = *p++;
                    }
                }
            }

            
            public unsafe static void FromUInt24E(Stream stream, UInt24E[] dst, int buffersize)
            {
                int size = dst.Length * Marshal.SizeOf(typeof(UInt24E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt24E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = *p++;
                    }
                }
            }

            
            public unsafe static void FromUInt32E(Stream stream, UInt32E[] dst, int buffersize)
            {
                int size = dst.Length * Marshal.SizeOf(typeof(UInt32E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt32E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = *p++;
                    }
                }
            }

            
            public unsafe static void FromUInt64E(Stream stream, UInt64E[] dst, int buffersize)
            {
                int size = dst.Length * Marshal.SizeOf(typeof(UInt64E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt64E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = *p++;
                    }
                }
            }

            
            public unsafe static void FromSingleE(Stream stream, SingleE[] dst, int buffersize)
            {
                int size = dst.Length * Marshal.SizeOf(typeof(SingleE));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (SingleE*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = *p++;
                    }
                }
            }

            
            public unsafe static void FromDoubleE(Stream stream, DoubleE[] dst, int buffersize)
            {
                int size = dst.Length * Marshal.SizeOf(typeof(DoubleE));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
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
            
            public unsafe static void FromByte<T>(Stream stream, T[] dst, Func<Byte, T> func, int buffersize) where T : struct
            {
                int size = dst.Length * Marshal.SizeOf(typeof(Byte));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Byte*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = func(*p++);
                    }
                }
            }

            
            public unsafe static void FromInt16<T>(Stream stream, T[] dst, Func<Int16, T> func, int buffersize) where T : struct
            {
                int size = dst.Length * Marshal.SizeOf(typeof(Int16));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int16*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = func(*p++);
                    }
                }
            }

            
            public unsafe static void FromInt24<T>(Stream stream, T[] dst, Func<Int24, T> func, int buffersize) where T : struct
            {
                int size = dst.Length * Marshal.SizeOf(typeof(Int24));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int24*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = func(*p++);
                    }
                }
            }

            
            public unsafe static void FromInt32<T>(Stream stream, T[] dst, Func<Int32, T> func, int buffersize) where T : struct
            {
                int size = dst.Length * Marshal.SizeOf(typeof(Int32));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int32*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = func(*p++);
                    }
                }
            }

            
            public unsafe static void FromInt64<T>(Stream stream, T[] dst, Func<Int64, T> func, int buffersize) where T : struct
            {
                int size = dst.Length * Marshal.SizeOf(typeof(Int64));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int64*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = func(*p++);
                    }
                }
            }

            
            public unsafe static void FromUInt16<T>(Stream stream, T[] dst, Func<UInt16, T> func, int buffersize) where T : struct
            {
                int size = dst.Length * Marshal.SizeOf(typeof(UInt16));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt16*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = func(*p++);
                    }
                }
            }

            
            public unsafe static void FromUInt24<T>(Stream stream, T[] dst, Func<UInt24, T> func, int buffersize) where T : struct
            {
                int size = dst.Length * Marshal.SizeOf(typeof(UInt24));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt24*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = func(*p++);
                    }
                }
            }

            
            public unsafe static void FromUInt32<T>(Stream stream, T[] dst, Func<UInt32, T> func, int buffersize) where T : struct
            {
                int size = dst.Length * Marshal.SizeOf(typeof(UInt32));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt32*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = func(*p++);
                    }
                }
            }

            
            public unsafe static void FromUInt64<T>(Stream stream, T[] dst, Func<UInt64, T> func, int buffersize) where T : struct
            {
                int size = dst.Length * Marshal.SizeOf(typeof(UInt64));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt64*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = func(*p++);
                    }
                }
            }

            
            public unsafe static void FromSingle<T>(Stream stream, T[] dst, Func<Single, T> func, int buffersize) where T : struct
            {
                int size = dst.Length * Marshal.SizeOf(typeof(Single));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Single*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = func(*p++);
                    }
                }
            }

            
            public unsafe static void FromDouble<T>(Stream stream, T[] dst, Func<Double, T> func, int buffersize) where T : struct
            {
                int size = dst.Length * Marshal.SizeOf(typeof(Double));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Double*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = func(*p++);
                    }
                }
            }

            
            public unsafe static void FromInt16E<T>(Stream stream, T[] dst, Func<Int16E, T> func, int buffersize) where T : struct
            {
                int size = dst.Length * Marshal.SizeOf(typeof(Int16E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int16E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = func(*p++);
                    }
                }
            }

            
            public unsafe static void FromInt24E<T>(Stream stream, T[] dst, Func<Int24E, T> func, int buffersize) where T : struct
            {
                int size = dst.Length * Marshal.SizeOf(typeof(Int24E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int24E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = func(*p++);
                    }
                }
            }

            
            public unsafe static void FromInt32E<T>(Stream stream, T[] dst, Func<Int32E, T> func, int buffersize) where T : struct
            {
                int size = dst.Length * Marshal.SizeOf(typeof(Int32E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int32E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = func(*p++);
                    }
                }
            }

            
            public unsafe static void FromInt64E<T>(Stream stream, T[] dst, Func<Int64E, T> func, int buffersize) where T : struct
            {
                int size = dst.Length * Marshal.SizeOf(typeof(Int64E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int64E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = func(*p++);
                    }
                }
            }

            
            public unsafe static void FromUInt16E<T>(Stream stream, T[] dst, Func<UInt16E, T> func, int buffersize) where T : struct
            {
                int size = dst.Length * Marshal.SizeOf(typeof(UInt16E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt16E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = func(*p++);
                    }
                }
            }

            
            public unsafe static void FromUInt24E<T>(Stream stream, T[] dst, Func<UInt24E, T> func, int buffersize) where T : struct
            {
                int size = dst.Length * Marshal.SizeOf(typeof(UInt24E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt24E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = func(*p++);
                    }
                }
            }

            
            public unsafe static void FromUInt32E<T>(Stream stream, T[] dst, Func<UInt32E, T> func, int buffersize) where T : struct
            {
                int size = dst.Length * Marshal.SizeOf(typeof(UInt32E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt32E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = func(*p++);
                    }
                }
            }

            
            public unsafe static void FromUInt64E<T>(Stream stream, T[] dst, Func<UInt64E, T> func, int buffersize) where T : struct
            {
                int size = dst.Length * Marshal.SizeOf(typeof(UInt64E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt64E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = func(*p++);
                    }
                }
            }

            
            public unsafe static void FromSingleE<T>(Stream stream, T[] dst, Func<SingleE, T> func, int buffersize) where T : struct
            {
                int size = dst.Length * Marshal.SizeOf(typeof(SingleE));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (SingleE*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = func(*p++);
                    }
                }
            }

            
            public unsafe static void FromDoubleE<T>(Stream stream, T[] dst, Func<DoubleE, T> func, int buffersize) where T : struct
            {
                int size = dst.Length * Marshal.SizeOf(typeof(DoubleE));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
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

        }

        #region Load to Int32
        
        public unsafe static void LoadByte(this int[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Byte));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Byte*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (int)*p++;
                    }
                }
        }

        
        public unsafe static void LoadInt16(this int[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int16));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int16*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (int)*p++;
                    }
                }
        }

        
        public unsafe static void LoadInt24(this int[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int24));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int24*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (int)*p++;
                    }
                }
        }

        
        public unsafe static void LoadInt64(this int[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int64));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int64*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (int)*p++;
                    }
                }
        }

        
        public unsafe static void LoadUInt16(this int[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt16));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt16*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (int)*p++;
                    }
                }
        }

        
        public unsafe static void LoadUInt24(this int[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt24));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt24*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (int)*p++;
                    }
                }
        }

        
        public unsafe static void LoadUInt32(this int[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt32));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt32*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (int)*p++;
                    }
                }
        }

        
        public unsafe static void LoadUInt64(this int[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt64));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt64*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (int)*p++;
                    }
                }
        }

        
        public unsafe static void LoadSingle(this int[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Single));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Single*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (int)*p++;
                    }
                }
        }

        
        public unsafe static void LoadDouble(this int[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Double));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Double*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (int)*p++;
                    }
                }
        }

        
        public unsafe static void LoadInt16E(this int[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int16E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int16E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (int)*p++;
                    }
                }
        }

        
        public unsafe static void LoadInt24E(this int[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int24E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int24E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (int)*p++;
                    }
                }
        }

        
        public unsafe static void LoadInt32E(this int[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int32E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int32E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (int)*p++;
                    }
                }
        }

        
        public unsafe static void LoadInt64E(this int[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int64E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int64E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (int)*p++;
                    }
                }
        }

        
        public unsafe static void LoadUInt16E(this int[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt16E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt16E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (int)*p++;
                    }
                }
        }

        
        public unsafe static void LoadUInt24E(this int[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt24E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt24E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (int)*p++;
                    }
                }
        }

        
        public unsafe static void LoadUInt32E(this int[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt32E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt32E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (int)*p++;
                    }
                }
        }

        
        public unsafe static void LoadUInt64E(this int[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt64E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt64E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (int)*p++;
                    }
                }
        }

        
        public unsafe static void LoadSingleE(this int[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(SingleE));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (SingleE*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (int)*p++;
                    }
                }
        }

        
        public unsafe static void LoadDoubleE(this int[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(DoubleE));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
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
        
        public unsafe static void LoadByte(this float[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Byte));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Byte*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (float)*p++;
                    }
                }
        }

        
        public unsafe static void LoadInt16(this float[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int16));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int16*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (float)*p++;
                    }
                }
        }

        
        public unsafe static void LoadInt24(this float[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int24));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int24*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (float)*p++;
                    }
                }
        }

        
        public unsafe static void LoadInt32(this float[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int32));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int32*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (float)*p++;
                    }
                }
        }

        
        public unsafe static void LoadInt64(this float[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int64));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int64*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (float)*p++;
                    }
                }
        }

        
        public unsafe static void LoadUInt16(this float[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt16));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt16*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (float)*p++;
                    }
                }
        }

        
        public unsafe static void LoadUInt24(this float[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt24));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt24*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (float)*p++;
                    }
                }
        }

        
        public unsafe static void LoadUInt32(this float[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt32));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt32*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (float)*p++;
                    }
                }
        }

        
        public unsafe static void LoadUInt64(this float[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt64));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt64*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (float)*p++;
                    }
                }
        }

        
        public unsafe static void LoadDouble(this float[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Double));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Double*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (float)*p++;
                    }
                }
        }

        
        public unsafe static void LoadInt16E(this float[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int16E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int16E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (float)*p++;
                    }
                }
        }

        
        public unsafe static void LoadInt24E(this float[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int24E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int24E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (float)*p++;
                    }
                }
        }

        
        public unsafe static void LoadInt32E(this float[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int32E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int32E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (float)*p++;
                    }
                }
        }

        
        public unsafe static void LoadInt64E(this float[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int64E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int64E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (float)*p++;
                    }
                }
        }

        
        public unsafe static void LoadUInt16E(this float[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt16E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt16E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (float)*p++;
                    }
                }
        }

        
        public unsafe static void LoadUInt24E(this float[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt24E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt24E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (float)*p++;
                    }
                }
        }

        
        public unsafe static void LoadUInt32E(this float[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt32E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt32E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (float)*p++;
                    }
                }
        }

        
        public unsafe static void LoadUInt64E(this float[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt64E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt64E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (float)*p++;
                    }
                }
        }

        
        public unsafe static void LoadSingleE(this float[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(SingleE));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (SingleE*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (float)*p++;
                    }
                }
        }

        
        public unsafe static void LoadDoubleE(this float[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(DoubleE));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
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
        
        public unsafe static void LoadByte(this double[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Byte));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Byte*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (double)*p++;
                    }
                }
        }

        
        public unsafe static void LoadInt16(this double[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int16));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int16*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (double)*p++;
                    }
                }
        }

        
        public unsafe static void LoadInt24(this double[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int24));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int24*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (double)*p++;
                    }
                }
        }

        
        public unsafe static void LoadInt32(this double[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int32));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int32*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (double)*p++;
                    }
                }
        }

        
        public unsafe static void LoadInt64(this double[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int64));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int64*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (double)*p++;
                    }
                }
        }

        
        public unsafe static void LoadUInt16(this double[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt16));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt16*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (double)*p++;
                    }
                }
        }

        
        public unsafe static void LoadUInt24(this double[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt24));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt24*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (double)*p++;
                    }
                }
        }

        
        public unsafe static void LoadUInt32(this double[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt32));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt32*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (double)*p++;
                    }
                }
        }

        
        public unsafe static void LoadUInt64(this double[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt64));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt64*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (double)*p++;
                    }
                }
        }

        
        public unsafe static void LoadSingle(this double[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Single));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Single*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (double)*p++;
                    }
                }
        }

        
        public unsafe static void LoadInt16E(this double[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int16E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int16E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (double)*p++;
                    }
                }
        }

        
        public unsafe static void LoadInt24E(this double[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int24E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int24E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (double)*p++;
                    }
                }
        }

        
        public unsafe static void LoadInt32E(this double[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int32E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int32E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (double)*p++;
                    }
                }
        }

        
        public unsafe static void LoadInt64E(this double[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(Int64E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (Int64E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (double)*p++;
                    }
                }
        }

        
        public unsafe static void LoadUInt16E(this double[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt16E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt16E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (double)*p++;
                    }
                }
        }

        
        public unsafe static void LoadUInt24E(this double[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt24E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt24E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (double)*p++;
                    }
                }
        }

        
        public unsafe static void LoadUInt32E(this double[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt32E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt32E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (double)*p++;
                    }
                }
        }

        
        public unsafe static void LoadUInt64E(this double[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(UInt64E));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (UInt64E*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (double)*p++;
                    }
                }
        }

        
        public unsafe static void LoadSingleE(this double[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(SingleE));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
                fixed (byte* pin = buffer)
                {
                    var p = (SingleE*)pin;
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (double)*p++;
                    }
                }
        }

        
        public unsafe static void LoadDoubleE(this double[] dst, Stream stream)
        {
            int size = dst.Length * Marshal.SizeOf(typeof(DoubleE));
                byte[] buffer = new byte[size];
                stream.Read(buffer, 0, size);
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
