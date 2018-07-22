using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Pixels.IO
{
    // LoadMarshal<T> : 433
    // Load<Int24> : 16

    public static partial class FileStream
    {

        public enum FileType
        {
            None,
            Byte,
            Int16,
            Int24,
            Int32,
            Int64,
            UInt16,
            UInt24,
            UInt32,
            UInt64,
            Single,
            Double,
            Int16E,
            Int24E,
            Int32E,
            Int64E,
            UInt16E,
            UInt24E,
            UInt32E,
            UInt64E,
            SingleE,
            DoubleE
        }

        public struct Option
        {
            int startindex;
            int buffersize;

        }


        public unsafe static void LoadMarshal<T>(this T[] dst, string path)
        {
            using (var fs = File.Open(path, FileMode.Open))
            {
                int size = Marshal.SizeOf(typeof(T));
                byte[] buffer = new byte[dst.Length * size];
                fs.Read(buffer, 0, dst.Length * size);
                var gch = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                try
                {
                    var p = gch.AddrOfPinnedObject();
                    for (var i = 0; i < dst.Length; i++)
                    {
                        dst[i] = (T)Marshal.PtrToStructure(p, typeof(T));
                        p += size;
                    }
                }
                finally
                {
                    gch.Free();
                }
            }
        }


        public unsafe static void Load<T>(this T[] dst, string path, FileType type = FileType.None)
        {
            void IsInt32(int[] src, Stream fs)
            {
                switch (type)
                {
                    case FileType.Int16: src.LoadInt16(fs); break;
                    case FileType.Int24: src.LoadInt24(fs); break;
                    case FileType.Int32: src.LoadInt32(fs); break;
                    case FileType.Int64: src.LoadInt64(fs); break;
                    case FileType.UInt16: src.LoadInt16(fs); break;
                    case FileType.UInt24: src.LoadInt24(fs); break;
                    case FileType.UInt32: src.LoadInt32(fs); break;
                    case FileType.UInt64: src.LoadInt64(fs); break;
                    case FileType.Single: src.LoadSingle(fs); break;
                    case FileType.Double: src.LoadDouble(fs); break;
                    case FileType.Int16E: src.LoadInt16E(fs); break;
                    case FileType.Int24E: src.LoadInt24E(fs); break;
                    case FileType.Int32E: src.LoadInt32E(fs); break;
                    case FileType.Int64E: src.LoadInt64E(fs); break;
                    case FileType.UInt16E: src.LoadInt16E(fs); break;
                    case FileType.UInt24E: src.LoadInt24E(fs); break;
                    case FileType.UInt32E: src.LoadInt32E(fs); break;
                    case FileType.UInt64E: src.LoadInt64E(fs); break;
                    case FileType.SingleE: src.LoadSingleE(fs); break;
                    case FileType.DoubleE: src.LoadDoubleE(fs); break;
                    default: throw new AggregateException();
                }
            }
            void IsSingle(float[] src, Stream fs)
            {
                switch (type)
                {
                    case FileType.Int16: src.LoadInt16(fs); break;
                    case FileType.Int24: src.LoadInt24(fs); break;
                    case FileType.Int32: src.LoadInt32(fs); break;
                    case FileType.Int64: src.LoadInt64(fs); break;
                    case FileType.UInt16: src.LoadInt16(fs); break;
                    case FileType.UInt24: src.LoadInt24(fs); break;
                    case FileType.UInt32: src.LoadInt32(fs); break;
                    case FileType.UInt64: src.LoadInt64(fs); break;
                    case FileType.Single: src.LoadSingle(fs); break;
                    case FileType.Double: src.LoadDouble(fs); break;
                    case FileType.Int16E: src.LoadInt16E(fs); break;
                    case FileType.Int24E: src.LoadInt24E(fs); break;
                    case FileType.Int32E: src.LoadInt32E(fs); break;
                    case FileType.Int64E: src.LoadInt64E(fs); break;
                    case FileType.UInt16E: src.LoadInt16E(fs); break;
                    case FileType.UInt24E: src.LoadInt24E(fs); break;
                    case FileType.UInt32E: src.LoadInt32E(fs); break;
                    case FileType.UInt64E: src.LoadInt64E(fs); break;
                    case FileType.SingleE: src.LoadSingleE(fs); break;
                    case FileType.DoubleE: src.LoadDoubleE(fs); break;
                    default: throw new AggregateException();
                }
            }
            void IsDouble(double[] src, Stream fs)
            {
                switch (type)
                {
                    case FileType.Int16: src.LoadInt16(fs); break;
                    case FileType.Int24: src.LoadInt24(fs); break;
                    case FileType.Int32: src.LoadInt32(fs); break;
                    case FileType.Int64: src.LoadInt64(fs); break;
                    case FileType.UInt16: src.LoadInt16(fs); break;
                    case FileType.UInt24: src.LoadInt24(fs); break;
                    case FileType.UInt32: src.LoadInt32(fs); break;
                    case FileType.UInt64: src.LoadInt64(fs); break;
                    case FileType.Single: src.LoadSingle(fs); break;
                    case FileType.Double: src.LoadDouble(fs); break;
                    case FileType.Int16E: src.LoadInt16E(fs); break;
                    case FileType.Int24E: src.LoadInt24E(fs); break;
                    case FileType.Int32E: src.LoadInt32E(fs); break;
                    case FileType.Int64E: src.LoadInt64E(fs); break;
                    case FileType.UInt16E: src.LoadInt16E(fs); break;
                    case FileType.UInt24E: src.LoadInt24E(fs); break;
                    case FileType.UInt32E: src.LoadInt32E(fs); break;
                    case FileType.UInt64E: src.LoadInt64E(fs); break;
                    case FileType.SingleE: src.LoadSingleE(fs); break;
                    case FileType.DoubleE: src.LoadDoubleE(fs); break;
                    default: throw new AggregateException();
                }
            }

            using (var fs = File.Open(path, FileMode.Open))
            {
                switch (dst)
                {
                    case Byte[] p: p.LoadByte(fs); break;
                    case Int16[] p: p.LoadInt16(fs); break;
                    case Int24[] p: p.LoadInt24(fs); break;
                    case Int32[] p: IsInt32(p, fs); break;
                    case Int64[] p: p.LoadInt64(fs); break;
                    case UInt16[] p: p.LoadUInt16(fs); break;
                    case UInt24[] p: p.LoadUInt24(fs); break;
                    case UInt32[] p: p.LoadUInt32(fs); break;
                    case UInt64[] p: p.LoadUInt64(fs); break;
                    case Single[] p: IsSingle(p, fs); break;
                    case Double[] p: IsDouble(p, fs); break;
                    case Int16E[] p: p.LoadInt16E(fs); break;
                    case Int24E[] p: p.LoadInt24E(fs); break;
                    case Int32E[] p: p.LoadInt32E(fs); break;
                    case Int64E[] p: p.LoadInt64E(fs); break;
                    case UInt16E[] p: p.LoadUInt16E(fs); break;
                    case UInt24E[] p: p.LoadUInt24E(fs); break;
                    case UInt32E[] p: p.LoadUInt32E(fs); break;
                    case UInt64E[] p: p.LoadUInt64E(fs); break;
                    case SingleE[] p: p.LoadSingleE(fs); break;
                    case DoubleE[] p: p.LoadDoubleE(fs); break;
                    default: throw new AggregateException();
                }

            }


        }


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


    }
}
