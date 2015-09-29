using SFML.Graphics;
using Soulmate_Remastered.Classes.DialogeBoxFolder;
using Soulmate_Remastered.Classes.GameObjectFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.EnemyFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.NPCFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.NPCFolder.ShopFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using Soulmate_Remastered.Classes.HUDFolder;
using Soulmate_Remastered.Classes.InGameMenuFolder;
using Soulmate_Remastered.Classes.MapFolder;
using Soulmate_Remastered.Core;
using System;
using System.IO;

namespace Soulmate_Remastered.Classes.GameStatesFolder
{
    abstract class AbstractGamePlay : GameState
    {
        public static bool loading = false;
        public static bool startNewGame = false;
        protected static readonly string savePlayer = "Saves/player.soul";
        /// <summary>
        /// bool if debug on or off
        /// </summary>
        bool debugging = false;
        public static string SavePlayerPath { get { return savePlayer; } }
        /// <summary>
        /// view for inventory, shop, inGameMenu
        /// </summary>
        protected View viewHelp;
        protected Map map;
        protected DialogeHandler dialoges;
        protected InGameMenu inGameMenu;
        protected HUD hud;
        public static View View { get; protected set; }

        //protected int index = 0; //WHY, no using

        /// <summary>
        /// <para> value to change the different game states; </para>
        /// <para> 1 = mainMenu; </para>
        /// <para> 2 = change between level and villaige; </para>
        /// 3 = inGameMenu COMMING SOON
        /// </summary>
        protected int returnValue = 0;


        /// <summary>
        /// things that shall happen when a key is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnKeyPress(object sender, KeyEventArgs e)
        {
            Console.WriteLine("AbstractGamePlay");

            if (e.Key == Controls.Key.Debugging)
            {
                debugging = !debugging;
            }

            if (e.Key == Controls.Key.DebugMapChange)
            {
                KeyboardControler.KeyPressed -= OnKeyPress;
                SaveGame.SavePath = savePlayer;
                SaveGame.SaveTheGame();
                KeyboardControler.Deleate();
                returnValue = 2;
            }
        }

        /// <summary>
        /// initialize time and view
        /// </summary>
        public void Initialize()
        {
            KeyboardControler.KeyPressed += OnKeyPress;

            View = new View(new FloatRect(0, 0, Game.WindowSizeX, Game.WindowSizeY));
            viewHelp = new View(new FloatRect(0, 0, Game.WindowSizeX, Game.WindowSizeY));
        }
        /// <summary>
        /// load content of the game
        /// </summary>
        public virtual void LoadContent()
        {
            dialoges = new DialogeHandler();
            inGameMenu = new InGameMenu();
            hud = new HUD();
            GameObjectHandler.LvlMap = map;
            GameObjectHandler.Initialize(map, GameObjectHandler.Lvl);
            EnemyHandler.EnemyInitialize();
            
            if (loading) 
            {
                try
                {
                    SaveGame.LoadGame();
                    Console.WriteLine("successfully loaded");
                    loading = false;
                    return; //if this part was succesful, no need for the rest
                }
                catch(Exception e)
                {
                    Console.WriteLine("Loading failed ;(");
                    Console.WriteLine(e);
                    loading = false;
                    LoadContent();
                }
            }

            else if (File.Exists(savePlayer) && !startNewGame)
            {
                SaveGame.LoadPath = savePlayer;
                SaveGame.LoadMapChange();
            }

            if (startNewGame)
            {
                startNewGame = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime">time of the game</param>
        /// <returns>returns current state of game</returns>
        public abstract EnumGameStates Update(GameTime gameTime);
        
        /// <summary>
        /// calculate the view vector
        /// </summary>
        /// <returns>view vector</returns>
        private Vector2 VectorForViewMove()
        {
            float Xmove = (PlayerHandler.Player.Position.X + (PlayerHandler.Player.HitBox.Width / 2)) - View.Center.X;
            float Ymove = (PlayerHandler.Player.Position.Y + (PlayerHandler.Player.HitBox.Height * 5 / 6)) - View.Center.Y;

            //view cannot go over the map edge
            if (View.Center.X + Xmove < View.Size.X / 2)
                Xmove = 0;
            if (View.Center.Y + Ymove < View.Size.Y / 2)
                Ymove = 0;
            if (View.Center.X + Xmove + (View.Size.X / 2) > map.MapSize.X)
                Xmove = 0;
            if (View.Center.Y + Ymove + (View.Size.Y / 2) > map.MapSize.Y)
                Ymove = 0;
            //*********************************

            return new Vector2(Xmove, Ymove);
        }

        /// <summary>
        /// updates what is similary in the gameplay scenes
        /// </summary>
        /// <param name="gameTime">time of the game</param>
        public void GameUpdate(GameTime gameTime)
        {
            if (inGameMenu.CloseGame) //if exit in inGameMenu clicked
            {
                GameObjectHandler.Deleate();
                File.Delete(savePlayer);
                returnValue = 1;
                return; //end method, because other updates throw errors after the gameObjectHandler is deleated
            }

            //no update needed if game was closed

            ItemHandler.ṔlayerInventory.Update(gameTime);
            inGameMenu.Update(gameTime);
            //************************************************

            if (!PlayerInventory.IsOpen && !inGameMenu.InGameMenuOpen && !Shop.ShopIsOpen) //run update for game, if no menu, inventory or shop is open
            {
                View.Move(VectorForViewMove());

                GameObjectHandler.Update(gameTime);

                if (PlayerHandler.Player.CurrentHP <= 0) //if player is dead go back to mainMenu
                {
                    GameObjectHandler.Deleate();
                    File.Delete(savePlayer);
                    returnValue = 1;
                }
            }
            else if (Shop.ShopIsOpen) //update the shop if it is open
            {
                NPCHandler.UpdateShop(gameTime);
                ItemHandler.Update(gameTime);
            }

            //no use, don't know it is here xD
            //if (inGameMenu.optionsOpen)
            //{
            //    returnValue = 3;
            //}

            hud.Update(gameTime);//must be update after gameObjectHandler
        }

        /// <summary>
        /// draw everything in the game what is needed
        /// </summary>
        /// <param name="window">window where it should be drawed</param>
        public void Draw(RenderWindow window)
        {
            window.SetView(View);
            map.draw(window);
            GameObjectHandler.Draw(window);
            dialoges.Draw(window);
            hud.Draw(window);

            if (PlayerInventory.IsOpen)
            {
                window.SetView(viewHelp);
                ItemHandler.ṔlayerInventory.Draw(window);
            }

            if (Shop.ShopIsOpen)
            {
                window.SetView(viewHelp);
                NPCHandler.Shop_.Draw(window);
            }

            if (inGameMenu.InGameMenuOpen)
            {
                window.SetView(viewHelp);
                inGameMenu.Draw(window);
            }

            if (debugging)
            {
                GameObjectHandler.DebugDraw(window);
                map.debugDraw(window);
            }
        }
    }
}
