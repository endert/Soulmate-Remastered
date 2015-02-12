using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameStatesFolder
{
    class ControlsSetting : AbstractMainMenu
    {
        Texture controlsTexture;
        Sprite controls;

        public override void initialize()
        {
            base.initialize();

            controls = new Sprite(controlsTexture);
            controls.Position = new Vector2f(0, 0);
        }

        public override void loadContent()
        {
            base.loadContent();

            controlsTexture = new Texture("Pictures/Menu/MainMenu/Controls/ControlsMenu.png");
        }

        public override EnumGameStates update(GameTime gameTime)
        {
            gameUpdate(gameTime);

            if (backValueSelected == 1)
                return EnumGameStates.mainMenu;

            return EnumGameStates.controls;
        }

        public override void draw(RenderWindow window)
        {
            base.draw(window);

            window.Draw(controls);
        }
    }
}
