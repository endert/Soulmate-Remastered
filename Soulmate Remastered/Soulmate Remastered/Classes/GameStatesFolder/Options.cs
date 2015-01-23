using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace Soulmate_Remastered.Classes.GameStatesFolders
{
    class Options : GameState
    {
        bool isPressedEnter;

        Texture optionsTexture;
        Sprite options;

        View view;

        public void initialize()
        {
            isPressedEnter = true;

            options = new Sprite(optionsTexture);
            options.Position = new Vector2f(0, 0);

            view = new View(new FloatRect(0, 0, Game.windowSizeX, Game.windowSizeY));
        }

        public void loadContent()
        {
            optionsTexture = new Texture("Pictures/MainMenu/Options/OptionsMenu.png");
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
            return EnumGameStates.options;
        }

        public void draw(RenderWindow window)
        {
            window.Draw(options);
        }
    }
}
