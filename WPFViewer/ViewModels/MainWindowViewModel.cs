using Prism.Mvvm;
using System.Reactive.Linq;
using Autofac;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Windows.Media.Imaging;
using System;
using System.IO;

namespace WPFViewer.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public Models.Model model { get; private set; }


        public ReactiveProperty<string> Title { get; private set; }

        // Canvas
        public ReactiveProperty<WriteableBitmap> img { get; private set; }

        public ReactiveProperty<double> Width { get; private set; }
        public ReactiveProperty<double> Height { get; private set; }
        public ReactiveProperty<double> Scale { get; private set; }

        public ReactiveProperty<string> Shapes { get; private set; }

        //ScrollViewer
        public ReactiveCommand FileDropCommand { get; private set; }
        public ReactiveCommand<string> ShortcutCommand { get; private set; }


        public MainWindowViewModel()
        {
            model = App.modelcontainer.Resolve<Models.Model>();

            Title = new ReactiveProperty<string>();
            Title = model.ObserveProperty(x => x.Title).Select(x =>
                x == null
                ? "RawAnalyzer"
                : $"{Path.GetFileName(x)} - {Path.GetDirectoryName(x)}"
            ).ToReactiveProperty();

            Scale = new ReactiveProperty<double>();
            Scale = model.ObserveProperty(x => x.Scale).ToReactiveProperty();

            img = new ReactiveProperty<WriteableBitmap>();
            img = model.ObserveProperty(x => x.img).ToReactiveProperty();

            Shapes = new ReactiveProperty<string>();
            Shapes = model.ObserveProperty(x => x.Shapes).ToReactiveProperty();

            Width = img.CombineLatest(Scale, (x, y) => (x?.PixelWidth ?? 0) * y).ToReactiveProperty();
            Height = img.CombineLatest(Scale, (x, y) => (x?.PixelHeight ?? 0) * y).ToReactiveProperty();

            FileDropCommand = new ReactiveCommand();
            FileDropCommand.Subscribe(x => FileDropMethod(x as string[]));

            ShortcutCommand = new ReactiveCommand<string>();
            ShortcutCommand.Subscribe(x => model.KeyDown(x));
        }


        void FileDropMethod(string[] paths)
        {
            switch (System.IO.Path.GetExtension(paths[0]).ToLower())
            {
                case ".yaml":
                case ".yml":
                case ".json":
                    model.Init(paths[0]);
                    break;
                case ".csx":
                    model.RunScript(paths[0]);
                    break;
                default:
                    model.ReadRaw(paths[0]);
                    break;

            }
        }
    }
}



/*
 *         public ReactiveProperty<bool> isLinkage { get; private set; }
        public ReactiveProperty<int> MouseWheel { get; private set; }
        public ReactiveProperty<List<Shape>> Shapes { get; private set; }
        public ReactiveProperty<Point> MouseMove { get; private set; }
        public ReactiveProperty<Rect> RectMove { get; private set; }

             MouseWheel = model.ToReactivePropertyAsSynchronized(
                x => x.Scale,
                convert: x => (int)Math.Log(x, 2),
                convertBack: x => Math.Pow(2, x))
                .AddTo(this.Disposable);
            MouseMove = new ReactiveProperty<Point>().AddTo(this.Disposable);
            RectMove = model.ToReactivePropertyAsSynchronized(x => x.Rect).AddTo(this.Disposable);
            Shapes = MouseMove.CombineLatest(RectMove, (x, y) =>
            {
                var i = new List<Shape>();
                DrawL(i, x);
                //DrawShift(i, y);
                return i;
            }).ToReactiveProperty().AddTo(this.Disposable);
     */


//public void DrawL(List<Shape> a, Point x)
//{
//    var c = Colors.Red;
//    var f = Colors.Red;
//    f.A = 30;

//    //値は取得できたら
//    //var lsb = model?.raws[this.ContentId][(int)(x.X), (int)(x.Y)].ToString() ?? "";
//    var lsb = "null";

//    a.Add(new Shape()
//    {
//        Key = "Text",
//        Left = (int)(x.X + 1),
//        Top = (int)(x.Y + 1),// - 16 / Scale.Value,
//        Size = 16,
//        Text = $"({(int)(x.X)},{(int)(x.Y)})\r\n{lsb}"
//    });
//    a.Add(new Shape()
//    {
//        Key = "Rect",
//        Left = (int)x.X,
//        Top = (int)x.Y,
//        Width = 1,
//        Height = 1,
//        Brush = c,
//        Fill = f
//    });
//}