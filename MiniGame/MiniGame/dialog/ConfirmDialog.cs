using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniGame
{
    public class ConfirmDialog : AbstractDialog
    {
        public List<Component> components = new List<Component>();
        Texture2D solidTexture;

        public ConfirmDialog(GraphicsDevice gd)
        {
            solidTexture = new Texture2D(gd, 1, 1);
            solidTexture.SetData(new Color[] { Color.White });


            components.Add(new Button("Button_blue", "NO", 450, 300, 0.8f));
            components.Add(new Button("Button_blue", "YES", 270, 300, 0.8f));
            components.Add(new Label("MenuText", "123", 270, 200, 1.0f));

        }
        public void setText(String text)
        {
            components[2].Text = text;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(solidTexture, new Rectangle(250, 200, 300, 150), null, Color.YellowGreen, 0f, Vector2.Zero, SpriteEffects.None, 0.5f);
            int n = components.Count;
            for (int i = 0; i < n; i++)
            {
                components[i].Draw(gameTime, spriteBatch);
            }
            base.Draw(gameTime, spriteBatch);
        }

        public int getOption(Vector2 pos)
        {
            for (int i = 0; i < 2; i++)
            {
                if (components[i].isSelected(pos))
                    return i;
            }
            return -1;
        }
    }
}