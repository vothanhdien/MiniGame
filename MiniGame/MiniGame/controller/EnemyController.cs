using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniGame.controller
{
    class EnemyController: Controller
    {
        public static List<Monster> monsters = new List<Monster>();

        public static void PlayerMove(Vector2 pos)
        {
            for(int i = 0; i < monsters.Count; i++)
            {
                monsters[i].Move(pos);
            }
        }

        public static void subscribe(Monster monster)
        {
            monsters.Add(monster);
        }
    }
}
