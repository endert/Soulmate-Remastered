using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace Soulmate_Remastered.Classes.GameStatesFolders
{
    class TitleScreen : GameState
    {
        bool isPressedEnter;
        
        Texture titleScreenTexture;
        Sprite titleScreen;

        View view;

        public void initialize()
        {
            titleScreen = new Sprite(titleScreenTexture);
            isPressedEnter = false;

            view = new View(new FloatRect(0, 0, Game.windowSizeX, Game.windowSizeY));
        }

        public void loadContent()
        {
            titleScreenTexture = new Texture("Pictures//MainMenu/StartScreen.png");
        }

        public EnumGameStates update(GameTime gameTime)
        {
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
        }
    }
}
