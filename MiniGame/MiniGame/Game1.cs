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
        DialogEnum dialogEven = DialogEnum.NONE;
        SubMenu subMenu;
        Random random;
        MissionLog log;
        ResultDialog resultDialog;
        ConfirmDialog confirmDialog;
        AnnounceDialog annouceDialog;
        InputTextDialog inputDialog;
        Menu menu;
        int currentRow = 15;
        int currentCol = 20;
        Song music;
        SoundEffect collect, endGame;
        string playerName = "";
        //private void load(int rows, int cols, int numZombies, int numMummies, int numScorpions, int numTreasuress)
        private void load()
        {
            int numTreasuress = 8;
            int numZombies = 0;
            int numMummies = 1;
            int numScorpions = 1;
            int cells = currentRow * currentCol;
            if(cells >= 300)
            {
                numZombies = 2;
                numMummies = 2;
                numScorpions = 2;
            }else if (cells >= 225)
            {
                numZombies = 1;
                numMummies = 2;
                numScorpions = 2;
            }
            monsterList.RemoveAll(v => true);
            treasureList.RemoveAll(v => true);
            Global.map = new Map(0, 0, "map\\", currentRow, currentCol);
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
            for (int i = 0; i < numTreasuress/4; i++)
                treasureList.Add((Treasure)UnitFactory.createInstance(a[random.Next(a.Count)], UnitTypeEnum.JEWELRY));
            for (int i = 0; i < numTreasuress / 4; i++)
                treasureList.Add((Treasure)UnitFactory.createInstance(a[random.Next(a.Count)], UnitTypeEnum.TOOL));
            for (int i = 0; i < numTreasuress / 4; i++)
                treasureList.Add((Treasure)UnitFactory.createInstance(a[random.Next(a.Count)], UnitTypeEnum.WEAPON));
            for (int i = 0; i < numTreasuress / 4; i++)
                treasureList.Add((Treasure)UnitFactory.createInstance(a[random.Next(a.Count)], UnitTypeEnum.STATURE));
            log.init(Global.map, treasureList, monsterList);

            MediaPlayer.Stop();
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(music);
            player.setPlayerName(playerName);
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
            annouceDialog = new AnnounceDialog(this.GraphicsDevice);
            inputDialog = new InputTextDialog(this.GraphicsDevice);
            menu = new Menu();
            log = new MissionLog();
            music = Global.loadSong();
            collect = Global.loadSoundEffect("Collect-sound");
            endGame = Global.loadSoundEffect("Game-Over");
            Global.ghostSound = Global.loadSoundEffect("Ghost");
            Global.zombieSound = Global.loadSoundEffect("Zombie");
            //load(currentRow, currentCol, 2, 2, 2, 8);


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

            

            Global.keyboardHelper.Update(gameTime);
            Global.mouseHelper.Update(gameTime);

            if (dialogEven == DialogEnum.SHOW_ANNOUNCE)
            {
                if (Global.mouseHelper.isLButtonUp())
                {
                    if (annouceDialog.getOption(Global.mouseHelper.getCurrentMousePosition()) == 0)
                        dialogEven = DialogEnum.NONE;
                    dialogEven = DialogEnum.NONE;
                }
                return;
            }
            if(dialogEven == DialogEnum.INPUTTEXT)
            {
                if (Global.keyboardHelper.IsKeyPressed(Keys.Back))
                {
                    playerName = "";
                    inputDialog.setText(playerName);
                }
                if (Global.keyboardHelper.IsKeyPressed(Keys.Enter))
                { 
                   dialogEven = DialogEnum.NONE;


                    currentGameState = GameStateEnum.GAME_LOAD;
                }
                if (Global.mouseHelper.isLButtonUp())
                {
                    if (annouceDialog.getOption(Global.mouseHelper.getCurrentMousePosition()) == 0)
                        dialogEven = DialogEnum.NONE;
                    dialogEven = DialogEnum.NONE;

                    currentGameState = GameStateEnum.GAME_LOAD;
                }
                if (Global.keyboardHelper.isPressAnykey())
                {
                     Keys k = Global.keyboardHelper.getKeyUp();
                    playerName += k.ToString();
                    inputDialog.setText(playerName);
                }

                return;
            }

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
                    {
                        dialogEven = DialogEnum.INPUTTEXT;
                    }
                    
                }
            }
            #endregion

            #region game load
            if (currentGameState == GameStateEnum.GAME_LOAD)
            {
                //load(currentRow, currentCol, 2, 2, 2, 8);
                load();
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
                Global.playerPos.X = player.LogicX;
                Global.playerPos.Y = player.LogicY;
                

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

                //Win game
                Vector2 exit = Global.map.getExit();
                if (player.LogicX == exit.X && player.LogicY == exit.Y)
                {
                    currentGameState = GameStateEnum.GAME_END;

                }
                
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
                            collect.Play();
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
                MediaPlayer.Stop();
                endGame.Play();

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
                        case 2:
                            log.outJsonFile();
                            annouceDialog.setText("File have been save!");
                            dialogEven = DialogEnum.SHOW_ANNOUNCE;
                            break;
                    }
                }

            }
            #endregion

            if(currentGameState == GameStateEnum.GAME_PLAYING || currentGameState == GameStateEnum.GAME_START)
            {
                if (Global.mouseHelper.isLButtonUp())
                {
                    int result = subMenu.getOption(Global.mouseHelper.getCurrentMousePosition());
                    switch (result)
                    {
                        case 0://exit
                            currentGameState = GameStateEnum.GAME_END;
                            break;
                        case 1://10x15
                            currentRow = 10;
                            currentCol = 15;
                            currentGameState = GameStateEnum.GAME_LOAD;
                            break;
                        case 2://15x15
                            currentRow = 15;
                            currentCol = 15;
                            currentGameState = GameStateEnum.GAME_LOAD;
                            break;
                        case 3://15x20
                            currentRow = 15;
                            currentCol = 20;
                            currentGameState = GameStateEnum.GAME_LOAD;
                            break;
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
            this.spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            if (currentGameState == GameStateEnum.GAME_MENU)
            {
                menu.Draw(gameTime, spriteBatch);
            }

            #region draw unit
            if (currentGameState == GameStateEnum.GAME_PLAYING /*|| currentGameState == GameStateEnum.GAME_LOAD*/
                || currentGameState == GameStateEnum.GAME_START || currentGameState == GameStateEnum.SHOW_RESULT)
            {
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
            }
            #endregion

            if (currentGameState == GameStateEnum.SHOW_RESULT)
            {
                resultDialog.Draw(gameTime, spriteBatch);
            }
            if (currentGameState == GameStateEnum.GAME_END) { 
                confirmDialog.Draw(gameTime, spriteBatch);
            }
            if(dialogEven == DialogEnum.SHOW_ANNOUNCE)
            {
                annouceDialog.Draw(gameTime, spriteBatch);
            }
            if (dialogEven == DialogEnum.INPUTTEXT)
            {
                inputDialog.Draw(gameTime, spriteBatch);
            }
            this.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
