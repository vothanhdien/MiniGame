using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniGame
{
    public class InputTextDialog : AbstractDialog
    {
        public List<Component> components = new List<Component>();
        Texture2D solidTexture;
        string text = "";
        Component textComponent;

        public InputTextDialog(GraphicsDevice gd)
        {
            solidTexture = new Texture2D(gd, 1, 1);
            solidTexture.SetData(new Color[] { Color.White });


            components.Add(new Button("Button_blue", "OK", 370, 300, 0.9f));
            components.Add(new Label("MenuText", "Enter your name", 270, 200, 1.0f));
            textComponent = new Label("MenuText", "", 270, 250, 1.0f);
        }
        public void setText(String text)
        {
            textComponent.Text = text;
        }

        public void appendText(string text)
        {
            textComponent.Text += text;
        }
        public void appendText(char c)
        {
            textComponent.Text += c;
        }
        public string getText()
        {
            return text;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(solidTexture, new Rectangle(250, 200, 300, 150), null, Color.YellowGreen, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
            int n = components.Count;
            for (int i = 0; i < n; i++)
            {
                components[i].Draw(gameTime, spriteBatch);
            }
            textComponent.Draw(gameTime, spriteBatch);
            base.Draw(gameTime, spriteBatch);
        }

        public int getOption(Vector2 pos)
        {
            for (int i = 0; i < 1; i++)
            {
                if (components[i].isSelected(pos))
                    return i;
            }
            return -1;
        }
    }
}