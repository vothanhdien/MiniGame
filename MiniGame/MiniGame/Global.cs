using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MiniGame
{
    public class Global
    {
        public static ContentManager Content;
        public static KeyboardHelper keyboardHelper = new KeyboardHelper();
        public static MouseHelper mouseHelper = new MouseHelper();

        public static string CONFIG_DP = "config.json";
        public static string APP_PATH = getpath();
        public static float TEXTURE_WIDTH = 32;

        public static Vector2 playerPos = Vector2.Zero;

        private static string getpath()
        {
            var codeBase = Assembly.GetExecutingAssembly();
            //var exeBase = new UriBuilder(codeBase).Path;
            return Path.GetDirectoryName(codeBase.Location);
        }

        public static List<Texture2D> loadTexture(string strResource)
        {
            List<Texture2D> res = new List<Texture2D>();
            string[] listTexture = Config.LoadUnitTextures(strResource);
            for (int i = 0; i < listTexture.Length; i++)
            {
                res.Add(Content.Load<Texture2D>(listTexture[i]));
            }

            return res;

        }
    }
}