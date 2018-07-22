using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Runtime.InteropServices;

//RyuJITは64bitのみ
using System.Numerics;
using System.IO;
using Pixels.IO;
using OpenCvSharp;
using Pixels;

namespace DebugConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            var path = @"C:\Users\Aki\Documents\Doc\Program\BIATs\新しいフォルダー\testpixs\000.bin";
            var sw = new System.Diagnostics.Stopwatch();
            void time(Action action)
            {
                sw.Restart();
                action();
                sw.Stop();
                Console.WriteLine($"　{sw.ElapsedMilliseconds}ms");
            }
            
            int w = 1408;
            int h = 1032;
            var dst32 = new Int32[w * h];
            var dst = new Int24[w * h];
            byte[] mat = new byte[w * h];


            time(() =>
            {
                
                for (var n = 0; n < 100; n++)
                {
                    dst.Load(path);
                    //Load_(dst32, path); //1867
                    //Load_2(dst, path); //おそい  
                }
                //4541
                for (int i = 0; i < mat.Length; i++)
                {
                        var hoge = dst[i] >> 9;
                        mat[i] = (byte)(hoge > 255 ? 255 : hoge < 0 ? 0 : hoge);
                }
            });

            //using (var m = new Mat(h, w, MatType.CV_8UC1, mat))
            //{
            //    Cv2.ImShow("result", m);
            //    Cv2.WaitKey(0);
            //}

            //time(() =>
            //{
            //    //7503                          
            //    for (var n = 0; n < 1; n++)
            //    {
            //        dst.Load(path);
            //    }
            //    //6325
            //    for (int i = 0; i < mat.Length; i++)
            //    {
            //        var hoge = dst[i] >> 9;
            //        mat[i] = (byte)(hoge > 255 ? 255 : hoge < 0 ? 0 : hoge);
            //    }
            //});

            using (var m = new Mat(h, w, MatType.CV_8UC1, mat))
            {
                Cv2.ImShow("result", m);
                Cv2.WaitKey(0);
            }


            //10672
            //sw.Restart();
            //for (var n = 0; n < 1; n++)
            //{
            //    LoadInt24(dst2);//11
            //    //Load<Int24>(dst2);//453
            //}

            //sw.Stop();
            //Console.WriteLine($"　{sw.ElapsedMilliseconds}ms");


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



            //byte[] a = new byte[] { 1, 0, 0, 0 };
            //Foo(a);
            //Foo2();

            //Load();
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


        static void Foo<T>(T[] bytes) where T : struct
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            IntPtr pbytes = Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0);

            unsafe
            {
                var pt = (int*)pbytes.ToPointer();
                Console.WriteLine(*pt);
                //pt++;

            }
            handle.Free();
        }

        private unsafe static void Foo2()
        {
            Vector3[] a = new Vector3[3];
            a[0] = new Vector3(1, 2, 3);
            a[1] = new Vector3(4, 5, 6);
            a[2] = new Vector3(7, 8, 9);

            GCHandle handle = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr pbytes = Marshal.UnsafeAddrOfPinnedArrayElement(a, 0);

            var pt = (float*)pbytes.ToPointer();
            for (var i=0;i < a.Length * 3;i++)
            {
                Console.WriteLine(*pt++);
            }


            handle.Free();
        }



        public static void Load()
        {
            using (var fs = File.Open(@"C:\Users\Aki\Documents\Doc\Program\BIATs\新しいフォルダー\testpixs\000.bin", FileMode.Open))
            {
                int length = 10;
                var size = Marshal.SizeOf(typeof(Int24));

                byte[] buffer = new byte[size * length];
                fs.Read(buffer, 0, size * length);

                var gch = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                //Int24[] hoge = (Int24[])Marshal.PtrToStructure(gch.AddrOfPinnedObject(), typeof(Int24[]));

                Int24 hoge = (Int24)Marshal.PtrToStructure(gch.AddrOfPinnedObject(), typeof(Int24));
                int hogehoge = hoge;


                gch.Free();
            }

            //using (BinaryReader br = new BinaryReader(File.Open("", FileMode.Open)))
            //{
            //    br.ReadBytes(10);
            //}
        }



 

    }



    unsafe class UnsafePixel<T> where T : struct
    {
        byte[] pix;
        GCHandle handle;
        IntPtr pbytes;

        public UnsafePixel(int w, int h)
        {
            pix = new byte[w * h * 4];
        }

        public int GetInt32(int index)
        {
            var p = (int*)((pbytes + index).ToPointer());
            return *(p + index);
        }

        public void Lock()
        {
            handle = GCHandle.Alloc(pix, GCHandleType.Pinned);

            IntPtr pbytes = Marshal.UnsafeAddrOfPinnedArrayElement(pix, 0);
        }

        public void UnLock()
        {
            handle.Free();
        }
    }

}
