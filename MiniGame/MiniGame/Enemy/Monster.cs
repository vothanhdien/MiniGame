using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniGame.controller;

namespace MiniGame
{
    public class Monster : Unit
    {

        public Monster(float left, float top, List<Texture2D> textures, float depth = 0.3F) : base(left, top, textures, depth)
        {
            EnemyController.subscribe(this);
        }

        public virtual void Move(Vector2 pos)
        {
            
        }
    }
}