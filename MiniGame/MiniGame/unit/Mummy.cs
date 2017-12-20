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
        public Mummy(float left, float top, List<Texture2D> textures, float depth = 0.3f)
        {
            this.logicCol = top;
            this.logicRow = left;



            _model = new Sprite2D(logicCol*32, logicRow*32, textures, depth);
            
        }

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