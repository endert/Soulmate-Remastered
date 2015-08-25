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

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder
{
    abstract class Entity : GameObject
    {
        public override String Type { get { return base.Type + ".Entity"; } }
        protected LifeBarForOthers lifeBar;
        protected Stopwatch[] stopWatchList = { new Stopwatch(), new Stopwatch(), new Stopwatch() }; //first for animation, second for vulnerable, third for transformation

        protected bool tookDmg;
        protected bool vulnerable = true;
        protected int inVulnerableFor = 500; //in milisec
            protected bool isVulnerable
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
        protected float knockBack;
        protected virtual AbstractItem[] drops { get; set; }

        public float BaseHp { get; protected set; }
        public float MaxHP { get; protected set; }
        public float CurrentHP { get; protected set; }
        public float BaseAtt { get; protected set; }
        public float Att { get; protected set; }
        public float BaseDef { get; protected set; }
        public float Def { get; protected set; }
        //attackHitBox

        public bool IsMoving { get; protected set; }
        public Vector2 FacingDirection { get; protected set; }
        public EDirection Direction
        {
            get
            {
                if (FacingDirection.Y>0)
                    return EDirection.Back;
                else if (FacingDirection.Y < 0)
                    return EDirection.Front;
                else if (FacingDirection.X > 0)
                    return EDirection.Right;
                else if(FacingDirection.X<0)
                    return EDirection.Left;
                else
                    return EDirection.Zero;
            }
        }
        protected bool moveAwayFromEntity;
        protected Vector2 movement;
        public float BaseMovementSpeed { get; protected set; }
        protected float movementSpeed;
        protected List<Vector2> hitFromDirections = new List<Vector2>();

        public enum EDirection
        {
            Back, Front,Right,Left,Zero
        }

        public void move(Vector2 direction)
        {
            if (!direction.Equals(new Vector2(0, 0)))
            {
                IsMoving = true;
                if (hitAnotherEntity() && !moveAwayFromEntity && moveHelp()) //if an entity is not a player it should not touch the player
                {
                    moveAwayFromEntity = true;

                    for (int i = 0; i < hitFromDirections.Count; i++)
                    {
                        if (Math.Abs((direction.X >= hitFromDirections[i].X) ? (direction.X) : (hitFromDirections[i].X)) > Math.Abs(direction.X - hitFromDirections[i].X) ||
                            Math.Abs((direction.X >= hitFromDirections[i].X) ? (direction.X) : (hitFromDirections[i].X)) < Math.Abs(direction.X - hitFromDirections[i].X))//if they have the same sign otherwise it doesn't matter
                        {
                            direction.X = -hitFromDirections[i].X;
                        }

                        if (Math.Abs((direction.Y >= hitFromDirections[i].Y) ? (direction.Y) : (hitFromDirections[i].Y)) > Math.Abs(direction.Y - hitFromDirections[i].Y) ||
                            Math.Abs((direction.Y >= hitFromDirections[i].Y) ? (direction.Y) : (hitFromDirections[i].Y)) < Math.Abs(direction.Y - hitFromDirections[i].Y))
                        {
                            direction.Y = -hitFromDirections[i].Y;
                        }
                    }
                    move(direction);
                }
                else
                {
                    moveAwayFromEntity = false;
                    Vector2 movement = new Vector2(0, 0);

                    if (direction.X > 0)
                        movement.X += movementSpeed;
                    else
                    {
                        if (direction.X < 0)
                            movement.X -= movementSpeed;
                        else
                            movement.X += 0;
                    }
                    if (direction.Y > 0)
                        movement.Y += movementSpeed;
                    else
                    {
                        if (direction.Y < 0)
                            movement.Y -= movementSpeed;
                        else
                            movement.Y += 0;
                    }
                    if (direction.X != 0 && direction.Y != 0)
                    {
                        //movement.X *= (float)Math.Sin(45);
                        //movement.Y *= (float)Math.Sin(45);
                    }
                    if (GameObjectHandler.lvlMap.getWalkable(HitBox, movement))    // only move if it's walkable
                    {
                        Position = new Vector2f(Position.X + movement.X, Position.Y + movement.Y);
                        FacingDirection = movement;
                    }
                    else
                    {
                        //Vector2f wayOutOfWall = new Vector2f(0, 0);
                        //if (position.X<30)
                        //{
                        //    wayOutOfWall.X = 35;
                        //}
                        //if (position.X>)
                        //{

                        //}
                    }
                }
                //Vector2 movement = movementSpeed * (direction * (1 / vectorLength(direction)));

                //if (GameObjectHandler.lvlMap.getWalkable(hitBox, movement) && !willHitAnotherEntity(movement) && moveHelp())
                //    position += movement;

                //facingDirection = movement;
            }
            else
            {
                IsMoving = false;
            }
        }

        public bool hitAnotherEntity()
        {
            for (int i = 0; i < GameObjectHandler.gameObjectList.Count; i++)
            {
                if ((i != IndexObjectList) && !GameObjectHandler.gameObjectList[i].Walkable && (HitBox.Hit(GameObjectHandler.gameObjectList[i].HitBox)) && hitAnotherEnityHelp(i))
                {
                    bool notFound = true;
                    for (int j = 0; j < hitFromDirections.Count; j++)
                    {
                        if (hitFromDirections[j].Equals(HitBox.hitFrom(GameObjectHandler.gameObjectList[i].HitBox)))
                        {
                            notFound = false;
                        }
                    }
                    if (notFound)
                    {
                        hitFromDirections.Add(HitBox.hitFrom(GameObjectHandler.gameObjectList[i].HitBox));
                    }
                }
            }
            if (hitFromDirections.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool willHitAnotherEntity(Vector2 movement)
        {
            for (int i = 0; i < GameObjectHandler.gameObjectList.Count; ++i)
            {
                if ((i != IndexObjectList) && !GameObjectHandler.gameObjectList[i].Walkable && HitBox.WillHit(movement, GameObjectHandler.gameObjectList[i].HitBox) && hitAnotherEnityHelp(i))
                {
                    return true;
                }
            }

            return false;
        }

        private bool hitAnotherEnityHelp(int index)
        {
            if (Type.Split('.')[2].Equals("Pet") || Type.Split('.')[2].Equals("Player"))
            {
                return !petPlayerCollision();
            }

            else
            {
                return !GameObjectHandler.gameObjectList[index].Walkable;
            }
        }

        public bool petPlayerCollision()
        {
            if (Type.Split('.')[2].Equals("Pet"))
            {
                foreach (Entity entity in EntityHandler.entityList)
                {
                    if (HitBox.Hit(entity.HitBox) && !entity.Type.Split('.')[2].Equals("Pet") && entity.Type.Split('.')[2].Equals("Player"))
                    {
                        return true;
                    }
                }
            }

            if (Type.Split('.')[2].Equals("Player"))
            {
                foreach (Entity entity in EntityHandler.entityList)
                {
                    if (HitBox.Hit(entity.HitBox) && !entity.Type.Split('.')[2].Equals("Player") && entity.Type.Split('.')[2].Equals("Pet"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool touchedPlayer()
        {
            if (HitBox.Hit(PlayerHandler.player.HitBox))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool moveHelp()
        {
            if (Type.Split('.')[2].Equals("Player"))
            {
                return true;
            }
            else
            {
                return !touchedPlayer();
            }
        }

        public virtual void Drop()
        {

        }

        public void takeDmg(float dmg)
        {
            if (isVulnerable)
            {
                if (dmg - Def >= 0)
                {
                    CurrentHP -= (dmg - Def);
                    tookDmg = true;
                }
            }
        }

        public Vector2 getPlayerDirection()
        {
            Vector2 playerDirection = (PlayerHandler.player.HitBox.Position + (1 / 2) * PlayerHandler.player.HitBox.Size) - (HitBox.Position + (1 / 2) * HitBox.Size);
            return playerDirection;
        }

        virtual public void animate()
        {
            if (isVulnerable)
            {
                if (FacingDirection.Y > 0)
                {
                    Sprite = new Sprite(TextureList[0]); //front
                }
                else if (FacingDirection.Y < 0)
                {
                    Sprite = new Sprite(TextureList[1]); // back
                }
                else if (FacingDirection.X > 0)
                {
                    Sprite = new Sprite(TextureList[2]); // right
                }
                else
                {
                    Sprite = new Sprite(TextureList[3]); // left
                }
            }
            else
            {
                if (TextureList.Count > 4)
                {
                    Sprite = new Sprite(TextureList[4]); // invulnerablety
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
