using Soulmate_Remastered.Classes.GameObjectFolder;
using System;

namespace Soulmate_Remastered.Core
{
    /// <summary>
    /// arguments of the collision
    /// </summary>
    class CollisionArgs : EventArgs
    {
        /// <summary>
        /// the gameobject with wich the sender collided
        /// </summary>
        public GameObject CollidedWith;
    }
}
