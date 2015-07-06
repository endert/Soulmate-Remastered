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

        public void Transform(Matrix3x3 m)
        {
            Position = m * new Vector3(Position);
        }
    }
}
