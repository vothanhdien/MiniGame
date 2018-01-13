using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniGame.controller;
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

        private float totalWeight = 0;

        private Label playerName;

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
            
            if (tr.Weight + totalWeight > 50)
            {
                return false;
            }
            TreaseList.Add(tr);
            totalWeight += tr.Weight;
            return true;
        }

        public void setPlayerName (string name)
        {
            playerName.Text = name;
        }

        public Player(float left, float top, List<Texture2D> textures, float depth = 0.3F) : base(left, top, textures, depth)
        {
            playerName = new Label("MenuText", "", left, top - 10 ,0.3f);
        }

        public static Player getInstance(float left, float top, List<Texture2D> textures, float depth = 0.3f)
        {
            if (instance == null)
                instance = new Player(left, top, textures, depth);
            return instance;
        }

        public void reloadPlayer(Vector2 pos)
        {
            reloadPlayer(pos.X, pos.Y);
        }

        public void reloadPlayer(float left, float top)
        {
            this.LogicX = left;
            this.LogicY = top;
            this.TreaseList.RemoveAll(v => true);
            this.totalStep = 0;
            this.totalWeight = 0;
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
            playerName.Draw(gameTime, spriteBatch);
            base.Draw(gameTime, spriteBatch);
        }

        public override void transact(float X, float Y)
        {
            TotalStep++;
            playerName.transact(X * Global.TEXTURE_WIDTH, Y * Global.TEXTURE_WIDTH - 10);
            EnemyController.PlayerMove(new Vector2(X,Y));
            base.transact(X, Y);
        }

        public float getTotalWeight()
        {
            return totalWeight;
        }
    }
}