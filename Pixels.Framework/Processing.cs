using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Pixels;
using Pixels.IO;
using Pixels.Processing;

namespace Pixels.Framework
{
    public class PixelType
    {
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
        public int Offset { get; set; } = 0;
        public string FileType { get; set; } = null;

        public Dictionary<string, Area> SubPlanes;

        public PixelType() { }

        public class Area
        {
            public int left;
            public int top;
            public int width;
            public int height;
        }

        public Type GetFileType()
        {
            switch (FileType)
            {
                case "Byte":
                case "Int16":
                case "Int32":
                case "Int64":
                case "UInt16":
                case "UInt32":
                case "UInt64":
                case "Single":
                case "Double":
                    return Type.GetType($"System.{FileType}");
                default:
                    return Type.GetType($"{nameof(Pixels)}.{nameof(Standard)}.{FileType}, {nameof(Pixels)}.{nameof(Standard)}");
            }
        }
    }

    // Raw -[RunPreProcess]-> intermediate -[]-> WriteableBitmap
    //
    //
    //
    //
    public class Processing
    {
        PixelType setting = new PixelType();
        public WriteableBitmap img => postProcess?.img;
        public PreProcess preProcess;
        public MainProcess mainProcess;
        public PostProcess postProcess;

        public Processing()
        {
            preProcess = new PreProcess();
            mainProcess = new MainProcess(preProcess);
            postProcess = new PostProcess(mainProcess);
        }

        public void Init(string configPath)
        {
            var hoge = Pixels.DDL.DDL.Deserialize<PixelType>(configPath);

            if (hoge.Width != setting.Width && hoge.Height != setting.Height)
            {
                preProcess.Init(hoge);
                mainProcess.Init(hoge.Width, hoge.Height, hoge.SubPlanes);
                postProcess.Init(hoge.Width, hoge.Height, hoge.SubPlanes);
            }
            setting = hoge;
        }

        public class PreProcess
        {
            public Pixel<int> raw;
            PixelType setting;

            public void Init(PixelType setting)
            {
                raw = new Pixel<int>(setting.Width, setting.Height);
                setting.SubPlanes.ToList().ForEach(x => raw.SubPlane.Add(x.Key, (x.Value.left, x.Value.top, x.Value.width, x.Value.height)));
                this.setting = setting;
            }

            public void Run(string path)
            {
                //raw.Load(path, setting.GetFileType());
            }

        }

        public class MainProcess
        {
            public Pixel<int> raw;
            PreProcess preProcess;
            public string name = "";

            public MainProcess(PreProcess pre)
            {
                preProcess = pre;
            }

            public void Init(int width, int height, Dictionary<string, PixelType.Area> subPlanes)
            {
                raw = new Pixel<int>(width, height);
                subPlanes.ToList().ForEach(x => raw.SubPlane.Add(x.Key, (x.Value.left, x.Value.top, x.Value.width, x.Value.height)));
            }
            public void Run(string code)
            {
                if (code == null)
                {
                    for(var i =0;i<raw.Width*raw.Height;i++)
                    {
                        //raw.pix[i] = preProcess.raw.pix[i] >>= 10;
                    }
                    
                    //src.Map(intermediate, (x, y, src, dst) =>
                    //{
                    //    dst[x, y] = src[x, y] >> 0;
                    //}, subPlane: "Full");
                }
                else
                {
                    //CAsync(src, intermediate, code).Wait();
                }

                //if (src.SubPlane.ContainsKey("Eff"))
                //{
                //    src.Map(intermediate, (x, y, src, dst) => {
                //        dst[x, y] = src[x, y];
                //    }, subPlane: "Full");
                //    src.Map(intermediate, (x, y, src, dst) => {
                //        dst[x, y] = src[x, y] >> 8;
                //    }, subPlane: "Eff");
                //}
                //else
                //    src.Map(intermediate, (x, y, src, dst) => {
                //        dst[x, y] = src[x, y] >> 8;
                //    }, subPlane: "Full");
            }
        }

        public class PostProcess
        {
            public WriteableBitmap img;
            Options option;
            public string name = "";
            MainProcess mainProcess;

            public PostProcess(MainProcess main)
            {
                mainProcess = main;
            }

            public void Init(int width, int height, Dictionary<string, PixelType.Area> subPlanes)
            {
                option = new Options();
                img = new WriteableBitmap(width, height, 96, 96, System.Windows.Media.PixelFormats.Bgr24, null);
            }
            public void Run(bool color, int bitshift)
            {
                //pd.bayer = Bayer.Mono;
                //pd.Developing(mainProcess.raw, plane: "Full", parallel: true);

                //pd.bitshift = bitshift;
                //if (color)
                //{
                //    pd.bayer = Bayer.RG;
                //    pd.SetGain(1.8f, 1f, 2.59f);
                //    pd.stagger = (0, 1);
                //    pd.Developing(mainProcess.raw, plane: "Full", parallel: true);
                //}
                mainProcess.raw.ToWriteableBitmap24(img);
            }
        }
    }
}




/*
 
     
     
             static void Main(string[] args)
        {
            //SpanTest();

            var a = DDL.DDL.Parse("config.yaml");
            var b = DDL.DDL.Parse("config2.yaml");
            b.Merge(a["C"]);
            var c = b.ToObject<YamlTest>();

            //Type t = typeof(YamlTest);
            var aa = c.GetType().GetProperty("D");
            Console.WriteLine(aa.GetValue(c).ToString());//.SetValue(c, "AAA");


            //Type t = Type.GetType(c.GetType().ToString());
            //object o = Activator.CreateInstance(t);

            // cobj.i = 100;の部分


            MainAsync().Wait();
            CAsync(c).Wait();

        }

        private static async Task CAsync(YamlTest a)
        {
            var result = await CSharpScript.EvaluateAsync("Console.WriteLine(D);"
                , ScriptOptions.Default.WithImports("System")
                , globals: a);

            //return result;
        }
        private static async Task BAsync(YamlTest a)
        {
            var result = await CSharpScript.EvaluateAsync("D = \"aaaa\";", globals: a);

            //return result;
        }

        private static async Task MainAsync()
        {
            var result = await CSharpScript.EvaluateAsync<int>(@"
var x = 1;
var y = 2;
x + y
");
            Console.WriteLine(result);
        }
     
     
     */


/*
     private async void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        var tb = (TextBox)sender;
        var s = tb.Text;

        if (string.IsNullOrWhiteSpace(s))
            return;

        if (s[s.Length - 1] == '\n')
        {
            try
            {
                if (_state == null)
                    _state = await CSharpScript.RunAsync(s, globals: ViewModel.Commander);
                else
                    _state = await _state.ContinueWithAsync(s);
            }
            catch { }

            tb.Text = "";
        }
    }
*/
