using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniGame
{
    public abstract class AbstactModel
    {
        private int state;

        public int State
        {
            get
            {
                return state;
            }

            set
            {
                state = value;
            }
        }



        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) { }

        public virtual bool IsSelected(Vector2 pos) { return false; }

        public virtual void Select() { }
    }
}