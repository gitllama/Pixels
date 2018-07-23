using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;


namespace Pixels.Standard.IO
{
    public static partial class FileStream
    {
        public static class LoadInt32
        {
            #region Load
            public unsafe static void FromByte(Stream stream, Int32[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Byte));
                var buffer = new byte[buffersize * size];

				int index = 0;
				var rest = (int)(stream.Length - stream.Position);
				while (rest >= buffer.Length)
				{
					rest -= stream.Read(buffer, 0, buffer.Length);
					fixed (byte* pin = buffer)
					{
						var p = (Byte*)pin;
						var last = index + buffersize;
						while(index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
					}
				}
            }

            
            public unsafe static void FromInt16(Stream stream, Int32[] dst, int buffersize)
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
						while(index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
					}
				}
            }

            
            public unsafe static void FromInt24(Stream stream, Int32[] dst, int buffersize)
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
						while(index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
					}
				}
            }

            
            public unsafe static void FromInt32(Stream stream, Int32[] dst, int buffersize)
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
						while(index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
					}
				}
            }

            
            public unsafe static void FromInt64(Stream stream, Int32[] dst, int buffersize)
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
						while(index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
					}
				}
            }

            
            public unsafe static void FromUInt16(Stream stream, Int32[] dst, int buffersize)
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
						while(index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
					}
				}
            }

            
            public unsafe static void FromUInt24(Stream stream, Int32[] dst, int buffersize)
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
						while(index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
					}
				}
            }

            
            public unsafe static void FromUInt32(Stream stream, Int32[] dst, int buffersize)
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
						while(index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
					}
				}
            }

            
            public unsafe static void FromUInt64(Stream stream, Int32[] dst, int buffersize)
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
						while(index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
					}
				}
            }

            
            public unsafe static void FromSingle(Stream stream, Int32[] dst, int buffersize)
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
						while(index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
					}
				}
            }

            
            public unsafe static void FromDouble(Stream stream, Int32[] dst, int buffersize)
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
						while(index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
					}
				}
            }

            
            public unsafe static void FromInt16E(Stream stream, Int32[] dst, int buffersize)
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
						while(index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
					}
				}
            }

            
            public unsafe static void FromInt24E(Stream stream, Int32[] dst, int buffersize)
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
						while(index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
					}
				}
            }

            
            public unsafe static void FromInt32E(Stream stream, Int32[] dst, int buffersize)
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
						while(index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
					}
				}
            }

            
            public unsafe static void FromInt64E(Stream stream, Int32[] dst, int buffersize)
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
						while(index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
					}
				}
            }

            
            public unsafe static void FromUInt16E(Stream stream, Int32[] dst, int buffersize)
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
						while(index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
					}
				}
            }

            
            public unsafe static void FromUInt24E(Stream stream, Int32[] dst, int buffersize)
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
						while(index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
					}
				}
            }

            
            public unsafe static void FromUInt32E(Stream stream, Int32[] dst, int buffersize)
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
						while(index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
					}
				}
            }

            
            public unsafe static void FromUInt64E(Stream stream, Int32[] dst, int buffersize)
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
						while(index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
					}
				}
            }

            
            public unsafe static void FromSingleE(Stream stream, Int32[] dst, int buffersize)
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
						while(index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
					}
				}
            }

            
            public unsafe static void FromDoubleE(Stream stream, Int32[] dst, int buffersize)
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
						while(index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
					}
				}
            }

            #endregion

        }

		public static class LoadSingle
        {
            #region Load
            public unsafe static void FromByte(Stream stream, Single[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Byte));
                var buffer = new byte[buffersize * size];

				int index = 0;
				var rest = (int)(stream.Length - stream.Position);
				while (rest >= buffer.Length)
				{
					rest -= stream.Read(buffer, 0, buffer.Length);
					fixed (byte* pin = buffer)
					{
						var p = (Byte*)pin;
						var last = index + buffersize;
						while(index < last)
                        {
                            dst[index++] = (float)(*p++);
                        }
					}
				}
            }

            
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
                        {
                            dst[index++] = (float)(*p++);
                        }
					}
				}
            }

            #endregion

        }

		public static class LoadDouble
        {
            #region Load
            public unsafe static void FromByte(Stream stream, Double[] dst, int buffersize)
            {
                int size = Marshal.SizeOf(typeof(Byte));
                var buffer = new byte[buffersize * size];

				int index = 0;
				var rest = (int)(stream.Length - stream.Position);
				while (rest >= buffer.Length)
				{
					rest -= stream.Read(buffer, 0, buffer.Length);
					fixed (byte* pin = buffer)
					{
						var p = (Byte*)pin;
						var last = index + buffersize;
						while(index < last)
                        {
                            dst[index++] = (double)(*p++);
                        }
					}
				}
            }

            
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
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
						while(index < last)
                        {
                            dst[index++] = (double)(*p++);
                        }
					}
				}
            }

            #endregion

        }
    }
}