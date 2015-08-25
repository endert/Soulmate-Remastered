﻿using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameStatesFolder
{
    class LoadGame : AbstractMainMenu
    {
        private readonly String loadFile = "Saves/save.soul";

        Texture loadSelected;
        Texture loadNotSelected;
        Sprite load;

        Texture newGameSelected;
        Texture newGameNotSelected;
        Sprite newGame;

        /// <summary>
        /// <para>value to define which sprite is selected</para>
        /// <para>0=>load-sprite</para>
        /// <para>1=>newGame-sprite></para>
        /// </summary>
        int spriteNumber;

        public override void initialize()
        {
            base.initialize();
            
            load = new Sprite(loadNotSelected);
            load.Position = new Vector2f(300, 300);

            newGame = new Sprite(newGameNotSelected);
            newGame.Position = new Vector2f(load.Position.X, load.Position.Y + 150);
        }

        public override void loadContent()
        {
            base.loadContent();
            
            loadSelected = new Texture("Pictures/Menu/MainMenu/Load/LoadSelected.png");
            loadNotSelected = new Texture("Pictures/Menu/MainMenu/Load/LoadNotSelected.png");

            newGameSelected = new Texture("Pictures/Menu/MainMenu/NewGame/NewGameSelected.png");
            newGameNotSelected = new Texture("Pictures/Menu/MainMenu/NewGame/NewGameNotSelected.png");
        }

        public override EnumGameStates update(GameTime gameTime)
        {
            gameUpdate(gameTime);
            
            if (NavigationHelp.isMouseInSprite(load))
            {
                spriteNumber = 0;
            }

            if (NavigationHelp.isMouseInSprite(newGame))
            {
                spriteNumber = 1;
            }

            if (Keyboard.IsKeyPressed(Controls.Up) && !Game.isPressed)
            {
                spriteNumber = (spriteNumber + 1) % 2;
                Game.isPressed = true;
            }

            if (Keyboard.IsKeyPressed(Controls.Down) && !Game.isPressed)
            {
                spriteNumber = (spriteNumber + 1) % 2;
                Game.isPressed = true;
            }

            if (spriteNumber == 0)
            {
                load.Texture = loadSelected;
                newGame.Texture = newGameNotSelected;
            }

            if (spriteNumber == 1)
            {
                load.Texture = loadNotSelected;
                newGame.Texture = newGameSelected;
            }

            if (backValueSelected == 1)
                return EnumGameStates.mainMenu;

            if (NavigationHelp.isSpriteKlicked(spriteNumber, 0, load, Controls.Return))
            {
                Game.isPressed = true;
                AbstractGamePlay.loading = true;
                SaveGame.loadPath = loadFile;
                SaveGame.loadGame();

                switch (GameObjectHandler.lvl)
                {
                    case 0:
                        return EnumGameStates.village;
                    case 1:
                        return EnumGameStates.inGame;
                    default:
                        return EnumGameStates.village;
                }
            }

            if (NavigationHelp.isSpriteKlicked(spriteNumber, 1, newGame, Controls.Return))
            {
                Game.isPressed = true;
                Console.WriteLine("new Game");
                AbstractGamePlay.startNewGame = true;
                return EnumGameStates.village;
            }           

            return EnumGameStates.loadGame;
        }

        public override void draw(RenderWindow window)
        {
            base.draw(window);

            window.Draw(load);
            window.Draw(newGame);
            window.Draw(back);
        }
    }
}
