using SFML.Graphics;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Core;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PetFolder
{
    /// <summary>
    /// a Wolf as a pet
    /// </summary>
    class PetWolf : AbstractPet
    {
        public PetWolf():this(PlayerHandler.Player) { }

        protected override void LoadTextures()
        {
            TextureList.Add(new Texture("Pictures/Entities/Pet/Wolf/WolfFront.png"));
            TextureList.Add(new Texture("Pictures/Entities/Pet/Wolf/WolfBack.png"));
            TextureList.Add(new Texture("Pictures/Entities/Pet/Wolf/WolfRight.png"));
            TextureList.Add(new Texture("Pictures/Entities/Pet/Wolf/WolfLeft.png"));
        }

        /// <summary>
        /// creates an instance
        /// </summary>
        /// <param name="player"></param>
        public PetWolf(AbstractPlayer player)
        {
            //Initialize Gameobject attributes********************************************************************************

            Position = new Vector2(player.Position.X - 150, player.Position.Y + player.HitBox.Height - TextureList[0].Size.Y);
            Sprite.Position = Position;

            //****************************************************************************************************************
            //Initialize Entity attributes************************************************************************************

            MaxHP = 50f;
            CurrentHP = MaxHP;
            BaseMovementSpeed = 0.4f;

            //****************************************************************************************************************
        }
    }
}
