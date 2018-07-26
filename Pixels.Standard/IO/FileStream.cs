using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

/* Code generated using the t4 templates <# 
    var src = File.ReadAllText(Host.ResolvePath(targetPath)); 
    var m = Regex.Matches(src, @"T4\[(?<key>[\s\S]*?)\]\{(?<value>[\s\S]*?)\/\/\}T4");
    var methods = m.Cast<Match>().ToDictionary<Match, string, string>(k => k.Groups["key"].Value, v => v.Groups["value"].Value);
#>*/
namespace Pixels.Standard.IO
{
    public static partial class FileStream
    {
        /*<#/*/
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

        public static partial class Load
        {

            /*<#/*/


            //T4[A]{
            public static unsafe void FromByte(Stream stream, Byte[] dst, int buffersize)
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
                            dst[index++] = *p++;
                        }
                    }
                }
            }
            //}T4

            //T4[B]{
            public unsafe static void FromByteTo<T>(Stream stream, T[] dst, Func<Byte, T> func, int buffersize) where T : struct
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
            //}T4

            /*/#>*/


            // <# foreach(var i in types.Where(n => n != "Byte")){ #><#= methods["A"].Replace("Byte", i) #><# } #>


            // <# foreach(var i in types.Where(n => n != "Byte")){ #><#= methods["B"].Replace("Byte", i) #><# } #>



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
