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
using Pixels.Standard;
using Pixels.Standard.IO;
using Pixels.Standard.Processing;
using Pixels.Framework;
using System.Windows.Media;

namespace DebugConsole
{
    class Program
    {
        static unsafe void Main(string[] args)
        {
            //BenchmarkRunner.Run<SomeTest>();

            var sample = @"..\..\..\sample\sample.bmp";

            var src = Utils.ConvertBmpToPixels(sample);
            var p = new Pixel<Int32>(src.Width, src.Height);

            for(var i = 0;i< src.Width * src.Height; i++)
            {
                p[i] = src[i];
            }

            Options option = new Options();
            option.bitshift = 0;
            option.bayer = Bayer.GB;

            var img = new WriteableBitmap(p.Width, p.Height, 96, 96, PixelFormats.Bgr24, null);
            p.ToWriteableBitmap24(img, option);

            img.Show();

            //var p = new Pixel<Int32>(256, 256);
            //p.Load(sample, typeof(Int24));

            //p.ForEach((i) => i);


        }
    }
}


//var p = new Pixel<Int32>(256, 256);
//var p = new Pixel<Int32>(192, 256);


//var img = new WriteableBitmap(p.Width, p.Height, 96, 96, PixelFormats.Bgr24, null);
//img.Lock();
//PixelDeveloper.Demosaic(p, img.BackBuffer, img.BackBufferStride, false);
//img.AddDirtyRect(new Int32Rect(0, 0, img.PixelWidth, img.PixelHeight));
//img.Unlock();


//    var p = new Pixel<Int32>(256, 256);
//p.Load(@"C:\Users\PC\Source\Repos\RawAnalyzer\000.bin");
//var img = new WriteableBitmap(p.Width, p.Height, 96, 96, PixelFormats.Bgr24, null);
//p.ForEach((i) => i >> 9);

