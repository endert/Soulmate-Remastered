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
        protected View viewInventory;
        protected Map map;
        protected GameObjectHandler gameObjectHandler;
        protected DialogeHandler dialoges;
        protected InGameMenu inGameMenu;
        protected HUD hud;
        public static View view;
        public static View VIEW { get { return view; } }

        //protected int index = 0; //WHY, no using

        /// <summary>
        /// value to change the different game states;
        /// 1 = mainMenu; 
        /// 2 = change between level and villaige; 
        /// 3 = inGameMenu COMMING SOON
        /// </summary>
        protected int returnValue = 0;
                
        public void initialize()
        {
            time = new GameTime();
            time.Start();
            view = new View(new FloatRect(0, 0, Game.windowSizeX, Game.windowSizeY));
            viewInventory = new View(new FloatRect(0, 0, Game.windowSizeX, Game.windowSizeY));
        }

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

        private Vector2f VectorForViewMove()
        {
            float Xmove = (PlayerHandler.player.position.X + (PlayerHandler.player.hitBox.width / 2)) - view.Center.X;
            float Ymove = (PlayerHandler.player.position.Y + (PlayerHandler.player.hitBox.height * 5 / 6)) - view.Center.Y;

            //view cannot go over the map edge
            if (view.Center.X + Xmove < view.Size.X / 2)
                Xmove = 0;
            if (view.Center.Y + Ymove < view.Size.Y / 2)
                Ymove = 0;
            if (view.Center.X + Xmove + (view.Size.X / 2) > map.MapSize.X)
                Xmove = 0;
            if (view.Center.Y + Ymove + (view.Size.Y / 2) > map.MapSize.Y)
                Ymove = 0;

            return new Vector2f(Xmove, Ymove);
        }

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

            if (Keyboard.IsKeyPressed(Keyboard.Key.L) && !Game.isPressed) //switches between level and village
            {
                Game.isPressed = true;
                SaveGame.savePath = savePlayer;
                SaveGame.saveGame();
                returnValue = 2;
            }

            if (!Inventory.inventoryOpen && !inGameMenu.inGameMenuOpen && !Shop.shopIsOpen) //run update for game, if no menu, inventory or shop is open
            {
                view.Move(VectorForViewMove());

                gameObjectHandler.update(gameTime);
                dialoges.update();

                if (PlayerHandler.player.getCurrentHP <= 0) //if player is dead go back to mainMenu
                {
                    gameObjectHandler.deleate();
                    File.Delete(savePlayer);
                    returnValue = 1;
                }
            }
            else if (Shop.shopIsOpen)
            {
                NPCHandler.updateShop(gameTime);
                GameObjectHandler.itemHandler.update(gameTime);
            }

            //no use, don't know it is here xD
            //if (inGameMenu.optionsOpen)
            //{
            //    returnValue = 3;
            //}
            
            //must be update after gameObjectHandler
            hud.update(gameTime);

            if (Keyboard.IsKeyPressed(Controls.debugging) && !Game.isPressed)
            {
                debugging = !debugging;
                Game.isPressed = true;
            }
        }

        public void draw(RenderWindow window)
        {
            window.SetView(view);
            map.draw(window);
            hud.draw(window);
            gameObjectHandler.draw(window);
            dialoges.draw(window);

            if (Inventory.inventoryOpen)
            {
                window.SetView(viewInventory);
                ItemHandler.playerInventory.draw(window);
            }

            if (Shop.shopIsOpen)
            {
                window.SetView(viewInventory);
                NPCHandler.shop.draw(window);
            }

            if (inGameMenu.inGameMenuOpen)
            {
                window.SetView(viewInventory);
                inGameMenu.draw(window);
            }

            if (debugging)
                gameObjectHandler.debugDraw(window);
        }
    }
}
