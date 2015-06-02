using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes
{
    class NavigationHelp
    {
        /// <summary>
        /// checking if the mouse is in the correct sprite
        /// </summary>
        /// <param name="sprite">sprite in which the mouse be should be</param>
        /// <returns></returns>
        public static bool isMouseInSprite(Sprite sprite)
        {
            RenderWindow window = AbstractGame.window; //don't no why, maybe i was to lazy to write every time the long
            return (Mouse.GetPosition(window).X >= sprite.Position.X && Mouse.GetPosition(window).X <= sprite.Position.X + sprite.Texture.Size.X
                 && Mouse.GetPosition(window).Y >= sprite.Position.Y && Mouse.GetPosition(window).Y <= sprite.Position.Y + sprite.Texture.Size.Y);
        }

        /// <summary>
        /// checking if the right sprite was klicked
        /// </summary>
        /// <param name="x">current value of the sprite which is selected</param>
        /// <param name="y">value for the correct sprite</param>
        /// <param name="sprite">sprite, which is klicked</param>
        /// <param name="key">key, which must klicked for the sprite</param>
        /// <returns></returns>
        public static bool isSpriteKlicked(int x, int y, Sprite sprite, Keyboard.Key key)
        {
            return (x == y && (Keyboard.IsKeyPressed(key) || (Mouse.IsButtonPressed(Mouse.Button.Left) && isMouseInSprite(sprite))) && !Game.isPressed);
        }

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
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list">list in which the null object should be deleted</param>
        /// <returns>same list without null objects</returns>
        public static void deleteNullObjectFromList(List<Stack<AbstractItem>> list)
        {
            for (int i = 0; i < list.Count; ++i)
            {
                if (list[i] == null)
                {
                    list.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
