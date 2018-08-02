using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Pixels.IO
{

    public class MarshalCopy : IDisposable
    {

        GCHandle handle;
        IntPtr pin;
        public byte[] buffer;
        public int size { get; set; }

        public MarshalCopy(object obj, int buffersize, Type type)
        {
            handle = GCHandle.Alloc(obj, GCHandleType.Pinned);
            pin = handle.AddrOfPinnedObject();
            size = Marshal.SizeOf(type);
            buffer = new byte[buffersize * size];
        }


        public void Read(byte[] dst)
        {
            Marshal.Copy(pin, dst, 0, dst.Length);
            pin += dst.Length;
        }

        public void Write(byte[] src)
        {
            Marshal.Copy(src, 0, pin, src.Length);
            pin += src.Length; //IntPtr.Add(pin, buffer.Length);
        }

        public void WriteNotInc(byte[] src)
        {
            Marshal.Copy(src, 0, pin, src.Length);
        }

        public void Dispose()
        {
            handle.Free();
            buffer = null;
        }

    }


    public static partial class FileStreamExtented
    {

        public static void Save<T>(this PixelByte<T> dst, string path, byte[] header = null) where T : struct
        {
            using (var fs = File.Open(path, FileMode.Create))
            {
                if (header != null)
                    fs.Write(header, 0, header.Length);

                fs.Write(dst.pix, 0, dst.pix.Length);
            }
        }

        public static void Load<T>(this PixelByte<T> dst, string path, int offset = 0) where T : struct
        {
            using (var fs = File.Open(path, FileMode.Open))
            {
                FileInfo.CheckFileType(path, fs, typeof(T), dst);
                fs.Seek(offset, SeekOrigin.Current);

                fs.Read(dst.pix, 0, dst.pix.Length);
            }
        }


        public static void Save<T>(this Pixel<T> dst, string path, int buffersize = 0, byte[] header = null) where T : struct
        {
            buffersize = buffersize > 0 ? buffersize : dst.Width * dst.Height;

            using (var fs = File.Open(path, FileMode.Create))
            using (var p = new MarshalCopy(dst.pix, buffersize, typeof(T)))
            {
                if(header != null)
                    fs.Write(header, 0, header.Length);

                var rest = dst.pix.Length * p.size;
                while (rest >= p.buffer.Length)
                {
                    p.Read(p.buffer);
                    fs.Write(p.buffer, 0, p.buffer.Length);
                    rest -= p.buffer.Length;
                }
            }
        }

        public static void Load<T>(this Pixel<T> dst, string path, int buffersize = 0, int offset = 0) where T : struct
        {
            buffersize = buffersize > 0 ? buffersize : dst.Width * dst.Height;

            using (var fs = File.Open(path, FileMode.Open))
            using (var p = new MarshalCopy(dst.pix, buffersize, typeof(T)))
            {
                FileInfo.CheckFileType(path, fs, typeof(T), dst);
                fs.Seek(offset, SeekOrigin.Current);

                var rest = fs.Length - fs.Position;
                while (rest >= p.buffer.Length)
                {
                    rest -= fs.Read(p.buffer, 0, p.buffer.Length);
                    p.Write(p.buffer);
                }
            }
        }


        public static void Load<T, U>(this Pixel<U> dst, string path, Func<T, U> func, int buffersize = 0, int offset = 0) where T : struct where U : struct
        {
            buffersize = buffersize > 0 ? buffersize : dst.Width * dst.Height;
            T[] hoge = new T[buffersize];

            using (var fs = File.Open(path, FileMode.Open))
            using (var p = new MarshalCopy(hoge, buffersize, typeof(T)))
            {
                FileInfo.CheckFileType(path, fs, typeof(T), dst);
                fs.Seek(offset, SeekOrigin.Current);

                int index = 0;
                var rest = fs.Length - fs.Position;
                while (rest >= p.buffer.Length)
                {
                    rest -= fs.Read(p.buffer, 0, p.buffer.Length);
                    p.WriteNotInc(p.buffer);
                    foreach (var i in hoge)
                        dst[index++] = func(i);
                }
            }
        }



        /*
        public static void Load<T, TT>(this Pixel<TT> dst, string path, Func<T, TT> func, int buffersize = 0, int offset = 0) where T : struct where TT : struct
        {
            //using (var fs = File.Open(path, FileMode.Open))
            //{
            //    FileInfo.CheckFileType(path, fs, typeof(T), dst);
            //    fs.Seek(offset, SeekOrigin.Current);

            //    _GetBufferSize(dst, ref buffersize, out int size, out byte[] buffer);

            //    var methodinfo = typeof(FileStream.Load).GetMethod(
            //        $"From{typeof(T).Name}To"
            //        , BindingFlags.Public | BindingFlags.Static);
            //    var constructed = methodinfo.MakeGenericMethod(typeof(TT));
            //    constructed.Invoke(null, new object[] { fs, dst.pix, func, buffersize });
            //}
        }


        public static void Load<T>(this Pixel<T> dst, string path, Type filetype, int buffersize = 0, int offset = 0) where T : struct
        {
            //if(typeof(T) == filetype)
            //{
            //    Load<T>(dst, path, buffersize, offset);
            //}
            //else
            //{
            //    using (var fs = File.Open(path, FileMode.Open))
            //    {
            //        FileInfo.CheckFileType(path, fs, typeof(T), dst);
            //        fs.Seek(offset, SeekOrigin.Current);

            //        _GetBufferSize(dst, ref buffersize, out int size, out byte[] buffer);

            //        filetype = filetype ?? typeof(T);
            //        Type callType = filetype == typeof(T) ? typeof(FileStream.Load)
            //                      : typeof(T) == typeof(Int32) ? typeof(FileStream.LoadInt32)
            //                      : typeof(T) == typeof(Single) ? typeof(FileStream.LoadSingle)
            //                      : typeof(T) == typeof(Double) ? typeof(FileStream.LoadDouble)
            //                      : throw new Exception();

            //        var methodinfo = callType.GetMethod(
            //            $"From{filetype.Name}"
            //            , BindingFlags.Public | BindingFlags.Static
            //            , null,
            //            new Type[] { typeof(Stream), typeof(T[]), typeof(int) }
            //            , null);
            //        methodinfo.Invoke(null, new object[] { fs, dst.pix, buffersize });
            //    }
            //    //typeof(int).MakeByRefType()
            //}
        }
        */

    }

}




//var a = src.pix.AsMemory().Pin();
//var b = a.Pointer;
//src.pix.AsMemory().Length;

    //強制 int double float 実装
    //, FileType type = FileType.Noneを使用した読み込み