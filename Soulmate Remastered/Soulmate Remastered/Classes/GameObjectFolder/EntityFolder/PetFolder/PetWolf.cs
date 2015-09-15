using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soulmate_Remastered.Classes.HUDFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;

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

            Position = new Vector2f(player.Position.X - 150, player.Position.Y + player.HitBox.height - TextureList[0].Size.Y);
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
