using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniGame
{
    public class Zombie : Unit
    {
        
        public Zombie(float left, float top, List<Texture2D> textures, float depth = 0.3f)
        {
            _model = new Sprite2D(left, top, textures, depth);
            
        }
        float t = 0;
        float dt = 0.02f;
        float d = 5;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}