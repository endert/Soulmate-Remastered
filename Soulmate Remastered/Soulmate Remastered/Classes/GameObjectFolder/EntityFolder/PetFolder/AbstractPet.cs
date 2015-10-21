using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.HUDFolder;
using Soulmate_Remastered.Core;
using System;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PetFolder
{
    abstract class AbstractPet : Entity
    {
        public AbstractPet()
        {
            lifeBar = new LifeBarForOthers();
        }

        /// <summary>
        /// saves the data of this instance as a String
        /// </summary>
        /// <returns></returns>
        public override string ToStringForSave()
        {
            string petForSave = GetType() + LineBreak.ToString();

            petForSave += CurrentHP + LineBreak.ToString();   //splitPetString[1]
            petForSave += Position.X + LineBreak.ToString(); //splitPetString[2]
            petForSave += Position.Y + LineBreak.ToString(); //splitPetString[3]

            return petForSave;
        }

        /// <summary>
        /// loads the data of this instance from a String
        /// </summary>
        /// <param name="petString"></param>
        public void Load(string petString)
        {
            string[] splitPetString = petString.Split(LineBreak);

            CurrentHP = Convert.ToSingle(splitPetString[1]);
            Position = new Vector2(Convert.ToSingle(splitPetString[2]), Convert.ToSingle(splitPetString[3]));
        }

        /// <summary>
        /// "revives" this instance
        /// </summary>
        public void Revive()
        {
            CurrentHP = MaxHP;
            Position = PlayerHandler.Player.Position;
            Sprite.Position = PlayerHandler.Player.Position;
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
                    Sprite.Position = new Vector2(Position.X - (TextureList[2].Size.X - HitBox.Width), Position.Y);
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
            if (PlayerHandler.Player.IsMoving)
            {
                if (IsBehindPlayer())
                {
                    return GetPlayerDirection();
                }
                else
                {
                    switch (PlayerHandler.Player.Direction)
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
                if (IsBehindPlayer() && HitBox.DistanceTo(PlayerHandler.Player.HitBox) >= 50f)
                {
                    return GetPlayerDirection();
                }
                else if (!IsBehindPlayer())    //If player dont facing diagonal
                {

                    switch (PlayerHandler.Player.Direction)
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
            bool res = true;

            Vector2 p1 = PlayerHandler.Player.Position;
            Vector2 S1 = PlayerHandler.Player.HitBox.Size;
            Vector2 fd = PlayerHandler.Player.FacingDirection;

            Vector2 p2 = Position;
            Vector2 S2 = HitBox.Size;

            Vector2 t = S1 / 2 * fd.GetNormalized();
            Vector2 u = p1 + S1 / 2;
            Vector2 w = (p2 + S2 / 2) + (S2 / 2 * fd.GetNormalized());
            Vector2 s = (w - (u - t));
            bool help = s.Length >= 30;
            s = s.GetNormalized();

            Vector2 v = -(fd.GetNormalized());

            s /= s.Abs();
            v /= v.Abs();

            if (v.X == 0)
                s.X = 0;

            if (v.Y == 0)
                s.Y = 0;

            res = s == v && help;

            return res;
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
            if (HitBox.DistanceTo(PlayerHandler.Player.HitBox) >= 800f)
                Position = PlayerHandler.Player.Position;

            //call animate
            Animate();

            //update the rest
            HitBox.Update(Sprite);
            SpritePositionUpdate();
            lifeBar.Update(this);

            movement = GetVectorForMove();
            Move(movement);
        }
    }
}
