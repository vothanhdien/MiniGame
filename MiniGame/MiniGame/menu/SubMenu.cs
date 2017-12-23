﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniGame
{
    public class SubMenu : GameVisibleEntity
    {
        public List<Component> components = new List<Component>();
        Texture2D solidTexture;
        Component weight;
        Component step;
        Component total;

        public SubMenu(GraphicsDevice gd)
        {
            solidTexture = new Texture2D(gd, 1, 1);
            solidTexture.SetData(new Color[] { Color.White });


            components.Add(new Button("Button_blue", "EXIT", 680, 200, 0.8f));
            components.Add(new Label("MenuText", "Treasures: ", 650, 25, 1.0f));
            components.Add(new Label("MenuText", "trs", 750, 50, 1.0f));
            components.Add(new Label("MenuText", "Total weigh: ", 650, 75, 1.0f));
            components.Add(new Label("MenuText", "kg", 750, 100, 1.0f));
            components.Add(new Label("MenuText", "Total step: ", 650, 125, 1.0f));
            components.Add(new Label("MenuText", "step", 750, 150, 1.0f));

            total = new Label("MenuText", "00", 680, 50, 1.0f);
            weight = new Label("MenuText", "00", 680, 100, 1.0f);
            step = new Label("MenuText", "0", 680, 150, 1.0f);
            
        }

        public void updateTotalWeight(float weight)
        {
            this.weight.Text = weight.ToString("00.00");
        }

        public void updateTotalStep(int Step)
        {
            this.step.Text = Step.ToString();
        }
        public void updateTotalTreasure(int total)
        {
            this.total.Text = total.ToString();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(solidTexture,new Rectangle(650, 0,600,400), Color.Navy);
            int n = components.Count;
            for(int i = 0; i < n; i++)
            {
                components[i].Draw(gameTime, spriteBatch);
            }
            //
            weight.Draw(gameTime, spriteBatch);
            step.Draw(gameTime, spriteBatch);
            total.Draw(gameTime, spriteBatch);
            base.Draw(gameTime, spriteBatch);
        }
    }
}