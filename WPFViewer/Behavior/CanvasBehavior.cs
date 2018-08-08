using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Behavior
{
    public class CanvasBehavior : BehaviorBase<Canvas>
    {
        // Point MouseMove
        // double Scale
        // string Shapes
        // Rect Rect
        // BitmapScalingMode Scaling

        #region Property

        public static readonly DependencyProperty MouseMoveProperty = DependencyProperty.Register(
            "MouseMove", typeof(Point), typeof(CanvasBehavior), new UIPropertyMetadata(null));
        public Point MouseMove { get => (Point)GetValue(MouseMoveProperty); set => SetValue(MouseMoveProperty, value); }

        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(
            "Scale", typeof(double), typeof(CanvasBehavior), new UIPropertyMetadata(1.0));
        public double Scale { get => (double)GetValue(ScaleProperty); set => SetValue(ScaleProperty, value); }

        public static readonly DependencyProperty RectProperty = DependencyProperty.Register(
            "Rect", typeof(Rect), typeof(CanvasBehavior), new UIPropertyMetadata(null));
        public Rect Rect { get => (Rect)GetValue(RectProperty); set => SetValue(RectProperty, value); }


        //プロパティ変更時にイベント発火させたいときはPropertyChangedを実装
        public static readonly DependencyProperty ShapesProperty = DependencyProperty.Register(
            "Shapes", typeof(string), typeof(CanvasBehavior), new UIPropertyMetadata(null, ShapesPropertyChanged));
        public string Shapes { get => (string)GetValue(ShapesProperty); set => SetValue(ShapesProperty, value); }
        private static void ShapesPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var crtl = d as CanvasBehavior;
            crtl?.ReDraw();
        }

        public static readonly DependencyProperty ScalingModeProperty = DependencyProperty.Register(
            "ScalingMode", typeof(BitmapScalingMode), typeof(CanvasBehavior), new UIPropertyMetadata(BitmapScalingMode.Unspecified, ScalingModePropertyChanged));
        public BitmapScalingMode ScalingMode { get => (BitmapScalingMode)GetValue(ScalingModeProperty); set => SetValue(ScalingModeProperty, value); }
        private static void ScalingModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Load前に呼び出される
            var crtl = d as CanvasBehavior;
            if (crtl.cnv == null) return;
            RenderOptions.SetBitmapScalingMode(crtl.cnv, (BitmapScalingMode)e.NewValue);
        }

        #endregion


        Canvas cnv;
        bool flagLeftShift = false;
        Point PointR_old;

        protected override void OnSetup()
        {
            base.OnSetup();
            this.AssociatedObject.Loaded += AssociatedObject_Loaded;
            this.AssociatedObject.MouseMove += AssociatedObject_MouseMove;
            this.AssociatedObject.MouseDown += AssociatedObject_MouseDown;
            this.AssociatedObject.KeyDown += AssociatedObject_KeyDown;
            this.AssociatedObject.SizeChanged += AssociatedObject_LayoutUpdated;
        }
        protected override void OnCleanup()
        {
            this.AssociatedObject.Loaded -= AssociatedObject_Loaded;
            this.AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
            this.AssociatedObject.MouseDown -= AssociatedObject_MouseDown;
            this.AssociatedObject.KeyDown -= AssociatedObject_KeyDown;
            this.AssociatedObject.SizeChanged -= AssociatedObject_LayoutUpdated;
            //cnv = null;
            base.OnCleanup();
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            cnv = sender as Canvas;
            RenderOptions.SetBitmapScalingMode(cnv, ScalingMode);
        }

        private void AssociatedObject_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void AssociatedObject_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
        {
            var buf = e.GetPosition(cnv);//(Canvas)sender

            MouseMove = new Point(buf.X / Scale, buf.Y / Scale);


            //Shift
            if ((Keyboard.Modifiers & ModifierKeys.Shift) > 0)
            {
                if (!flagLeftShift)
                {
                    PointR_old = MouseMove;
                    Rect = RectClip(new Rect(PointR_old, MouseMove));
                }
                else
                {
                    Rect = RectClip(new Rect(PointR_old, MouseMove));
                }
                flagLeftShift = true;
            }
            else if ((Keyboard.Modifiers & ModifierKeys.Shift) == 0)
            {
                flagLeftShift = false;
            }

            Rect RectClip(Rect src)
            {
                Point sta = src.TopLeft;
                Point end = src.BottomRight;

                sta = new Point((int)sta.X, (int)sta.Y);
                end = new Point((int)end.X, (int)end.Y);

                return new Rect(sta, end);
            }
        }

        private void AssociatedObject_LayoutUpdated(object sender, EventArgs e)
        {
            //Scaleが変わった時の再描画に使える
            ReDraw();
        }

        public void ReDraw()
        {
            if (cnv == null) return;
            cnv.Children.Clear();
            if (Shapes == null) return;

            try
            {
                var json = JObject.Parse(Shapes);
                DrawShape.Draw(cnv, json, Scale);
            }
            catch
            {

            }
        }


        private void SaveCanvas()
        {
            // CreateCanvas() は描画済みのCanvasを返すとする
            var canvas = cnv;

            // PNG形式で保存
            toImage(cnv, @"C:¥Path¥To¥Test.png");

            // JPEG形式で保存
            var encoder = new JpegBitmapEncoder();
            toImage(cnv, @"c:¥Path¥To¥Test.jpg", encoder);
        }
        public static void toImage(Canvas canvas, string path, BitmapEncoder encoder = null)
        {
            // レイアウトを再計算させる
            var size = new Size(canvas.Width, canvas.Height);
            canvas.Measure(size);
            canvas.Arrange(new Rect(size));

            // VisualObjectをBitmapに変換する
            var renderBitmap = new RenderTargetBitmap((int)size.Width,       // 画像の幅
                                                      (int)size.Height,      // 画像の高さ
                                                      96.0d,                 // 横96.0DPI
                                                      96.0d,                 // 縦96.0DPI
                                                      PixelFormats.Pbgra32); // 32bit(RGBA各8bit)
            renderBitmap.Render(canvas);

            // 出力用の FileStream を作成する
            using (var os = new FileStream(path, FileMode.Create))
            {
                // 変換したBitmapをエンコードしてFileStreamに保存する。
                // BitmapEncoder が指定されなかった場合は、PNG形式とする。
                encoder = encoder ?? new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                encoder.Save(os);
            }
        }
    }

    public static class CanvasBehaviorExtentions
    {
        public static double TryParse(this string value, double defaultValue)
        {
            return double.TryParse(value, out double i) ? i : defaultValue;
        }
    }


    class Shape
    {
        //実座標いれて
        public string Key { get; set; }
        public string Text { get; set; } = "";
        public double Left { get; set; }
        public double Top { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Size { get; set; }
        public Color Brush { get; set; } = Colors.Red;
        public Color Fill { get; set; } = new Color() { A = 0 };
    }


    public static class DrawShape
    {
        public static void Draw(Canvas cnv, JObject shapes, double scale)
        {
            foreach (var i in shapes["data"])
            {
                UIElement element = null;

                switch (i["type"].ToString())
                {
                    case "circle":
                        {
                            //c[1].TryParse(0)
                            double x = scale * i["x"].Value<int>() * scale;
                            double y = scale * i["y"].Value<int>() * scale;
                            double r = scale * i["r"].Value<int>() * scale;

                            if (r <= 0.0) return;

                            element = new Ellipse
                            {
                                Width = 2.0 * r,
                                Height = 2.0 * r,
                                StrokeThickness = 1.0,
                                Stroke = Brushes.Red
                            };
                            Canvas.SetLeft(element, x - r);
                            Canvas.SetTop(element, y - r);
                        }
                        break;
                    case "Caption":
                        //if (i.Size < 0.0) return;
                        //element = new TextBlock
                        //{
                        //    FontSize = i.Size,
                        //    Foreground = new SolidColorBrush(i.Brush),
                        //    Text = i.Text
                        //};
                        //Canvas.SetLeft(element, i.Left);
                        //Canvas.SetTop(element, i.Top);
                        break;
                    case "Text":
                        //if (i.Size < 0.0) return;
                        //element = new TextBlock
                        //{
                        //    FontSize = i.Size,
                        //    Foreground = new SolidColorBrush(i.Brush),
                        //    Text = i.Text
                        //};
                        //Canvas.SetLeft(element, i.Left * scale);
                        //Canvas.SetTop(element, i.Top * scale);
                        break;
                    case "Rectangle":
                    case "Rect":
                        //if (i.Width <= 0.0 || i.Height <= 0.0) return;

                        //element = new Rectangle
                        //{
                        //    Width = i.Width * scale,
                        //    Height = i.Height * scale,
                        //    StrokeThickness = 1.0,
                        //    Stroke = new SolidColorBrush(i.Brush),
                        //    Fill = new SolidColorBrush(i.Fill)
                        //};
                        //Canvas.SetLeft(element, i.Left * scale);
                        //Canvas.SetTop(element, i.Top * scale);
                        break;
                    //case "Grid":
                    //    DrawGrid(arry);
                    //    break;
                    default:
                        break;
                }

                if (element != null) cnv.Children.Add(element);
            }
        }

        //private static void DrawGrid(Canvas ctrl, string[] c)
        //{
        //    int num2;
        //    int num = int.TryParse(c[1], out num2) ? num2 : 0;
        //    if (num < 1)
        //    {
        //        return;
        //    }
        //    int num3 = 0;
        //    while ((double)num3 < this.uWidth)
        //    {
        //        Line element = new Line
        //        {
        //            X1 = (double)num3 * this.uScale,
        //            Y1 = 0.0,
        //            X2 = (double)num3 * this.uScale,
        //            Y2 = this.uHeight * this.uScale,
        //            StrokeThickness = 1.0,
        //            Stroke = Brushes.Red
        //        };
        //        cnv.Children.Add(element);
        //        num3 += num;
        //    }
        //    int num4 = 0;
        //    while ((double)num4 < this.uHeight)
        //    {
        //        Line element2 = new Line
        //        {
        //            X1 = 0.0,
        //            Y1 = (double)num4 * this.uScale,
        //            X2 = this.uWidth * this.uScale,
        //            Y2 = (double)num4 * this.uScale,
        //            StrokeThickness = 1.0,
        //            Stroke = Brushes.Red
        //        };
        //        cnv.Children.Add(element2);
        //        num4 += num;
        //    }
        //}
    }
}
