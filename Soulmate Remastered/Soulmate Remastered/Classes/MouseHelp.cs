using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes
{
    class MouseHelp
    {
        public static bool isMouseInSprite(Sprite sprite)
        {
            RenderWindow window = AbstractGame.window;
            return (Mouse.GetPosition(window).X >= sprite.Position.X && Mouse.GetPosition(window).X <= sprite.Position.X + sprite.Texture.Size.X
                 && Mouse.GetPosition(window).Y >= sprite.Position.Y && Mouse.GetPosition(window).Y <= sprite.Position.Y + sprite.Texture.Size.Y);
        }

    }
}
