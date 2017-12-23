using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniGame
{
    public class Component : GameVisibleEntity
    {
        protected Sprite2D _sprite = null;
        private string text;
        protected SpriteFont _font;

        protected float _top;
        protected float _left;

        private float depth;

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        //public virtual void setText(string text)
        //{
        //    Text = te
        //}
    
        public float Depth
        {
            get
            {
                return depth;
            }

            set
            {
                depth = value;
            }
        }

        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
            }
        }
    }
}