using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.EnemyFolder;
using Soulmate_Remastered.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.ProjectileFolder
{
    /// <summary>
    /// base class for all projectiles
    /// </summary>
    abstract class AbstractProjectile : Entity
    {
        /// <summary>
        /// time until despawn in sec
        /// </summary>
        protected float duration;
        /// <summary>
        /// the start position
        /// </summary>
        protected Vector2 startPosition;
        /// <summary>
        /// is this a solid object or not, it is not
        /// </summary>
        public override bool Walkable { get { return true; } }
        /// <summary>
        /// the type of this instance
        /// </summary>
        public override String Type { get { return base.Type + ".Projectile"; } }
        protected Entity owner;

        /// <summary>
        /// adds all events up
        /// </summary>
        public AbstractProjectile()
        {
            EntityCollision += DoDamage;
            IsAlive = true;
            IsVisible = true;
        }

        /// <summary>
        /// bool if it should despawn or not
        /// </summary>
        /// <returns></returns>
        public bool ShouldDespawn()
        {
            if ((duration * 1000) < stopWatchList[0].ElapsedMilliseconds)
                return false;
            else
                return true;
        }

        /// <summary>
        /// does damage to the hit entity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void DoDamage(object sender, CollisionArgs args)
        {
            if (sender == this)
            {
                Entity hit = (Entity)args.CollidedWith;
                if (!hit.Type.Equals(owner.Type))
                {
                    hit.takeDmg(Att);
                    IsAlive = false;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            Sprite.Position = Position;
            HitBox.Position = Position;

            stopWatchList[0].Start();

            if (ShouldDespawn())
            {
                move(FacingDirection);
                animate();
            }
            else
                IsAlive = false;
        }
    }
}
