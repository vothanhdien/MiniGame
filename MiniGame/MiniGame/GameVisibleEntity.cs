using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniGame
{
    public abstract class GameVisibleEntity : GameEntity
    {
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //_model.Draw(gameTime, spriteBatch);
        }
    }
}