using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniGame
{
    public class ResultDialog : AbstractDialog
    {
        public List<Component> components = new List<Component>();
        Texture2D solidTexture;
        Component playerScore;

        public ResultDialog(GraphicsDevice gd)
        {
            solidTexture = new Texture2D(gd, 1, 1);
            solidTexture.SetData(new Color[] { Color.White });


            components.Add(new Button("Button_blue", "EXIT", 450, 350, 0.6f));
            components.Add(new Button("Button_blue", "RESTART", 250, 350, 0.6f));
            components.Add(new Button("Button_blue", "SAVE", 350, 350, 0.6f));
            components.Add(new Label("MenuText", "Your score: ", 160, 120, 0.7f));
            components.Add(new Label("MenuText", "1st: ", 300, 175, 0.7f));
            components.Add(new Label("MenuText", "2nd: ", 300, 200, 0.7f));
            components.Add(new Label("MenuText", "3rd: ", 300, 225, 0.7f));
            components.Add(new Label("MenuText", "4th: ", 300, 250, 0.7f));
            components.Add(new Label("MenuText", "5th: ", 300, 275, 0.7f));

            playerScore = new Label("MenuText", "00", 300, 120, 1.0f);

        }

        public void updateResultDialog(float playerScore)
        {
            this.playerScore.Text = playerScore.ToString();
            float[] old = Config.Instance.getHighScore();
            float tmp = -1;
            bool b = true;
            for(int i = 0; i < old.Length; i++)
            {
                if(playerScore > old[i] && b)
                {
                    tmp = old[i];
                    old[i] = playerScore;
                    b = false;
                }
                else if(tmp != -1)
                {
                    float f = old[i];
                    old[i] = tmp;
                    tmp = f;
                    b = false;
                }
            }

            components[4].Text = "1st: "+ old[0];
            components[5].Text = "2nd: "+ old[1];
            components[6].Text = "3rd: "+ old[2];
            components[7].Text = "4th: "+ old[3];
            components[8].Text = "5th: "+ old[4];

            Config.Instance.saveHighScore(old);
        }

 
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(solidTexture, new Rectangle(150, 100, 500, 300), null,Color.DarkViolet,0f,Vector2.Zero,SpriteEffects.None,0.5f);
            int n = components.Count;
            for (int i = 0; i < n; i++)
            {
                components[i].Draw(gameTime, spriteBatch);
            }
            //
            playerScore.Draw(gameTime, spriteBatch);
            base.Draw(gameTime, spriteBatch);
        }

        public int getOption(Vector2 pos)
        {
            for (int i = 0; i < 3; i++)
            {
                if (components[i].isSelected(pos))
                    return i;
            }

            return -1;
        }
    }
}