using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MiniGame
{
    public class Config
    {
        public static JObject jsonConfig = null;
        private static Config _instance = null;

        private static string CONFIG_DP = "config.json";
        private static string HIGH_SCORE = "highScore.json";
        private static string APP_PATH = getpath();


        private static string getpath()
        {
            var codeBase = Assembly.GetExecutingAssembly();
            //var exeBase = new UriBuilder(codeBase).Path;
            return Path.GetDirectoryName(codeBase.Location);
        }

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
            var path = $"{APP_PATH}\\{CONFIG_DP}";
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

        public static string LoadSpriteFont(String fontName)
        {
            JToken token = jsonConfig["SpriteFont"][fontName];
            var o = token.Select(v => v.ToString());
            
            return o.ToString();
        }

        public float[] getHighScore()
        {
            var path = $"{APP_PATH}\\{HIGH_SCORE}";
            var jsonText = File.ReadAllText(path);
            JObject jsonScores = JObject.Parse(jsonText);

            List<float> scoreList = new List<float>();
            JToken token = jsonScores["HightScore"];
            var o = token.Select(v => float.Parse(v.ToString()));
            float[] res = o.ToArray();

            return res;
        }
        public bool saveHighScore(float[] scores)
        {
            var path = $"{APP_PATH}\\{HIGH_SCORE}";

            string content = "{"
                + "HightScore: [";
            for(int i = 0; i< scores.Length; i++)
            {
                content += scores[i] + ",";
            }
            content +="]}";

            File.WriteAllText(path,content);
            return true;
        }
    }
}