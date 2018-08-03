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

        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public int Size { get => Width * Height; }

        public ref T this[int x, int y] => ref pix[x + y * Width];

        public ref T this[int index] => ref pix[index];


        public int GetIndex(int x, int y) => x + y * Width;

        public (int x, int y) GetPoint(int index) => (index % Width, index / Width);


        #region Constructor

        protected Pixel()
        {
            this.Bayer = this.Bayer ??  new Dictionary<string, (int x, int y, int width, int height)>();
            this.SubPlane = this.SubPlane ?? new Dictionary<string, (int left, int top, int width, int height)>();
        }

        public Pixel(int width, int height) : this()
        {
            Width = width;
            Height = height;
            pix = new T[width * height];
        }

        #endregion


        #region Planes / Bayer

        public Dictionary<string, (int left, int top, int width, int height)> SubPlane;

        public (int left, int top, int width, int height) GetPlane(string name)
        {
            name = name ?? "";
            return SubPlane.ContainsKey(name) ? SubPlane[name] : (0, 0, Width, Height);
        }


        public Dictionary<string, (int x, int y, int width, int height)> Bayer;

        public (int x, int y, int width, int height) GetBayer(string name)
        {
            name = name ?? "";
            return Bayer.ContainsKey(name) ? Bayer[name] : (0, 0, 1, 1);
        }


        protected internal (int left, int top, int width, int height, int incX, int incY) LoopState(string plane, string bayer)
        {
            var _plane = GetPlane(plane);
            var _bayer = GetBayer(bayer);

            return (
                _plane.left + _bayer.x,
                _plane.top + _bayer.y,
                _plane.width - _bayer.x,
                _plane.height - _bayer.y,
                _bayer.width,
                _bayer.height
            );
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
            SubPlane = new Dictionary<string, (int left, int top, int width, int height)>();
        }


        #region Planes

        public Dictionary<string, (int left, int top, int width, int height)> SubPlane;

        public (int left, int top, int width, int height) GetPlane(string name)
        {
            return SubPlane.ContainsKey(name) ? SubPlane[name] : (0, 0, Width, Height);
        }

        #endregion



    }


    public class PixelByte<T> : Pixel<byte> where T : struct
    {

        public int Bytesize { get; private set; }

        public new ref T this[int x, int y] => ref Unsafe.As<byte, T>(ref pix[(x + y * Width) * Bytesize]);

        public new ref T this[int index] => ref Unsafe.As<byte, T>(ref pix[index * Bytesize]);


        public PixelByte(int width, int height) : base()
        {
            Width = width;
            Height = height;
            Bytesize  = Marshal.SizeOf(typeof(T));
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

        public IntPtr AddrOfPinnedObject()
        {
            return handle.AddrOfPinnedObject();
        }

        public unsafe void* ToPointer()
        {
            return (handle.AddrOfPinnedObject()).ToPointer();
        }
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