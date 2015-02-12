using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace Soulmate_Remastered.Classes.GameStatesFolder
{
    class Options : GameState
    {
        Texture optionsTexture;
        Texture backSelected;
        Texture backNotSelected;

        Sprite options;
        Sprite back;

        View view;

        public void initialize()
        {
            options = new Sprite(optionsTexture);
            options.Position = new Vector2f(0, 0);

            back = new Sprite(backNotSelected);
            back.Position = MainMenu.getBackPostion();

            view = new View(new FloatRect(0, 0, Game.windowSizeX, Game.windowSizeY));
        }

        public void loadContent()
        {
            optionsTexture = new Texture("Pictures/Menu/MainMenu/Options/OptionsMenu.png");

            backSelected = new Texture("Pictures/Menu/MainMenu/Back/BackSelected.png");
            backNotSelected = new Texture("Pictures/Menu/MainMenu/Back/BackNotSelected.png");
        }

        public EnumGameStates update(GameTime gameTime)
        {
            if (NavigationHelp.isMouseInSprite(back))
            {
                back.Texture = backSelected;
            }

            if (!NavigationHelp.isMouseInSprite(back))
            {
                back.Texture = backNotSelected;
            }

            if (NavigationHelp.isSpriteKlicked(0, 0, Game.isPressed, back, Controls.Escape))
            {
                Game.isPressed = true;
                return EnumGameStates.mainMenu;
            }

            return EnumGameStates.options;
        }

        public void draw(RenderWindow window)
        {
            window.Draw(options);
            window.Draw(back);
        }
    }
}
