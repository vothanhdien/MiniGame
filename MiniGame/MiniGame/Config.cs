using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MiniGame
{
    public class Config
    {
        public static JObject jsonConfig = null;
        private static Config _instance = null;

        internal static Config Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Config();
                return _instance;
            }
        }
        private Config()
        {

        }
        public void Load()
        {
            var path = $"{Global.APP_PATH}\\{Global.CONFIG_DP}";
            var jsonText = File.ReadAllText(path);
            jsonConfig = JObject.Parse(jsonText);
        }

        public void Save()
        {

        }

        public static string[] LoadUnitTextures(string unitName)
        {
            //JToken token1 = jsonConfig["Textures"];
            JToken token = jsonConfig["Textures"][unitName];
            var o = token.Select(v => v.ToString());
            string[] res = o.ToArray();

            return res;
            //return (jsonConfig["Textures"][unitName] as JArray).Select(v => v.ToString()).ToArray();
        }
    }
}