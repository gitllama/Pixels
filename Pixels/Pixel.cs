using System.Numerics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Pixels
{

    public class Pixel<T> where T : struct
    {

        protected internal T[] pix;
        public ref T GetPinnableReference() => ref pix[0];

        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public int Size { get => Width * Height; }

        public ref T this[int x, int y] => ref pix[x + y * Width];

        public ref T this[int index] => ref pix[index];


        public int GetIndex(int x, int y) => x + y * Width;

        public (int x, int y) GetPoint(int index) => (index % Width, index / Width);


        #region Constructor

        protected internal Pixel()
        {
            this.CFA = this.CFA ??  new Dictionary<string, (int x, int y, int width, int height)>();
            this.SubPlane = this.SubPlane ?? new Dictionary<string, (int left, int top, int width, int height)>();
        }

        protected internal Pixel(int width, int height) : this()
        {
            Width = width;
            Height = height;
            pix = new T[width * height];
        }

        #endregion


        #region Planes / CFA

        public Dictionary<string, (int left, int top, int width, int height)> SubPlane;
        public Dictionary<string, (int x, int y, int width, int height)> CFA;

        public (int left, int top, int width, int height) GetPlane(string name)
        {
            name = name ?? "";
            return
                name == "" ? (0, 0, Width, Height) :
                SubPlane.ContainsKey(name) ? SubPlane[name] :
                throw new KeyNotFoundException();
        }

        public (int x, int y, int width, int height) GetCFA(string name)
        {
            name = name ?? "";
            return
                name == "" ? (0, 0, 1, 1) :
                CFA.ContainsKey(name) ? CFA[name] :
                throw new KeyNotFoundException();
        }


        #endregion


        public T[] ToArray()
        {
            //        using (UnmanagedMemoryStream streamSrc = new UnmanagedMemoryStream((byte*)src, size)
            //        using (UnmanagedMemoryStream streamDst = new UnmanagedMemoryStream((byte*)dst, size)
            //{
            //    streamSrc.CopyTo(streamDst);
            //    }
            T[] dst = new T[pix.Length];
            Array.Copy(pix, dst, pix.Length);

            return dst;
        }


    }


    public class ReadOnlyPixel<T> : Pixel<T> where T : struct
    {

        public new ref readonly T this[int x, int y] => ref pix[x + y * Width];

        public new ref readonly T this[int index] => ref pix[index];

        public ReadOnlyPixel(int width, int height)
        {
            Width = width;
            Height = height;
            pix = new T[width * height];
        }

    }


    public class PixelByte<T> : Pixel<byte> where T : struct
    {

        public int Bytesize { get => Unsafe.SizeOf<T>(); }

        public new ref T this[int x, int y] => ref Unsafe.As<byte, T>(ref pix[(x + y * Width) * Bytesize]);

        public new ref T this[int index] => ref Unsafe.As<byte, T>(ref pix[index * Bytesize]);


        public PixelByte(int width, int height) : base()
        {
            Width = width;
            Height = height;
            pix = new byte[width * height * Bytesize ];
        }

        public new T[] ToArray()
        {
            ref var src = ref Unsafe.As<byte[], T[]>(ref pix);
            T[] dst = new T[src.Length];
            Array.Copy(src, dst, src.Length);
            return dst;
        }
    }


    public class PixelVector4 : Pixel<Vector4>
    {

        //public new ref T this[int x, int y] => ref Unsafe.As<byte, T>(ref pix[(x + y * Width) * Bytesize]);

        //public new ref T this[int index] => ref Unsafe.As<byte, T>(ref pix[index * Bytesize]);


        public PixelVector4(int width, int height) : base()
        {
            Width = width;
            Height = height;
            pix = new Vector4[width * height / 4];
        }

    }


    // Factory Class

    public struct PixelTemplate
    {
        public int Width;
        public int Height;
        public Dictionary<string, Planes> SubPlane;
        public Dictionary<string, CFArray> CFA;

        public Type type;
        public int offset;
        public char[] separator;

        public struct Planes
        {
            public int left;
            public int top;
            public int width;
            public int height;
        }
        public struct CFArray
        {
            public int x;
            public int y;
            public int width;
            public int height;
        }
    }


    public static class PixelBuilder
    {
        public static Pixel<T> Create<T>(int width, int height) where T : struct
        {
            return new Pixel<T>(width, height);
        }

        public static Pixel<T> Create<T>(int width, int height, T[] src) where T : struct
        {
            return new Pixel<T>(width, height)
            {
                pix = src
            };
        }


        // var hoge = new PixelTemplate
        // {
        //    Age = 10,
        //    Name = "hogehoge"
        // }.Create();
        public static Pixel<T> Create<T>(this PixelTemplate template) where T : struct
        {
            var dst = new Pixel<T>(template.Width, template.Height);

            foreach (var i in template.SubPlane ?? new Dictionary<string, PixelTemplate.Planes>())
            {
                dst.SubPlane.Add(i.Key, (i.Value.left, i.Value.top, i.Value.width, i.Value.height));
            }
            foreach (var i in template.CFA ?? new Dictionary<string, PixelTemplate.CFArray>())
            {
                dst.CFA.Add(i.Key, (i.Value.x, i.Value.y, i.Value.width, i.Value.height));
            }
            return dst;
        }

        public static Pixel<T> Create<T>(string path) where T : struct
        {
            return DDL.DDL.Deserialize<PixelTemplate>(path).Create<T>();
        }


        public static Pixel<U> Clone<T, U>(this Pixel<T> src) where T : struct where U : struct
        {
            return new Pixel<U>(src.Width, src.Height)
            {
                SubPlane = src.SubPlane,
                CFA = src.CFA
            };
        }
        public static Pixel<T> Clone<T>(this Pixel<T> src) where T : struct
        {
            return new Pixel<T>(src.Width, src.Height)
            {
                SubPlane = src.SubPlane,
                CFA = src.CFA
            };
        }
    }
}

