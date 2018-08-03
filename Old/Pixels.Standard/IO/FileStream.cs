using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;


/* <#@ include file="T4Base.t4" once="true" #> */

namespace Pixels.Standard.IO
{
    public static partial class FileStream
    {
        #region private / protected
        /*<#/*/

        public static void _GetBufferSize<T>(int length, ref int buffersize, out int size, out byte[] buffer) where T : struct
        {
            buffersize = buffersize > 0 ? buffersize : length;
            size = Marshal.SizeOf(typeof(T));
            buffer = new byte[buffersize * size];
        }

        static unsafe void MemSet0<T>(ref T x) where T : unmanaged
        {
            fixed (T* p = &x)
            {
                var b = (byte*)p;
                var size = sizeof(T);
                for (int i = 0; i < size; i++)
                {
                    b[i] = 0;
                }
            }
        }

        /*/#>*/
        #endregion



        public static partial class Load
        {

            #region [T4]FromTypeToType


            /*<# Method(@"*/
            public static unsafe void FromInt32ToInt32(Stream stream, Int32[] dst, byte[] buffer)
            {
                int index = 0;
                var rest = (int)(stream.Length - stream.Position);
                while (rest >= buffer.Length)
                {
                    rest -= stream.Read(buffer, 0, buffer.Length);
                    fixed (byte* pin = buffer)
                    {
                        var p = (Int32*)pin;
                        //var last = index + buffersize;
                        //while (index < last)
                        //{
                        //    dst[index++] = *p++;
                        //}
                    }
                }
            }
            /*", types, (i,j)=> i.Replace("ToInt32",$"<{j}>"));#>*/


            /*<# Method(@"*/
            public static unsafe void FromByteTo<T>(Stream stream, T[] dst, Func<Byte, T> func, int buffersize) where T : struct
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
                        while (index < last)
                        {
                            dst[index++] = func(*p++);
                        }
                    }
                }
            }
            /*", types, (i,j)=> i.Replace("<Int32>",$"<{j}>"));#>*/


            #endregion





        }


        public static partial class LoadInt32
        {
            // <#/*

            //T4[C]{
            public static unsafe void FromByte(Stream stream, Int32[] dst, int buffersize)
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
                        while (index < last)
                        {
                            dst[index++] = (int)(*p++);
                        }
                    }
                }
            }
            //}T4

            // */#>

            // <# foreach(var i in types.Where(n => n != "Byte")){ #><#= methods["C"].Replace("Byte", i) #><# } #>

        }


        public static partial class LoadSingle
        {

            // <#/*

            //T4[D]{
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
                        while (index < last)
                        {
                            dst[index++] = (float)(*p++);
                        }
                    }
                }
            }
            //}T4

            // */#>

            // <# foreach(var i in types.Where(n => n != "Byte")){ #><#= methods["D"].Replace("Byte", i) #><# } #>

        }


        public static partial class LoadDouble
        {
            // <#/*

            //T4[E]{
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
                        while (index < last)
                        {
                            dst[index++] = (double)(*p++);
                        }
                    }
                }
            }
            //}T4

            // */#>


            // <# foreach(var i in types.Where(n => n != "Byte")){ #><#= methods["E"].Replace("Byte", i) #><# } #>

        }

        #region MyRegion
        /*<#/*/

        public unsafe static void LoadTest()
        {

            using (var fs = File.OpenRead("test.data"))
            {
                int bufferSize = 128;
#if NETSTANDARD
                var buffer = new byte[bufferSize];
                var rest = fs.Length;
                while (rest > 0)
                {
                    var read = fs.Read(buffer, 0, buffer.Length);
                    rest -= read;
                }
#elif NETCore2_1
                //GC外なので
                var buffer = bufferSize <= 128 ? stackalloc byte[bufferSize] : new byte[bufferSize];
                var rest = fs.Length;
                while (rest > 0)
                {
                    var read = fs.Read(buffer);
                    rest -= read;
                }
#endif
            }
        }


        public unsafe static void LoadString<T>(this T[] dst, string path, Func<String, T> func, char[] separator) where T : struct
        {
            using (var sr = new StreamReader(path))
            {
                var hoge = sr.ReadToEnd().Split(separator);
                for (var i = 0; i < hoge.Length; i++)
                {
                    dst[i] = func(hoge[i]);
                }
            }
        }



        /*/#>*/
        #endregion


    }
}
