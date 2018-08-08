using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Pixels.Extensions;
using Pixels;
using Pixels.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Xunit.Abstractions;
using System.IO;
using SixLabors.ImageSharp.Processing;

namespace XUnitTest.Freamwork
{
    public class FreamworkTest
    {
        readonly ITestOutputHelper output;
        string path;
        string outputpath;
        public FreamworkTest(ITestOutputHelper _output)
        {
            path = Directory.GetCurrentDirectory() + @"\Sample\sample24.bin";
            outputpath = Directory.GetCurrentDirectory() + @"\Sample\output.bmp";
            output = _output;
        }

        [Fact]
        public void Test_Bitmap()
        {
            var src = PixelBuilder.Create<Int24E>(256, 256);
            src.Load(path);

            using (var image = new Image<Rgba32>(256,256))
            {
                src.ForEach((x, y, p) =>
                {
                    var mono = (byte)((int)p[x, y]);
                    image[x, y] = new Rgba32(mono, mono, mono);
                });
                image.Save(outputpath);
            }
            Utils.CallProcess(outputpath);
        }

        //var p = new Pixel<Int32>(2256, 1178);
        //p.Load(@"C:\Users\PC\Source\Repos\RawAnalyzer\000.bin");

        //var img = new WriteableBitmap(p.Width, p.Height, 96, 96, PixelFormats.Bgr24, null);
        //p.ToWriteableBitmap24(img);

        //using (var stream = new System.IO.FileStream("temp.bmp", FileMode.Create, FileAccess.Write))
        //{
        //    var encoder = new BmpBitmapEncoder();
        //    encoder.Frames.Add(BitmapFrame.Create(img));
        //    encoder.Save(stream);
        //}


    }
}
