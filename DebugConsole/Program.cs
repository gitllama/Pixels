using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;

//RyuJITは64bitのみ
using System.Numerics;
using System.IO;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using Pixels.Standard;
using Pixels.Framework;

namespace DebugConsole
{
    class Program
    {
        unsafe static void Main(string[] args)
        {
            byte[] a = new byte[8000 * 4000];
            byte[] b = new byte[8000 * 4000];

            Timer(() =>
            {
                for (int y = 0; y < 4000; y++)
                {
                    for (int x = 0; x < 8000; x++)
                    {
                        a[x + y * 8000] = b[x + y * 8000];
                    }
                }
            });

            Timer(() =>
            {
                for (int y = 0; y < 4000; y++)
                {
                    var span_A = a.AsSpan().Slice(y * 8000, 8000);
                    var span_B = b.AsSpan().Slice(y * 8000, 8000);
                    for (int x = 0; x < span_A.Length; x++)
                    {
                        span_A[x] = span_B[x];
                    }
                }
            });

            Timer(() =>
            {
                var gcha = GCHandle.Alloc(a, GCHandleType.Pinned);
                var gchb = GCHandle.Alloc(b, GCHandleType.Pinned);
                var pina = gcha.AddrOfPinnedObject();
                var pinb = gchb.AddrOfPinnedObject();

                for (int y = 0; y < 4000; y++)
                {
                    for (int x = 0; x < 8000; x++)
                    {
                        pina = (IntPtr)Marshal.ReadByte(pinb);
                        pina += sizeof(byte);
                        pinb += sizeof(byte);
                    }
                }

                gcha.Free();
                gchb.Free();
            });

            Timer(() =>
            {
                var gcha = GCHandle.Alloc(a, GCHandleType.Pinned);
                var gchb = GCHandle.Alloc(b, GCHandleType.Pinned);
                var pina = gcha.AddrOfPinnedObject();
                var pinb = gchb.AddrOfPinnedObject();

                //byte* pa = (byte*)pina;
                byte[] buf = new byte[8000];
                for (int y = 0; y < 4000; y++)
                {
                    for (int x = 0; x < buf.Length; x++)
                    {
                        buf[x] = Marshal.ReadByte(pinb);
                        pinb += sizeof(byte);
                    }
                    Marshal.Copy(buf, 0, pina, buf.Length);
                    IntPtr.Add(pina, sizeof(byte) * buf.Length);
                }

                gcha.Free();
                gchb.Free();
            });

            Timer(() =>
            {
                fixed (byte* pina = a)
                fixed (byte* pinb = b)
                {
                    for (int y = 0; y < 4000; y++)
                    {
                        for (int x = 0; x < 8000; x++)
                        {
                            pina[x + y * 8000] = pinb[x + y * 8000];
                        }
                    }
                }
            });

            var path = @"C:\Users\Aki\Documents\Doc\Program\BIATs\新しいフォルダー\testpixs\000.bin";
            var configpath = @"default.yaml";


            var p = new Pixels.Framework.Processing();

            p.Init(configpath);

            p.preProcess.Run(path);
            p.mainProcess.Run(null);
            p.postProcess.Run(true,0);


            using (var stream = new FileStream("temp.bmp", FileMode.Create, FileAccess.Write))
            {
                var encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(p.img));
                encoder.Save(stream);
                using (var m = new Mat("temp.bmp"))
                {
                    Cv2.ImShow("result", m);
                    Cv2.WaitKey(0);
                }
            }
        }

        static void Timer(Action action)
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Restart();
            action();
            sw.Stop();
            Console.WriteLine($"　{sw.ElapsedMilliseconds}ms");
        }




        static void Load_(int[] dst,string path)
        {
            using (var fs = File.Open(path, FileMode.Open))
            {
                int size = dst.Length * 3;
                byte[] buffer = new byte[size];
                fs.Read(buffer, 0, size);
                
                for(var i = 0; i < dst.Length; i++)
                {
                    dst[i] = (int)Int24.FromByte(buffer, i * 3);
                    //dst[i] = BitConverterEx.ToInt24(buffer, i * 3);
                    //dst[i] = (buffer[i * 3 + 2] << 24 | (buffer[i * 3 + 1] << 16) | (buffer[i * 3 + 0] << 8)) >> 8;
                }
            }
        }


        //static void Foo<T>(T[] bytes) where T : struct
        //{
        //    GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
        //    IntPtr pbytes = Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0);

        //    unsafe
        //    {
        //        var pt = (int*)pbytes.ToPointer();
        //        Console.WriteLine(*pt);
        //        //pt++;

        //    }
        //    handle.Free();
        //}

        private static void Foo2()
        {
            //Vector3[] a = new Vector3[3];
            //a[0] = new Vector3(1, 2, 3);
            //a[1] = new Vector3(4, 5, 6);
            //a[2] = new Vector3(7, 8, 9);

            //GCHandle handle = GCHandle.Alloc(a, GCHandleType.Pinned);
            //IntPtr pbytes = Marshal.UnsafeAddrOfPinnedArrayElement(a, 0);

            //var pt = (float*)pbytes.ToPointer();
            //for (var i=0;i < a.Length * 3;i++)
            //{
            //    Console.WriteLine(*pt++);
            //}


            //handle.Free();
        }




 

    }



    //unsafe class UnsafePixel<T> where T : struct
    //{
    //    byte[] pix;
    //    GCHandle handle;
    //    IntPtr pbytes;

    //    public UnsafePixel(int w, int h)
    //    {
    //        pix = new byte[w * h * 4];
    //    }

    //    public int GetInt32(int index)
    //    {
    //        var p = (int*)((pbytes + index).ToPointer());
    //        return *(p + index);
    //    }

    //    public void Lock()
    //    {
    //        handle = GCHandle.Alloc(pix, GCHandleType.Pinned);

    //        IntPtr pbytes = Marshal.UnsafeAddrOfPinnedArrayElement(pix, 0);
    //    }

    //    public void UnLock()
    //    {
    //        handle.Free();
    //    }
    //}

}


//int w = 1408;
//int h = 1032;
//var dst32 = new Int32[w * h];
//var dst = new Int24[w * h];
//byte[] mat = new byte[w * h];


//time(() =>
//{

//    for (var n = 0; n < 1; n++)
//    {
//        dst.Load(path);
//        //Load_(dst32, path); //1867
//        //Load_2(dst, path); //おそい  
//    }
//    //4541
//    for (int i = 0; i < mat.Length; i++)
//    {
//            var hoge = dst[i] >> 9;
//            mat[i] = (byte)(hoge > 255 ? 255 : hoge < 0 ? 0 : hoge);
//    }
//});




//var p = new Pixel<byte>(200, 200);
//p.ForEach((i) => i = 100);

//var bmp = new WriteableBitmap(640, 480, 96, 96, PixelFormats.Bgr32, null);
//bmp.Lock();
//unsafe
//{
//    fixed(byte* pin = p.pix)
//        PixelExtention.ConvertTo(pin, (byte*)bmp.BackBuffer, bmp.PixelWidth, bmp.PixelHeight, bmp.BackBufferStride);

//}
//bmp.Unlock();



