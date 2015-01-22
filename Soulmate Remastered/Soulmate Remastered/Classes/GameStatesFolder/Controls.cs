using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameStatesFolders
{
    class Controls : GameState
    {
        Texture controlsTexture;
        Sprite controls;

        View view;

        public void initialize()
        {
            controls = new Sprite(controlsTexture);
            controls.Position = new Vector2f(0, 0);

            view = new View(new FloatRect(0, 0, 1280, 720));
        }

        public void loadContent()
        {
            controlsTexture = new Texture("Pictures/ControlsTextBild.png");
        }

        public EnumGameStates update(GameTime gameTime)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
                return EnumGameStates.mainMenu;

            return EnumGameStates.controls;
        }

        public void draw(RenderWindow window)
        {
            window.Draw(controls);
        }
    }
}
