using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniGame
{
    public class UnitFactory : AbstractFactory
    {
        private static List<Texture2D> tex;
        public static Unit createInstance(Vector2 pos, UnitTypeEnum type)
        {
            return createInstance(pos.X, pos.Y, type);
        }

        public static Unit createInstance(float left, float top, UnitTypeEnum type)
        {
            switch (type)
            {
                case UnitTypeEnum.ZOMBIE:
                    tex = new List<Texture2D>();
                    for (int i = 0; i < 12; i++)
                    {
                        tex.Add(Global.Content.Load<Texture2D>("zombies/" + i.ToString("00")));
                    }
                    return new Zombies(left, top, tex);
                case UnitTypeEnum.MUMMY:
                    tex = new List<Texture2D>();
                    for (int i = 0; i < 12; i++)
                    {
                        tex.Add(Global.Content.Load<Texture2D>("mummies/" + i.ToString("00")));
                    }
                    return new Mummies(left, top, tex);
                case UnitTypeEnum.SCORPION:
                    tex = new List<Texture2D>();
                    for (int i = 0; i < 12; i++)
                    {
                        tex.Add(Global.Content.Load<Texture2D>("scorpions/" + i.ToString("00")));
                    }
                    return new Scorpions(left, top, tex);
                case UnitTypeEnum.CHARACTER:
                    tex = new List<Texture2D>();
                    for (int i = 0; i < 12; i++)
                    {
                        tex.Add(Global.Content.Load<Texture2D>("scorpions/" + i.ToString("00")));
                    }
                    //
                    break;
                case UnitTypeEnum.TREASURE:
                    tex = new List<Texture2D>();
                    for (int i = 0; i < 12; i++)
                    {
                        tex.Add(Global.Content.Load<Texture2D>("scorpions/" + i.ToString("00")));
                    }
                    //
                    break;
                default:
                    return null;
            }
        }

    }
}