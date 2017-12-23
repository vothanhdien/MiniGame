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

        public Button(string strResource, string text, float left, float top, float depth)
        {
            _font = Global.loadSpriteFont("MenuText");
            this._sprite = new Sprite2D(left, top, Global.loadTextures(strResource), depth);

            float stringLeft = left + _sprite.Width / 2 - (25 * text.Length) / 2;
            float stringTop = top + 20;
            Text = text;

            //float newLeft = _sprite.Left + (_sprite.Left + _sprite.With)/2 - _text.Width / 2;
            //float newTop = _text.Top + 20;

            //_text.Left = left + 400;
            //_text.Top = newTop;
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _sprite.Draw(gameTime, spriteBatch);
            //spriteBatch.Draw(this.Textures[_iTexture], new Rectangle((int)Left, (int)Top, (int)(Width / scale), (int)(Height / scale)), null, Color, 0f, Vector2.Zero, SpriteEffects.None, _depth);
            spriteBatch.DrawString(_font, Text, new Vector2(this._sprite.Left, _sprite.Top), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, Depth + 0.1f);
            base.Draw(gameTime, spriteBatch);
        }
    }
}