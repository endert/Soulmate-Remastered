using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using System.Diagnostics;

namespace Soulmate_Remastered.Classes.GameStatesFolders
{
    class TitleScreen : GameState
    {
        bool isPressedEnter;

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
            isPressedEnter = false;

            enter = new Sprite(pressEnter);
            enter.Position = new Vector2f((Game.windowSizeX / 2) - (pressEnter.Size.X / 2), (Game.windowSizeY + 25) - pressEnter.Size.Y);
            animationTime.Start();

            view = new View(new FloatRect(0, 0, Game.windowSizeX, Game.windowSizeY));
        }

        public void loadContent()
        {
            titleScreenTexture = new Texture("Pictures//MainMenu/StartScreen.png");
            pressEnter = new Texture("Pictures/MainMenu/Enter/Enter.png");
            pressEnter2 = new Texture("Pictures/MainMenu/Enter/EnterSelected.png");
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

            if (Keyboard.IsKeyPressed(Keyboard.Key.Return) && !isPressedEnter)
            {
                isPressedEnter = true;
                return EnumGameStates.mainMenu;
            }

            if (!Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                isPressedEnter = false;
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
