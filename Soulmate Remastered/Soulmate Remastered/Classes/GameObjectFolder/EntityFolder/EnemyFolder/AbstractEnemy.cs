using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.MoneyFolder;
using Soulmate_Remastered.Classes.HUDFolder;
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

        public AbstractEnemy() : base()
        {
            lifeBar = new LifeBarForOthers();
        }

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
                        Move(Vector2.BACK);
                    stopWatchList[2].Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case MovingDirection.Direction.UpRight:
                    if (GameObjectHandler.lvlMap.getWalkable(HitBox, upRight))
                        Move(upRight);
                    stopWatchList[2].Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case MovingDirection.Direction.Right:
                    if (GameObjectHandler.lvlMap.getWalkable(HitBox, Vector2.RIGHT))
                        Move(Vector2.RIGHT);
                    stopWatchList[2].Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case MovingDirection.Direction.DownRight:
                    if (GameObjectHandler.lvlMap.getWalkable(HitBox, downRight))
                        Move(downRight);
                    stopWatchList[2].Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case MovingDirection.Direction.Front:
                    if (GameObjectHandler.lvlMap.getWalkable(HitBox, Vector2.FRONT))
                        Move(Vector2.FRONT);
                    stopWatchList[2].Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case MovingDirection.Direction.DownLeft:
                    if (GameObjectHandler.lvlMap.getWalkable(HitBox, downLeft))
                        Move(downLeft);
                    stopWatchList[2].Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case MovingDirection.Direction.Left:
                    if (GameObjectHandler.lvlMap.getWalkable(HitBox, Vector2.LEFT))
                        Move(Vector2.LEFT);
                    stopWatchList[2].Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case MovingDirection.Direction.UpLeft:
                    if (GameObjectHandler.lvlMap.getWalkable(HitBox, upLeft))
                        Move(upLeft);
                    stopWatchList[2].Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                default:    //stand still
                    Move(Vector2.ZERO);
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
            if (Drops[rand].DropRate >= 100 * random.NextDouble())
            {
                Drops[rand].CloneAndDrop(new Vector2f(Position.X + random.Next(50), Position.Y + random.Next(50)));
            }

            int goldAmount = random.Next(100);

            //drop gold
            for (int i = 0; i < goldAmount; i++)
            {
                new Gold().CloneAndDrop(new Vector2f(Position.X + random.Next(50), Position.Y + random.Next(50)));
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
