﻿using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.DialogeBoxFolder;
using Soulmate_Remastered.Classes.GameObjectFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.EnemyFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using Soulmate_Remastered.Classes.HUDFolder;
using Soulmate_Remastered.Classes.InGameMenuFolder;
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
        protected readonly String savePlayer = "Saves/player.soul";
        protected GameTime time = new GameTime();
        protected View viewInventory;
        protected Map map;
        protected GameObjectHandler gameObjectHandler;
        protected DialogeHandler dialoges;
        protected InGameMenu inGameMenu;
        protected HUD hud;
        public static View view;
            public static View VIEW { get { return view; } }

        protected bool inventoryOpen;
        protected bool isKlickedInventory = false;

        protected bool isKlicked = false;

        protected int index = 0;
        protected int returnValue = 0;

        public bool getInventoryOpen()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.I) && !isKlickedInventory && !inventoryOpen)
            {
                isKlickedInventory = true;
                ItemHandler.playerInventory.setOpen();
                return inventoryOpen = true;
            }

            if (!Keyboard.IsKeyPressed(Keyboard.Key.I))
                isKlickedInventory = false;

            if ((Keyboard.IsKeyPressed(Keyboard.Key.I) || Keyboard.IsKeyPressed(Keyboard.Key.Escape)) && !isKlickedInventory && inventoryOpen == true)
            {
                isKlickedInventory = true;
                return inventoryOpen = false;
            }

            return false;
        }

        public void inventoryUpdate(GameTime gameTime)
        {
            getInventoryOpen();
            if (inventoryOpen == true)
            {
                ItemHandler.playerInventory.update(gameTime);
            }
        }
                
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
                Console.WriteLine("is loading...");
                SaveGame.loadGame();
                Console.WriteLine("successfully loaded");
                loading = false;
            }
            if (File.Exists(savePlayer))
            {
                SaveGame.loadPath = savePlayer;
                SaveGame.loadMapChange();
            }
        }

        public abstract EnumGameStates update(GameTime gameTime);

        public void GameUpdate(GameTime gameTime)
        {
            time.Update();
            inventoryUpdate(gameTime);
            inGameMenu.update(gameTime);

            if (Keyboard.IsKeyPressed(Keyboard.Key.L) && !isKlicked)
            {
                isKlicked = true;
                SaveGame.savePath = savePlayer;
                SaveGame.saveGame();
                returnValue = 2;
            }

            if (!Keyboard.IsKeyPressed(Keyboard.Key.L))
            {
                isKlicked = false;
            }

            if (inGameMenu.closeGame) //if exit clicked
            {
                gameObjectHandler.deleate();
                File.Delete(savePlayer);
                returnValue = 1;
            }

            if (!inventoryOpen && !inGameMenu.inGameMenuOpen)
            {
                view.Move(new Vector2f((PlayerHandler.player.position.X + (PlayerHandler.player.hitBox.width / 2)),
                                       (PlayerHandler.player.position.Y + (PlayerHandler.player.hitBox.height * 5 / 6))) - view.Center); //View als letztes updaten und der sprite springt nicht mehr 

                gameObjectHandler.update(gameTime);
                hud.update(gameTime);
                dialoges.update();

                if (PlayerHandler.player.getCurrentHP <= 0)
                {
                    gameObjectHandler.deleate();
                    returnValue = 1;
                }
            }
        }

        public void draw(RenderWindow window)
        {
            window.SetView(view);
            map.draw(window);
            hud.draw(window);
            gameObjectHandler.draw(window);
            dialoges.draw(window);

            if (inventoryOpen == true)
            {
                window.SetView(viewInventory);
                ItemHandler.playerInventory.draw(window);
            }

            if (inGameMenu.inGameMenuOpen == true)
            {
                window.SetView(viewInventory);
                inGameMenu.draw(window);
            }
        }
    }
}
