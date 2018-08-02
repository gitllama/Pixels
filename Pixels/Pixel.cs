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

        public T[] pix;

        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public ref T this[int x, int y] => ref pix[x + y * Width];

        public ref T this[int index] => ref pix[index];


        public int GetIndex(int x, int y) => x + y * Width;


        public (int x, int y) GetPoint(int index) => (index % Width, index / Width);


        #region Constructor

        protected Pixel()
        {

        }

        public Pixel(int width, int height)
        {
            Width = width;
            Height = height;
            pix = new T[width * height];
            SubPlane = new Dictionary<string, (int left, int top, int width, int height)>();
        }

        #endregion


        #region Planes

        public Dictionary<string, (int left, int top, int width, int height)> SubPlane;

        public (int left, int top, int width, int height) GetPlane(string name)
        {
            name = name ?? "";
            return SubPlane.ContainsKey(name) ? SubPlane[name] : (0, 0, Width, Height);
        }

        #endregion

    }


    [Obsolete("未実装")]
    public class ReadOnlyPixel<T> where T : struct
    {

        private T[] pix;

        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public ref readonly T this[int x, int y] => ref pix[x + y * Width];

        public ref readonly T this[int index] => ref pix[index];


        public int GetIndex(int x, int y) => x + y * Width;

        public (int x, int y) GetPoint(int index) => (index % Width, index / Width);


        #region Constructor

        protected ReadOnlyPixel()
        {

        }

        public ReadOnlyPixel(int width, int height)
        {
            Width = width;
            Height = height;
            pix = new T[width * height];
            SubPlane = new Dictionary<string, (int left, int top, int width, int height)>();
        }

        #endregion


        #region Planes

        public Dictionary<string, (int left, int top, int width, int height)> SubPlane;

        public (int left, int top, int width, int height) GetPlane(string name)
        {
            return SubPlane.ContainsKey(name) ? SubPlane[name] : (0, 0, Width, Height);
        }

        #endregion

    }


    [Obsolete("未実装")]
    public class PixelByte<T> : Pixel<byte> where T : struct 
    {

        private int size = 0;

        public new ref T this[int x, int y] => ref Unsafe.As<byte, T>(ref pix[(x + y * Width)* size]);


        public new ref T this[int index] => ref Unsafe.As<byte, T>(ref pix[index * size]);


        public PixelByte(int width, int height) : base()
        {
            Width = width;
            Height = height;
            size = Marshal.SizeOf(typeof(T));
            pix = new byte[width * height * size];

            SubPlane = new Dictionary<string, (int left, int top, int width, int height)>();

        }

        [Conditional("DEBUG")]
        static void DebugMethod()
        {
        }
    }


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
