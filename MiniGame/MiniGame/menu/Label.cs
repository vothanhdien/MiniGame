using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniGame
{
    public class Label : Component
    {
        public Label(string font,string text, float left, float top, float depth)
        {
            this.Text = text;
            this._font = Global.loadSpriteFont(font);
            this._left = left;
            this._top = top;
            this.Depth = depth;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, Text, new Vector2(_left, _top), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, this.Depth);
            base.Draw(gameTime, spriteBatch);
        }

        public void aksjdbaskjdbasjkd()
        {

        }
    }
}