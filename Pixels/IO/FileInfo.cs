using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Pixels.IO
{
    public static class FileInfo
    {
        public static void CheckFileType<T>(string path, Stream stream, Type inputtype, Pixel<T> dst) where T : struct
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

        public static unsafe void CheckTRW<T>(Stream stream, Type inputtype, Pixel<T> src) where T : struct
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
