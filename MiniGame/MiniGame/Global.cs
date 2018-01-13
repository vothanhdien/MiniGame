using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
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
        public static Map map;
        private static ContentManager Content;
        public static KeyboardHelper keyboardHelper = new KeyboardHelper();
        public static MouseHelper mouseHelper = new MouseHelper();

        public static SoundEffect ghostSound, zombieSound;
        public static int WINDOW_WIDTH;
        public static int WINDOW_HEIGHT;

        public static void init(ContentManager content)
        {
            Content = content;
        }

        public static float TEXTURE_WIDTH = 32;

        //public static Vector2 playerPos = Vector2.Zero;


        public static List<Texture2D> loadTextures(string strResource)
        {
            List<Texture2D> res = new List<Texture2D>();
            string[] listTexture = Config.LoadUnitTextures(strResource);
            for (int i = 0; i < listTexture.Length; i++)
            {
                res.Add(Content.Load<Texture2D>(listTexture[i]));
            }

            return res;

        }

        public static SpriteFont loadSpriteFont(string strResoure)
        {
            string font = Config.LoadSpriteFont(strResoure);
            return Content.Load<SpriteFont>(strResoure);
        }

        public static SoundEffect loadSoundEffect(string strResoure)
        {
            string effect = Config.LoadSoundEffect(strResoure);
            return Content.Load<SoundEffect>(effect);
        }

        public static Song loadSong()
        {
            return Content.Load<Song>("song/happy");
        }
    }
}