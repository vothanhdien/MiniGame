using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniGame
{
    public class Mummy : Monster
    {
        public Mummy(float left, float top, List<Texture2D> textures, float depth = 0.3F) : base(left, top, textures, depth)
        {
        }

        float t = 0;
        float dt = 0.25f;

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
        }

        public override void Update(GameTime gameTime)
        {
            //if (t % 5 == 0)
            //{
            //    for (float i = LogicX + 1; i < Global.map.Cols; i++)
            //    {
            //        if (!Global.map.canGo(i, LogicY))
            //        {
            //            break;
            //        }
            //        if (Global.playerPos.X == i && LogicY == Global.playerPos.Y)
            //        {
            //            this.transact(LogicX + 1, LogicY);
            //            break;
            //        }
            //    }
            //    for (float i = LogicX - 1; i > 0; i--)
            //    {
            //        if (!Global.map.canGo(i, LogicY))
            //        {
            //            break;
            //        }
            //        if (Global.playerPos.X == i && LogicY == Global.playerPos.Y)
            //        {
            //            this.transact(LogicX - 1, LogicY);
            //            break;
            //        }
            //    }
            //    for (float i = LogicY + 1; i < Global.map.Rows; i++)
            //    {
            //        if (!Global.map.canGo(LogicX, i))
            //        {
            //            break;
            //        }
            //        if (Global.playerPos.X == LogicX && Global.playerPos.Y == i)
            //        {
            //            this.transact(LogicX, LogicY + 1);
            //            break;
            //        }
            //    }
            //    for (float i = LogicY - 1; i > 0; i--)
            //    {
            //        if (!Global.map.canGo(LogicX, i))
            //        {
            //            break;
            //        }
            //        if (Global.playerPos.X == LogicX && Global.playerPos.Y == i)
            //        {
            //            this.transact(LogicX, LogicY - 1);
            //            break;
            //        }
            //    }
            //}
            t += dt;
            base.Update(gameTime);
        } 

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public override string convertToJson()
        {
            return base.convertToJson();
        }

        public override void transact(float X, float Y)
        {
            Global.ghostSound.Play();
            base.transact(X, Y);
        }
    }
}