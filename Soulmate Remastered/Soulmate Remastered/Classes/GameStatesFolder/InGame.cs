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
using Soulmate_Remastered.Classes.InGameMenuFolder;
using Soulmate_Remastered.Classes.DialogeBoxFolder;
using System.IO;

namespace Soulmate_Remastered.Classes.GameStatesFolders
{
    class InGame : GameState
    {
        GameTime time = new GameTime();
        static View view;
        View viewInventory;
        Map map;
        GameObjectHandler gameObjectHandler;
        DialogeHandler dialoges;
        InGameMenu inGameMenu;
        HUD hud;
        private readonly String savePath = "save.soul";

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

            if(File.Exists(savePath))
                load("save.soul");
            else
            {



            }
        }

        public void loadContent()
        {
            map = new Map(new Bitmap("Pictures/Map/Map2.bmp"));
            
            gameObjectHandler = new GameObjectHandler(map, 0);

            dialoges = new DialogeHandler();

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

        public void save(String path)
        {
            StreamWriter writer = new StreamWriter(path);
            writer.WriteLine("Bacon");
            writer.WriteLine("Reiswaffel");

            writer.Flush();
            writer.Close();
        }

        public void load(String path)
        {
            StreamReader reader = new StreamReader(path);

            while(!reader.EndOfStream)
            {
                Console.WriteLine(reader.ReadLine());
            }
            reader.Close();

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
                view.Move(new Vector2f((PlayerHandler.player.position.X + (PlayerHandler.player.hitBox.width / 2)), (PlayerHandler.player.position.Y + (PlayerHandler.player.hitBox.height * 5 / 6))) - view.Center); //View als letztes updaten und der sprite springt nicht mehr 

                gameObjectHandler.update(gameTime);
                hud.update(gameTime);
                dialoges.update();

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
            window.SetView(view);
            map.draw(window);
            gameObjectHandler.draw(window);
            dialoges.draw(window);
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