namespace Pixels.Deprecated
{
    //where : unmanaged と fixed(T*) で代用可
    public class LockPixel<T> : IDisposable where T : struct 
    {
        GCHandle handle;

        public LockPixel(Pixel<T> src) 
        {
            handle = GCHandle.Alloc(src.pix, GCHandleType.Pinned);
        }

        public void Dispose()
        {
            handle.Free();
        }

        public IntPtr AddrOfPinnedObject() => handle.AddrOfPinnedObject();
        public unsafe void* ToPointer() => (handle.AddrOfPinnedObject()).ToPointer();
        public unsafe byte* ToBytePointer() => (byte*)((handle.AddrOfPinnedObject()).ToPointer());

    }
}
namespace Pixels.Future
{

    [Obsolete("未実装")]
    public class PixelIntPtr<T> where T : struct
    {
        public IntPtr pix;

        public readonly int Width;
        public readonly int Height;

        public Dictionary<string, (int left, int top, int width, int height)> SubPlane;


        //public T this[int x, int y]
        //{
        //    get { return pix[x + y * Width]; }
        //    set { pix[x + y * Width] = value; }
        //}

        //public T this[int index]
        //{
        //    get {
        //        IntPtr.Zero;

        //        return pix[index]; }
        //    set { pix[index] = value; }
        //}

        public int GetIndex(int x, int y) => x + y * Width;

        public (int x, int y) GetPoint(int index) => (index % Width, index / Width);

        public (int left, int top, int width, int height) GetPlane(string name)
        {
            return SubPlane.ContainsKey(name) ? SubPlane[name] : (0, 0, Width, Height);
        }

        public PixelIntPtr(int width, int height)
        {
            Width = width;
            Height = height;
            pix = Marshal.AllocHGlobal(width * height * Marshal.SizeOf(typeof(T)));
            SubPlane = new Dictionary<string, (int left, int top, int width, int height)>();
        }

        ~PixelIntPtr()
        {
            Marshal.FreeHGlobal(pix);
        }

        //    var a = new byte[w];
        //for (int y = 0; y<h; y++)
        //{
        // for (int x = 0; x<a.Length; x++)
        // {
        //  var val = x ^ y;
        //    var v = (byte)val;
        //    a[x] = v;
        // }
        //Marshal.Copy(a, 0, dst, a.Length);
        // IntPtr.Add(dst, sizeof(byte) * stride);
        //}

        [Conditional("DEBUG")]
        static void DebugMethod()
        {
        }
    }


    [Obsolete("未実装")]
    public unsafe class PixelPointer<T> where T : unmanaged 
    {
        public T* pix;

        public readonly int Width;
        public readonly int Height;

        public ref T this[int index] => ref pix[index];

        public Dictionary<string, (int left, int top, int width, int height)> SubPlane;


        ref readonly int M(in int x)
        {
            ref readonly var r = ref x;
            return ref r;
        }
    }

}