using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace Soulmate_Remastered.Classes.GameStatesFolder
{
    class Credits : GameState
    {
        bool isPressed;

        Texture creditsTexture;
        Texture backSelected;
        Texture backNotSelected;

        Sprite credits;
        Sprite back;

        View view;

        public void initialize()
        {
            isPressed = true;

            credits = new Sprite(creditsTexture);
            credits.Position = new Vector2f(0, 0);

            back = new Sprite(backNotSelected);
            back.Position = MainMenu.getBackPostion();

            view = new View(new FloatRect(0, 0, Game.windowSizeX, Game.windowSizeY));
        }

        public void loadContent()
        {
            creditsTexture = new Texture("Pictures/Menu/MainMenu/Credits/CreditsMenu.png");

            backSelected = new Texture("Pictures/Menu/MainMenu/Back/BackSelected.png");
            backNotSelected = new Texture("Pictures/Menu/MainMenu/Back/BackNotSelected.png");
        }

        public EnumGameStates update(GameTime gameTime)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Return) && !isPressed)
            {
                isPressed = true;
                return EnumGameStates.mainMenu;
            }

            if (!NavigationHelp.isAnyKeyPressed())
            {
                isPressed = false;
            }

            if (NavigationHelp.isMouseInSprite(back))
            {
                back.Texture = backSelected;
            }

            if (!NavigationHelp.isMouseInSprite(back))
            {
                back.Texture = backNotSelected;
            }

            if (Keyboard.IsKeyPressed(Controls.Escape) || (NavigationHelp.isMouseInSprite(back) && Mouse.IsButtonPressed(Mouse.Button.Left)))
            {
                isPressed = true;
                return EnumGameStates.mainMenu;
            }

            return EnumGameStates.credits;
        }

        public void draw(RenderWindow window)
        {
            window.Draw(credits);
            window.Draw(back);
        }
    }
}
