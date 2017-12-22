using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniGame
{
    public class Player : Unit
    {
        private static Player instance = null;

        private List<Treasure> treaseList = new List<Treasure>();

        private int totalStep = 0;

        public List<Treasure> TreaseList
        {
            get
            {
                return treaseList;
            }

            set
            {
                treaseList = value;
            }
        }

        public int TotalStep
        {
            get
            {
                return totalStep;
            }

            set
            {
                totalStep = value;
            }
        }

        public bool collectTreasure(Treasure tr)
        {
            if (TreaseList.Count >= 6)
            {
                return false;
            }
           TreaseList.Add(tr);
            return true;
        }

        public Player(float left, float top, List<Texture2D> textures, float depth = 0.3F) : base(left, top, textures, depth)
        {
        }

        public static Player getInstance(float left, float top, List<Texture2D> textures, float depth = 0.3f)
        {
            if (instance == null)
                instance = new Player(left, top, textures, depth);
            return instance;
        }

        //public Player(float left, float top, List<Texture2D> textures, float depth = 0.3f)
        //{
        //    //this.LogicRow = left;
        //    //this.LogicCol = top;

        //    //_model = new Sprite2D(LogicRow * 32, LogicCol * 32, textures, depth);

        //}

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
            TotalStep++;
            base.transact(X, Y);
        }


    }
}