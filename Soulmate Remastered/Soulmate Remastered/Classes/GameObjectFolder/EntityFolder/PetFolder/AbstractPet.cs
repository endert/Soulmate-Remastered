using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PetFolder
{
    abstract class AbstractPet : Entity
    {
        public override String type { get { return base.type + ".Pet"; } }

        

        public String toStringForSave()
        {
            String petForSave = "pt" + lineBreak.ToString();

            petForSave += getCurrentHP + lineBreak.ToString();   //splitPetString[1]
            petForSave += position.X + lineBreak.ToString(); //splitPetString[2]
            petForSave += position.Y + lineBreak.ToString(); //splitPetString[3]
            petForSave += type.Split('.')[type.Split('.').Length-1];  //splitPetString[4]

            return petForSave;
        }

        public void load(String petString)
        {
            String[] splitPetString = petString.Split(lineBreak);

            currentHP = Convert.ToSingle(splitPetString[1]);
            position = new Vector2f(Convert.ToSingle(splitPetString[2]), Convert.ToSingle(splitPetString[3]));
        }

        public virtual void spritePositionUpdate()
        {
            switch (numFacingDirection)
            {
                case 0:
                    {
                        sprite.Position = position;
                        break;
                    }
                case 1:
                    {
                        sprite.Position = position;
                        break;
                    }
                case 2:
                    {
                        sprite.Position = position;
                        break;
                    }
                case 3:
                    {
                        sprite.Position = new Vector2f(position.X - (textureList[2].Size.X - hitBox.width), position.Y);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        public Vector2f getVectorForMove()
        {
            if (PlayerHandler.player.getIsMoving)
            {
                if (isBehindPlayer())
                {
                    return getPlayerDirection();
                }
                else if (!(PlayerHandler.player.getFacingDirection.X != 0 && PlayerHandler.player.getFacingDirection.Y != 0))    //If player dont facing diagonal
                {
                    return new Vector2f(-PlayerHandler.player.getFacingDirection.X, -PlayerHandler.player.getFacingDirection.Y);
                }
                else
                {
                    switch (PlayerHandler.player.getNumFacingDirection)
                    {
                        case 0:
                            return new Vector2f(0, -1);
                        case 1:
                            return new Vector2f(0, 1);
                        case 2:
                            return new Vector2f(-1, 0);
                        case 3:
                            return new Vector2f(1, 0);
                        default:
                            return new Vector2f(0, 0);
                    }
                }
            }
            else
            {
                if (isBehindPlayer() && hitBox.distanceTo(PlayerHandler.player.hitBox) >= 50f)
                {
                    return getPlayerDirection();
                }
                else if (!isBehindPlayer())    //If player dont facing diagonal
                {
                    if (!(PlayerHandler.player.getFacingDirection.X != 0 && PlayerHandler.player.getFacingDirection.Y != 0))    //If player dont facing diagonal
                    {
                        return new Vector2f(-PlayerHandler.player.getFacingDirection.X, -PlayerHandler.player.getFacingDirection.Y);
                    }
                    else
                    {
                        switch (PlayerHandler.player.getNumFacingDirection)
                        {
                            case 0:
                                return new Vector2f(0, -1);
                            case 1:
                                return new Vector2f(0, 1);
                            case 2:
                                return new Vector2f(-1, 0);
                            case 3:
                                return new Vector2f(1, 0);
                            default:
                                return new Vector2f(0, 0);
                        }
                    }
                }
                else
                {
                    return new Vector2f(0, 0);
                }
            }
        }

        public bool isBehindPlayer()
        {
            Vector2f playerMovingDirection = PlayerHandler.player.getFacingDirection;
            bool behindX = false;   // for diagonal moving;
            bool behindY = false;

            if (!(playerMovingDirection.X != 0 && playerMovingDirection.Y != 0))
            {
                switch (PlayerHandler.player.getNumFacingDirection)
                {
                    case (0):
                        if (position.Y + hitBox.height <= PlayerHandler.player.position.Y)
                        {
                            return true;
                        }
                        break;
                    case (1):
                        if (position.Y >= PlayerHandler.player.position.Y + PlayerHandler.player.hitBox.height)
                        {
                            return true;
                        }
                        break;
                    case (2):
                        if (position.X + hitBox.width <= PlayerHandler.player.position.X)
                        {
                            return true;
                        }
                        break;
                    case (3):
                        if (position.X >= PlayerHandler.player.position.X + PlayerHandler.player.hitBox.width)
                        {
                            return true;
                        }
                        break;
                    default:
                        break;
                }
            }
            else      // moving diagonal
            {
                if (playerMovingDirection.X > 0)
                {
                    if (position.X + hitBox.width <= PlayerHandler.player.position.X)
                    {
                        behindX = true;
                    }
                }
                else if (playerMovingDirection.X < 0)
                {
                    if (position.X >= PlayerHandler.player.position.X + PlayerHandler.player.hitBox.width)
                    {
                        behindX = true;
                    }
                }

                if (playerMovingDirection.Y > 0)
                {
                    if (position.Y + hitBox.height <= PlayerHandler.player.position.Y)
                    {
                        behindY = true;
                    }
                }
                else if (playerMovingDirection.Y < 0)
                {
                    if (position.Y >= PlayerHandler.player.position.Y + PlayerHandler.player.hitBox.height)
                    {
                        behindY = true;
                    }
                }

                return (behindX || behindY);
            }
            return false;
        }

        public override void drop()
        {
            drops[0].drop(position);
        }

        public override void update(GameTime gameTime)
        {
            if (getCurrentHP <= 0)
            {
                drop();
                isAlive = false;
            }

            if (hitBox.distanceTo(PlayerHandler.player.hitBox) >= 800f)
            {
                position = PlayerHandler.player.position;
            }

            animate(textureList);

            hitBox.update(sprite);
            spritePositionUpdate();
            hitBox.setPosition(position);
            lifeBar.update(this);

            movementSpeed = movementSpeedConstant * (float)gameTime.EllapsedTime.TotalMilliseconds;
            movement = getVectorForMove();
            move(movement);

            hitFromDirections.Clear();
        }
    }
}
