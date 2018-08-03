using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

using Pixels;
using Pixels.IO;

namespace XUnitTest.Freamwork
{
    public class FreamworkTest
    {

        [Fact(Skip ="未実装")]
        public void Test_Writeablebitmap()
        {
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

            //System.Diagnostics.Process pro = new System.Diagnostics.Process();
            //pro.StartInfo.FileName = "temp.bmp";
            //pro.Start();
        }

    }
}
