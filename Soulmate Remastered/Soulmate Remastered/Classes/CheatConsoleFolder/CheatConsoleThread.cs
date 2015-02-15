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
    class CheatConsoleThread
    {
        public static Thread normalGameThread;
        Thread cheatConsoleThread;

        public CheatConsoleThread()
        {
            normalGameThread = Thread.CurrentThread;
            cheatConsoleThread = new Thread(new ThreadStart(CheatConsoleThread.cheatConsoleMain));
            cheatConsoleThread.Start();
        }

        public static void cheatConsoleMain()
        {
            CheatConsole cheatConsole = new CheatConsole();

            try
            {
                Thread.Sleep(Timeout.Infinite);
            }
            catch (ThreadInterruptedException)
            {
                CheatConsole.isPressed = true;
            }

            while (true)
            {
                cheatConsole.update();
            }
        }

        public void delete()
        {
            cheatConsoleThread.Abort();
        }

        public void update()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.T) && !Game.isPressed)
            {
                cheatConsoleThread.Interrupt();

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
