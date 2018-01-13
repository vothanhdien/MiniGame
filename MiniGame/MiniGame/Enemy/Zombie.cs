using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniGame
{
    public class Zombie : Monster
    {
        public Zombie(float left, float top, List<Texture2D> textures, float depth = 0.3F) : base(left, top, textures, depth)
        {
        }
        public override void Move(Vector2 pos)
        {
            base.Move(pos);
            for (float i = LogicX + 1; i < Global.map.Cols; i++)
            {
                if (!Global.map.canGo(i, LogicY))
                {
                    break;
                }
                if (pos.X == i && LogicY == pos.Y)
                {
                    this.transact(LogicX + 1, LogicY);
                    break;
                }
            }
            for (float i = LogicX - 1; i > 0; i--)
            {
                if (!Global.map.canGo(i, LogicY))
                {
                    break;
                }
                if (pos.X == i && LogicY == pos.Y)
                {
                    this.transact(LogicX - 1, LogicY);
                    break;
                }
            }
            for (float i = LogicY + 1; i < Global.map.Rows; i++)
            {
                if (!Global.map.canGo(LogicX, i))
                {
                    break;
                }
                if (pos.X == LogicX && pos.Y == i)
                {
                    this.transact(LogicX, LogicY + 1);
                    break;
                }
            }
            for (float i = LogicY - 1; i > 0; i--)
            {
                if (!Global.map.canGo(LogicX, i))
                {
                    break;
                }
                if (pos.X == LogicX && pos.Y == i)
                {
                    this.transact(LogicX, LogicY - 1);
                    break;
                }
            }
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
                if (pos.X == newX && pos.Y == newY && Global.map.canGo(LogicX + 1, LogicY))
                {
                    this.transact(LogicX + 1, LogicY);
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
                if (pos.X == newX && pos.Y == newY && Global.map.canGo(LogicX - 1, LogicY))
                {
                    this.transact(LogicX - 1, LogicY);
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
                if (pos.X == newX && pos.Y == newY && Global.map.canGo(LogicX, LogicY + 1))
                {
                    this.transact(LogicX, LogicY + 1);
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
                if (pos.X == newX && pos.Y == newY && Global.map.canGo(LogicX, LogicY - 1))
                {
                    this.transact(LogicX, LogicY - 1);
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
            Global.zombieSound.Play();
            base.transact(X, Y);
        }
    }
}