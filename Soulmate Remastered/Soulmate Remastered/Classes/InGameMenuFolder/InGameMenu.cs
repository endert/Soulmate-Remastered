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
        Texture inGameMenuBackGroundTexture = new Texture("Pictures/InGameMenu/InGameMenuBackground.png");
        Sprite inGameMenuBackGround;

        Texture continueNotSelected = new Texture("Pictures/InGameMenu/ContinueNotSelected.png");
        Texture continueSelected = new Texture("Pictures/InGameMenu/ContinueSelected.png");
        Sprite continueGame;

        Texture exitNotSelected = new Texture("Pictures/InGameMenu/ExitNotSelected.png");
        Texture exitSelected = new Texture("Pictures/InGameMenu/ExitSelected.png");
        Sprite exit;

        bool isPressed = false;
        int x = 0; //Inventarsteurung

        public Vector2f getInGameMenuBackGroundPosition()
        {
            return new Vector2f((Game.windowSizeX - inGameMenuBackGroundTexture.Size.X) / 2, (Game.windowSizeY - inGameMenuBackGroundTexture.Size.Y) / 2);
        }

        public Vector2f getContinueGamePosition()
        {
            return new Vector2f(inGameMenuBackGround.Position.X + (inGameMenuBackGround.Texture.Size.X / 2) - (continueGame.Texture.Size.X / 2), inGameMenuBackGround.Position.Y + 100);
        }

        public Vector2f getExitPosition()
        {
            return new Vector2f(inGameMenuBackGround.Position.X + (inGameMenuBackGround.Texture.Size.X / 2) - (exit.Texture.Size.X / 2), inGameMenuBackGround.Position.Y + 300);
        }

        public int getX()
        {
            return this.x;
        }

        public InGameMenu()
        {
            inGameMenuBackGround = new Sprite(inGameMenuBackGroundTexture);
            inGameMenuBackGround.Position = getInGameMenuBackGroundPosition();

            continueGame = new Sprite(continueSelected);
            continueGame.Position = getContinueGamePosition();

            exit = new Sprite(exitNotSelected);
            exit.Position = getExitPosition();
        }

        public void update(GameTime gameTime)
        {
            selected();

            inGameMenuBackGround.Position = getInGameMenuBackGroundPosition();
            continueGame.Position = getContinueGamePosition();
            exit.Position = getExitPosition();
        }

        public void selected()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && !isPressed)
            {
                x = (x + 1) % 2;
                isPressed = true;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && !isPressed)
            {
                x = (x + 1) % 2;
                isPressed = true;
            }

            if (!Keyboard.IsKeyPressed(Keyboard.Key.Down) && !Keyboard.IsKeyPressed(Keyboard.Key.Up))
                isPressed = false;

            if (x == 0)
            {
                continueGame = new Sprite(continueSelected);
                exit = new Sprite(exitNotSelected);
            }

            if (x == 1)
            {
                continueGame = new Sprite(continueNotSelected);
                exit = new Sprite(exitSelected);
            }
        }

        public void draw(RenderWindow window)
        {
            window.Draw(inGameMenuBackGround);
            window.Draw(continueGame);
            window.Draw(exit);
        }
    }
}
