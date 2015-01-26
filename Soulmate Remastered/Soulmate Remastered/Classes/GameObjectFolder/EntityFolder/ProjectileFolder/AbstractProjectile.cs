using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.ProjectileFolder
{
    abstract class AbstractProjectile : Entity
    {
        protected float duration;
        protected Vector2f startPosition;
        public override String type { get { return base.type + ".Projectile"; } }

        public bool inRange()
        {
            if ((duration * 1000) < stopWatchList[0].ElapsedMilliseconds)
                return false;
            else
                return true;
        }

        public override void update(GameTime gameTime)
        {
            movementSpeed = movementSpeedConstant * (float)gameTime.EllapsedTime.TotalMilliseconds;
            sprite.Position = position;
            hitBox.Position = position;

            stopWatchList[0].Start();

            if (inRange())
            {
                move(facingDirection);
            }

            else
            {
                isAlive = false;
            }
            
        }
    }
}
