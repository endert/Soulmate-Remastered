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
        protected readonly String savePlayer = "Saves/player.soul";
        protected readonly String saveFile = "Saves/save.soul";
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

        protected bool inGameMenuOpen;
        protected bool isKlickedInGameMenu = false;

        protected bool isKlicked = false;

        protected int index = 0;

        //public void load(String path)
        //{
        //    StreamReader reader = new StreamReader(path);

        //        PlayerHandler.player.toPlayer(reader.ReadLine());

        //    reader.Close();
        //}

        public bool getInventoryOpen()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.I) && !isKlickedInventory && !inventoryOpen)
            {
                isKlickedInventory = true;
                return inventoryOpen = true;
            }

            if (!Keyboard.IsKeyPressed(Keyboard.Key.I))
                isKlickedInventory = false;

            if ((Keyboard.IsKeyPressed(Keyboard.Key.I)||Keyboard.IsKeyPressed(Keyboard.Key.Escape)) && !isKlickedInventory && inventoryOpen == true)
            {
                isKlickedInventory = true;
                isKlickedInGameMenu = true;
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
        
        public bool getInGameMenuOpen()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape) && !isKlickedInGameMenu && !inGameMenuOpen && !inventoryOpen)
            {
                isKlickedInGameMenu = true;
                return inGameMenuOpen = true;
            }

            if (((Keyboard.IsKeyPressed(Keyboard.Key.Escape) && !isKlickedInGameMenu) || (Keyboard.IsKeyPressed(Keyboard.Key.Return) && inGameMenu.getX() == 0)) && inGameMenuOpen == true)
            {
                isKlickedInGameMenu = true;
                return inGameMenuOpen = false;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Return) && inGameMenu.getX() == 1 && inGameMenuOpen == true) //saveGame
            {
                isKlickedInGameMenu = true;
                Console.WriteLine("saving Game");
                SaveGame.saveGame(saveFile);
                Console.WriteLine("successfuly saved Game");
            }

            if (!Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                isKlickedInGameMenu = false;

            return false;
        }

        public void inGameMenuUpdate(GameTime gameTime)
        {
            getInGameMenuOpen();
            if (inGameMenuOpen == true)
                inGameMenu.update(gameTime);
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
            gameObjectHandler = new GameObjectHandler(map, GameObjectHandler.lvl);
            EnemyHandler.enemyInitialize();
        }

        public abstract EnumGameStates update(GameTime gameTime);

        public void GameUpdate(GameTime gameTime)
        {
            time.Update();
            inventoryUpdate(gameTime);
            inGameMenuUpdate(gameTime);
        }

        public void draw(SFML.Graphics.RenderWindow window)
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

            if (inGameMenuOpen == true)
            {
                window.SetView(viewInventory);
                inGameMenu.draw(window);
            }
        }
    }
}
