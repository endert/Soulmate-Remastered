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
        /// <summary>
        /// random generator for the random movement
        /// </summary>
        protected Random random = new Random();
        /// <summary>
        /// distance in wich the enemy reacts to the player
        /// </summary>
        protected float aggroRange;
        /// <summary>
        /// the number of milliseconds the enemy continues to walk in one Direction before turning in another one 
        /// </summary>
        protected int movingFor = 0;
        /// <summary>
        /// the direction in wich the enemy moves
        /// </summary>
        protected MovingDirection.Direction randomMovingDirection;
        /// <summary>
        /// the type of this Instance
        /// </summary>
        public override String type { get { return base.type + ".Enemy"; } }

        /// <summary>
        /// bool if the player is in aggro range
        /// </summary>
        public bool sensePlayer
        {
            get
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
        }

        /// <summary>
        /// moves in a random direction
        /// </summary>
        public void moveRandom()
        {
            //define missng vectors
            Vector2 upRight = (Vector2.UP + Vector2.RIGHT).getNormalized();
            Vector2 downRight = (Vector2.DOWN + Vector2.RIGHT).getNormalized();
            Vector2 downLeft = (Vector2.DOWN + Vector2.LEFT).getNormalized();
            Vector2 upLeft = (Vector2.UP + Vector2.LEFT).getNormalized();

            //if needed evaluate new direction
            if (stopWatchList[2].ElapsedMilliseconds >= movingFor)
            {
                randomMovingDirection = (MovingDirection.Direction)random.Next(9);
                stopWatchList[2].Restart();
                movingFor = 0;
            }

            //move in the direction for 500 millisecounds minimum and 1500 millisec maximum
            switch (randomMovingDirection)
            {
                case MovingDirection.Direction.Up:
                    if (GameObjectHandler.lvlMap.getWalkable(hitBox, Vector2.UP))
                        move(Vector2.UP);
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
                    if (GameObjectHandler.lvlMap.getWalkable(hitBox, Vector2.RIGHT))
                        move(Vector2.RIGHT);
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
                    if (GameObjectHandler.lvlMap.getWalkable(hitBox, Vector2.DOWN))
                        move(Vector2.DOWN);
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
                    if (GameObjectHandler.lvlMap.getWalkable(hitBox, Vector2.LEFT))
                        move(Vector2.LEFT);
                    stopWatchList[2].Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case MovingDirection.Direction.UpLeft:
                    if (GameObjectHandler.lvlMap.getWalkable(hitBox, upLeft))
                        move(upLeft);
                    stopWatchList[2].Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                default:    //stand still
                    move(Vector2.ZERO);
                    stopWatchList[2].Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
            }
        }

        /// <summary>
        /// when killed drop Items and Gold
        /// </summary>
        public override void drop()
        {
            //chose random wich item should be droped
            int rand = random.Next(drops.Length);

            //check if it is droped because of droprate
            if (drops[rand].DROPRATE >= 100 * random.NextDouble())
            {
                drops[rand].cloneAndDrop(new Vector2f(position.X + random.Next(50), position.Y + random.Next(50)));
            }

            //drop gold
            for (int i = 0; i < random.Next(100); i++)
            {
                new Gold().cloneAndDrop(new Vector2f(position.X + random.Next(50), position.Y + random.Next(50)));
            }
        }

        /// <summary>
        /// what the enemy does if it's sense a player should only be called once in the update
        /// </summary>
        protected abstract void react();

        /// <summary>
        /// what the enemy normaly does, if it does not sense the player
        /// </summary>
        public abstract void notReact();

        /// <summary>
        /// the update, should only be called once by an handler
        /// </summary>
        /// <param name="gameTime"></param>
        public override void update(GameTime gameTime)
        {
            //regulating the movement by using the game time
            movementSpeed = BaseMovementSpeed * (float)gameTime.EllapsedTime.TotalMilliseconds;

            //animate if needed
            animate(textureList);

            //setting sprite at the current position
            sprite.Position = position;

            //cheacking if still alive
            if (currentHP <= 0)
            {
                isAlive = false;
            }

            if (isAlive)
            {
                //sets position of the HitBox 
                hitBox.setPosition(sprite.Position);

                //react to Player if needed
                if (sensePlayer)
                    react();
                else
                    notReact();
            }

            //call life bar update
            lifeBar.update(this);

            //clears the List of Vectors from where it was Hit
            hitFromDirections.Clear();
        }
    }
}
