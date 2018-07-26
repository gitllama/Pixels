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
    public static partial class FileStreamExtented
    {


        private static void _GetBufferSize<T>(Pixel<T> dst, ref int buffersize, out int size, out byte[] buffer) where T : struct
        {
            buffersize = buffersize > 0 ? buffersize : dst.Width* dst.Height;
            size = Marshal.SizeOf(typeof(T));
            buffer = new byte[buffersize * size];
        }


        public static void Save<T>(this Pixel<T> dst, string path, int buffersize = 0) where T : struct
        {
            using (var fs = File.Open(path, FileMode.Create))
            {
                _GetBufferSize(dst, ref buffersize, out int size, out byte[] buffer);

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


        public static void Load<T>(this Pixel<T> dst, string path, int buffersize = 0, int offset = 0) where T : struct
        {
            using (var fs = File.Open(path, FileMode.Open))
            {
                FileInfo.CheckFileType(path, fs, typeof(T), dst);
                fs.Seek(offset, SeekOrigin.Current);

                _GetBufferSize(dst, ref buffersize, out int size, out byte[] buffer);
                buffersize = buffersize > 0 ? buffersize : dst.Width * dst.Height;


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


        public static void Load<T, TT>(this Pixel<TT> dst, string path, Func<T, TT> func, int buffersize = 0, int offset = 0) where T : struct where TT : struct
        {
            using (var fs = File.Open(path, FileMode.Open))
            {
                FileInfo.CheckFileType(path, fs, typeof(T), dst);
                fs.Seek(offset, SeekOrigin.Current);

                _GetBufferSize(dst, ref buffersize, out int size, out byte[] buffer);

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
                    FileInfo.CheckFileType(path, fs, typeof(T), dst);
                    fs.Seek(offset, SeekOrigin.Current);

                    _GetBufferSize(dst, ref buffersize, out int size, out byte[] buffer);

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



    }
}
