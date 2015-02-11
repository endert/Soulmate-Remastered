using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameStatesFolder
{
    class MainMenu : GameState
    {
        public static bool isPressed;
        int x; //für Menüsteuerung

        Texture backGroundTexture;

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

        Texture backSelected;
        Texture backNotSelected;

        Sprite backGround;
        Sprite start;
        Sprite optionsButton;
        Sprite controls;
        Sprite credits;
        Sprite end;
        static Sprite back;

        View view;

        public static Vector2f getBackPostion()
        {
            return new Vector2f(Game.windowSizeX - back.Texture.Size.X - 10, 650);
        }

        public void initialize()
        {
            isPressed = false;
            isPressed = true;
            x = 0;

            backGround = new Sprite(backGroundTexture);
            backGround.Position = new Vector2f(0, 0);

            start = new Sprite(startNotSelected);
            start.Position = new Vector2f(300, 225);

            optionsButton = new Sprite(optionsNotSelected);
            optionsButton.Position = new Vector2f(start.Position.X, start.Position.Y + 75 * 1);

            controls = new Sprite(controlsNotSelected);
            controls.Position = new Vector2f(start.Position.X, start.Position.Y + 75 * 2);

            credits = new Sprite(creditsNotSelected);
            credits.Position = new Vector2f(start.Position.X, start.Position.Y + 75 * 3);

            end = new Sprite(endNotSelected);
            end.Position = new Vector2f(start.Position.X, start.Position.Y + 75 * 4);

            back = new Sprite(backNotSelected);
            back.Position = getBackPostion();

            view = new View(new FloatRect(0, 0, Game.windowSizeX, Game.windowSizeY));
        }

        public void loadContent()
        {
            backGroundTexture = new Texture("Pictures/Menu/MainMenu/Background.png");

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

            backSelected = new Texture("Pictures/Menu/MainMenu/Back/BackSelected.png");
            backNotSelected = new Texture("Pictures/Menu/MainMenu/Back/BackNotSelected.png");
        }

        public EnumGameStates update(GameTime gameTime)
        {            
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

            if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && !isPressed)
            {
                x = (x + 4) % 5;
                isPressed = true;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && !isPressed)
            {
                x = (x + 1) % 5;
                isPressed = true;
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

            if (NavigationHelp.isSpriteKlicked(x, 0, isPressed, start))
            {
                isPressed = true;
                return EnumGameStates.loadGame;
            }
            if (NavigationHelp.isSpriteKlicked(x, 1, isPressed, optionsButton))
            {
                isPressed = true;
                Console.WriteLine("load Options");
                return EnumGameStates.options;
            }
            if (NavigationHelp.isSpriteKlicked(x, 2, isPressed, controls))
            {
                isPressed = true;
                Console.WriteLine("load Controls");
                return EnumGameStates.controls;
            }
            if (NavigationHelp.isSpriteKlicked(x, 3, isPressed, credits))
            {
                isPressed = true;
                Console.WriteLine("load Credits");
                return EnumGameStates.credits;
            }
            if (NavigationHelp.isSpriteKlicked(x, 4, isPressed, end))
            {
                isPressed = true;
                return EnumGameStates.none;
            }

            if (NavigationHelp.isMouseInSprite(back))
            {
                back.Texture = backSelected;
            }

            if(!NavigationHelp.isMouseInSprite(back))
            {
                back.Texture = backNotSelected;
            }

            if(Keyboard.IsKeyPressed(Keyboard.Key.Back) || (NavigationHelp.isMouseInSprite(back) && Mouse.IsButtonPressed(Mouse.Button.Left)))
            {
                isPressed = true;
                return EnumGameStates.titleSreen;
            }

            if (!Mouse.IsButtonPressed(Mouse.Button.Left) && !NavigationHelp.isAnyKeyPressed())
            {
                isPressed = false;
            }

            return EnumGameStates.mainMenu;
        }

        public void draw(RenderWindow window)
        {
            window.Draw(backGround);
            window.SetView(view);
            window.Draw(start);
            window.Draw(optionsButton);
            window.Draw(controls);
            window.Draw(credits);
            window.Draw(end);
            window.Draw(back);
        }
    }
}
