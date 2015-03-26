﻿using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.GameStatesFolder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes
{
    class Game : Soulmate_Remastered.Classes.AbstractGame
    {
        public static uint windowSizeX = 1280;
        public static uint windowSizeY = 720;

        public static bool isPressed { get; set; }
        public static Font font = new Font("FontFolder/arial_narrow_7.ttf");

        EnumGameStates currentGameState = EnumGameStates.titleSreen;
        EnumGameStates prevGameState;

        GameState gameState;

        public Game() : base(windowSizeX, windowSizeY, "Soulmate") { }

        public override void update(GameTime time)
        {
            if (currentGameState != prevGameState)
            {
                handleGameState();
            }

            if (!NavigationHelp.isAnyKeyPressed() && !Mouse.IsButtonPressed(Mouse.Button.Left) && !Mouse.IsButtonPressed(Mouse.Button.Right))
                isPressed = false;

            currentGameState = gameState.update(time);
        }

        public override void draw(RenderWindow window)
        {
            gameState.draw(window);
        }

        void handleGameState()
        {
            switch (currentGameState)
            {
                case EnumGameStates.none:
                    window.Close();
                    break;
                case EnumGameStates.mainMenu:
                    gameState = new MainMenu();
                    break;
                case EnumGameStates.inGame:
                    gameState = new InGame();
                    break;
                case EnumGameStates.controls:
                    gameState = new ControlsSetting();
                    break;
                case EnumGameStates.options:
                    gameState = new Options();
                    break;
                case EnumGameStates.titleSreen:
                    gameState = new TitleScreen();
                    break;
                case EnumGameStates.village:
                    gameState = new Village();
                    break;
                case EnumGameStates.loadGame:
                    gameState = new LoadGame();
                    break;
                case EnumGameStates.credits:
                    gameState = new Credits();
                    break;
                //case EGameStates.gameWon:
                //    gameState = new GameWon();
                //    break;
                default:
                    throw new NotFiniteNumberException();
            }

            gameState.loadContent();

            gameState.initialize();

            prevGameState = currentGameState;
        }
    }
}
