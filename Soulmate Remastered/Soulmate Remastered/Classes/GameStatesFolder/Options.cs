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
        Texture optionsTexture;
        Sprite options;

        View view;

        public void initialize()
        {
            options = new Sprite(optionsTexture);
            options.Position = new Vector2f(0, 0);

            view = new View(new FloatRect(0, 0, 1280, 720));
        }

        public void loadContent()
        {
            optionsTexture = new Texture("Pictures/OptionsTest.png");
        }

        public EnumGameStates update(GameTime gameTime)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
                return EnumGameStates.mainMenu;

            return EnumGameStates.options;
        }

        public void draw(RenderWindow window)
        {
            window.Draw(options);
        }
    }
}
