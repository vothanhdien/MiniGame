using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniGame
{
    public class Button : Component
    {
        Label lb = null;
        public Button(string strResource, string text, float left, float top, float depth)
        {
            _font = Global.loadSpriteFont("MenuText");
            this._sprite = new Sprite2D(left, top, Global.loadTextures(strResource), depth);

            float stringLeft = left + _sprite.Width / 2 - (25 * text.Length) / 2;
            float stringTop = top + 20;
            Text = text;
            float newLeft = _sprite.Left + (_sprite.Width - text.Length*12) / 2;
            lb = new Label("MenuText", text, newLeft , top, depth + 0.1f);

            //_text.Left = left + 400;
            //_text.Top = newTop;
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _sprite.Draw(gameTime, spriteBatch);
            //spriteBatch.DrawString(_font, Text, new Vector2(this._sprite.Left, _sprite.Top), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            lb.Draw(gameTime, spriteBatch);
            base.Draw(gameTime, spriteBatch);
        }
    }
}