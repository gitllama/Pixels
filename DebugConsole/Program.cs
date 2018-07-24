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
using Pixels.Standard.IO;
using Pixels.Framework;
using System.Windows.Media;
using System.Windows;

namespace DebugConsole
{
    class Program
    {
        static void Timer(Action action)
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Restart();
            action();
            sw.Stop();
            Console.WriteLine($"　{sw.ElapsedMilliseconds}ms");
        }

        static unsafe void Main(string[] args)
        {
            using (var mat = new Mat("../../../sample.bmp", ImreadModes.Color))
            {
                var p = new Pixel<Int24>(256, 256);
                for(int y=0 ; y< 256; y++)
                    for (int x = 0; x < 256; x++)
                    {
                        var bytes = mat.At<Vec3b>(y, x);

                        var bayer = (x % 2, y % 2);
                        if(bayer == (1, 0))
                        {
                            //R
                            p[x, y] = (Int24)(bytes[2] << 16);
                        }
                        else if(bayer == (0, 1))
                        {
                            p[x, y] = (Int24)(bytes[0] << 16);
                        }
                        else
                        {
                            p[x, y] = (Int24)(bytes[1] << 16);
                        }
                    }
                p.Save("sample24.bin");

            }

            //var img = new WriteableBitmap(p.Width, p.Height, 96, 96, PixelFormats.Bgr24, null);
            //img.Lock();
            //PixelDeveloper.Demosaic(p, img.BackBuffer, img.BackBufferStride, false);
            //img.AddDirtyRect(new Int32Rect(0, 0, img.PixelWidth, img.PixelHeight));
            //img.Unlock();

            //using (var stream = new System.IO.FileStream("temp.bmp", FileMode.Create, FileAccess.Write))
            //{
            //    var encoder = new BmpBitmapEncoder();
            //    encoder.Frames.Add(BitmapFrame.Create(img));
            //    encoder.Save(stream);
            //}
            //System.Diagnostics.Process pro = new System.Diagnostics.Process();
            //pro.StartInfo.FileName = "temp.bmp";
            //pro.Start();


            //    var p = new Pixel<Int32>(256, 256);
            //p.Load(@"C:\Users\PC\Source\Repos\RawAnalyzer\000.bin");
            //var img = new WriteableBitmap(p.Width, p.Height, 96, 96, PixelFormats.Bgr24, null);
            //p.ForEach((i) => i >> 9);

            //img.Lock();
            //foreach (var i in new int[10000])
            //{
            //    //PixelDeveloper.Demosaic5(p.pix, img.BackBuffer, img.PixelWidth, img.PixelHeight, img.BackBufferStride);
            //}
            //foreach (var i in new int[10000])
            //{
            //    //PixelDeveloper.Demosaic4(p.pix, img.BackBuffer, img.PixelWidth, img.PixelHeight, img.BackBufferStride);
            //}


            //img.AddDirtyRect(new Int32Rect(0, 0, img.PixelWidth, img.PixelHeight));
            //img.Unlock();
            //using (var stream = new System.IO.FileStream("temp.bmp", FileMode.Create, FileAccess.Write))
            //{
            //    var encoder = new BmpBitmapEncoder();
            //    encoder.Frames.Add(BitmapFrame.Create(img));
            //    encoder.Save(stream);
            //}

            //System.Diagnostics.Process pro = new System.Diagnostics.Process();
            //pro.StartInfo.FileName = "temp.bmp";
            //pro.Start();
        }
    }
}

