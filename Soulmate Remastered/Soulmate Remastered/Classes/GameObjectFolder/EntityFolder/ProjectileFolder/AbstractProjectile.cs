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
        public override bool Walkable
        {
            get
            {
                return true;
            }
        }
        public override String Type { get { return base.Type + ".Projectile"; } }

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
            for (int i = 0; i < EnemyHandler.EnemyList.Count; i++)
            {
                if ((i != IndexObjectList) && (HitBox.Hit(EnemyHandler.EnemyList[i].HitBox)))
                {
                    enemyList.Add(EnemyHandler.EnemyList[i]);
                }
            }
            return enemyList;
        }

        public void doDamage()
        {
            if (hitAnotherEntity() && getTouchedEnemy().Count > 0)
            {
                getTouchedEnemy()[0].takeDmg(Att);
                IsAlive = false;
            }
        }

        public override void Update(GameTime gameTime)
        {
            movementSpeed = BaseMovementSpeed * (float)gameTime.EllapsedTime.TotalMilliseconds;
            Sprite.Position = Position;
            HitBox.Position = Position;

            IsAlive = true;
            IsVisible = true;

            stopWatchList[0].Start();

            if (inRange())
            {
                Att = Att;
                
                move(FacingDirection);
                animate();
                Sprite.Position = Position;
                doDamage();

                if (touchedPlayer())
                    Att = 0;

                if (!touchedPlayer() && hitAnotherEntity())
                {
                    movementSpeed = 0;
                    Att = 0;
                }
            }

            else
            {
                IsAlive = false;
            }
            
        }
    }
}
