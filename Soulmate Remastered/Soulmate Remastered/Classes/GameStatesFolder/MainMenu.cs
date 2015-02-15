using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameStatesFolder
{
    class MainMenu : AbstractMainMenu
    {
        Texture startSelected;
        Texture startNotSelected;
                        
        Texture optionsSelected;
        Texture optionsNotSelected;

        Texture controlsSelected;
        Texture controlsNotSelected;

        Texture creditsSelected;
        Texture creditsNotSelected;

        Texture endSelected;
        Texture endNotSelected;

        Sprite start;
        Sprite optionsButton;
        Sprite controls;
        Sprite credits;
        Sprite end;
       
        public override void initialize()
        {
            base.initialize();

            start = new Sprite(startNotSelected);
            start.Position = new Vector2f(300, 300);

            optionsButton = new Sprite(optionsNotSelected);
            optionsButton.Position = new Vector2f(start.Position.X, start.Position.Y + 75 * 1);

            controls = new Sprite(controlsNotSelected);
            controls.Position = new Vector2f(start.Position.X, start.Position.Y + 75 * 2);

            credits = new Sprite(creditsNotSelected);
            credits.Position = new Vector2f(start.Position.X, start.Position.Y + 75 * 3);

            end = new Sprite(endNotSelected);
            end.Position = new Vector2f(start.Position.X, start.Position.Y + 75 * 4);
        }

        public override void loadContent()
        {
            base.loadContent();

            startSelected = new Texture("Pictures/Menu/MainMenu/Start/StartSelected.png");
            startNotSelected = new Texture("Pictures/Menu/MainMenu/Start/StartNotSelected.png");

            optionsSelected = new Texture("Pictures/Menu/MainMenu/Options/OptionsSelected.png");
            optionsNotSelected = new Texture("Pictures/Menu/MainMenu/Options/OptionsNotSelected.png");

            creditsSelected = new Texture("Pictures/Menu/MainMenu/Credits/CreditsSelected.png");
            creditsNotSelected = new Texture("Pictures/Menu/MainMenu/Credits/CreditsNotSelected.png");

            controlsSelected = new Texture("Pictures/Menu/MainMenu/Controls/ControlsSelected.png");
            controlsNotSelected = new Texture("Pictures/Menu/MainMenu/Controls/ControlsNotSelected.png");

            endSelected = new Texture("Pictures/Menu/MainMenu/End/EndSelected.png");
            endNotSelected = new Texture("Pictures/Menu/MainMenu/End/EndNotSelected.png");   
        }

        public override EnumGameStates update(GameTime gameTime)
        {
            gameUpdate(gameTime);

            if (NavigationHelp.isMouseInSprite(start))
            {
                x = 0;
            }
            if (NavigationHelp.isMouseInSprite(optionsButton))
            {
                x = 1;
            }
            if (NavigationHelp.isMouseInSprite(controls))
            {
                x = 2;
            }
            if (NavigationHelp.isMouseInSprite(credits))
            {
                x = 3;
            }
            if (NavigationHelp.isMouseInSprite(end))
            {
                x = 4;
            }

            if (Keyboard.IsKeyPressed(Controls.Up) && !Game.isPressed)
            {
                x = (x + 4) % 5;
                Game.isPressed = true;
            }

            if (Keyboard.IsKeyPressed(Controls.Down) && !Game.isPressed)
            {
                x = (x + 1) % 5;
                Game.isPressed = true;
            }

            if (x == 0)
            {
                start.Texture = startSelected;
                optionsButton.Texture = optionsNotSelected;
                controls.Texture = controlsNotSelected;
                credits.Texture = creditsNotSelected;
                end.Texture = endNotSelected;
            }

            if (x == 1)
            {
                start.Texture = startNotSelected;
                optionsButton.Texture = optionsSelected;
                controls.Texture = controlsNotSelected;
                credits.Texture = creditsNotSelected;
                end.Texture = endNotSelected;
            }

            if (x == 2)
            {
                start.Texture = startNotSelected;
                optionsButton.Texture = optionsNotSelected;
                controls.Texture = controlsSelected;
                credits.Texture = creditsNotSelected;
                end.Texture = endNotSelected;
            }

            if (x == 3)
            {
                start.Texture = startNotSelected;
                optionsButton.Texture = optionsNotSelected;
                controls.Texture = controlsNotSelected;
                credits.Texture = creditsSelected;
                end.Texture = endNotSelected;
            }

            if (x == 4)
            {
                start.Texture = startNotSelected;
                optionsButton.Texture = optionsNotSelected;
                controls.Texture = controlsNotSelected;
                credits.Texture = creditsNotSelected;
                end.Texture = endSelected;
            }

            if (NavigationHelp.isSpriteKlicked(x, 0, Game.isPressed, start, Controls.Return))
            {
                Game.isPressed = true;
                return EnumGameStates.loadGame;
            }
            if (NavigationHelp.isSpriteKlicked(x, 1, Game.isPressed, optionsButton, Controls.Return))
            {
                Game.isPressed = true;
                Console.WriteLine("load Options");
                return EnumGameStates.options;
            }
            if (NavigationHelp.isSpriteKlicked(x, 2, Game.isPressed, controls, Controls.Return))
            {
                Game.isPressed = true;
                Console.WriteLine("load Controls");
                return EnumGameStates.controls;
            }
            if (NavigationHelp.isSpriteKlicked(x, 3, Game.isPressed, credits, Controls.Return))
            {
                Game.isPressed = true;
                Console.WriteLine("load Credits");
                return EnumGameStates.credits;
            }
            if (NavigationHelp.isSpriteKlicked(x, 4, Game.isPressed, end, Controls.Return))
            {
                Game.isPressed = true;
                return EnumGameStates.none;
            }

            if (backValueSelected == 1)
                return EnumGameStates.titleSreen;

            return EnumGameStates.mainMenu;
        }

        public override void draw(RenderWindow window)
        {
            base.draw(window);

            window.Draw(start);
            window.Draw(optionsButton);
            window.Draw(controls);
            window.Draw(credits);
            window.Draw(end);
        }
    }
}
