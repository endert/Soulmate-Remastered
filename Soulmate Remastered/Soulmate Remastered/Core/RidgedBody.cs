using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Core
{
    /// <summary>
    /// Transformable Body
    /// </summary>
    class RidgedBody
    {
        List<Vertex> vertecies;
        public Vector2 Position { get; protected set; }

        public RidgedBody(Vector2 pos)
        {
            Position = pos;
            vertecies = new List<Vertex>();
            vertecies.Add(new Vertex(pos));
        }

    }
}
