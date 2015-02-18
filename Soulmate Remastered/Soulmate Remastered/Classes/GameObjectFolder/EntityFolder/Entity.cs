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

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder
{
    abstract class Entity : GameObject
    {
        public override String type { get { return base.type + ".Entity"; } }
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

        protected float baseHp;
            public float getBaseHp { get { return baseHp; } }
        protected float maxHP;
            public float getMaxHP { get { return maxHP; } }
        protected float currentHP;
            public float getCurrentHP { get { return currentHP; } }

        protected float baseAtt;
        public float getBaseAtt { get { return baseAtt; } }
        protected float att;
            public float getAtt { get { return att; } }

        protected float baseDef;
            public float getBaseDef { get { return baseDef; } }
        protected float def;
            public float getDef { get { return def; } }
        //attackHitBox

        protected bool isMoving;
            public bool getIsMoving { get { return isMoving; } }
        protected Vector2f facingDirection;
            public Vector2f getFacingDirection { get { return facingDirection; } }
        protected int numFacingDirection { get; set; }
        public int getNumFacingDirection
        {
            get
            {
                if (facingDirection.Y>0)
                {
                    numFacingDirection = 0;
                    return 0;
                }
                else if (facingDirection.Y < 0)
                {
                    numFacingDirection = 1;
                    return 1;
                }
                else if (facingDirection.X > 0)
                {
                    numFacingDirection = 2;
                    return 2;
                }
                else if(facingDirection.X<0)
                {
                    numFacingDirection = 3;
                    return 3;
                }
                else
                {
                    return numFacingDirection;
                }
            }
        }
        protected bool moveAwayFromEntity;
        protected Vector2f movement;
        protected float movementSpeedConstant;
            public float getMovementSpeedConstant { get { return movementSpeedConstant; } }
        protected float movementSpeed;
        protected List<Vector2f> hitFromDirections = new List<Vector2f>();


        public void move(Vector2f direction)
        {
            if (!direction.Equals(new Vector2f(0, 0)))
            {
                isMoving = true;
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
                    Vector2f movement = new Vector2f(0, 0);

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
                    if (GameObjectHandler.lvlMap.getWalkable(hitBox, movement))    // only move if it's walkable
                    {
                        position = new Vector2f(position.X + movement.X, position.Y + movement.Y);
                        facingDirection = movement;
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
            }
            else
            {
                isMoving = false;
            }
        }

        public bool hitAnotherEntity()
        {
            for (int i = 0; i < GameObjectHandler.gameObjectList.Count; i++)
            {
                if ((i != indexObjectList) && !GameObjectHandler.gameObjectList[i].walkable && (hitBox.hit(GameObjectHandler.gameObjectList[i].hitBox)) && hitAnotherEnityHelp(i))
                {
                    bool notFound = true;
                    for (int j = 0; j < hitFromDirections.Count; j++)
                    {
                        if (hitFromDirections[j].Equals(hitBox.hitFrom(GameObjectHandler.gameObjectList[i].hitBox)))
                        {
                            notFound = false;
                        }
                    }
                    if (notFound)
                    {
                        hitFromDirections.Add(hitBox.hitFrom(GameObjectHandler.gameObjectList[i].hitBox));
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

        private bool hitAnotherEnityHelp(int index)
        {
            if (type.Split('.')[2].Equals("Pet") || type.Split('.')[2].Equals("Player"))
            {
                return !petPlayerCollision();
            }

            else
            {
                return !GameObjectHandler.gameObjectList[index].walkable;
            }
        }

        public bool petPlayerCollision()
        {
            if (type.Split('.')[2].Equals("Pet"))
            {
                foreach (Entity entity in EntityHandler.entityList)
                {
                    if (hitBox.hit(entity.hitBox) && !entity.type.Split('.')[2].Equals("Pet") && entity.type.Split('.')[2].Equals("Player"))
                    {
                        return true;
                    }
                }
            }

            if (type.Split('.')[2].Equals("Player"))
            {
                foreach (Entity entity in EntityHandler.entityList)
                {
                    if (hitBox.hit(entity.hitBox) && !entity.type.Split('.')[2].Equals("Player") && entity.type.Split('.')[2].Equals("Pet"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool touchedPlayer()
        {
            if (hitBox.hit(PlayerHandler.player.hitBox))
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
            if (type.Split('.')[2].Equals("Player"))
            {
                return true;
            }
            else
            {
                return !touchedPlayer();
            }
        }

        public virtual void drop()
        {

        }

        public void moveOutOfWall(Vector2f theWayOut)
        {
            position = new Vector2f(position.X + (theWayOut.X / Math.Abs(theWayOut.X)) * movementSpeed, position.Y + (theWayOut.Y / Math.Abs(theWayOut.Y)) * movementSpeed);
        }

        public void takeDmg(float dmg)
        {
            if (isVulnerable)
            {
                if (dmg - def >= 0)
                {
                    currentHP -= (dmg - def);
                    tookDmg = true;
                }
            }
        }

        public Vector2f getPlayerDirection()
        {
            Vector2f playerDirection = new Vector2f(0, 0);
            if (PlayerHandler.player.hitBox.Position.X + PlayerHandler.player.hitBox.width < hitBox.Position.X) //player is to the left
            {
                playerDirection.X = -1;
            }
            else if (hitBox.Position.X + hitBox.width < PlayerHandler.player.hitBox.Position.X)
            {
                playerDirection.X = 1;
            }

            if (PlayerHandler.player.hitBox.Position.Y + PlayerHandler.player.hitBox.height < hitBox.Position.Y)
            {
                playerDirection.Y = -1;
            }
            else if (hitBox.Position.Y + hitBox.height < PlayerHandler.player.hitBox.Position.Y)
            {
                playerDirection.Y = 1;
            }
            return playerDirection;
        }

        virtual public void animate(List<Texture> textureList)
        {
            if (isVulnerable)
            {
                if (facingDirection.Y > 0)
                {
                    sprite = new Sprite(textureList[0]); //front
                }
                else if (facingDirection.Y < 0)
                {
                    sprite = new Sprite(textureList[1]); // back
                }
                else if (facingDirection.X > 0)
                {
                    sprite = new Sprite(textureList[2]); // right
                }
                else
                {
                    sprite = new Sprite(textureList[3]); // left
                }
            }
            else
            {
                if (textureList.Count > 4)
                {
                    sprite = new Sprite(textureList[4]); // invulnerablety
                }
            }

            sprite.Position = position;
        }

        public abstract override void update(GameTime gameTime);

        public override void draw(RenderWindow window)
        {
            base.draw(window);
            if(lifeBar!=null)
            {
                lifeBar.draw(window);
            }
        }
    }
}
