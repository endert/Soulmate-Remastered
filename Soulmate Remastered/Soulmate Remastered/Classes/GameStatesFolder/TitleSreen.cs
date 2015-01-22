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
        Texture titleScreenTexture;
        Sprite titleScreen;

        View view;

        public void initialize()
        {
            titleScreen = new Sprite(titleScreenTexture);

            view = new View(new FloatRect(0, 0, 1280, 720));
        }

        public void loadContent()
        {
            titleScreenTexture = new Texture("Pictures/StartScreen Kopie.png");
        }

        public EnumGameStates update(GameTime gameTime)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                return EnumGameStates.mainMenu;
            }

            return EnumGameStates.titleSreen;
        }

        public void draw(RenderWindow window)
        {
            window.Draw(titleScreen);
        }
    }
}
