using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniGame
{
    public class UnitFactory : AbstractFactory
    {
        private static List<Texture2D> texs;
        public static Unit createInstance(Vector2 pos, UnitTypeEnum type)
        {
            return createInstance(pos.X, pos.Y, type);
        }

        public static Unit createInstance(float X, float Y, UnitTypeEnum type)
        {
            switch (type)
            {
                case UnitTypeEnum.ZOMBIE:
                    texs = Global.loadTextures("Zombies");
                    return new Zombie(X, Y, texs, 0.3f);
                case UnitTypeEnum.MUMMY:
                    texs = Global.loadTextures("Mummies");
                    return new Mummy(X, Y, texs, 0.3f);
                case UnitTypeEnum.SCORPION:
                    texs = Global.loadTextures("Scorpions");
                    return new Scorpion(X, Y, texs, 0.3f);
                case UnitTypeEnum.CHARACTER:
                    texs = Global.loadTextures("Player");
                    return Player.getInstance(X, Y, texs,0.25f);
                case UnitTypeEnum.JEWELRY:
                    texs = Global.loadTextures("Treasure");
                    return new Treasure(X, Y, texs,0.2f);
                case UnitTypeEnum.WEAPON:
                    texs = Global.loadTextures("Sword");
                    return new Treasure(X, Y, texs, 0.2f);
                case UnitTypeEnum.TOOL:
                    texs = Global.loadTextures("Tool");
                    return new Treasure(X, Y, texs, 0.2f);
                case UnitTypeEnum.STATURE:
                    texs = Global.loadTextures("Statue");
                    return new Treasure(X, Y, texs, 0.2f);
                default:
                    return null;
            }
        }

    }
}