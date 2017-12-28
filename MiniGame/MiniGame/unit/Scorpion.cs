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

        float t = 0;
        float dt = 0.25f;
        public override void Update(GameTime gameTime)
        {
            if (t % 5 == 0)
            {
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
                    if (Global.playerPos.X == newX && Global.playerPos.Y == newY)
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
                    if (Global.playerPos.X == newX && Global.playerPos.Y == newY)
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
                    if (Global.playerPos.X == newX && Global.playerPos.Y == newY)
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
                    if (Global.playerPos.X == newX && Global.playerPos.Y == newY)
                    {
                        this.transact(LogicX + 1, LogicY - 1);
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
        public override void transact(float X, float Y)
        {
            Global.ghostSound.Play();
            base.transact(X, Y);
        }
    }
}