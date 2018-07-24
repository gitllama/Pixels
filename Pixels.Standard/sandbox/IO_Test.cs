using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Pixels.Standard.sandbox
{
    public static class IO_Test
    {

        public static unsafe void B(Stream stream, int[] dst, int buffersize)
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
                    while (index < last)
                    {
                        dst[index++] = *p++;
                    }
                }
            }
        }

        public static unsafe void A(Stream stream, int[] dst, int buffersize)
        {
            int size = Marshal.SizeOf(typeof(Int32));
            var buffer = new byte[buffersize * size];

            var handle = GCHandle.Alloc(dst, GCHandleType.Pinned);
            var pin = handle.AddrOfPinnedObject();

            var rest = (int)(stream.Length - stream.Position);

            while (rest >= buffer.Length)
            {
                rest -= stream.Read(buffer, 0, buffer.Length);

                Marshal.Copy(buffer, 0, pin, buffer.Length);

                IntPtr.Add(pin, buffer.Length);
            }
            handle.Free();
        }

    }
}
