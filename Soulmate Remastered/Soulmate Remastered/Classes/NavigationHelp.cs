using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using Soulmate_Remastered.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes
{
    /// <summary>
    /// contains helpful methods for different things
    /// </summary>
    class NavigationHelp
    {
        /// <summary>
        /// checking if any key was pressed
        /// </summary>
        /// <returns>returns true if any key was pressed</returns>
        public static bool isAnyKeyPressed()
        {
            for (int i = 0; i < (int)Keyboard.Key.KeyCount; i++)
            {
                if (Keyboard.IsKeyPressed((Keyboard.Key)i))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
