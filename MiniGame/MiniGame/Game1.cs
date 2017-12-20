using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MiniGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Map map;
        List<Unit> unitList = new List<Unit>();
        Unit player = null;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.IsMouseVisible = true;
            Global.Content = this.Content;
            Config.Instance.Load();
            // TODO: use this.Content to load your game content here

            map = new Map(0, 0, "map\\", 15, 20);

            Unit tmp = UnitFactory.createInstance(2, 1, UnitTypeEnum.MUMMY);

            player = UnitFactory.createInstance(map.getEntrance(), UnitTypeEnum.CHARACTER);

            unitList.Add(tmp);
            unitList.Add(UnitFactory.createInstance(3,1,UnitTypeEnum.TREASURE));
            unitList.Add(UnitFactory.createInstance(8, 10, UnitTypeEnum.TREASURE));
            unitList.Add(UnitFactory.createInstance(18, 10, UnitTypeEnum.TREASURE));
            unitList.Add(UnitFactory.createInstance(5, 5, UnitTypeEnum.SCORPION));
            unitList.Add(UnitFactory.createInstance(13, 9, UnitTypeEnum.ZOMBIE));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            Global.keyboardHelper.Update(gameTime);
            Global.mouseHelper.Update(gameTime);

            if (Global.keyboardHelper.IsKeyPressed(Keys.A))
            {
                player.setState(UnitStateEnum.MOVELEFT);
                if (map.canGo((int)player.LogicX - 1, (int)player.LogicY))
                    player.transact(player.LogicX - 1, player.LogicY);

            } else if (Global.keyboardHelper.IsKeyPressed(Keys.D))
            {
                player.setState(UnitStateEnum.MOVERIGHT);
                if (map.canGo((int)player.LogicX + 1, (int)player.LogicY))
                    player.transact(player.LogicX + 1, player.LogicY );
            }
            else if (Global.keyboardHelper.IsKeyPressed(Keys.W))
            {
                player.setState(UnitStateEnum.MOVEBACK);
                if (map.canGo((int)player.LogicX, (int)player.LogicY - 1))
                    player.transact(player.LogicX, player.LogicY - 1);
            }
            else if (Global.keyboardHelper.IsKeyPressed(Keys.S))
            {
                player.setState(UnitStateEnum.MOVEFORWAR);
                if (map.canGo((int)player.LogicX, (int)player.LogicY + 1))
                    player.transact(player.LogicX , player.LogicY + 1);
            }

            //Win game
            Vector2 exit = map.getExit();
            if (player.LogicX == exit.X && player.LogicY == exit.Y)
                Window.Title = "Win";

            // TODO: Add your update logic here
            int n = unitList.Count;
            for (int i = 0; i < n; i++)
            {
                unitList[i].Update(gameTime);
            }
            player.Update(gameTime);

            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            this.spriteBatch.Begin(SpriteSortMode.FrontToBack,BlendState.AlphaBlend);

            map.Draw(gameTime, spriteBatch);
            int n = unitList.Count;
            for(int  i =0; i< n; i++)
            {
                unitList[i].Draw(gameTime, spriteBatch);
            }

            player.Draw(gameTime, spriteBatch);

            this.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
