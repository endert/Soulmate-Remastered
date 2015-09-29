using SFML.Graphics;
using Soulmate_Remastered.Classes.CheatConsoleFolder;
using Soulmate_Remastered.Core;

namespace Soulmate_Remastered.Classes
{
    abstract class AbstractGame
    {
        /// <summary>
        /// the cheat console thread
        /// </summary>
        static CheatConsoleThreadStart cheatConsole;
        /// <summary>
        /// the window
        /// </summary>
        public static RenderWindow window;
        /// <summary>
        /// the GameTime as static property
        /// </summary>
        public static GameTime SGameTime { get; private set; }

        public AbstractGame(uint width, uint height, string title, SFML.Window.Styles screen)
        {
            window = new RenderWindow(new SFML.Window.VideoMode(width, height), title, screen);

            window.Closed += (sender, e) => { ((RenderWindow)sender).Close(); };

            SGameTime = new GameTime();
            cheatConsole = new CheatConsoleThreadStart();
            MouseControler.Initialize();
            KeyboardControler.Initialize();
        }

        /// <summary>
        /// Draws everything, checks logic calls all updates, overall let the game run
        /// </summary>
        public void Run()
        {
            SGameTime.Start();

            while(window.IsOpen())
            {
                window.Clear(new Color(101, 156, 239)); //CornFlowerBlue
                window.DispatchEvents();
                SGameTime.Update();
                Update(SGameTime);
                Draw(window);
                window.Display();
            }

            cheatConsole.Delete();
        }

        public abstract void Draw(RenderWindow window);

        public abstract void Update(GameTime gameTime);
    }
}
