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
        protected int inVulnerableFor; //in milisec
            protected bool isVulnerable
        {
            get
            {
                bool vulnerable = true;
                if (tookDmg)
                {
                    vulnerable = false;
                    stopWatchList[0].Start();
                    if (stopWatchList[0].ElapsedMilliseconds >= inVulnerableFor)
                    {
                        tookDmg = false;
                        vulnerable = true;
                        stopWatchList[0].Reset();
                    }
                }
                return vulnerable;
            }
        }
        protected float knockBack;
        protected AbstractItem[] drops;
        protected bool hitPlayer;
        protected float damage;

        protected float maxHP;
            public float getMaxHP { get { return maxHP; } }
        protected float currentHP;
            public float getCurrentHP { get { return currentHP; } }
        protected float att;
            public float getAtt { get { return att; } }
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
                if ((i != indexObjectList) && (hitBox.hit(GameObjectHandler.gameObjectList[i].hitBox)) && !walkable)
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

        public void takeDmg()
        {

        }

        public Vector2f getPlayerDirection()
        {
            Vector2f playerDirection = new Vector2f(0, 0);
            if (PlayerHandler.player.hitBox.getPosition().X + PlayerHandler.player.hitBox.getWidth() < position.X) //player is to the left
            {
                playerDirection.X = -1;
            }
            else if (position.X + hitBox.getWidth() < PlayerHandler.player.hitBox.getPosition().X)
            {
                playerDirection.X = 1;
            }

            if (PlayerHandler.player.hitBox.getPosition().Y + PlayerHandler.player.hitBox.getHeight() < position.Y)
            {
                playerDirection.Y = -1;
            }
            else if (position.Y + hitBox.height < PlayerHandler.player.hitBox.getPosition().Y)
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
