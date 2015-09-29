using Soulmate_Remastered.Classes.CheatConsoleFolder.CheatConsoleThreadFolder;
using Soulmate_Remastered.Core;
using System.Threading;

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
        /// opens the cheatconsole
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OpenCheatConsole(object sender, KeyEventArgs e)
        {
            if (e.Key == Controls.Key.CheatConsole)
            {
                cheatConsoleThread.Interrupt();
                Input.SetKeyPressed(Controls.Cast(Controls.Key.CheatConsole));

                try
                {
                    Thread.Sleep(Timeout.Infinite);
                }
                catch (ThreadInterruptedException)
                {
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public CheatConsoleThreadStart()
        {
            KeyboardControler.KeyPressed += OpenCheatConsole;

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
            Input.SetKeyPressed(Controls.Cast(Controls.Key.CheatConsole));

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
    }
}
