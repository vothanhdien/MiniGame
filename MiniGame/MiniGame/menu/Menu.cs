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

            background = Global.loadTextures("Background");
            components.Add(new Button("Circle_button", "", 350, 150, 0.8f));
            components.Add(new Label("MenuText", "MINI PROJECT: CRAZY MAZE", 270, 100, 0.9f));

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background[0], new Rectangle(0, 0, Global.WINDOW_WIDTH, Global.WINDOW_HEIGHT), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.75f);

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