using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Pixels.Standard;
using Pixels.Standard.IO;
using Pixels.Framework;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Random cRandom = new System.Random();

            var input = new Pixel<Int32>(2256, 1178);
            var output = new Pixel<Int32>(2256, 1178);

            for (var i = 0;i<input.Width*input.Height;i++)
            {
                input[i]= cRandom.Next(Int32.MinValue, Int32.MaxValue);
            }

            input.Save("temp.bin");

            output.Load("temp.bin");

            for (var i = 0; i < input.Width * input.Height; i++)
            {
                Assert.AreEqual(input[i], output[i]);
            }
            

            //p.Load(@"C:\Users\PC\Source\Repos\RawAnalyzer\000.bin");

            //Assert.AreEqual(p[0], 15678);
            //Assert.AreEqual(p[0, 0], 15678);

            //var q = new Pixel<Int24>(2256, 1178);
            //q.Load(@"C:\Users\PC\Source\Repos\RawAnalyzer\000.bin");

            //Assert.AreEqual(q[0], 15678);

            //var r = new Pixel<Single>(2256, 1178);
            //r.Load(@"C:\Users\PC\Source\Repos\RawAnalyzer\000.bin", typeof(Int32));

            //Assert.AreEqual(r[0], 15678f);
        }

    }

    [TestClass]
    public class UnitFramework
    {
        [TestMethod]
        public void TestView()
        {
            var p = new Pixel<Int32>(2256, 1178);
            p.Load(@"C:\Users\PC\Source\Repos\RawAnalyzer\000.bin");

            var img = new WriteableBitmap(p.Width, p.Height, 96, 96, PixelFormats.Bgr24, null);
            p.ToWriteableBitmap24(img);

            using (var stream = new System.IO.FileStream("temp.bmp", FileMode.Create, FileAccess.Write))
            {
                var encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(img));
                encoder.Save(stream);
            }

            System.Diagnostics.Process pro = new System.Diagnostics.Process();
            pro.StartInfo.FileName = "temp.bmp";
            pro.Start();
        }
    }
}
