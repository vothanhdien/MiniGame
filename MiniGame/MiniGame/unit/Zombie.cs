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
        float t = 0;
        float dt = 0.25f;
        public override void Update(GameTime gameTime)
        {
            if (t % 5 == 0)
            {
                for (float i = LogicX + 1; i < Global.map.Cols; i++)
                {
                    if (!Global.map.canGo(i, LogicY))
                    {
                        break;
                    }
                    if (Global.playerPos.X == i && LogicY == Global.playerPos.Y)
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
                    if (Global.playerPos.X == i && LogicY == Global.playerPos.Y)
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
                    if (Global.playerPos.X == LogicX && Global.playerPos.Y == i)
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
                    if (Global.playerPos.X == LogicX && Global.playerPos.Y == i)
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
                    if (Global.playerPos.X == newX && Global.playerPos.Y == newY && Global.map.canGo(LogicX+1, LogicY))
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
                    if (Global.playerPos.X == newX && Global.playerPos.Y == newY && Global.map.canGo(LogicX - 1, LogicY))
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
                    if (Global.playerPos.X == newX && Global.playerPos.Y == newY && Global.map.canGo(LogicX, LogicY + 1))
                    {
                        this.transact(LogicX , LogicY + 1);
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
                    if (Global.playerPos.X == newX && Global.playerPos.Y == newY && Global.map.canGo(LogicX, LogicY - 1))
                    {
                        this.transact(LogicX, LogicY - 1);
                        break;
                    }
                }
            }
            t += dt;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}