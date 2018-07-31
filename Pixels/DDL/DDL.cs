using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YamlDotNet;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

namespace Pixels.Standard.DDL
{
    public static class DDL
    {
        /*
            public static void Merge(this JObject src1, JObject src2)
            {
            src1.Merge(src2, new JsonMergeSettings
            {
            // union array values together to avoid duplicates
            MergeArrayHandling = MergeArrayHandling.Union
            });
            }
        */
        public static JObject Parse(string path)
        {
            using (var sr = new StreamReader(path))
            using (var sw = new StringWriter())
            {
                switch (Path.GetExtension(path).ToLower())
                {
                    case ".yaml":
                    case ".yml":
                        var deserializer = new Deserializer();
                        var yamlObject = deserializer.Deserialize(sr);
                        var js = new JsonSerializer();
                        js.Serialize(sw, yamlObject);
                        return JObject.Parse(sw.ToString());
                    default:
                        return JObject.Parse(sr.ReadToEnd());
                }
            }
        }

        public static T Deserialize<T>(string path)
        {
            return Parse(path).ToObject<T>();
        }
    }
}
