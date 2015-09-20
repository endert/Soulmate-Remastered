using SFML.Graphics;
using SFML.Window;
using SFML.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soulmate_Remastered.Classes;
using Soulmate_Remastered.Classes.GameObjectFolder;
using Soulmate_Remastered.Classes.GameStatesFolder;
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

        public AbstractGame(uint width, uint height, string title, Styles screen)
        {
            window = new RenderWindow(new VideoMode(width, height), title, screen);

            window.Closed += (sender, e) => { ((RenderWindow)sender).Close(); };

            SGameTime = new GameTime();
            cheatConsole = new CheatConsoleThreadStart();
            MouseControler.Initialize();
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
                cheatConsole.Update();
                Draw(window);
                window.Display();
            }

            cheatConsole.Delete();
        }

        public abstract void Draw(RenderWindow window);

        public abstract void Update(GameTime gameTime);
    }
}
