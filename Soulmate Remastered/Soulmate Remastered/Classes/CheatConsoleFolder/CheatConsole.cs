using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.CheatConsoleFolder
{
    class CheatConsole
    {
        bool isOpen;

        public CheatConsole()
        {
            isOpen = false;
        }

        public void open()
        {
            isOpen = true;
        }

        public void update()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.T))
            {
                isOpen = !isOpen;
            }

            if (isOpen)
            {
                Console.Read();
                isOpen = false;
            }
        }
    }
}
