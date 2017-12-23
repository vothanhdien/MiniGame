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
        List<Unit> monsterList = new List<Unit>();
        List<Treasure> treasureList = new List<Treasure>();
        Player player = null;
        GameStateEnum currentGameState = GameStateEnum.GAME_PLAYING;
        SubMenu subMenu;
        Random random;
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
            random = new Random();
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.IsMouseVisible = true;
            //Global.Content = this.Content;
            Global.init(this.Content);
            Config.Instance.Load();
            // TODO: use this.Content to load your game content here

            Global.map = new Map(0, 0, "map\\", 15, 20);

            subMenu = new SubMenu(this.GraphicsDevice);

            player = (Player)UnitFactory.createInstance(Global.map.getEntrance(), UnitTypeEnum.CHARACTER);


            List<Vector2> a = Global.map.getListRoad();

            monsterList.Add(UnitFactory.createInstance(a[random.Next(a.Count)], UnitTypeEnum.ZOMBIE));
            monsterList.Add(UnitFactory.createInstance(a[random.Next(a.Count)], UnitTypeEnum.MUMMY));
            monsterList.Add(UnitFactory.createInstance(a[random.Next(a.Count)], UnitTypeEnum.SCORPION));
            treasureList.Add((Treasure)UnitFactory.createInstance(a[random.Next(a.Count)], UnitTypeEnum.TREASURE));
            treasureList.Add((Treasure)UnitFactory.createInstance(a[random.Next(a.Count)], UnitTypeEnum.TREASURE));
            treasureList.Add((Treasure)UnitFactory.createInstance(a[random.Next(a.Count)], UnitTypeEnum.TREASURE));
            treasureList.Add((Treasure)UnitFactory.createInstance(a[random.Next(a.Count)], UnitTypeEnum.TREASURE));
            treasureList.Add((Treasure)UnitFactory.createInstance(a[random.Next(a.Count)], UnitTypeEnum.TREASURE));
            treasureList.Add((Treasure)UnitFactory.createInstance(a[random.Next(a.Count)], UnitTypeEnum.TREASURE));
            treasureList.Add((Treasure)UnitFactory.createInstance(a[random.Next(a.Count)], UnitTypeEnum.TREASURE));
            treasureList.Add((Treasure)UnitFactory.createInstance(a[random.Next(a.Count)], UnitTypeEnum.TREASURE));
            treasureList.Add((Treasure)UnitFactory.createInstance(a[random.Next(a.Count)], UnitTypeEnum.TREASURE));
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

            Window.Title = player.LogicX + " : " + player.LogicY;
            if (currentGameState == GameStateEnum.GAME_PAUSE || currentGameState == GameStateEnum.GAME_END)
            {
                Window.Title = " all step " + player.TotalStep + " treasuse: " + player.TreaseList.Count;
                return;
            }

            Global.playerPos.X = player.LogicX;
            Global.playerPos.Y = player.LogicY;


            Global.keyboardHelper.Update(gameTime);
            Global.mouseHelper.Update(gameTime);

            if (Global.keyboardHelper.IsKeyPressed(Keys.A))
            {
                player.setState(UnitStateEnum.MOVELEFT);
                if (Global.map.canGo((int)player.LogicX - 1, (int)player.LogicY))
                    player.transact(player.LogicX - 1, player.LogicY);

            } else if (Global.keyboardHelper.IsKeyPressed(Keys.D))
            {
                player.setState(UnitStateEnum.MOVERIGHT);
                if (Global.map.canGo((int)player.LogicX + 1, (int)player.LogicY))
                    player.transact(player.LogicX + 1, player.LogicY );
            }
            else if (Global.keyboardHelper.IsKeyPressed(Keys.W))
            {
                player.setState(UnitStateEnum.MOVEBACK);
                if (Global.map.canGo((int)player.LogicX, (int)player.LogicY - 1))
                    player.transact(player.LogicX, player.LogicY - 1);
            }
            else if (Global.keyboardHelper.IsKeyPressed(Keys.S))
            {
                player.setState(UnitStateEnum.MOVEFORWAR);
                if (Global.map.canGo((int)player.LogicX, (int)player.LogicY + 1))
                    player.transact(player.LogicX , player.LogicY + 1);
            }

            subMenu.updateTotalStep(player.TotalStep);

            //Win game
            Vector2 exit = Global.map.getExit();
            if (player.LogicX == exit.X && player.LogicY == exit.Y)
                Window.Title = "Win";

            // TODO: Add your update logic here
            int n = monsterList.Count;
            for (int i = 0; i < n; i++)
            {
                monsterList[i].Update(gameTime);
                if (monsterList[i].isOverridePlayer(player.LogicX, player.LogicY)){
                    Window.Title = "you die";
                    currentGameState = GameStateEnum.GAME_END;
                }
            }
            player.Update(gameTime);

            int tn = treasureList.Count;
            for(int i = 0; i< tn; i++)
            {
                treasureList[i].Update(gameTime);
                if (treasureList[i].isOverridePlayer(player.LogicX, player.LogicY)){
                    if(player.collectTreasure(treasureList[i])){
                        Window.Title = "you collect a tresure";
                        treasureList.RemoveAt(i);
                        subMenu.updateTotalWeight(player.getTotalWeight());
                        tn--;
                    }
                    else
                    {
                        Window.Title = "your bag is full";
                    }
                }
            }
            
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

            subMenu.Draw(gameTime, spriteBatch);
            Global.map.Draw(gameTime, spriteBatch);
            int n = monsterList.Count;
            for(int  i =0; i< n; i++)
            {
                monsterList[i].Draw(gameTime, spriteBatch);
            }

            n = treasureList.Count;
            for (int i = 0; i < n; i++)
            {
                treasureList[i].Draw(gameTime, spriteBatch);
            }

            player.Draw(gameTime, spriteBatch);
            this.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
