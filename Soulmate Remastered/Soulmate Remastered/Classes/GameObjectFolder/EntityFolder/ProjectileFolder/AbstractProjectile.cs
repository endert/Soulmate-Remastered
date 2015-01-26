using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.EnemyFolder;
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

        public List<AbstractEnemy> getTouchedEnemy()
        {
            List<AbstractEnemy> enemyList = new List<AbstractEnemy>();
            for (int i = 0; i < EnemyHandler.enemyList.Count; i++)
            {
                if ((i != indexObjectList) && (hitBox.hit(EnemyHandler.enemyList[i].hitBox)))
                {
                    enemyList.Add(EnemyHandler.enemyList[i]);
                }
            }
            return enemyList;
        }

        public void doDamage()
        {
            if (hitAnotherEntity() && getTouchedEnemy().Count>0)
            {
                getTouchedEnemy()[0].takeDmg(att);
                isAlive = false;
            }
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
                animate(textureList);
                doDamage();
            }

            else
            {
                isAlive = false;
            }
            
        }
    }
}
