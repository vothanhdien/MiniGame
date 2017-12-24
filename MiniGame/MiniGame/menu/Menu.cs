using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniGame
{
    public class Menu : GameVisibleEntity
    {
        public List<Component> components = new List<Component>();
        List<Texture2D> background = new List<Texture2D>();

        public Menu()
        {

            background = Global.loadTextures("background");
            components.Add(new Button("Circle_button", "", 450, 300, 0.8f));
            components.Add(new Button("Button_blue", "YES", 270, 300, 0.8f));
            components.Add(new Label("MenuText", "MINI PROJECT: CRAZY MAZE", 270, 200, 0.9f));

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background[0], new Rectangle(0, 0, 300, 150), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.7f);

            int n = components.Count;
            for (int i = 0; i < n; i++)
            {
                components[i].Draw(gameTime, spriteBatch);
            }
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