using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.CheatConsoleFolder.CheatConsoleThreadFolder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.CheatConsoleFolder
{
    /// <summary>
    /// the connection between main game and console
    /// </summary>
    class CheatConsoleThreadStart
    {
        /// <summary>
        /// the thread of the Main Game.
        /// </summary>
        public static Thread normalGameThread;

        /// <summary>
        /// the thread of the Console
        /// </summary>
        Thread cheatConsoleThread;

        /// <summary>
        /// the Console
        /// </summary>
        public static CheatConsole cheatConsole { get; protected set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public CheatConsoleThreadStart()
        {
            //save game thread
            normalGameThread = Thread.CurrentThread;

            //initializing new thread by giving a Main to start
            cheatConsoleThread = new Thread(new ThreadStart(CheatConsoleThreadStart.cheatConsoleThreadMain));

            //Start the thread
            cheatConsoleThread.Start();
        }

        /// <summary>
        /// Main of the cheatConsoleThread
        /// </summary>
        public static void cheatConsoleThreadMain()
        {
            //initialize
            cheatConsole = new CheatConsole();
            Input.setKeyPressed(Keyboard.Key.T);

            //sleep till interupted
            try
            {
                Thread.Sleep(Timeout.Infinite);
            }
            catch (ThreadInterruptedException)
            {
            }

            //updatate till another sleep order
            while (true)
            {
                cheatConsole.update();
            }
        }

        /// <summary>
        /// terminate the console thread
        /// </summary>
        public void delete()
        {
            cheatConsoleThread.Abort();
        }

        /// <summary>
        /// switches from Game to Console when the ConsoleOpen button is pressed
        /// </summary>
        public void update()
        {
            if (Keyboard.IsKeyPressed(Controls.CheatConsoleOpen) && !Game.isPressed)
            {
                cheatConsoleThread.Interrupt();
                Input.setKeyPressed(Keyboard.Key.T);

                try
                {
                    Thread.Sleep(Timeout.Infinite);
                }
                catch (ThreadInterruptedException)
                {
                    Game.isPressed = true;
                }
            }
        }
    }
}
