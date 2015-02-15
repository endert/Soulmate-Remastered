using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.CheatConsoleFolder.CheatConsoleThreadFolder
{
    class Input
    {
        public static void InputHandle()
        {
            if (NavigationHelp.isAnyKeyPressed())
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Back) && !CheatConsole.isPressed)
                {
                    CheatConsole.isPressed = true;
                    CheatConsole.watch.Restart();
                    if (CheatConsole.text.DisplayedString.Length > 0)
                    {
                        CheatConsole.text.DisplayedString = CheatConsole.text.DisplayedString.Remove(CheatConsole.text.DisplayedString.Length - 1);
                    }
                }
                else
                {
                    CheatConsole.text.DisplayedString += Input.ButtonString();
                }
            }
        }

        public static String ButtonString()
        {
            if (!CheatConsole.isPressed)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                {
                    CheatConsole.isPressed = true;
                    CheatConsole.watch.Restart();
                    return " ";
                }
                if ((Keyboard.IsKeyPressed(Keyboard.Key.LShift) || Keyboard.IsKeyPressed(Keyboard.Key.RShift)) && Keyboard.IsKeyPressed(Keyboard.Key.Num7))
                {
                    CheatConsole.isPressed = true;
                    CheatConsole.watch.Restart();
                    return "/";
                }

                for (int i = 0; i < 36; i++)
                {
                    if (Keyboard.IsKeyPressed((Keyboard.Key)i))
                    {
                        CheatConsole.isPressed = true;
                        CheatConsole.watch.Restart();
                        if (i > 25)
                        {
                            return ((Keyboard.Key)i).ToString().ToCharArray()[3].ToString();
                        }
                        return ((Keyboard.Key)i).ToString();
                    }
                }
            }
            return null;
        }
    }
}
