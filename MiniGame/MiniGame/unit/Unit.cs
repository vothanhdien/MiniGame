using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace MiniGame
{
    public class Unit : GameVisibleEntity
    {
        
        private float logicX,logicY;
        protected AbstactModel _model;
        float t = 0;
        float dt = 0.25f;
        #region properties
        public float LogicX
        {
            get
            {
                return logicX;
            }

            set
            {
                logicX = value;
            }
        }

        public float LogicY
        {
            get
            {
                return logicY;
            }

            set
            {
                logicY = value;
            }
        }
        #endregion

        public Unit(float left, float top, List<Texture2D> textures, float depth = 0.5f)
        {
            this.LogicX = left;
            this.LogicY = top;

            _model = new Sprite2D(LogicX * Global.TEXTURE_WIDTH, LogicY * Global.TEXTURE_WIDTH, textures, depth);

        }

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

        public void transact(Vector2 newpos)
        {
            transact(newpos.X, newpos.Y);
        }

        public virtual void transact(float X, float Y)
        {
            LogicX = X;
            LogicY = Y;
            _model.transact(logicX * 32, logicY * 32);
        }

        public virtual void setState(UnitStateEnum state)
        {
            _model.State = state;
        }

        public virtual bool isOverridePlayer( Vector2 playerPos)
        {
            return isOverridePlayer(playerPos.X, playerPos.Y);
        }

        public virtual bool isOverridePlayer(float X, float Y)
        {
            return this.LogicX == X && this.LogicY == Y;
        }

        public virtual string convertToJson()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            return json;
        }
    }
}