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
        /// <summary>
        /// the type of this instance
        /// </summary>
        public override String Type { get { return base.Type + ".PetWolf"; } }

        /// <summary>
        /// creates an instance
        /// </summary>
        /// <param name="player"></param>
        public PetWolf(AbstractPlayer player)
        {
            //Initialize Gameobject attributes********************************************************************************

            TextureList.Add(new Texture("Pictures/Entities/Pet/Wolf/WolfFront.png"));
            TextureList.Add(new Texture("Pictures/Entities/Pet/Wolf/WolfBack.png"));
            TextureList.Add(new Texture("Pictures/Entities/Pet/Wolf/WolfRight.png"));
            TextureList.Add(new Texture("Pictures/Entities/Pet/Wolf/WolfLeft.png"));

            IsAlive = true;
            IsVisible = true;
            Sprite = new Sprite(TextureList[0]);
            Position = new Vector2f(player.Position.X - 150, player.Position.Y + player.HitBox.height - TextureList[0].Size.Y);
            Sprite.Position = Position;
            HitBox = new HitBox(Sprite.Position, TextureList[0].Size.X, TextureList[0].Size.Y);

            //****************************************************************************************************************
            //Initialize Entity attributes************************************************************************************

            MaxHP = 50f;
            CurrentHP = MaxHP;
            BaseMovementSpeed = 0.4f;
            lifeBar = new LifeBarForOthers();

            //****************************************************************************************************************
        }
    }
}
