using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameStatesFolder
{
    class LoadGame : GameState
    {
        private readonly String loadFile = "Saves/save.soul";
        bool isPressed;
        int x; //für Menüsteuerung

        Texture backGroundTexture;
        Texture loadSelected;
        Texture loadNotSelected;
        Texture newGameSelected;
        Texture newGameNotSelected;

        Sprite load;
        Sprite newGame;
        Sprite backGround;

        View view;

        public int getX() { return this.x; }

        public void initialize()
        {
            isPressed = false;
            isPressed = true;
            x = 0;

            backGround = new Sprite(backGroundTexture);
            backGround.Position = new Vector2f(0, 0);

            load = new Sprite(loadNotSelected);
            load.Position = new Vector2f(300, 250);

            newGame = new Sprite(newGameNotSelected);
            newGame.Position = new Vector2f(300, 400);

            view = new View(new FloatRect(0, 0, Game.windowSizeX, Game.windowSizeY));
        }

        public void loadContent()
        {
            backGroundTexture = new Texture("Pictures/Menu/MainMenu/Background.png");

            loadSelected = new Texture("Pictures/Menu/MainMenu/Load/LoadSelected.png");
            loadNotSelected = new Texture("Pictures/Menu/MainMenu/Load/LoadNotSelected.png");

            newGameSelected = new Texture("Pictures/Menu/MainMenu/NewGame/NewGameSelected.png");
            newGameNotSelected = new Texture("Pictures/Menu/MainMenu/NewGame/NewGameNotSelected.png");
        }

        public EnumGameStates update(GameTime gameTime)
        {
            if (NavigationHelp.isMouseInSprite(load))
            {
                x = 0;
            }
            if (NavigationHelp.isMouseInSprite(newGame))
            {
                x = 1;
            }

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

            if (x == 0)
            {
                load.Texture = loadSelected;
                newGame.Texture = newGameNotSelected;
            }

            if (x == 1)
            {
                load.Texture = loadNotSelected;
                newGame.Texture = newGameSelected;
            }

            if (NavigationHelp.isSpriteKlicked(x, 0, isPressed))
            {
                isPressed = true;
                Console.WriteLine("load Game");
                AbstractGamePlay.loading = true;
                SaveGame.loadPath = loadFile;
                SaveGame.loadGame();

                switch (GameObjectHandler.lvl)
                {
                    case 0:
                        return EnumGameStates.village;
                    case 1:
                        return EnumGameStates.inGame;
                    default:
                        return EnumGameStates.village;
                }
            }
            if (NavigationHelp.isSpriteKlicked(x, 1, isPressed))
            {
                isPressed = true;
                Console.WriteLine("new Game");
                return EnumGameStates.village;
            }

            if (!Keyboard.IsKeyPressed(Keyboard.Key.Return) && !Mouse.IsButtonPressed(Mouse.Button.Left) && !Keyboard.IsKeyPressed(Keyboard.Key.Down) && !Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                isPressed = false;
            }

            return EnumGameStates.loadGame;
        }

        public void draw(RenderWindow window)
        {
            window.Draw(backGround);
            window.SetView(view);
            window.Draw(load);
            window.Draw(newGame);
        }
    }
}
