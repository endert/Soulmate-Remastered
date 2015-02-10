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
        bool isPressed;
        bool isMouseKlicked;
        int x; //für Menüsteuerung

        Texture backGroundTexture;
        Texture startSelected;
        Texture startNotSelected;
        Texture endSelected;
        Texture endNotSelected;
        Texture controlsSelected;
        Texture controlsNotSelected;
        Texture optionsSelected;
        Texture optionsNotSelected;

        Sprite backGround;
        Sprite start;
        Sprite end;
        Sprite optionsButton;
        Sprite controls;

        View view;

        public void initialize()
        {
            isPressed = false;
            isPressed = true;
            x = 0;

            backGround = new Sprite(backGroundTexture);
            backGround.Position = new Vector2f(0, 0);

            start = new Sprite(startNotSelected);
            start.Position = new Vector2f(300, 250);

            optionsButton = new Sprite(optionsNotSelected);
            optionsButton.Position = new Vector2f(300, 325);

            controls = new Sprite(controlsNotSelected);
            controls.Position = new Vector2f(300, 400);

            end = new Sprite(endNotSelected);
            end.Position = new Vector2f(300, 475);

            view = new View(new FloatRect(0, 0, Game.windowSizeX, Game.windowSizeY));
        }

        public void loadContent()
        {
            backGroundTexture = new Texture("Pictures/Menu/MainMenu/Background.png");

            startSelected = new Texture("Pictures/Menu/MainMenu/Start/StartSelected.png");
            startNotSelected = new Texture("Pictures/Menu/MainMenu/Start/StartNotSelected.png");

            endSelected = new Texture("Pictures/Menu/MainMenu/End/EndSelected.png");
            endNotSelected = new Texture("Pictures/Menu/MainMenu/End/EndNotSelected.png");

            optionsSelected = new Texture("Pictures/Menu/MainMenu/Options/OptionsSelected.png");
            optionsNotSelected = new Texture("Pictures/Menu/MainMenu/Options/OptionsNotSelected.png");

            controlsSelected = new Texture("Pictures/Menu/MainMenu/Controls/ControlsSelected.png");
            controlsNotSelected = new Texture("Pictures/Menu/MainMenu/Controls/ControlsNotSelected.png");
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
            if (NavigationHelp.isMouseInSprite(end))
            {
                x = 3;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && !isPressed)
            {
                x = (x + 3) % 4;
                isPressed = true;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && !isPressed)
            {
                x = (x + 1) % 4;
                isPressed = true;
            }

            if (x == 0)
            {
                start.Texture = startSelected;
                optionsButton.Texture = optionsNotSelected;
                controls.Texture = controlsNotSelected;
                end.Texture = endNotSelected;
            }

            if (x == 1)
            {
                start.Texture = startNotSelected;
                optionsButton.Texture = optionsSelected;
                controls.Texture = controlsNotSelected;
                end.Texture = endNotSelected;
            }

            if (x == 2)
            {
                start.Texture = startNotSelected;
                optionsButton.Texture = optionsNotSelected;
                controls.Texture = controlsSelected;
                end.Texture = endNotSelected;
            }

            if (x == 3)
            {
                start.Texture = startNotSelected;
                optionsButton.Texture = optionsNotSelected;
                controls.Texture = controlsNotSelected;
                end.Texture = endSelected;
            }

            if (NavigationHelp.isSpriteKlicked(x, 0, isPressed))
            {
                isPressed = true;
                return EnumGameStates.loadGame;
            }
            if (NavigationHelp.isSpriteKlicked(x, 1, isPressed))
            {
                isPressed = true;
                Console.WriteLine("load Options");
                return EnumGameStates.options;
            }
            if (NavigationHelp.isSpriteKlicked(x, 2, isPressed))
            {
                isPressed = true;
                Console.WriteLine("load Controls");
                return EnumGameStates.controls;
            }
            if (NavigationHelp.isSpriteKlicked(x, 3, isPressed))
            {
                isPressed = true;
                return EnumGameStates.none;
            }

            if (!Keyboard.IsKeyPressed(Keyboard.Key.Return) && !Mouse.IsButtonPressed(Mouse.Button.Left) && !Keyboard.IsKeyPressed(Keyboard.Key.Down) && !Keyboard.IsKeyPressed(Keyboard.Key.Up))
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
            window.Draw(end);
        }
    }
}
