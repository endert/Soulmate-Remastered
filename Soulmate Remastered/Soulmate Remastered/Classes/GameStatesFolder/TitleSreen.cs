using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using System.Diagnostics;

namespace Soulmate_Remastered.Classes.GameStatesFolder
{
    class TitleScreen : GameState
    {
        Stopwatch animationTime;

        Texture titleScreenTexture;
        Sprite titleScreen;

        Texture pressEnter;
        Texture pressEnter2;
        Sprite enter;

        View view;

        public void initialize()
        {
            animationTime = new Stopwatch();

            titleScreen = new Sprite(titleScreenTexture);
           
            enter = new Sprite(pressEnter);
            enter.Position = new Vector2f((Game.windowSizeX / 2) - (pressEnter.Size.X / 2), (Game.windowSizeY - pressEnter.Size.Y) - 50);
            animationTime.Start();

            view = new View(new FloatRect(0, 0, Game.windowSizeX, Game.windowSizeY));
        }

        public void loadContent()
        {
            titleScreenTexture = new Texture("Pictures/Menu/MainMenu/Background.png");
            pressEnter = new Texture("Pictures/Menu/MainMenu/AnyKey/AnyKeyNotSelected.png");
            pressEnter2 = new Texture("Pictures/Menu/MainMenu/AnyKey/AnyKeySelected.png");
        }

        public EnumGameStates update(GameTime gameTime)
        {
            if (animationTime.ElapsedMilliseconds <= 500)
            {
                enter.Texture = pressEnter;
            }
            else if (animationTime.ElapsedMilliseconds <= 1000)
            {
                enter.Texture = pressEnter2;
            }
            else
            {
                animationTime.Restart();
            }

            if ((NavigationHelp.isAnyKeyPressed() || Mouse.IsButtonPressed(Mouse.Button.Left)) && !Game.isPressed)
            {
                Game.isPressed = true;
                return EnumGameStates.mainMenu;
            }

            return EnumGameStates.titleSreen;
        }

        public void draw(RenderWindow window)
        {
            window.Draw(titleScreen);
            window.Draw(enter);
        }
    }
}
