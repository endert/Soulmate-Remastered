using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Core
{
    /// <summary>
    /// a Vertex
    /// </summary>
    struct Vertex
    {
        public Vector2 Position;
        
        public Vertex(Vector2 pos)
        {
            Position = pos;
        }

        //public static Vertex operator *(Matrix3x3 trans, Vertex v)
        //{

        //}
    }
}
