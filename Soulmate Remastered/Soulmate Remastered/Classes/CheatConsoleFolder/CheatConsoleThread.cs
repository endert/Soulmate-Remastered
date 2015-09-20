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
        public static CheatConsole CheatConsole { get; protected set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public CheatConsoleThreadStart()
        {
            //save game thread
            normalGameThread = Thread.CurrentThread;

            //initializing new thread by giving a Main to start
            cheatConsoleThread = new Thread(new ThreadStart(CheatConsoleThreadStart.CheatConsoleThreadMain));

            //Start the thread
            cheatConsoleThread.Start();
        }

        /// <summary>
        /// Main of the cheatConsoleThread
        /// </summary>
        public static void CheatConsoleThreadMain()
        {
            //initialize
            CheatConsole = new CheatConsole();
            Input.SetKeyPressed(Keyboard.Key.T);

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
                CheatConsole.Update();
            }
        }

        /// <summary>
        /// terminate the console thread
        /// </summary>
        public void Delete()
        {
            cheatConsoleThread.Abort();
        }

        /// <summary>
        /// switches from Game to Console when the ConsoleOpen button is pressed
        /// </summary>
        public void Update()
        {
            if (Keyboard.IsKeyPressed(Controls.CheatConsoleOpen) && !Game.IsPressed)
            {
                cheatConsoleThread.Interrupt();
                Input.SetKeyPressed(Keyboard.Key.T);

                try
                {
                    Thread.Sleep(Timeout.Infinite);
                }
                catch (ThreadInterruptedException)
                {
                    Game.IsPressed = true;
                }
            }
        }
    }
}
