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
        GameStateEnum currentGameState = GameStateEnum.GAME_MENU;
        SubMenu subMenu;
        Random random;
        MissionLog log;
        ResultDialog resultDialog;
        ConfirmDialog confirmDialog;
        Menu menu;

        private void load(int rows, int cols, int numZombies, int numMummies, int numScorpions, int numTreasuress)
        {
            monsterList.RemoveAll(v => true);
            treasureList.RemoveAll(v => true);
            Global.map = new Map(0, 0, "map\\", rows, cols);
            if (player == null)
                player = (Player)UnitFactory.createInstance(Global.map.getEntrance(), UnitTypeEnum.CHARACTER);
            else
                player.reloadPlayer(Global.map.getEntrance());
            List<Vector2> a = Global.map.getListRoad();

            for(int i = 0; i < numZombies; i++)
                monsterList.Add(UnitFactory.createInstance(a[random.Next(a.Count)], UnitTypeEnum.ZOMBIE));
            for (int i = 0; i < numMummies; i++)
                monsterList.Add(UnitFactory.createInstance(a[random.Next(a.Count)], UnitTypeEnum.MUMMY));
            for (int i = 0; i < numScorpions; i++)
                monsterList.Add(UnitFactory.createInstance(a[random.Next(a.Count)], UnitTypeEnum.SCORPION));
            for (int i = 0; i < numTreasuress; i++)
                treasureList.Add((Treasure)UnitFactory.createInstance(a[random.Next(a.Count)], UnitTypeEnum.TREASURE));
            //treasureList.Add((Treasure)UnitFactory.createInstance(a[random.Next(a.Count)], UnitTypeEnum.TREASURE));
            //treasureList.Add((Treasure)UnitFactory.createInstance(a[random.Next(a.Count)], UnitTypeEnum.TREASURE));
            //treasureList.Add((Treasure)UnitFactory.createInstance(a[random.Next(a.Count)], UnitTypeEnum.TREASURE));
            //treasureList.Add((Treasure)UnitFactory.createInstance(a[random.Next(a.Count)], UnitTypeEnum.TREASURE));
            //treasureList.Add((Treasure)UnitFactory.createInstance(a[random.Next(a.Count)], UnitTypeEnum.TREASURE));
            //treasureList.Add((Treasure)UnitFactory.createInstance(a[random.Next(a.Count)], UnitTypeEnum.TREASURE));
            //treasureList.Add((Treasure)UnitFactory.createInstance(a[random.Next(a.Count)], UnitTypeEnum.TREASURE));
            //treasureList.Add((Treasure)UnitFactory.createInstance(a[random.Next(a.Count)], UnitTypeEnum.TREASURE));



            log.init(Global.map, treasureList, monsterList);
        }

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
            Global.WINDOW_WIDTH = GraphicsDevice.Viewport.Width;
            Global.WINDOW_HEIGHT = GraphicsDevice.Viewport.Height;


            subMenu = new SubMenu(this.GraphicsDevice);
            resultDialog = new ResultDialog(this.GraphicsDevice);
            confirmDialog = new ConfirmDialog(this.GraphicsDevice);
            menu = new Menu();
            log = new MissionLog();

            load(15, 20, 2, 2, 2, 8);
            
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || currentGameState == GameStateEnum.WINDOW_CLOSE)
                this.Exit();

            Global.playerPos.X = player.LogicX;
            Global.playerPos.Y = player.LogicY;
            Global.keyboardHelper.Update(gameTime);
            Global.mouseHelper.Update(gameTime);

            Window.Title = player.LogicX + " : " + player.LogicY;
            if (currentGameState == GameStateEnum.GAME_PAUSE)
            {
                //Window.Title = " all step " + player.TotalStep + " treasuse: " + player.TreaseList.Count;
                return;
            }
            #region show menu
            if(currentGameState == GameStateEnum.GAME_MENU)
            {
                if (Global.mouseHelper.isLButtonUp())
                {
                    if (menu.getOption(Global.mouseHelper.getCurrentMousePosition()) == 0)
                        currentGameState = GameStateEnum.GAME_LOAD;
                    
                }
            }
            #endregion

            #region game load
            if (currentGameState == GameStateEnum.GAME_LOAD)
            {
                load(15, 20, 2, 2, 2, 8);
                player.transact(Global.map.getEntrance());
                currentGameState = GameStateEnum.GAME_START;
            }
            #endregion

            #region game start
            if (currentGameState == GameStateEnum.GAME_START)
            {
                player.Update(gameTime);
                if (Global.keyboardHelper.isPressAnykey())
                    currentGameState = GameStateEnum.GAME_PLAYING;
            }
            #endregion

            #region game playing
            if (currentGameState == GameStateEnum.GAME_PLAYING)
            {
                if (Global.keyboardHelper.IsKeyPressed(Keys.A))
                {
                    player.setState(UnitStateEnum.MOVELEFT);
                    if (Global.map.canGo((int)player.LogicX - 1, (int)player.LogicY))
                        player.transact(player.LogicX - 1, player.LogicY);

                }
                else if (Global.keyboardHelper.IsKeyPressed(Keys.D))
                {
                    player.setState(UnitStateEnum.MOVERIGHT);
                    if (Global.map.canGo((int)player.LogicX + 1, (int)player.LogicY))
                        player.transact(player.LogicX + 1, player.LogicY);
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
                        player.transact(player.LogicX, player.LogicY + 1);
                }

                subMenu.updateTotalStep(player.TotalStep);
                log.outJsonFile("out.json");

                //Win game
                Vector2 exit = Global.map.getExit();
                if (player.LogicX == exit.X && player.LogicY == exit.Y)
                {
                    currentGameState = GameStateEnum.GAME_END;

                }

                // TODO: Add your update logic here
                int n = monsterList.Count;
                for (int i = 0; i < n; i++)
                {
                    monsterList[i].Update(gameTime);
                    if (monsterList[i].isOverridePlayer(player.LogicX, player.LogicY))
                    {
                        currentGameState = GameStateEnum.GAME_END;
                    }
                }
                player.Update(gameTime);

                int tn = treasureList.Count;
                for (int i = 0; i < tn; i++)
                {
                    treasureList[i].Update(gameTime);
                    if (treasureList[i].isOverridePlayer(player.LogicX, player.LogicY))
                    {
                        if (player.collectTreasure(treasureList[i]))
                        {
                            treasureList.RemoveAt(i);
                            subMenu.updateTotalWeight(player.getTotalWeight());
                            subMenu.updateTotalTreasure(player.TreaseList.Count);
                            tn--;
                        }
                    }
                }
            }
            #endregion

            #region game end
            if (currentGameState == GameStateEnum.GAME_END)
            {
                float playerScore = 0;
                if(player.TreaseList.Count > 1 && player.isOverridePlayer(Global.map.getExit()))
                {
                    int count = Global.map.getListRoad().Count;
                    float score = 3 * count - player.TotalStep;
                    playerScore = score > 0 ? score : 0;
                }
                resultDialog.updateResultDialog(playerScore);
                currentGameState = GameStateEnum.SHOW_RESULT;
            }
            #endregion

            #region show result
            if (currentGameState == GameStateEnum.SHOW_RESULT)
            {
                if (Global.mouseHelper.isLButtonUp())
                {
                    int result = resultDialog.getOption(Global.mouseHelper.getCurrentMousePosition());
                    switch (result)
                    {
                        case 0:
                            currentGameState = GameStateEnum.WINDOW_CLOSE;
                            break;
                        case 1:
                            currentGameState = GameStateEnum.GAME_LOAD;
                            break;
                    }
                }

            }
            #endregion

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
            this.spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            if (currentGameState == GameStateEnum.GAME_MENU)
            {
                menu.Draw(gameTime, spriteBatch);
            }

            #region draw unit
            subMenu.Draw(gameTime, spriteBatch);
            Global.map.Draw(gameTime, spriteBatch);
            int n = monsterList.Count;
            for (int i = 0; i < n; i++)
            {
                monsterList[i].Draw(gameTime, spriteBatch);
            }

            n = treasureList.Count;
            for (int i = 0; i < n; i++)
            {
                treasureList[i].Draw(gameTime, spriteBatch);
            }

            player.Draw(gameTime, spriteBatch);

            #endregion

            if (currentGameState == GameStateEnum.SHOW_RESULT)
            {
                resultDialog.Draw(gameTime, spriteBatch);
            }
            if (currentGameState == GameStateEnum.GAME_END) { 
                confirmDialog.Draw(gameTime, spriteBatch);
            }

            this.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
