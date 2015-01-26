using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameStatesFolders
{
    class MainMenu : GameState
    {
        bool isPressed;
        bool isPressedEnter;
        int x; //für Menüsteuerung

        Texture backGroundTexture;
        Texture startSelected;
        Texture startNotSelected;
        Texture exitSelected;
        Texture exitNotSelected;
        Texture controlsSelected;
        Texture controlsNotSelected;
        Texture optionsSelected;
        Texture optionsNotSelected;

        Sprite backGround;
        Sprite start;
        Sprite exit;
        Sprite optionsButton;
        Sprite controls;

        View view;

        public void initialize()
        {
            isPressed = false;
            isPressedEnter = true;
            x = 0;

            backGround = new Sprite(backGroundTexture);
            backGround.Position = new Vector2f(0, 0);

            start = new Sprite(startNotSelected);
            start.Position = new Vector2f(300, 250);

            exit = new Sprite(exitNotSelected);
            exit.Position = new Vector2f(300, 325);

            optionsButton = new Sprite(optionsNotSelected);
            optionsButton.Position = new Vector2f(300, 400);

            controls = new Sprite(controlsNotSelected);
            controls.Position = new Vector2f(300, 475);

            view = new View(new FloatRect(0, 0, Game.windowSizeX, Game.windowSizeY));
        }

        public void loadContent()
        {
            backGroundTexture = new Texture("Pictures/MainMenu/StartScreen.png");

            startSelected = new Texture("Pictures/MainMenu/Start/StartSelected.png");
            startNotSelected = new Texture("Pictures/MainMenu/Start/StartNotSelected.png");

            exitSelected = new Texture("Pictures/MainMenu/End/EndSelected.png");
            exitNotSelected = new Texture("Pictures/MainMenu/End/EndNotSelected.png");

            optionsSelected = new Texture("Pictures/MainMenu/Options/OptionsSelected.png");
            optionsNotSelected = new Texture("Pictures/MainMenu/Options/OptionsNotSelected.png");

            controlsSelected = new Texture("Pictures/MainMenu/Controls/ControlsSelected.png");
            controlsNotSelected = new Texture("Pictures/MainMenu/Controls/ControlsNotSelected.png");
        }

        public EnumGameStates update(GameTime gameTime)
        {
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

            if (!Keyboard.IsKeyPressed(Keyboard.Key.Down) && !Keyboard.IsKeyPressed(Keyboard.Key.Up))
                isPressed = false;

            if (x == 0)
            {
                start.Texture = startSelected;
                exit.Texture = exitNotSelected;
                optionsButton.Texture = optionsNotSelected;
                controls.Texture = controlsNotSelected;
            }

            if (x == 1)
            {
                start.Texture = startNotSelected;
                exit.Texture = exitSelected;
                optionsButton.Texture = optionsNotSelected;
                controls.Texture = controlsNotSelected;
            }

            if (x == 2)
            {
                start.Texture = startNotSelected;
                exit.Texture = exitNotSelected;
                optionsButton.Texture = optionsSelected;
                controls.Texture = controlsNotSelected;
            }

            if (x == 3)
            {
                start.Texture = startNotSelected;
                exit.Texture = exitNotSelected;
                optionsButton.Texture = optionsNotSelected;
                controls.Texture = controlsSelected;
            }

            if (x == 0 && Keyboard.IsKeyPressed(Keyboard.Key.Return) && !isPressedEnter)
            {
                isPressedEnter = true;
                return EnumGameStates.inGame;
            }
            if (x == 1 && Keyboard.IsKeyPressed(Keyboard.Key.Return) && !isPressedEnter)
            {
                isPressedEnter = true;
                return EnumGameStates.none;
            }
            if (x == 2 && Keyboard.IsKeyPressed(Keyboard.Key.Return) && !isPressedEnter)
            {
                isPressedEnter = true;
                return EnumGameStates.options;
            }
            if (x == 3 && Keyboard.IsKeyPressed(Keyboard.Key.Return) && !isPressedEnter)
            {
                isPressedEnter = true;
                return EnumGameStates.controls;
            }

            if(!Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                isPressedEnter = false;
            }

            return EnumGameStates.mainMenu;
        }

        public void draw(RenderWindow window)
        {
            window.Draw(backGround);
            window.SetView(view);
            window.Draw(start);
            window.Draw(exit);
            window.Draw(optionsButton);
            window.Draw(controls);
        }
    }
}
