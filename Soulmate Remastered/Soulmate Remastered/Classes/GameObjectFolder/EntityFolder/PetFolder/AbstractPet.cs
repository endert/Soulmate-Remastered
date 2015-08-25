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
        public override String Type { get { return base.Type + ".Pet"; } }

        /// <summary>
        /// saves the data of this instance as a String
        /// </summary>
        /// <returns></returns>
        public String ToStringForSave()
        {
            String petForSave = "pt" + LineBreak.ToString();

            petForSave += CurrentHP + LineBreak.ToString();   //splitPetString[1]
            petForSave += Position.X + LineBreak.ToString(); //splitPetString[2]
            petForSave += Position.Y + LineBreak.ToString(); //splitPetString[3]
            petForSave += Type.Split('.')[Type.Split('.').Length - 1] + LineBreak.ToString();  //splitPetString[4]

            return petForSave;
        }

        /// <summary>
        /// loads the data of this instance from a String
        /// </summary>
        /// <param name="petString"></param>
        public void Load(String petString)
        {
            String[] splitPetString = petString.Split(LineBreak);

            CurrentHP = Convert.ToSingle(splitPetString[1]);
            Position = new Vector2f(Convert.ToSingle(splitPetString[2]), Convert.ToSingle(splitPetString[3]));
        }

        /// <summary>
        /// "revives" this instance
        /// </summary>
        public void Revive()
        {
            CurrentHP = MaxHP;
            Position = PlayerHandler.player.Position;
            Sprite.Position = PlayerHandler.player.Position;
            IsAlive = true;
        }

        /// <summary>
        /// Updates the sprite position
        /// </summary>
        public virtual void SpritePositionUpdate()
        {
            switch (Direction)
            {
                case EDirection.Back:
                    Sprite.Position = Position;
                    break;
                case EDirection.Front:
                    Sprite.Position = Position;
                    break;
                case EDirection.Right:
                    Sprite.Position = Position;
                    break;
                case EDirection.Left:
                    Sprite.Position = new Vector2(Position.X - (TextureList[2].Size.X - HitBox.width), Position.Y);
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
                if (IsBehindPlayer())
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
                if (IsBehindPlayer() && HitBox.DistanceTo(PlayerHandler.player.HitBox) >= 50f)
                {
                    return getPlayerDirection();
                }
                else if (!IsBehindPlayer())    //If player dont facing diagonal
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
        public bool IsBehindPlayer()
        {
            Vector2 playerMovingDirection = PlayerHandler.player.FacingDirection;
            bool behindX = false;   // for diagonal moving;
            bool behindY = false;

            if (PlayerHandler.player.FacingDirection.X == 0 || PlayerHandler.player.FacingDirection.Y == 0)
            {
                switch (PlayerHandler.player.Direction)
                {
                    case (EDirection.Back):
                        if (Position.Y + HitBox.height <= PlayerHandler.player.Position.Y)
                            return true;
                        break;
                    case (EDirection.Front):
                        if (Position.Y >= PlayerHandler.player.Position.Y + PlayerHandler.player.HitBox.height)
                            return true;
                        break;
                    case (EDirection.Right):
                        if (Position.X + HitBox.width <= PlayerHandler.player.Position.X)
                            return true;
                        break;
                    case (EDirection.Left):
                        if (Position.X >= PlayerHandler.player.Position.X + PlayerHandler.player.HitBox.width)
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
                    if (Position.X + HitBox.width <= PlayerHandler.player.Position.X)
                        behindX = true;
                else if (playerMovingDirection.X < 0)
                    if (Position.X >= PlayerHandler.player.Position.X + PlayerHandler.player.HitBox.width)
                        behindX = true;

                if (playerMovingDirection.Y > 0)
                    if (Position.Y + HitBox.height <= PlayerHandler.player.Position.Y)
                        behindY = true;
                else if (playerMovingDirection.Y < 0)
                    if (Position.Y >= PlayerHandler.player.Position.Y + PlayerHandler.player.HitBox.height)
                        behindY = true;

                return (behindX || behindY);
            }
        }

        /// <summary>
        /// updates this instance
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            //check if it is still alive
            if (CurrentHP <= 0)
                IsAlive = false;

            //teleport to player if the distance gets to big
            if (HitBox.DistanceTo(PlayerHandler.player.HitBox) >= 800f)
                Position = PlayerHandler.player.Position;

            //call animate
            animate();

            //update the rest
            HitBox.update(Sprite);
            SpritePositionUpdate();
            lifeBar.update(this);

            movementSpeed = BaseMovementSpeed * (float)gameTime.EllapsedTime.TotalMilliseconds;
            movement = GetVectorForMove();
            move(movement);

            hitFromDirections.Clear();
        }
    }
}
