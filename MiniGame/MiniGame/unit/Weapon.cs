using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace MiniGame
{
    public class Weapon : Treasure
    {
        public Weapon(float left, float top, List<Texture2D> textures, float depth = 0.3F) : base(left, top, textures, depth)
        {
        }
    }
}