using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniGame
{
    public class Mummy : Unit
    {
        public Mummy(float left, float top, List<Texture2D> textures, float depth = 0.3F) : base(left, top, textures, depth)
        {
        }

        //public Mummy(float left, float top, List<Texture2D> textures, float depth = 0.3f)
        //{
        //    this.LogicX = left;
        //    this.LogicY = top;

        //    _model = new Sprite2D(LogicX*32, LogicY*32, textures, depth);

        //}

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}