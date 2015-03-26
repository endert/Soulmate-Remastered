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
        protected AbstractTreasureChest chest;

        protected int index = 0;
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
            chest = new TreasureChest(new Vector2f(500, 500));
            if (loading)
            {
                Console.WriteLine("is loading...");
                SaveGame.loadGame();
                Console.WriteLine("successfully loaded");
                loading = false;
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

            if (view.Center.X + Xmove < view.Size.X/2)
                Xmove = 0;
            if (view.Center.Y + Ymove < view.Size.Y / 2)
                Ymove = 0;
            if (view.Center.X + Xmove + (view.Size.X / 2) > map.MapSize.X)
                Xmove = 0;
            if (view.Center.Y + Ymove + (view.Size.Y / 2) > map.MapSize.Y)
                Ymove = 0;

            return new Vector2f( Xmove, Ymove);
        }

        public void GameUpdate(GameTime gameTime)
        {
            if (inGameMenu.closeGame) //if exit clicked
            {
                gameObjectHandler.deleate();
                File.Delete(savePlayer);
                returnValue = 1;
                return;
            }

            time.Update();
            ItemHandler.playerInventory.update(gameTime);
            inGameMenu.update(gameTime);

            if (Keyboard.IsKeyPressed(Keyboard.Key.L) && !Game.isPressed)
            {
                Game.isPressed = true;
                SaveGame.savePath = savePlayer;
                SaveGame.saveGame();
                returnValue = 2;
            }

            if (!Inventory.inventoryOpen && !inGameMenu.inGameMenuOpen && !Shop.shopIsOpen)
            {
                
                view.Move(VectorForViewMove());

                gameObjectHandler.update(gameTime);
                dialoges.update();

                if (PlayerHandler.player.getCurrentHP <= 0)
                {
                    gameObjectHandler.deleate();
                    returnValue = 1;
                }
            }
            else if (Shop.shopIsOpen)
            {
                NPCHandler.updateShop();
            }

            if (inGameMenu.optionsOpen)
            {
                returnValue = 3;
            }

            hud.update(gameTime);
        }

        public void draw(RenderWindow window)
        {
            window.SetView(view);
            map.draw(window);
            hud.draw(window);
            gameObjectHandler.draw(window);
            dialoges.draw(window);
            chest.draw(window);

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
        }
    }
}
