/* Code generated using the t4 templates <# 
    var src = File.ReadAllText(Host.ResolvePath(targetPath)); 
    var m = Regex.Matches(src, @"T4\[(?<key>[\s\S]*?)\]\{(?<value>[\s\S]*?)\/\/\}T4");
    var methods = m.Cast<Match>().ToDictionary<Match, string, string>(k => k.Groups["key"].Value, v => v.Groups["value"].Value);
#>*/
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

    /*<#/*/
    public static class FileStreamExtented
    {

        #region Save

        public static void Save<T>(this Pixel<T> dst, string path, int buffersize = 0) where T : struct
        {
            using (var fs = File.Open(path, FileMode.Create))
            {
                buffersize = buffersize > 0 ? buffersize : dst.Width * dst.Height;

                int size = Marshal.SizeOf(typeof(T));
                var buffer = new byte[buffersize * size];

                var handle = GCHandle.Alloc(dst.pix, GCHandleType.Pinned);
                var pin = handle.AddrOfPinnedObject();

                var rest = dst.pix.Length * size;
                while (rest >= buffer.Length)
                {
                    Marshal.Copy(pin, buffer, 0, buffer.Length);
                    pin += buffer.Length;
                    fs.Write(buffer, 0, buffer.Length);
                    rest -= buffer.Length;
                }
                handle.Free();
            }
        }


        #endregion


        #region Load

        public static void Load<T>(this Pixel<T> dst, string path, int buffersize = 0, int offset = 0) where T : struct
        {
            using (var fs = File.Open(path, FileMode.Open))
            {
                CheckFileType(path, fs, typeof(T), dst);
                buffersize = buffersize > 0 ? buffersize : dst.Width * dst.Height;

                fs.Seek(offset, SeekOrigin.Current);

                int size = Marshal.SizeOf(typeof(T));
                var buffer = new byte[buffersize * size];

                var handle = GCHandle.Alloc(dst.pix, GCHandleType.Pinned);
                var pin = handle.AddrOfPinnedObject();

                var rest = fs.Length - fs.Position;
                while (rest >= buffer.Length)
                {
                    rest -= fs.Read(buffer, 0, buffer.Length);
                    Marshal.Copy(buffer, 0, pin, buffer.Length);
                    pin += buffer.Length; //IntPtr.Add(pin, buffer.Length);
                }
                handle.Free();
            }
        }

        #endregion


        #region Load Reflection 

        public static void Load<T, TT>(this Pixel<TT> dst, string path, Func<T, TT> func, int buffersize = 0, int offset = 0) where T : struct where TT : struct
        {
            using (var fs = File.Open(path, FileMode.Open))
            {
                CheckFileType(path, fs, typeof(T), dst);
                buffersize = buffersize > 0 ? buffersize : dst.Width * dst.Height;

                fs.Seek(offset, SeekOrigin.Current);

                var methodinfo = typeof(FileStream.Load).GetMethod(
                    $"From{typeof(T).Name}To"
                    , BindingFlags.Public | BindingFlags.Static);
                var constructed = methodinfo.MakeGenericMethod(typeof(TT));
                constructed.Invoke(null, new object[] { fs, dst.pix, func, buffersize });
            }
        }

        public static void Load<T>(this Pixel<T> dst, string path, Type filetype, int buffersize = 0, int offset = 0) where T : struct
        {
            if(typeof(T) == filetype)
            {
                Load<T>(dst, path, buffersize, offset);
            }
            else
            {
                using (var fs = File.Open(path, FileMode.Open))
                {
                    CheckFileType(path, fs, typeof(T), dst);
                    buffersize = buffersize > 0 ? buffersize : dst.Width * dst.Height;

                    fs.Seek(offset, SeekOrigin.Current);

                    filetype = filetype ?? typeof(T);
                    Type callType = filetype == typeof(T) ? typeof(FileStream.Load)
                                  : typeof(T) == typeof(Int32) ? typeof(FileStream.LoadInt32)
                                  : typeof(T) == typeof(Single) ? typeof(FileStream.LoadSingle)
                                  : typeof(T) == typeof(Double) ? typeof(FileStream.LoadDouble)
                                  : throw new Exception();

                    var methodinfo = callType.GetMethod(
                        $"From{filetype.Name}"
                        , BindingFlags.Public | BindingFlags.Static
                        , null,
                        new Type[] { typeof(Stream), typeof(T[]), typeof(int) }
                        , null);
                    methodinfo.Invoke(null, new object[] { fs, dst.pix, buffersize });
                }
                //typeof(int).MakeByRefType()
            }

        }

        #endregion


        #region LoadMarshal

        public static void LoadMarshal<T>(this Pixel<T> dst, string path, int buffersize = 0) where T : struct
        {
            using (var fs = File.Open(path, FileMode.Open))
            {
                CheckFileType(path, fs, typeof(T), dst);
                buffersize = buffersize > 0 ? buffersize : dst.Width * dst.Height;

                LoadMarshal<T>(fs, dst.pix, buffersize);
            }
        }

        /// <summary>
        /// 遅い
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dst"></param>
        /// <param name="path"></param>
        /// <param name="buffersize"></param>
        public unsafe static void LoadMarshal<T>(Stream stream, T[] dst, int buffersize)
        {
            int size = Marshal.SizeOf(typeof(T));
            var buffer = new byte[buffersize * size];

            int index = 0;
            var rest = (int)(stream.Length - stream.Position);

            while (rest >= buffer.Length)
            {
                rest -= stream.Read(buffer, 0, buffer.Length);
                var gch = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                try
                {
                    var p = gch.AddrOfPinnedObject();
                    for (var i = 0; i < buffer.Length; i += size)
                    {
                        dst[index++] = (T)Marshal.PtrToStructure(p, typeof(T));
                        p += size;
                    }
                }
                finally
                {
                    gch.Free();
                }
            }
        }

        #endregion


        private static void CheckFileType<T>(string path, Stream stream, Type inputtype, Pixel<T> dst) where T : struct
        {
            switch (Path.GetExtension(path).ToLower())
            {
                case ".trw": CheckTRW(stream, inputtype, dst); break;
                default: break;
            }
        }

        #region TRW

        /// <summary>
        /// 計16
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TRW
        {
            public int head;
            public int type;
            public int width;
            public int height;
        }

        private unsafe static void CheckTRW<T>(Stream stream, Type inputtype ,Pixel<T> src) where T : struct
        {
            var size = Marshal.SizeOf(typeof(TRW));
            var buffer = new byte[size];
            stream.Read(buffer, 0, buffer.Length);
            TRW trw;
            fixed (byte* pin = buffer) trw = *(TRW*)pin; 

            switch (trw.type)
            {
                case 0:
                    if (inputtype.Name != "Int16") throw new ArgumentException();
                    break;
                case 1:
                    if (inputtype.Name != "Int32") throw new ArgumentException();
                    break;
                case 2:
                    if (inputtype.Name != "Single") throw new ArgumentException();
                    break;
                default:
                    throw new ArgumentException();
            }
            if (trw.width != src.Width) throw new ArgumentException();
            if (trw.height != src.Height) throw new ArgumentException();


            //(FileType)Enum.ToObject(typeof(FileType), BitConverter.ToInt32(buffer, 0));
        }

        #endregion





    }
    /*/#>*/


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
