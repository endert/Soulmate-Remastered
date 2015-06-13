using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Core
{
    /// <summary>
    /// contains only an enum with the directions
    /// </summary>
    public static class MovingDirection
    {
        /// <summary>
        /// the movingDirection
        /// </summary>
        public enum Direction
        {
            Back,
            Right,
            Left,
            Front,
            UpRight,
            UpLeft,
            DownRight,
            DownLeft,
            Zero
        }
    }
}
