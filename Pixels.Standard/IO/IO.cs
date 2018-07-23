using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Pixels.Standard.IO
{
    //強制 int double float 実装

    public static class FileStreamExtented
    {
        //, FileType type = FileType.Noneを使用した読み込み
        #region Load Reflection 



        public static void Load<T, TT>(this Pixel<TT> dst, string path, Func<T, TT> func, int buffersize = 0, int offset = 0) where T : struct where TT : struct
        {
            using (var fs = File.Open(path, FileMode.Open))
            {
                CheckFileType(path, fs, typeof(T), dst);
                buffersize = buffersize > 0 ? buffersize : dst.Width * dst.Height;

                fs.Position += offset;

                var methodinfo = typeof(FileStream.Load).GetMethod(
                    $"From{typeof(T).Name}To"
                    , BindingFlags.Public | BindingFlags.Static);
                var constructed = methodinfo.MakeGenericMethod(typeof(TT));
                constructed.Invoke(null, new object[] { fs, dst.pix, func, buffersize });
            }
        }

        public static void Load<T>(this Pixel<T> dst, string path, Type filetype = null, int buffersize = 0, int offset = 0) where T : struct
        {
            using (var fs = File.Open(path, FileMode.Open))
            {
                CheckFileType(path, fs, typeof(T), dst);
                buffersize = buffersize > 0 ? buffersize : dst.Width * dst.Height;

                fs.Position += offset;

                filetype = filetype ?? typeof(T);
                Type callType = filetype == typeof(T) ? typeof(FileStream.Load)
                              : typeof(T) == typeof(Int32) ? typeof(FileStream.LoadInt32)
                              : typeof(T) == typeof(Single) ? typeof(FileStream.LoadSingle)
                              : typeof(T) == typeof(Double) ? typeof(FileStream.LoadDouble)
                              : throw new Exception();

                var methodinfo = callType.GetMethod(
                    $"From{typeof(T).Name}"
                    , BindingFlags.Public | BindingFlags.Static
                    , null,
                    new Type[] { typeof(Stream), typeof(T[]), typeof(int) }
                    , null);
                methodinfo.Invoke(null, new object[] { fs, dst.pix, buffersize });
            }
            //typeof(int).MakeByRefType()
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

}
