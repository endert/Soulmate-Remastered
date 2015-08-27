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
    /// <summary>
    /// the evil in this world
    /// </summary>
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
        public override String Type { get { return base.Type + ".Enemy"; } }

        /// <summary>
        /// bool if the player is in aggro range
        /// </summary>
        public bool SensePlayer
        {
            get
            {
                if (HitBox.DistanceTo(PlayerHandler.Player.HitBox) <= aggroRange)
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
        public void MoveRandom()
        {
            //define missng vectors
            Vector2 upRight = (Vector2.BACK + Vector2.RIGHT).GetNormalized();
            Vector2 downRight = (Vector2.FRONT + Vector2.RIGHT).GetNormalized();
            Vector2 downLeft = (Vector2.FRONT + Vector2.LEFT).GetNormalized();
            Vector2 upLeft = (Vector2.BACK + Vector2.LEFT).GetNormalized();

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
                case MovingDirection.Direction.Back:
                    if (GameObjectHandler.lvlMap.getWalkable(HitBox, Vector2.BACK))
                        move(Vector2.BACK);
                    stopWatchList[2].Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case MovingDirection.Direction.UpRight:
                    if (GameObjectHandler.lvlMap.getWalkable(HitBox, upRight))
                        move(upRight);
                    stopWatchList[2].Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case MovingDirection.Direction.Right:
                    if (GameObjectHandler.lvlMap.getWalkable(HitBox, Vector2.RIGHT))
                        move(Vector2.RIGHT);
                    stopWatchList[2].Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case MovingDirection.Direction.DownRight:
                    if (GameObjectHandler.lvlMap.getWalkable(HitBox, downRight))
                        move(downRight);
                    stopWatchList[2].Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case MovingDirection.Direction.Front:
                    if (GameObjectHandler.lvlMap.getWalkable(HitBox, Vector2.FRONT))
                        move(Vector2.FRONT);
                    stopWatchList[2].Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case MovingDirection.Direction.DownLeft:
                    if (GameObjectHandler.lvlMap.getWalkable(HitBox, downLeft))
                        move(downLeft);
                    stopWatchList[2].Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case MovingDirection.Direction.Left:
                    if (GameObjectHandler.lvlMap.getWalkable(HitBox, Vector2.LEFT))
                        move(Vector2.LEFT);
                    stopWatchList[2].Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case MovingDirection.Direction.UpLeft:
                    if (GameObjectHandler.lvlMap.getWalkable(HitBox, upLeft))
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
        public override void Drop()
        {
            //chose random wich item should be droped
            int rand = random.Next(Drops.Length);

            //check if it is droped because of droprate
            if (Drops[rand].DROPRATE >= 100 * random.NextDouble())
            {
                Drops[rand].cloneAndDrop(new Vector2f(Position.X + random.Next(50), Position.Y + random.Next(50)));
            }

            //drop gold
            for (int i = 0; i < random.Next(100); i++)
            {
                new Gold().cloneAndDrop(new Vector2f(Position.X + random.Next(50), Position.Y + random.Next(50)));
            }
        }

        /// <summary>
        /// what the enemy does if it's sense a player should only be called once in the update
        /// </summary>
        protected abstract void React();

        /// <summary>
        /// what the enemy normaly does, if it does not sense the player
        /// </summary>
        public abstract void NotReact();

        /// <summary>
        /// the update, should only be called once by an handler
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            //animate if needed
            Animate();

            //setting sprite at the current position
            Sprite.Position = Position;

            //cheacking if still alive
            if (CurrentHP <= 0)
            {
                IsAlive = false;
            }

            if (IsAlive)
            {
                //sets position of the HitBox 
                HitBox.update(Sprite);

                //react to Player if needed
                if (SensePlayer)
                    React();
                else
                    NotReact();
            }

            //call life bar update
            lifeBar.update(this);
        }
    }
}
