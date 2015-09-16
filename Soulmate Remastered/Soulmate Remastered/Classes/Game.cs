using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.GameStatesFolder;
using Soulmate_Remastered.Core;
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
        public static uint windowSizeX { get { if (fullscreen) return 1920; return 1280; } }
        public static uint windowSizeY { get { if (fullscreen) return 1080; return 720; } }

        public static bool isPressed { get; set; }
        public static Font font = new Font("FontFolder/arial_narrow_7.ttf");

        EnumGameStates currentGameState = EnumGameStates.TitleScreen;
        EnumGameStates prevGameState;

        GameState gameState;

        static Styles screen = Styles.Default;

        public static bool fullscreen { get { return screen.Equals(Styles.Fullscreen); } }

        public Game() : base(windowSizeX, windowSizeY, "Soulmate", screen) { }

        public override void Update(GameTime time)
        {
            MouseControler.Update();

            if (currentGameState != prevGameState)
            {
                handleGameState();
            }

            if (!NavigationHelp.isAnyKeyPressed() && !Mouse.IsButtonPressed(Mouse.Button.Left) && !Mouse.IsButtonPressed(Mouse.Button.Right))
                isPressed = false;

            currentGameState = gameState.Update(time);
        }

        public override void Draw(RenderWindow window)
        {
            gameState.Draw(window);
        }

        void handleGameState()
        {
            //coole version des switches:
            if(currentGameState == EnumGameStates.None)
            {
                window.Close();
                return;
            }

            bool StateFound = false;

            string className = currentGameState.ToString().Split('.').Last();

            IEnumerable<Type> classes = typeof(Game).Assembly.GetTypes().Where(type => type.GetInterfaces().Contains(typeof(GameState)));

            foreach (Type t in classes)
            {
                if (t.Name.Equals(className))
                {
                    gameState = (GameState)Activator.CreateInstance(t);
                    StateFound = true;
                }
            }

            if(!StateFound)
                throw new NotFiniteNumberException();

            //switch (currentGameState)
            //{
            //    case EnumGameStates.None:
            //        window.Close();
            //        break;
            //    case EnumGameStates.MainMenu:
            //        gameState = new MainMenu();
            //        break;
            //    case EnumGameStates.InGame:
            //        gameState = new InGame();
            //        break;
            //    case EnumGameStates.ControlsSetting:
            //        gameState = new ControlsSetting();
            //        break;
            //    case EnumGameStates.Options:
            //        gameState = new Options();
            //        break;
            //    case EnumGameStates.TitleScreen:
            //        gameState = new TitleScreen();
            //        break;
            //    case EnumGameStates.Village:
            //        gameState = new Village();
            //        break;
            //    case EnumGameStates.LoadGame:
            //        gameState = new LoadGame();
            //        break;
            //    case EnumGameStates.Credits:
            //        gameState = new Credits();
            //        break;
            //    //case EGameStates.gameWon:
            //    //    gameState = new GameWon();
            //    //    break;
            //    default:
            //        throw new NotFiniteNumberException();
            //}

            gameState.LoadContent();

            gameState.Initialize();

            prevGameState = currentGameState;
        }
    }
}
