using SFML.Graphics;
using SFML.Window;
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
        bool isMouseKlicked;
        int x = 0; //Inventarsteurung
        int xMouse = 0;

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

        public int getX() { return this.x; }
        public int getXMouse() { return this.xMouse; }

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
            selected();

            inGameMenuBackGround.Position = getInGameMenuBackGroundPosition();
            continueGame.Position = getContinueGamePosition();
            save.Position = getSavePosition();
            exit.Position = getExitPosition();
        }

        public void selected()
        {
            if (!Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                isMouseKlicked = false;
                xMouse = 0;
            }

            if (Mouse.IsButtonPressed(Mouse.Button.Left) && MouseHelp.isMouseInSprite(continueGame) && !isMouseKlicked) //Continue
            {
                xMouse = 1;
                isMouseKlicked = true;
            }

            if (Mouse.IsButtonPressed(Mouse.Button.Left) && MouseHelp.isMouseInSprite(save) && !isMouseKlicked) //Save
            {
                xMouse = 2;
                isMouseKlicked = true;
            }

            if (Mouse.IsButtonPressed(Mouse.Button.Left) && MouseHelp.isMouseInSprite(exit) && !isMouseKlicked) //Exit
            {
                xMouse = 3;
                isMouseKlicked = true;
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

            if (!Keyboard.IsKeyPressed(Keyboard.Key.Down) && !Keyboard.IsKeyPressed(Keyboard.Key.Up))
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
