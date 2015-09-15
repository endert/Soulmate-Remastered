using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using System.Diagnostics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.HUDFolder;
using Soulmate_Remastered.Core;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PetFolder;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder
{
    /// <summary>
    /// everything that moves, i guess?
    /// </summary>
    abstract class Entity : GameObject
    {
        //Events*************************************************************************************

        /// <summary>
        /// risen if this object collides
        /// </summary>
        public event EventHandler<CollisionArgs> EntityCollision;
        /// <summary>
        /// rise the EntityCollision event
        /// </summary>
        /// <param name="c"></param>
        protected void OnEntityCollision(CollisionArgs c)
        {
            EventHandler<CollisionArgs> handler = EntityCollision;
            if (handler != null)
            {
                handler(this, c);
            }
        }

        //Constants**********************************************************************************



        //Properties*********************************************************************************


        /// <summary>
        /// bool if this entity is vulnerable or not
        /// </summary>
        protected bool IsVulnerable
        {
            get
            {
                if (tookDmg)
                {
                    vulnerable = false;
                    stopWatchList[1].Start();
                    if (stopWatchList[1].ElapsedMilliseconds >= inVulnerableFor)
                    {
                        tookDmg = false;
                        vulnerable = true;
                        stopWatchList[1].Reset();
                    }
                }
                return vulnerable;
            }
        }
        /// <summary>
        /// the strength of this entities knockback -> how strong this can knock back
        /// <para>to implement</para>
        /// </summary>
        public float KnockBack { get; protected set; }
        /// <summary>
        /// all possible drops of this entity
        /// </summary>
        protected virtual AbstractItem[] Drops { get; set; }

        /// <summary>
        /// base stat
        /// </summary>
        public float BaseHp { get; protected set; }
        /// <summary>
        /// base stat
        /// </summary>
        public float BaseAtt { get; protected set; }
        /// <summary>
        /// base stat
        /// </summary>
        public float BaseDef { get; protected set; }
        /// <summary>
        /// base stat
        /// </summary>
        public float BaseMovementSpeed { get; protected set; }
        /// <summary>
        /// scaled value 
        /// </summary>
        public float MaxHP { get; protected set; }
        /// <summary>
        /// scaled value 
        /// </summary>
        public float Att { get; protected set; }
        /// <summary>
        /// scaled value 
        /// </summary>
        public float Def { get; protected set; }
        /// <summary>
        /// the movement speed with concern for the ellapsed time
        /// </summary>
        protected float MovementSpeed { get { return BaseMovementSpeed * (float)AbstractGame.gameTime.EllapsedTime.Milliseconds; } }
        /// <summary>
        /// the current hp
        /// </summary>
        public float CurrentHP { get; protected set; }
        /// <summary>
        /// bool if moving or not
        /// </summary>
        public bool IsMoving { get; protected set; }
        /// <summary>
        /// the direction in which this object faces
        /// </summary>
        public Vector2 FacingDirection { get; protected set; }
        /// <summary>
        /// the movement vector
        /// </summary>
        public Vector2 movement { get; protected set; }

        public EDirection Direction
        {
            get
            {
                if (FacingDirection.Y > 0)
                    return EDirection.Front;
                else if (FacingDirection.Y < 0)
                    return EDirection.Back;
                else if (FacingDirection.X > 0)
                    return EDirection.Right;
                else if (FacingDirection.X < 0)
                    return EDirection.Left;
                else
                    return EDirection.Zero;
            }
        }

        //Attributes*********************************************************************************

        /// <summary>
        /// the lifebar
        /// </summary>
        protected LifeBarForOthers lifeBar;
        /// <summary>
        /// <para>0 = animation</para>
        /// <para>1 = vulnerable</para>
        /// <para>2 = transformation</para>
        /// </summary>
        protected Stopwatch[] stopWatchList = { new Stopwatch(), new Stopwatch(), new Stopwatch() };

        protected bool tookDmg;
        /// <summary>
        /// bool for checking if vulnerable or not
        /// </summary>
        protected bool vulnerable = true;

        /// <summary>
        /// in milliseconds
        /// </summary>
        protected int inVulnerableFor = 500;

        public enum EDirection
        {
            /// <summary>
            /// away from camera
            /// </summary>
            Back,
            /// <summary>
            /// to the camera
            /// </summary>
            Front,
            Right,
            Left,
            Zero
        }

        //Methodes***********************************************************************************

        /// <summary>
        /// moves this in the given direction if possible
        /// </summary>
        /// <param name="direction"></param>
        public void Move(Vector2 direction)
        {
            if (!direction.Equals(Vector2.ZERO))
            {
                Vector2 movement = MovementSpeed * (direction.GetNormalized());

                if (GameObjectHandler.lvlMap.getWalkable(HitBox, movement) && !WillHitAnotherEntity(movement))
                    Position += movement;

                FacingDirection = movement;
            }
            else
            {
                IsMoving = false;
            }
        }

        /// <summary>
        /// checks if in movingdirection a collision would be if yes the EntityCollision event will be risen
        /// </summary>
        /// <param name="movement"></param>
        /// <returns></returns>
        private bool WillHitAnotherEntity(Vector2 movement)
        {
            for (int i = 0; i < GameObjectHandler.gameObjectList.Count; ++i)
            {
                if ((i != IndexObjectList) && !GameObjectHandler.gameObjectList[i].Walkable && HitBox.WillHit(movement, GameObjectHandler.gameObjectList[i].HitBox) && HitAnotherEnityHelp(i))
                {
                    CollisionArgs args = new CollisionArgs();
                    args.CollidedWith = GameObjectHandler.gameObjectList[i];
                    OnEntityCollision(args);

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// chooses if petPlayerCollision or walkable must be returned 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool HitAnotherEnityHelp(int index)
        {
            if (GetType().IsSubclassOf(typeof(AbstractPet)) || GetType().IsSubclassOf(typeof(AbstractPlayer)))
                return !PetPlayerCollision(index);
            else
                return !GameObjectHandler.gameObjectList[index].Walkable;
        }

        /// <summary>
        /// checks if player and pet are coliding
        /// </summary>
        /// <returns></returns>
        public bool PetPlayerCollision(int index)
        {
            if (GetType().IsSubclassOf(typeof(AbstractPet)) && GameObjectHandler.gameObjectList[index].GetType().IsSubclassOf(typeof(AbstractPlayer)))
                return true;

            if (GetType().IsSubclassOf(typeof(AbstractPlayer)) && GameObjectHandler.gameObjectList[index].GetType().IsSubclassOf(typeof(AbstractPet)))
                return true;

            return false;
        }

        /// <summary>
        /// bool if the player is touched
        /// </summary>
        /// <returns></returns>
        public bool TouchedPlayer()
        {
            if (HitBox.Hit(PlayerHandler.Player.HitBox))
                return true;
            else
                return false;
        }

        /// <summary>
        /// drops the possible drops
        /// </summary>
        public virtual void Drop()
        {

        }

        /// <summary>
        /// lowers the hp by substracting this def from dmg
        /// </summary>
        /// <param name="dmg"></param>
        public void TakeDmg(float dmg)
        {
            if (IsVulnerable)
            {
                if (dmg - Def >= 0)
                {
                    CurrentHP -= (dmg - Def);
                    tookDmg = true;
                }
            }
        }

        /// <summary>
        /// the vector, which points from this mid to the players mid
        /// </summary>
        /// <returns></returns>
        public Vector2 GetPlayerDirection()
        {
            Vector2 playerDirection = (PlayerHandler.Player.HitBox.Position + (1 / 2) * PlayerHandler.Player.HitBox.Size) - (HitBox.Position + (1 / 2) * HitBox.Size);
            return playerDirection;
        }

        /// <summary>
        /// animate the sprite according to the facing direction
        /// </summary>
        virtual public void Animate()
        {
            if (IsVulnerable)
            {
                switch (Direction)
                {
                    case EDirection.Front:
                        Sprite = new Sprite(TextureList[0]);
                        break;
                    case EDirection.Back:
                        Sprite = new Sprite(TextureList[1]);
                        break;
                    case EDirection.Right:
                        Sprite = new Sprite(TextureList[2]);
                        break;
                    case EDirection.Left:
                        Sprite = new Sprite(TextureList[3]);
                        break;
                    default:
                        Sprite = new Sprite(TextureList[0]);
                        break;
                }
            }
            else
            {
                try
                {
                    switch (Direction)
                    {
                        case EDirection.Front:
                            Sprite = new Sprite(TextureList[4]);
                            break;
                        case EDirection.Back:
                            Sprite = new Sprite(TextureList[5]);
                            break;
                        case EDirection.Right:
                            Sprite = new Sprite(TextureList[6]);
                            break;
                        case EDirection.Left:
                            Sprite = new Sprite(TextureList[7]);
                            break;
                        default:
                            break;
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("u are missing some textures");
                }
            }

            Sprite.Position = Position;
        }

        public abstract override void Update(GameTime gameTime);

        public override void Draw(RenderWindow window)
        {
            base.Draw(window);
            if(lifeBar!=null)
            {
                lifeBar.draw(window);
            }
        }
    }
}
