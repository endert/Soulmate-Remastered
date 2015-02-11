using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes
{
    class NavigationHelp
    {
        public static bool isMouseInSprite(Sprite sprite)
        {
            RenderWindow window = AbstractGame.window;
            return (Mouse.GetPosition(window).X >= sprite.Position.X && Mouse.GetPosition(window).X <= sprite.Position.X + sprite.Texture.Size.X
                 && Mouse.GetPosition(window).Y >= sprite.Position.Y && Mouse.GetPosition(window).Y <= sprite.Position.Y + sprite.Texture.Size.Y);
        }

        public static bool isSpriteKlicked(int x, int y, bool isPressed, Sprite sprite)
        {
            return (x == y && (Keyboard.IsKeyPressed(Keyboard.Key.Return) || (Mouse.IsButtonPressed(Mouse.Button.Left) && isMouseInSprite(sprite))) && !isPressed);
        }

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
