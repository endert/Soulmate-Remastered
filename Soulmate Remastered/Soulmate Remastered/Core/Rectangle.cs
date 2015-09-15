using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Core
{
    /// <summary>
    /// declares a rectangle, needed for mouse controls
    /// </summary>
    struct Rectangle
    {
        /// <summary>
        /// the upper left corner
        /// </summary>
        public Vector2 Position;
        /// <summary>
        /// the size of the rectangle
        /// </summary>
        public Vector2 Size;

        /// <summary>
        /// initialize a rectangle
        /// </summary>
        /// <param name="position">the position of the upper left corner</param>
        /// <param name="size">the size of the rectangle, the diagonal to the down right corner</param>
        public Rectangle(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
        }
    }
}
