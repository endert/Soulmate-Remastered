using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder;
using Soulmate_Remastered.Classes.ItemFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.InGameMenuFolder
{
    class InGameMenu
    {
        Texture inGameMenuBackGroundTexture = new Texture("Pictures/Menu/InGameMenu/InGameMenuBackground.png");
        Sprite inGameMenuBackGround;

        Texture continueNotSelected = new Texture("Pictures/Menu/InGameMenu/Continue/ContinueNotSelected.png");
        Texture continueSelected = new Texture("Pictures/Menu/InGameMenu/Continue/ContinueSelected.png");
        Sprite continueGame;

        Texture saveNotSelected = new Texture("Pictures/Menu/InGameMenu/Save/SaveNotSelected.png");
        Texture saveSelected = new Texture("Pictures/Menu/InGameMenu/Save/SaveSelected.png");
        Sprite save;

        Texture exitNotSelected = new Texture("Pictures/Menu/InGameMenu/Exit/ExitNotSelected.png");
        Texture exitSelected = new Texture("Pictures/Menu/InGameMenu/Exit/ExitSelected.png");
        Sprite exit;

        bool isPressed = false;
        int x = 0; //Inventarsteurung

        readonly String saveFile = "Saves/save.soul";
        
        public bool inGameMenuOpen { get; set; }
        public bool closeGame { get; set; }

        public Vector2f getInGameMenuBackGroundPosition()
        {
            return new Vector2f((Game.windowSizeX - inGameMenuBackGroundTexture.Size.X) / 2, (Game.windowSizeY - inGameMenuBackGroundTexture.Size.Y) / 2);
        }

        public Vector2f getContinueGamePosition()
        {
            return new Vector2f(inGameMenuBackGround.Position.X + (inGameMenuBackGround.Texture.Size.X / 2) - (continueGame.Texture.Size.X / 2), inGameMenuBackGround.Position.Y + 100);
        }

        public Vector2f getSavePosition()
        {
            return new Vector2f(getContinueGamePosition().X, inGameMenuBackGround.Position.Y + 200);
        }

        public Vector2f getExitPosition()
        {
            return new Vector2f(getContinueGamePosition().X, inGameMenuBackGround.Position.Y + 300);
        }

        public bool getInGameMenuOpen()
        {
            if (!Mouse.IsButtonPressed(Mouse.Button.Left) && !NavigationHelp.isAnyKeyPressed())
                isPressed = false;
            
            if(Keyboard.IsKeyPressed(Keyboard.Key.Escape) && !isPressed && !inGameMenuOpen && !Inventory.inventoryOpen)
            {
                isPressed = true;
                inGameMenuOpen = true;
            }
            return inGameMenuOpen;
        }
        
        public InGameMenu()
        {
            inGameMenuBackGround = new Sprite(inGameMenuBackGroundTexture);
            inGameMenuBackGround.Position = getInGameMenuBackGroundPosition();

            continueGame = new Sprite(continueSelected);
            continueGame.Position = getContinueGamePosition();

            save = new Sprite(saveNotSelected);
            save.Position = getSavePosition();

            exit = new Sprite(exitNotSelected);
            exit.Position = getExitPosition();
        }

        public void update(GameTime gameTime)
        {
            getInGameMenuOpen();
            if (inGameMenuOpen)
            {
                manage();

                inGameMenuBackGround.Position = getInGameMenuBackGroundPosition();
                continueGame.Position = getContinueGamePosition();
                save.Position = getSavePosition();
                exit.Position = getExitPosition();
            }
        }

        public void manage()
        {
            if (NavigationHelp.isMouseInSprite(continueGame)) //Continue
            {
                x = 0;
            }

            if (NavigationHelp.isMouseInSprite(save)) //Save
            {
                x = 1;
            }

            if (NavigationHelp.isMouseInSprite(exit)) //Exit
            {
                x = 2;
            }
            
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && !isPressed)
            {
                x = (x + 2) % 3;
                isPressed = true;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && !isPressed)
            {
                x = (x + 1) % 3;
                isPressed = true;
            }

            if (!Mouse.IsButtonPressed(Mouse.Button.Left) && !NavigationHelp.isAnyKeyPressed())
                isPressed = false;

            if (x == 0)
            {
                continueGame = new Sprite(continueSelected);
                save = new Sprite(saveNotSelected);
                exit = new Sprite(exitNotSelected);
            }

            if (x == 1)
            {
                continueGame = new Sprite(continueNotSelected);
                save = new Sprite(saveSelected);
                exit = new Sprite(exitNotSelected);
            }

            if (x == 2)
            {
                continueGame = new Sprite(continueNotSelected);
                save = new Sprite(saveNotSelected);
                exit = new Sprite(exitSelected);
            }

            if (NavigationHelp.isSpriteKlicked(x, 0, isPressed) || (Keyboard.IsKeyPressed(Keyboard.Key.Escape) && !isPressed))
            {
                isPressed = true;
                inGameMenuOpen = false;
            }

            if (NavigationHelp.isSpriteKlicked(x, 1, isPressed))
            {
                isPressed = true;
                Console.WriteLine("saving Game");
                SaveGame.savePath = saveFile;
                SaveGame.saveGame();
                Console.WriteLine("successfuly saved Game");
            }

            if (NavigationHelp.isSpriteKlicked(x, 2, isPressed))
            {
                isPressed = true;
                inGameMenuOpen = false;
                closeGame = true;
            }
            closeGame = false;
        }

        public void draw(RenderWindow window)
        {
            window.Draw(inGameMenuBackGround);
            window.Draw(continueGame);
            window.Draw(save);
            window.Draw(exit);
        }
    }
}
