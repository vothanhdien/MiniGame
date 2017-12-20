using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace MiniGame
{
    public class Unit : GameVisibleEntity
    {
        
        protected float logicRow,logicCol;
        protected AbstactModel _model;
        float t = 0;
        float dt = 0.25f;
        public override void Update(GameTime gameTime)
        {
            
            if(t % 2 == 0)
            {
                _model.Update(gameTime);
                base.Update(gameTime);
            }
            t += dt;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _model.Draw(gameTime, spriteBatch);
            base.Draw(gameTime, spriteBatch);
        }
    }
}