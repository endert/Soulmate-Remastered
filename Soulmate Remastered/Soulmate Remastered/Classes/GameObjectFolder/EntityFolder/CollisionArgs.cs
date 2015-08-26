using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder
{
    /// <summary>
    /// arguments of the collision
    /// </summary>
    class CollisionArgs : EventArgs
    {
        /// <summary>
        /// the gameobject with wich the sender collided
        /// </summary>
        public GameObject CollidedWith { get; set; }
    }
}
