using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniGame
{
    public class Scorpion : Monster
    {
        public Scorpion(float left, float top, List<Texture2D> textures, float depth = 0.3F) : base(left, top, textures, depth)
        {
        }

        public override void Move(Vector2 pos)
        {
            base.Move(pos);
            float newX = LogicX;
            float newY = LogicY;
            while (true)
            {
                newX = newX + 1;
                newY = newY + 1;
                if (!Global.map.canGo(newX, newY))
                {
                    break;
                }
                if (pos.X == newX && pos.Y == newY)
                {
                    this.transact(LogicX + 1, LogicY + 1);
                    break;
                }
            }
            newX = LogicX;
            newY = LogicY;
            while (true)
            {
                newX = newX - 1;
                newY = newY - 1;
                if (!Global.map.canGo(newX, newY))
                {
                    break;
                }
                if (pos.X == newX && pos.Y == newY)
                {
                    this.transact(LogicX - 1, LogicY - 1);
                    break;
                }
            }
            newX = LogicX;
            newY = LogicY;
            while (true)
            {
                newX = newX - 1;
                newY = newY + 1;
                if (!Global.map.canGo(newX, newY))
                {
                    break;
                }
                if (pos.X == newX && pos.Y == newY)
                {
                    this.transact(LogicX - 1, LogicY + 1);
                    break;
                }
            }
            newX = LogicX;
            newY = LogicY;
            while (true)
            {
                newX = newX + 1;
                newY = newY - 1;
                if (!Global.map.canGo(newX, newY))
                {
                    break;
                }
                if (pos.X == newX && pos.Y == newY)
                {
                    this.transact(LogicX + 1, LogicY - 1);
                    break;
                }
            }
        }

        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
        public override void transact(float X, float Y)
        {
            Global.ghostSound.Play();
            base.transact(X, Y);
        }
    }
}