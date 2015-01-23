using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.MapFolder;
using Soulmate_Remastered.Classes.GameObjectFolder;
using Soulmate_Remastered.Classes.HUDFolder;
using System.Drawing;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;

namespace Soulmate_Remastered.Classes.GameStatesFolders
{
    class InGame : GameState
    {
        GameTime time = new GameTime();
        static View view;
        View viewInventory;
        Texture backGroundTex;
        Sprite backGround;
        Map map;
        GameObjectHandler gameObjectHandler;
        InGameMenu inGameMenu;
        HUD hud;

        public static View VIEW
        {
            get
            {
                return view;
            }
        }

        bool inventoryOpen;
        bool isKlickedInventory = false;

        bool inGameMenuOpen;
        bool isKlickedInGameMenu = false;

        public void initialize()
        {
            time = new GameTime();
            time.Start();
            view = new View(new FloatRect(0, 0, Game.windowSizeX, Game.windowSizeY));
            viewInventory = new View(new FloatRect(0, 0, Game.windowSizeX, Game.windowSizeY));

            backGround = new Sprite(backGroundTex);
            backGround.Position = new Vector2f(0, 0);
        }

        public void loadContent()
        {
            backGroundTex = new Texture("Pictures/Hintergrund.png");

            map = new Map(new Bitmap("Pictures/Map/Map2.bmp"));
            
            gameObjectHandler = new GameObjectHandler(map, 0);

            inGameMenu = new InGameMenu();

            hud = new HUD();
        }

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

        public EnumGameStates update(GameTime gameTime)
        {
            time.Update();
            getInventoryOpen();

            if (inventoryOpen == true)
            {
                ItemHandler.playerInventory.update(gameTime);
            }

            getInGameMenuOpen();

            if (Keyboard.IsKeyPressed(Keyboard.Key.Return) && inGameMenu.getX() == 1) //if exit clicked
            {
                gameObjectHandler.deleate();
                return EnumGameStates.mainMenu;
            }

            if (inGameMenuOpen == true)
            {
                inGameMenu.update(gameTime);
            }

            else if (!inventoryOpen && !inGameMenuOpen)
            {
                backGround.Position = new Vector2f(view.Center.X - Game.windowSizeX / 2, view.Center.Y - Game.windowSizeY / 2);
                view.Move(new Vector2f((PlayerHandler.player.position.X + (PlayerHandler.player.hitBox.width / 2)), (PlayerHandler.player.position.Y + (PlayerHandler.player.hitBox.height / 2))) - view.Center); //View als letztes updaten und der sprite springt nicht mehr 

                gameObjectHandler.update(gameTime);
                hud.update(gameTime);

                if (PlayerHandler.player.getCurrentHP <= 0)
                {
                    gameObjectHandler.deleate();
                    return EnumGameStates.mainMenu;
                }
            }
            return EnumGameStates.inGame;
        }

        public void draw(RenderWindow window)
        {
            window.Draw(backGround);
            window.SetView(view);
            map.draw(window);
            gameObjectHandler.draw(window);
            hud.draw(window);

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
