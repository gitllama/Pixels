using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Pixels.Standard.IO
{
    static class IO_Test
    {
        //public static void Load<T, U>(this T[] dst, string path, FileType type = FileType.None)
        //{
        //void IsInt32(int[] src, Stream fs)
        //{
        //    switch (type)
        //    {
        //        case FileType.Int16: FileStream.Load.ToInt16(fs, src); break;
        //        case FileType.Int24: src.LoadInt24(fs); break;
        //        case FileType.Int32: src.LoadInt32(fs); break;
        //        case FileType.Int64: src.LoadInt64(fs); break;
        //        case FileType.UInt16: src.LoadInt16(fs); break;
        //        case FileType.UInt24: src.LoadInt24(fs); break;
        //        case FileType.UInt32: src.LoadInt32(fs); break;
        //        case FileType.UInt64: src.LoadInt64(fs); break;
        //        case FileType.Single: src.LoadSingle(fs); break;
        //        case FileType.Double: src.LoadDouble(fs); break;
        //        case FileType.Int16E: src.LoadInt16E(fs); break;
        //        case FileType.Int24E: src.LoadInt24E(fs); break;
        //        case FileType.Int32E: src.LoadInt32E(fs); break;
        //        case FileType.Int64E: src.LoadInt64E(fs); break;
        //        case FileType.UInt16E: src.LoadInt16E(fs); break;
        //        case FileType.UInt24E: src.LoadInt24E(fs); break;
        //        case FileType.UInt32E: src.LoadInt32E(fs); break;
        //        case FileType.UInt64E: src.LoadInt64E(fs); break;
        //        case FileType.SingleE: src.LoadSingleE(fs); break;
        //        case FileType.DoubleE: src.LoadDoubleE(fs); break;
        //        default: throw new AggregateException();
        //    }
        //}
        //void IsSingle(float[] src, Stream fs)
        //{
        //    switch (type)
        //    {
        //        case FileType.Int16: FileStream.Load.Int16(fs, ); break;
        //        case FileType.Int24: FileStream.Load.Int24(fs); break;
        //        case FileType.Int32: FileStream.Load.Int32(fs); break;
        //        case FileType.Int64: FileStream.Load.Int64(fs); break;
        //        case FileType.UInt16: FileStream.Load.Int16(fs); break;
        //        case FileType.UInt24: FileStream.Load.Int24(fs); break;
        //        case FileType.UInt32: FileStream.Load.Int32(fs); break;
        //        case FileType.UInt64: FileStream.Load.Int64(fs); break;
        //        case FileType.Single: FileStream.Load.Single(fs); break;
        //        case FileType.Double: FileStream.Load.Double(fs); break;
        //        case FileType.Int16E: FileStream.Load.Int16E(fs); break;
        //        case FileType.Int24E: FileStream.Load.Int24E(fs); break;
        //        case FileType.Int32E: FileStream.Load.Int32E(fs); break;
        //        case FileType.Int64E: FileStream.Load.Int64E(fs); break;
        //        case FileType.UInt16E: FileStream.Load.Int16E(fs); break;
        //        case FileType.UInt24E: FileStream.Load.Int24E(fs); break;
        //        case FileType.UInt32E: FileStream.Load.Int32E(fs); break;
        //        case FileType.UInt64E: FileStream.Load.Int64E(fs); break;
        //        case FileType.SingleE: FileStream.Load.SingleE(fs); break;
        //        case FileType.DoubleE: FileStream.Load.DoubleE(fs); break;
        //        default: throw new AggregateException();
        //    }
        //}
        //void IsDouble(double[] src, Stream fs)
        //{
        //    switch (type)
        //    {
        //        case FileType.Int16: src.LoadInt16(fs); break;
        //        case FileType.Int24: src.LoadInt24(fs); break;
        //        case FileType.Int32: src.LoadInt32(fs); break;
        //        case FileType.Int64: src.LoadInt64(fs); break;
        //        case FileType.UInt16: src.LoadInt16(fs); break;
        //        case FileType.UInt24: src.LoadInt24(fs); break;
        //        case FileType.UInt32: src.LoadInt32(fs); break;
        //        case FileType.UInt64: src.LoadInt64(fs); break;
        //        case FileType.Single: src.LoadSingle(fs); break;
        //        case FileType.Double: src.LoadDouble(fs); break;
        //        case FileType.Int16E: src.LoadInt16E(fs); break;
        //        case FileType.Int24E: src.LoadInt24E(fs); break;
        //        case FileType.Int32E: src.LoadInt32E(fs); break;
        //        case FileType.Int64E: src.LoadInt64E(fs); break;
        //        case FileType.UInt16E: src.LoadInt16E(fs); break;
        //        case FileType.UInt24E: src.LoadInt24E(fs); break;
        //        case FileType.UInt32E: src.LoadInt32E(fs); break;
        //        case FileType.UInt64E: src.LoadInt64E(fs); break;
        //        case FileType.SingleE: src.LoadSingleE(fs); break;
        //        case FileType.DoubleE: src.LoadDoubleE(fs); break;
        //        default: throw new AggregateException();
        //    }
        //}

        //using (var fs = File.Open(path, FileMode.Open))
        //{
        //switch (dst)
        //{
        //    case Byte[] p: FileStream.Load.ToByte(fs, p); break;
        //    case Int16[] p: FileStream.Load.ToInt16(fs, p); break;
        //    case Int24[] p: FileStream.Load.ToInt24(fs, p); break;
        //    //case Int32[] p: IsInt32(p, fs); break;
        //    case Int64[] p: FileStream.Load.ToInt64(fs, p); break;
        //    case UInt16[] p: FileStream.Load.ToUInt16(fs, p); break;
        //    case UInt24[] p: FileStream.Load.ToUInt24(fs, p); break;
        //    case UInt32[] p: FileStream.Load.ToUInt32(fs, p); break;
        //    case UInt64[] p: FileStream.Load.ToUInt64(fs, p); break;
        //    //case Single[] p: IsSingle(p, fs); break;
        //    //case Double[] p: IsDouble(p, fs); break;
        //    case Int16E[] p: FileStream.Load.ToInt16E(fs, p); break;
        //    case Int24E[] p: FileStream.Load.ToInt24E(fs, p); break;
        //    case Int32E[] p: FileStream.Load.ToInt32E(fs, p); break;
        //    case Int64E[] p: FileStream.Load.ToInt64E(fs, p); break;
        //    case UInt16E[] p: FileStream.Load.ToUInt16E(fs, p); break;
        //    case UInt24E[] p: FileStream.Load.ToUInt24E(fs, p); break;
        //    case UInt32E[] p: FileStream.Load.ToUInt32E(fs, p); break;
        //    case UInt64E[] p: FileStream.Load.ToUInt64E(fs, p); break;
        //    case SingleE[] p: FileStream.Load.ToSingleE(fs, p); break;
        //    case DoubleE[] p: FileStream.Load.ToDoubleE(fs, p); break;
        //    default: throw new AggregateException();
        //}

        //    }


        //}


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






        private static void _ConvertToInt32(Stream stream, FileType filetype, int buffersize, int size, Pixel<int> dst, Action<int, int, int, Pixel<int>> action)
        {
            var buffer = new byte[buffersize * size];

            int index = 0;
            var rest = (int)(stream.Length - stream.Position);


            switch (filetype)
            {
                case FileType.Int32:
                    while (rest >= buffer.Length)
                    {
                        rest -= stream.Read(buffer, 0, buffer.Length);
                        for (int i = 0; i < buffer.Length; i += size)
                        {
                            int val = 0;
                            val = buffer[i + 3];
                            val <<= 8;
                            val |= buffer[i + 2];
                            val <<= 8;
                            val |= buffer[i + 1];
                            val <<= 8;
                            val |= buffer[i + 0];
                            action(index % dst.Width, index / dst.Width, val, dst);
                            index++;
                        }
                    }
                    break;
                default:
                    throw new ArgumentException();
            }
        }

    }
}

//ref int val =  dst.pix[index++]思ったより速度でない

/*
private static void Convert<T, TT>(FileStream stream, FileType filetype, int buffersize, int size, int[] dst)
where T : struct
where TT : struct
{
        while (rest >= buffer.Length)
        {
            rest -= stream.Read(buffer, 0, buffer.Length);
            for (int i = 0; i < buffer.Length; i += size)
            {
                dst[i] = buffer[i];
            }
        }
}
*/

// Span<byte> buffer = stackalloc byte[BufferSize]; //Core 2.1
// rest -= stream.Read(buffer); //Core 2.1