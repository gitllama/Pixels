using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Pixels.IO;

namespace Pixels.IO.Deprecated
{
    public static class FileStreamExtented
    {

        [System.ObsoleteAttribute("use Load<T>")]
        public static void LoadMarshal<T>(this Pixel<T> dst, string path, int buffersize = 0) where T : struct
        {
            using (var fs = File.Open(path, FileMode.Open))
            {
                FileInfo.CheckFileType(path, fs, typeof(T), dst);
                buffersize = buffersize > 0 ? buffersize : dst.Width * dst.Height;

                LoadMarshal<T>(fs, dst.pix, buffersize);
            }
        }

        [System.ObsoleteAttribute("use Load<T>")]
        private static unsafe void LoadMarshal<T>(Stream stream, T[] dst, int buffersize)
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

    }
}
