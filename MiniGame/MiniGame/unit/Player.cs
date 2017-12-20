using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniGame
{
    public class Player : Unit
    {
        private static Player instance = null;

        public static Player getInstance(float left, float top, List<Texture2D> textures, float depth = 0.3f)
        {
            if (instance == null)
                instance = new Player(left, top, textures, depth);
            return instance;
        }

        public Player(float left, float top, List<Texture2D> textures, float depth = 0.3f)
        {
            _model = new Sprite2D(left, top, textures, depth);

        }
    }
}