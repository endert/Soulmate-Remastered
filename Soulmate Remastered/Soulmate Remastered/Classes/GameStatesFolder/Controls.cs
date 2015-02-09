﻿using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameStatesFolder
{
    class Controls : GameState
    {
        bool isPressedEnter;
     
        Texture controlsTexture;
        Sprite controls;

        View view;

        public void initialize()
        {
            isPressedEnter = true;
            controls = new Sprite(controlsTexture);
            controls.Position = new Vector2f(0, 0);

            view = new View(new FloatRect(0, 0, Game.windowSizeX, Game.windowSizeY));
        }

        public void loadContent()
        {
            controlsTexture = new Texture("Pictures/Menu/MainMenu/Controls/ControlsMenu.png");
        }

        public EnumGameStates update(GameTime gameTime)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Return) && !isPressedEnter)
            {
                isPressedEnter = true;
                return EnumGameStates.mainMenu;
            }

            if (!Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                isPressedEnter = false;
            }

            return EnumGameStates.controls;
        }

        public void draw(RenderWindow window)
        {
            window.Draw(controls);
        }
    }
}
