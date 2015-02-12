﻿using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameStatesFolder
{
    class ControlsSetting : GameState
    {
        Texture controlsTexture;
        Texture backSelected;
        Texture backNotSelected;

        Sprite controls;
        Sprite back;

        View view;

        public void initialize()
        {
            controls = new Sprite(controlsTexture);
            controls.Position = new Vector2f(0, 0);

            back = new Sprite(backNotSelected);
            back.Position = MainMenu.getBackPostion();

            view = new View(new FloatRect(0, 0, Game.windowSizeX, Game.windowSizeY));
        }

        public void loadContent()
        {
            controlsTexture = new Texture("Pictures/Menu/MainMenu/Controls/ControlsMenu.png");

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

            if (Keyboard.IsKeyPressed(Keyboard.Key.Return) && !Game.isPressed)
            {
                Game.isPressed = true;
                return EnumGameStates.mainMenu;
            }

            if (NavigationHelp.isSpriteKlicked(0, 0, Game.isPressed, back, Controls.Escape))
            {
                Game.isPressed = true;
                return EnumGameStates.mainMenu;
            }

            return EnumGameStates.controls;
        }

        public void draw(RenderWindow window)
        {
            window.Draw(controls);
            window.Draw(back);
        }
    }
}
