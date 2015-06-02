using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.CheatConsoleFolder;
using Soulmate_Remastered.Classes.DialogeBoxFolder;
using Soulmate_Remastered.Classes.GameObjectFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.EnemyFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.NPCFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.NPCFolder.ShopFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.TreasureChestFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using Soulmate_Remastered.Classes.HUDFolder;
using Soulmate_Remastered.Classes.InGameMenuFolder;
using Soulmate_Remastered.Classes.ItemFolder;
using Soulmate_Remastered.Classes.MapFolder;
using Soulmate_Remastered.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameStatesFolder
{
    abstract class AbstractGamePlay : GameState
    {
        public static bool loading = false;
        public static bool startNewGame = false;
        protected static readonly String savePlayer = "Saves/player.soul";
        bool debugging = false;
        public static String savePlayerPath { get { return savePlayer; } }
        protected GameTime time = new GameTime();
        /// <summary>
        /// view for inventory, shop, inGameMenu
        /// </summary>
        protected View viewHelp;
        protected Map map;
        protected GameObjectHandler gameObjectHandler;
        protected DialogeHandler dialoges;
        protected InGameMenu inGameMenu;
        protected HUD hud;
        public static View VIEW { get; protected set; }

        //protected int index = 0; //WHY, no using

        /// <summary>
        /// value to change the different game states;
        /// 1 = mainMenu; 
        /// 2 = change between level and villaige; 
        /// 3 = inGameMenu COMMING SOON
        /// </summary>
        protected int returnValue = 0;
                
        /// <summary>
        /// initialize time and view
        /// </summary>
        public void initialize()
        {
            time = new GameTime();
            time.Start();
            VIEW = new View(new FloatRect(0, 0, Game.windowSizeX, Game.windowSizeY));
            viewHelp = new View(new FloatRect(0, 0, Game.windowSizeX, Game.windowSizeY));
        }
        /// <summary>
        /// load content of the game
        /// </summary>
        public virtual void loadContent()
        {
            dialoges = new DialogeHandler();
            inGameMenu = new InGameMenu();
            hud = new HUD();
            GameObjectHandler.lvlMap = map;
            gameObjectHandler = new GameObjectHandler(map, GameObjectHandler.lvl);
            EnemyHandler.enemyInitialize();
            
            if (loading) 
            {
                try
                {
                    SaveGame.loadGame();
                    Console.WriteLine("successfully loaded");
                    loading = false;
                    return; //if this part was succesful, no need for the rest
                }
                catch
                {
                    Console.WriteLine("Loading failed ;(");
                    loading = false;
                    loadContent();
                }
            }

            else if (File.Exists(savePlayer) && !startNewGame)
            {
                SaveGame.loadPath = savePlayer;
                SaveGame.loadMapChange();
            }

            if (startNewGame)
            {
                startNewGame = false;
            }
        }

        public abstract EnumGameStates update(GameTime gameTime);
        
        /// <summary>
        /// calculate the view vector
        /// </summary>
        /// <returns>view vector</returns>
        private Vector2 VectorForViewMove()
        {
            float Xmove = (PlayerHandler.player.position.X + (PlayerHandler.player.hitBox.width / 2)) - VIEW.Center.X;
            float Ymove = (PlayerHandler.player.position.Y + (PlayerHandler.player.hitBox.height * 5 / 6)) - VIEW.Center.Y;

            //view cannot go over the map edge
            if (VIEW.Center.X + Xmove < VIEW.Size.X / 2)
                Xmove = 0;
            if (VIEW.Center.Y + Ymove < VIEW.Size.Y / 2)
                Ymove = 0;
            if (VIEW.Center.X + Xmove + (VIEW.Size.X / 2) > map.MapSize.X)
                Xmove = 0;
            if (VIEW.Center.Y + Ymove + (VIEW.Size.Y / 2) > map.MapSize.Y)
                Ymove = 0;
            //*********************************

            return new Vector2(Xmove, Ymove);
        }

        /// <summary>
        /// update the game
        /// </summary>
        /// <param name="gameTime"></param>
        public void GameUpdate(GameTime gameTime)
        {
            if (inGameMenu.closeGame) //if exit in inGameMenu clicked
            {
                gameObjectHandler.deleate();
                File.Delete(savePlayer);
                returnValue = 1;
                return; //end method, because other updates throw failures after the gameObjectHandler is deleated
            }

            //no update needed if game was closed
            time.Update();
            ItemHandler.playerInventory.update(gameTime);
            inGameMenu.update(gameTime);
            //************************************************

            if (Keyboard.IsKeyPressed(Keyboard.Key.L) && !Game.isPressed) //switches between level and village
            {
                Game.isPressed = true;
                SaveGame.savePath = savePlayer;
                SaveGame.saveGame();
                returnValue = 2;
            }

            if (!Inventory.inventoryOpen && !inGameMenu.inGameMenuOpen && !Shop.shopIsOpen) //run update for game, if no menu, inventory or shop is open
            {
                VIEW.Move(VectorForViewMove());

                gameObjectHandler.update(gameTime);
                dialoges.update();

                if (PlayerHandler.player.getCurrentHP <= 0) //if player is dead go back to mainMenu
                {
                    gameObjectHandler.deleate();
                    File.Delete(savePlayer);
                    returnValue = 1;
                }
            }
            else if (Shop.shopIsOpen) //update the shop if it is open
            {
                NPCHandler.updateShop(gameTime);
                GameObjectHandler.itemHandler.update(gameTime);
            }

            //no use, don't know it is here xD
            //if (inGameMenu.optionsOpen)
            //{
            //    returnValue = 3;
            //}

            hud.update(gameTime);//must be update after gameObjectHandler
            
            debug();
        }

        /// <summary>
        /// draw a rectangle shape for the hitbox
        /// </summary>
        public void debug()
        {
            if (Keyboard.IsKeyPressed(Controls.debugging) && !Game.isPressed)
            {
                debugging = !debugging;
                Game.isPressed = true;
            }
        }

        /// <summary>
        /// draw everything in the game what is needed
        /// </summary>
        /// <param name="window"></param>
        public void draw(RenderWindow window)
        {
            window.SetView(VIEW);
            map.draw(window);
            gameObjectHandler.draw(window);
            dialoges.draw(window);
            hud.draw(window);

            if (Inventory.inventoryOpen)
            {
                window.SetView(viewHelp);
                ItemHandler.playerInventory.draw(window);
            }

            if (Shop.shopIsOpen)
            {
                window.SetView(viewHelp);
                NPCHandler.shop.draw(window);
            }

            if (inGameMenu.inGameMenuOpen)
            {
                window.SetView(viewHelp);
                inGameMenu.draw(window);
            }

            if (debugging)
                gameObjectHandler.debugDraw(window);

        }
    }
}
