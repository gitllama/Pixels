using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTest.Utils
{
    class Others
    {
        [Obsolete(".Net CoreでWriteableBitmapの使用保留")]
        public static void Show(this WriteableBitmap img)
        {
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

        public static Pixel<Byte> ConvertBmpToPixels(string path)
        {
            using (var mat = new Mat(path, ImreadModes.Color))
            {
                var dst = new Pixel<Byte>(mat.Width, mat.Height);
                for (int y = 0; y < mat.Height; y++)
                    for (int x = 0; x < mat.Width; x++)
                    {
                        var bytes = mat.At<Vec3b>(y, x);

                        var bayer = (x % 2, y % 2);
                        if (bayer == (1, 0)) //R
                        {

                            dst[x, y] = bytes[2];
                        }
                        else if (bayer == (0, 1)) //B
                        {
                            dst[x, y] = bytes[0];
                        }
                        else //G
                        {
                            dst[x, y] = bytes[1];
                        }
                    }
                return dst;
                //p.Save("sample24.bin");
            }
        }

        public static void Test()
        {
            var img = new WriteableBitmap(p.Width, p.Height, 96, 96, PixelFormats.Bgr24, null);
            img.Lock();
            PixelDeveloper.Demosaic(p, img.BackBuffer, img.BackBufferStride, false);
            img.AddDirtyRect(new Int32Rect(0, 0, img.PixelWidth, img.PixelHeight));
            img.Unlock();
        }
    }
    public class PixelDevelopingOpenCV
    {
        int width;
        int height;
        public UInt16[] CV_16UC1;
        public UInt16[] CV_16UC3;
        public Byte[] CV_8UC3;

        public PixelDevelopingOpenCV(int width, int height)
        {
            this.width = width;
            this.height = height;
            CV_16UC1 = new UInt16[width * height];
            CV_16UC3 = new UInt16[width * height * 3];
            CV_8UC3 = new Byte[width * height * 3];
        }

        public void Show()
        {
            using (var matsrc = new Mat(height, width, MatType.CV_8UC3, CV_8UC3))
            {
                Cv2.ImShow("result", matsrc);
                Cv2.WaitKey(0);
                Cv2.DestroyAllWindows();
            }
        }

        public void ToWriteableBitmap(WriteableBitmap dst)
        {
            using (var matsrc = new Mat(height, width, MatType.CV_8UC3, CV_8UC3))
            {
                WriteableBitmapConverter.ToWriteableBitmap(matsrc, dst);
            }
        }

        public void Developing<T>(Pixel<T> src) where T : struct
        {
            //src -> CV_16UC1 -> CV_16UC3
        }

        public void To16UC1(Pixel<int> src)
        {
            Parallel.For(0, height, y =>
            {
                if ((y & 1) == 0)
                {
                    for (int x = y * width; x < y * width + width; x++)
                    {
                        var hoge = (src.pix[x] + 0) >> 0;
                        CV_16UC1[x] = (UInt16)(hoge > UInt16.MaxValue ? UInt16.MaxValue : hoge < UInt16.MinValue ? UInt16.MinValue : hoge);
                    }
                }
                else
                {
                    //stagger
                    for (int x = y * width + 1; x < y * width + width - 1; x++)
                    {
                        var hoge = (src.pix[x /* + dst.stagger.BG*/] + 0) >> 0;
                        CV_16UC1[x] = (UInt16)(hoge > UInt16.MaxValue ? UInt16.MaxValue : hoge < UInt16.MinValue ? UInt16.MinValue : hoge);
                    }
                }
            });
        }

        private void DevelopingMono()
        {
            Parallel.For(0, height, y =>
            {
                int c = y * width * 3;
                for (int x = y * width; x < y * width + width; x++)
                {
                    var hoge = (byte)0;// dst.LUT[dst.CV_16UC1[x]];
                    CV_8UC3[c++] = hoge;
                    CV_8UC3[c++] = hoge;
                    CV_8UC3[c++] = hoge;
                }
            });
        }

        private void DevelopingColor(Bayer bayer, float rgain, float bgain)
        {
            var gain = new float[] {
                bgain, 1, rgain,
                bgain, 1, rgain,
                bgain, 1, rgain
            };
            var matrix = new float[]
            {
                1,0,0,
                0,1,0,
                0,0,1
            };
            using (var mat16UC1 = new Mat(height, width, MatType.CV_16UC1, CV_16UC1))
            using (var mat16UC3 = new Mat(height, width, MatType.CV_16UC3, CV_16UC3))
            using (var matMatrix = new Mat(3, 3, MatType.CV_32FC1, matrix))
            using (var matGain = new Mat(3, 3, MatType.CV_32FC1, gain))
            {
                switch (bayer)
                {
                    case Bayer.BG:
                        Cv2.CvtColor(mat16UC1, mat16UC3, ColorConversionCodes.BayerBG2BGR);
                        break;
                    case Bayer.GB:
                        Cv2.CvtColor(mat16UC1, mat16UC3, ColorConversionCodes.BayerGB2BGR);
                        break;
                    case Bayer.RG:
                        Cv2.CvtColor(mat16UC1, mat16UC3, ColorConversionCodes.BayerRG2BGR);
                        break;
                    default:
                        Cv2.CvtColor(mat16UC1, mat16UC3, ColorConversionCodes.BayerGR2BGR);
                        break;
                }
                Cv2.Transform(mat16UC3, mat16UC3, matMatrix.Mul(matGain));


                Parallel.For(0, height, y =>
                {
                    for (int x = y * width * 3; x < y * width * 3 + width * 3; x++)
                    {
                        CV_8UC3[x] = 0; // dst.LUT[dst.CV_16UC3[x]];
                    }

                    //int s = y * dst.Width;
                    //var span1 = new ReadOnlySpan<ushort>(dst.CV_16UC1).Slice(s + 1, dst.Width - 1);
                    //var span2 = new ReadOnlySpan<ushort>(dst.CV_16UC1).Slice(s + 1, dst.Width - 1);
                    //var span3 = new ReadOnlySpan<ushort>(dst.CV_16UC1).Slice(s + 1, dst.Width - 1);

                    //for (int x = y * dst.Width * 3; x < y * dst.Width * 3 + dst.Width * 3; x++)
                    //{
                    //    var index = 3 * (x + y * dst.Width);

                    //    /*B*/
                    //    dst.CV_8UC3[index + 0] = dst.LUT[dst.CV_16UC3[x]];
                    //    /*G*/
                    //    dst.CV_8UC3[index + 1] = dst.LUT[dst.CV_16UC3[x]];
                    //    /*R*/
                    //    dst.CV_8UC3[index + 2] = dst.LUT[dst.CV_16UC3[x]];
                    //}
                });
            }
        }
    }
}
