using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using Soulmate_Remastered.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PetFolder
{
    abstract class AbstractPet : Entity
    {
        /// <summary>
        /// the type of this instance
        /// </summary>
        public override String type { get { return base.type + ".Pet"; } }

        /// <summary>
        /// saves the data of this instance as a String
        /// </summary>
        /// <returns></returns>
        public String ToStringForSave()
        {
            String petForSave = "pt" + lineBreak.ToString();

            petForSave += CurrentHP + lineBreak.ToString();   //splitPetString[1]
            petForSave += position.X + lineBreak.ToString(); //splitPetString[2]
            petForSave += position.Y + lineBreak.ToString(); //splitPetString[3]
            petForSave += type.Split('.')[type.Split('.').Length - 1] + lineBreak.ToString();  //splitPetString[4]

            return petForSave;
        }

        /// <summary>
        /// loads the data of this instance from a String
        /// </summary>
        /// <param name="petString"></param>
        public void Load(String petString)
        {
            String[] splitPetString = petString.Split(lineBreak);

            CurrentHP = Convert.ToSingle(splitPetString[1]);
            position = new Vector2f(Convert.ToSingle(splitPetString[2]), Convert.ToSingle(splitPetString[3]));
        }

        /// <summary>
        /// "revives" this instance
        /// </summary>
        public void Revive()
        {
            CurrentHP = MaxHP;
            position = PlayerHandler.player.position;
            sprite.Position = PlayerHandler.player.position;
            isAlive = true;
        }

        /// <summary>
        /// Updates the sprite position
        /// </summary>
        public virtual void SpritePositionUpdate()
        {
            switch (Direction)
            {
                case EDirection.Back:
                    sprite.Position = position;
                    break;
                case EDirection.Front:
                    sprite.Position = position;
                    break;
                case EDirection.Right:
                    sprite.Position = position;
                    break;
                case EDirection.Left:
                    sprite.Position = new Vector2(position.X - (textureList[2].Size.X - hitBox.width), position.Y);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// returns the move Vector
        /// </summary>
        /// <returns></returns>
        public Vector2 GetVectorForMove()
        {
            if (PlayerHandler.player.IsMoving)
            {
                if (isBehindPlayer())
                {
                    return getPlayerDirection();
                }
                else if (!(PlayerHandler.player.FacingDirection.X != 0 && PlayerHandler.player.FacingDirection.Y != 0))    //If player dont facing diagonal
                {
                    return -PlayerHandler.player.FacingDirection;
                }
                else
                {
                    switch (PlayerHandler.player.Direction)
                    {
                        case EDirection.Back:
                            return Vector2.FRONT;
                        case EDirection.Front:
                            return Vector2.BACK;
                        case EDirection.Right:
                            return Vector2.LEFT;
                        case EDirection.Left:
                            return Vector2.RIGHT;
                        default:
                            return Vector2.ZERO;
                    }
                }
            }
            else
            {
                if (isBehindPlayer() && hitBox.DistanceTo(PlayerHandler.player.hitBox) >= 50f)
                {
                    return getPlayerDirection();
                }
                else if (!isBehindPlayer())    //If player dont facing diagonal
                {
                    if (PlayerHandler.player.FacingDirection.X == 0 || PlayerHandler.player.FacingDirection.Y == 0)    //If player dont facing diagonal
                    {
                        return -PlayerHandler.player.FacingDirection;
                    }
                    else
                    {
                        switch (PlayerHandler.player.Direction)
                        {
                            case EDirection.Back:
                                return Vector2.FRONT;
                            case EDirection.Front:
                                return Vector2.BACK;
                            case EDirection.Right:
                                return Vector2.LEFT;
                            case EDirection.Left:
                                return Vector2.RIGHT;
                            default:
                                return Vector2.ZERO;
                        }
                    }
                }
                else
                {
                    return Vector2.ZERO;
                }
            }
        }

        /// <summary>
        /// returns if the pet is behind the player or not
        /// </summary>
        /// <returns></returns>
        public bool isBehindPlayer()
        {
            Vector2 playerMovingDirection = PlayerHandler.player.FacingDirection;
            bool behindX = false;   // for diagonal moving;
            bool behindY = false;

            if (PlayerHandler.player.FacingDirection.X == 0 || PlayerHandler.player.FacingDirection.Y == 0)
            {
                switch (PlayerHandler.player.Direction)
                {
                    case (EDirection.Back):
                        if (position.Y + hitBox.height <= PlayerHandler.player.position.Y)
                            return true;
                        break;
                    case (EDirection.Front):
                        if (position.Y >= PlayerHandler.player.position.Y + PlayerHandler.player.hitBox.height)
                            return true;
                        break;
                    case (EDirection.Right):
                        if (position.X + hitBox.width <= PlayerHandler.player.position.X)
                            return true;
                        break;
                    case (EDirection.Left):
                        if (position.X >= PlayerHandler.player.position.X + PlayerHandler.player.hitBox.width)
                            return true;
                        break;
                    default:
                        break;
                }
                return false;
            }
            else      // moving diagonal
            {
                if (playerMovingDirection.X > 0)
                    if (position.X + hitBox.width <= PlayerHandler.player.position.X)
                        behindX = true;
                else if (playerMovingDirection.X < 0)
                    if (position.X >= PlayerHandler.player.position.X + PlayerHandler.player.hitBox.width)
                        behindX = true;

                if (playerMovingDirection.Y > 0)
                    if (position.Y + hitBox.height <= PlayerHandler.player.position.Y)
                        behindY = true;
                else if (playerMovingDirection.Y < 0)
                    if (position.Y >= PlayerHandler.player.position.Y + PlayerHandler.player.hitBox.height)
                        behindY = true;

                return (behindX || behindY);
            }
        }

        /// <summary>
        /// updates this instance
        /// </summary>
        /// <param name="gameTime"></param>
        public override void update(GameTime gameTime)
        {
            //check if it is still alive
            if (CurrentHP <= 0)
                isAlive = false;

            //teleport to player if the distance gets to big
            if (hitBox.DistanceTo(PlayerHandler.player.hitBox) >= 800f)
                position = PlayerHandler.player.position;

            //call animate
            animate();

            //update the rest
            hitBox.update(sprite);
            SpritePositionUpdate();
            lifeBar.update(this);

            movementSpeed = BaseMovementSpeed * (float)gameTime.EllapsedTime.TotalMilliseconds;
            movement = GetVectorForMove();
            move(movement);

            hitFromDirections.Clear();
        }
    }
}
