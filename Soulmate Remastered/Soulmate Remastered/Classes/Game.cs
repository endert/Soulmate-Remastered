using SFML.Graphics;
using Soulmate_Remastered.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Soulmate_Remastered.Classes
{
    class Game : AbstractGame
    {
        /// <summary>
        /// the x size of the window
        /// </summary>
        public static uint WindowSizeX { get { if (Fullscreen) return 1920; return 1280; } }
        /// <summary>
        /// the y size of the window
        /// </summary>
        public static uint WindowSizeY { get { if (Fullscreen) return 1080; return 720; } }

        /// <summary>
        /// the font, that is used for all texts
        /// </summary>
        public static readonly Font font = new Font("FontFolder/arial_narrow_7.ttf");

        /// <summary>
        /// the current gamestate wich is on
        /// </summary>
        EnumGameStates currentGameState = EnumGameStates.TitleScreen;
        /// <summary>
        /// previous gamestate, the gamestate of the last iteration of the gameloop
        /// </summary>
        EnumGameStates prevGameState;

        /// <summary>
        /// the class that is the actual gamestate
        /// </summary>
        GameState gameState;

        /// <summary>
        /// the style for the game (fullscreen etc.)
        /// </summary>
        static SFML.Window.Styles screen = SFML.Window.Styles.Default;

        /// <summary>
        /// bool if the game is in fullscreen
        /// </summary>
        public static bool Fullscreen { get { return screen.Equals(SFML.Window.Styles.Fullscreen); } }

        public Game() : base(WindowSizeX, WindowSizeY, "Soulmate", screen) { }

        /// <summary>
        /// updates the game
        /// </summary>
        /// <param name="time"></param>
        public override void Update(GameTime time)
        {
            MouseControler.Update();
            KeyboardControler.Update();

            if (currentGameState != prevGameState)
            {
                HandleGameState();
            }

            currentGameState = gameState.Update(time);
        }

        /// <summary>
        /// draws everything
        /// </summary>
        /// <param name="window"></param>
        public override void Draw(RenderWindow window)
        {
            gameState.Draw(window);
        }

        /// <summary>
        /// handles the gamestates
        /// <para>may only be called when currentgamestate != prevgamestate</para>
        /// <para>decides what to do then</para>
        /// </summary>
        void HandleGameState()
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

            gameState.LoadContent();

            gameState.Initialize();

            prevGameState = currentGameState;
        }
    }
}
