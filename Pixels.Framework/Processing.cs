using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Pixels;
using Pixels.IO;
using Pixels.Extensions;
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


    }

    public class ProcessingData<T,U> where T : struct where U : struct
    {
        public int Width;
        public int Height;
        public int Offset;
        public string FileType;

        public Pixel<T> raw;
        public Pixel<U> buf;
        public WriteableBitmap img;

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


    public static class Processing
    {
        public static ProcessingData<T, U> Init<T, U>(this ProcessingData<T, U> data, string configPath) where T : struct where U : struct
        {
            var hoge = Pixels.DDL.DDL.Deserialize<PixelType>(configPath);

            if (hoge.Width != data.Width || hoge.Height != data.Height)
            {
                data.Width = hoge.Width;
                data.Height = hoge.Height;
                data.InitRaw();
                data.InitBuf();
                data.InitImg();
            }

            return data;
        }

        public static ProcessingData<T, U> InitRaw<T, U>(this ProcessingData<T, U> data) where T : struct where U : struct
        {
            data.raw = PixelBuilder.Create<T>(data.Width, data.Height);
            return data;
        }
        public static ProcessingData<T, U> InitBuf<T, U>(this ProcessingData<T, U> data) where T : struct where U : struct
        {
            data.buf = PixelBuilder.Create<U>(data.Width, data.Height);
            return data;
        }
        public static ProcessingData<T, U> InitImg<T, U>(this ProcessingData<T, U> data) where T : struct where U : struct
        {
            data.img = new WriteableBitmap(data.Width, data.Height, 96, 96, System.Windows.Media.PixelFormats.Bgr24, null);
            return data;
        }

        //public static void ProcessingAll<T,U>(this ProcessingData<T,U> data) where T : struct where U : struct
        //{
        //    preProcess = new PreProcess();
        //    mainProcess = new MainProcess(preProcess);
        //    postProcess = new PostProcess(mainProcess);
        //}

        public static ProcessingData<T, U> PreProcess<T, U>(this ProcessingData<T, U> data, string path) where T : struct where U : struct
        {
            //var constructedType = typeof(Pixel<>).MakeGenericType(type);
            //var method = constructedType.GetMethod("Fuga");
            //return method.Invoke(obj, null);

            data.raw.Load<T>(path); //data.GetFileType()
            return data;
        }

        public static ProcessingData<T, U> MainProcess<T, U>(this ProcessingData<T, U> data) where T : unmanaged where U : unmanaged
        {
            //data.raw.Map(data.buf, (i)=> i.);
            return data;
        }

        public static ProcessingData<T, U> MainProcess<T, U>(this ProcessingData<T, U> data, Func<T,U> func) where T : unmanaged where U : unmanaged
        {
            data.raw.Map(data.buf, func);
            return data;
        }

        public static ProcessingData<T, U> MainProcess<T, U>(this ProcessingData<T, U> data, string code) where T : struct where U : struct
        {
            if (code == null)
            {
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
            return data;
        }


        public static void PostProcess<T, U>(this ProcessingData<T, U> data, bool color, int bitshift) where T : unmanaged where U : unmanaged
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
            data.raw.ToWriteableBitmap24(data.img);
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
