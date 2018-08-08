using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Pixels;
using Pixels.DDL;
using Pixels.Framework;
//using Newtonsoft.Json.Linq;
//using Microsoft.CodeAnalysis.Scripting;
//using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace WPFViewer.Models
{

    public static class ModelBilder
    {
        public static Model Build()
        {
            var path = System.AppDomain.CurrentDomain.BaseDirectory;
            var fullpath = System.IO.Path.Combine(path, "config.yaml");
            var json = DDL.Parse(fullpath);
            return json.ToObject<Model>();
        }
    }

    public class Model : BindableBase
    {
        #region Property

        string _title = null;
        public string Title { get => _title; set => this.SetProperty(ref this._title, value); }

        ProcessingData<int,int> process = new ProcessingData<int, int>();
        private WriteableBitmap dummyImg = new WriteableBitmap(new BitmapImage(new Uri("pack://application:,,,/logo.png", UriKind.Absolute)));
        public WriteableBitmap img => process.img ?? dummyImg;
        
        private BitmapScalingMode _ScalingMode;
        public BitmapScalingMode ScalingMode { get => _ScalingMode; set => SetProperty(ref _ScalingMode, value); }

        double _Scale = 1;
        public double Scale { get => _Scale; set => this.SetProperty(ref this._Scale, value); }

        string _Shapes = null;
        public string Shapes { get => _Shapes; set => this.SetProperty(ref this._Shapes, value); }

        Dictionary<string, string> _Shortcut;
        public Dictionary<string, string> Shortcut { get => _Shortcut; set => this.SetProperty(ref this._Shortcut, value); }

        #endregion

        string _dir = "";
        public string Dir
        {
            get => _dir;
            set
            {
                selectedindex = 0;
                subFolders = System.IO.Directory.GetDirectories(
                                           value, "*", System.IO.SearchOption.TopDirectoryOnly);
                this.SetProperty(ref this._dir, value);
            }
        }


        //private TextDocument _Document = new TextDocument();
        //public TextDocument Document { get => _Document; set => this.SetProperty(ref this._Document, value); }

        //private string _Output = "";
        //public string Output { get => _Output; set => this.SetProperty(ref this._Output, value); }


        string[] subFolders = new string[] { "" };
        int selectedindex = 0;
        string selecttype = "";



        string mode = "";


        #region public Method 

        public Model()
        {

        }

        public void Init(string path)
        {
            process.Init(path);
            Title = null;
            RaisePropertyChanged("img");
        }

        public void ReadRaw(string path)
        {
            process
                .PreProcess(path)
                .MainProcess((i) => i)
                .PostProcess(false, 0);

            Title = path;
            RaisePropertyChanged("img");
        }

        internal void RunScript(string path)
        {
            //process.RunProcess(File.ReadAllText(path));
            //process.RunPostProcess(true, 0);
            //RaisePropertyChanged("img");
        }

        public void Draw()
        {
            //process.RunPostProcess(true, 0);
            //RaisePropertyChanged("img");
        }

        public void KeyDown(string x)
        {
            //Console.WriteLine($"KeyDown : {x}");
            //KeyDown_View(x);
            //RaisePropertyChanged("img");
        }

        #endregion

        #region private Method 

        private void KeyDown_View(string x)
        {
            switch (x)
            {
                //case "NumPad0":
                //    process.RunPostProcess(true, 0);
                //    break;
                //case "NumPad1":
                //    process.RunPostProcess(true, 1);
                //    break;
                //case "NumPad2":
                //    process.RunPostProcess(true, 2);
                //    break;
                //case "NumPad3":
                //    process.RunPostProcess(true, 3);
                //    break;
                //case "NumPad4":
                //    process.RunPostProcess(true, 4);
                //    break;
                //case "NumPad5":
                //    process.RunPostProcess(true, 5);
                //    break;
                //case "NumPad6":
                //    process.RunPostProcess(true, 6);
                //    break;
                //case "NumPad7":
                //    process.RunPostProcess(true, 7);
                //    break;
                //case "NumPad8":
                //    process.RunPostProcess(true, 8);
                //    break;
                //case "NumPad9":
                //    process.RunPostProcess(true, 9);
                //    break;
                default:
                    break;
            }
        }

        private void KeyDown_View2(string x)
        {
            if (Shortcut.ContainsKey(x))
            {
                switch (Shortcut[x])
                {
                    case "next":
                        selectedindex = selectedindex >= (subFolders.Length - 1) ? 0 : selectedindex + 1;
                        break;
                    case "back":
                        selectedindex = selectedindex <= 0 ? subFolders.Length - 1 : selectedindex - 1;
                        break;
                    case "color":
                        mode = "color";
                        break;
                    case "color2":
                        mode = "color2";
                        break;
                    case "mono":
                        mode = "mono";
                        break;
                    default:
                        selecttype = Shortcut[x];
                        break;
                }
                var newfile = Path.Combine(Dir, subFolders[selectedindex], selecttype);


                //ファイルが存在したら
                if (System.IO.File.Exists(newfile))
                {
                    //var hoge = ReadFile(newfile, mode);
                    //hoge.Freeze();
                    //img = hoge;
                    ////ファイル名分岐
                    //switch(Path.GetFileName("newfile").ToLower())
                    //{
                    //    case "ave.trw":
                    //        break;
                    //    case "sgl.trw":
                    //        break;
                    //}
                }

                //傷チェック
                /*
                var median = new Pixel<int>(2256, 1178);
                var div = new Pixel<int>(2256, 1178);
                Pixels.Standard.PixelExtented.Median(src, median, (5, 5));
                Pixels.Standard.PixelExtented.Diff(src, median, div);
                (int result, var list) = src.Count(upper : 255);

                JObject jsonDst = new JObject();
                JArray jsonArry = new JArray();
                foreach (var i in list)
                {
                    JObject d = new JObject();
                    d.Add("type", new JValue("circle"));
                    d.Add("x", new JValue(i.x));
                    d.Add("y", new JValue(i.y));
                    d.Add("r", new JValue(2));
                    jsonArry.Add(d);
                }
                jsonDst.Add("data", jsonArry);
                Shapes = jsonDst.ToString();
                */
                //RaisePropertyChanged("img");
            }
        }

        #endregion


        #region MyRegion
        /*
        public class Globals
        {
            public Pixel<int> src;
            public Pixel<int> dst;
        }

        private static async Task CAsync(Pixel<int> src, Pixel<int> dst, string code)
        {
            var ssr = ScriptSourceResolver.Default.WithBaseDirectory(Environment.CurrentDirectory);
            var smr = ScriptMetadataResolver.Default.WithBaseDirectory(System.Reflection.Assembly.GetEntryAssembly().Location);
            var options = ScriptOptions.Default
                .WithSourceResolver(ssr)
                .WithMetadataResolver(smr)
                .WithReferences(Assembly.GetEntryAssembly())
                .AddImports("System", "System.IO", "Pixels.Standard");

            var result = await CSharpScript.EvaluateAsync(code, options
                , globals: new Globals()
                {
                    src = src,
                    dst = dst
                });

            // return result;
        }
        */
        #endregion
    }




}

//Text = Dir;
//Document.Text = File.ReadAllText("Script.csx");

//フォルダの一覧
//string[] subFolders = System.IO.Directory.GetDirectories(
//                           Dir, "*", System.IO.SearchOption.TopDirectoryOnly);
//Console.WriteLine(subFolders);

//Document.Text = File.ReadAllText("Script.csx");

//フォルダの一覧
//string[] subFolders = System.IO.Directory.GetDirectories(
//                           Dir, "*", System.IO.SearchOption.TopDirectoryOnly);
//Console.WriteLine(subFolders);

//pd.Show();