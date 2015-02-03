using SFML.Graphics;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameStatesFolder
{
    abstract class AbstractGamePlay : GameState
    {
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

        protected int change = 0;

        public bool getInventoryOpen()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.I) && !isKlickedInventory && !inventoryOpen)
            {
                isKlickedInventory = true;
                return inventoryOpen = true;
            }

            if (!Keyboard.IsKeyPressed(Keyboard.Key.I))
                isKlickedInventory = false;

            if (Keyboard.IsKeyPressed(Keyboard.Key.I) && !isKlickedInventory && inventoryOpen == true)
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
        
        public bool getInGameMenuOpen()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape) && !isKlickedInGameMenu && !inGameMenuOpen)
            {
                isKlickedInGameMenu = true;
                return inGameMenuOpen = true;
            }

            if (!Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                isKlickedInGameMenu = false;

            if (((Keyboard.IsKeyPressed(Keyboard.Key.Escape) && !isKlickedInGameMenu) || (Keyboard.IsKeyPressed(Keyboard.Key.Return) && inGameMenu.getX() == 0)) && inGameMenuOpen == true)
            {
                isKlickedInGameMenu = true;
                return inGameMenuOpen = false;
            }

            return false;
        }

        public void inGameMenuUpdate(GameTime gameTime)
        {
            if (inGameMenuOpen == true)
                inGameMenu.update(gameTime);
        }
        
        public void initialize()
        {
            time = new GameTime();
            time.Start();
            view = new View(new FloatRect(0, 0, Game.windowSizeX, Game.windowSizeY));
            viewInventory = new View(new FloatRect(0, 0, Game.windowSizeX, Game.windowSizeY));
            Console.WriteLine(change);
            if (change == 0)
            {
                gameObjectHandler = new GameObjectHandler(map, 0);
                change++;
            }

        }

        public virtual void loadContent()
        {
            dialoges = new DialogeHandler();
            inGameMenu = new InGameMenu();
            hud = new HUD();
            EnemyHandler.enemyUpdate();
        }

        public abstract EnumGameStates update(GameTime gameTime);

        public void stuff(GameTime gameTime)
        {
            time.Update();

            inventoryUpdate(gameTime);
            getInGameMenuOpen();
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
