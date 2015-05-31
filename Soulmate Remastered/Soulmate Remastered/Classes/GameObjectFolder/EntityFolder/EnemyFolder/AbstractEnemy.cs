using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.MoneyFolder;
using Soulmate_Remastered.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.EnemyFolder
{
    abstract class AbstractEnemy : Entity
    {
        protected Random random = new Random(); //random movement
        protected float aggroRange;
        protected int movingFor = 0; //moving for millisek in one direction
        protected MovingDirection.Direction randomMovingDirection;

        public override String type { get { return base.type + ".Enemy"; } }

        public bool sensePlayer()
        {
            if (hitBox.distanceTo(PlayerHandler.player.hitBox) <= aggroRange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void moveRandom()
        {
            Vector2f up = new Vector2f(0, -1);
            Vector2f upRight = new Vector2f(1, -1);
            Vector2f right = new Vector2f(1, 0);
            Vector2f downRight = new Vector2f(1, 1);
            Vector2f down = new Vector2f(0, 1);
            Vector2f downLeft = new Vector2f(-1, 1);
            Vector2f left = new Vector2f(-1, 0);
            Vector2f upLeft = new Vector2f(-1, -1);

            if (stopWatchList[2].ElapsedMilliseconds >= movingFor) //only evaluate a new direction, if the enemy isnt moving already
            {
                randomMovingDirection = (MovingDirection.Direction)random.Next(9);
                stopWatchList[2].Restart();
                movingFor = 0;
            }
            switch (randomMovingDirection)  //move in the direction for 1000 millisecounds so 1 second
            {
                case MovingDirection.Direction.Up:
                    if (GameObjectHandler.lvlMap.getWalkable(hitBox, up))
                        move(up);
                    stopWatchList[2].Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case MovingDirection.Direction.UpRight:
                    if (GameObjectHandler.lvlMap.getWalkable(hitBox, upRight))
                        move(upRight);
                    stopWatchList[2].Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case MovingDirection.Direction.Right:
                    if (GameObjectHandler.lvlMap.getWalkable(hitBox, right))
                        move(right);
                    stopWatchList[2].Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case MovingDirection.Direction.DownRight:
                    if (GameObjectHandler.lvlMap.getWalkable(hitBox, downRight))
                        move(downRight);
                    stopWatchList[2].Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case MovingDirection.Direction.Down:
                    if (GameObjectHandler.lvlMap.getWalkable(hitBox, down))
                        move(down);
                    stopWatchList[2].Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case MovingDirection.Direction.DownLeft:
                    if (GameObjectHandler.lvlMap.getWalkable(hitBox, downLeft))
                        move(downLeft);
                    stopWatchList[2].Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case MovingDirection.Direction.Left:
                    if (GameObjectHandler.lvlMap.getWalkable(hitBox, left))
                        move(left);
                    stopWatchList[2].Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case MovingDirection.Direction.UpLeft:
                    if (GameObjectHandler.lvlMap.getWalkable(hitBox, upLeft))
                        move(upLeft);
                    stopWatchList[2].Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                default:    //stand still for one sek
                    move(new Vector2f(0, 0));
                    stopWatchList[2].Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
            }
        }

        public override void drop()
        {
            int rand = random.Next(drops.Length);
            if (drops[rand].DROPRATE > 100 * random.NextDouble())
            {
                drops[rand].cloneAndDrop(new Vector2f(position.X + random.Next(50), position.Y + random.Next(50)));
            }

            for (int i = 0; i < random.Next(100); i++)
            {
                new Gold().cloneAndDrop(new Vector2f(position.X + random.Next(50), position.Y + random.Next(50)));
            }
        }

        public abstract void react();   //what the enemy does if it's sense a player

        public abstract void notReact();    //what the enemy normaly does

        public override void update(GameTime gameTime)
        {
            movementSpeed = 0.2f * (float)gameTime.EllapsedTime.TotalMilliseconds;
            animate(textureList);
            sprite.Position = position;

            if (currentHP <= 0)
            {
                isAlive = false;
            }

            if (isAlive)
            {
                hitBox.setPosition(sprite.Position);

                if (sensePlayer())  //if a player is sensed (is in aggroRange) react else not ;)
                {
                    react();
                }
                else
                {
                    notReact();
                }
            }

            lifeBar.update(this);

            hitFromDirections.Clear();
        }
    }
}
