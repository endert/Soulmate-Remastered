﻿using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PetFolder
{
    abstract class AbstractPet : Entity
    {
        public String type { get { return base.type + ".Pet"; } }

        public void update(GameTime gameTime)
        {
            animate(textureList);

            spritePositionUpdate();
            hitBox.setPosition(position);

            movementSpeed = 0.4f * (float)gameTime.EllapsedTime.TotalMilliseconds;
            movement = getVectorForMove();
            move(movement);
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
                        sprite.Position = new Vector2f(position.X - (textureList[2].Size.X - hitBox.getWidth()), position.Y);
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
    }
}
