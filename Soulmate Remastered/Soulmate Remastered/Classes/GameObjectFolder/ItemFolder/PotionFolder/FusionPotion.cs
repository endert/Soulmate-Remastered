using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.ItemFolder;
using Soulmate_Remastered.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.PotionFolder
{
    /// <summary>
    /// a potion to increase the fusion value
    /// </summary>
    class FusionPotion : AbstractPotion
    {
        /// <summary>
        /// the ID = 121
        /// </summary>
        public override float ID
        {
            get
            {
                return base.ID * 10 + 1;
            }
        }

        protected override void LoadTextures()
        {
            TextureList.Add(new Texture("Pictures/Items/Potion/FusionPotion/FusionPotionSmall.png"));
        }

        /// <summary>
        /// initialize a new fusion potion
        /// </summary>
        /// <param name="fusionPotionSize">determines how much it will raise the fusion value</param>
        public FusionPotion(PotionSize fusionPotionSize)
        {
            Position = new Vector2();
            DropRate = 100;
            Size = fusionPotionSize;

            switch (fusionPotionSize)
            {
                case PotionSize.small:
                    recoveryValue = 20;
                    break;
                case PotionSize.middle:
                    recoveryValue = 50;
                    break;
                case PotionSize.large:
                    recoveryValue = 100;
                    break;
                default:
                    throw new NotFiniteNumberException();
            }

            Sprite.Texture = TextureList[(int)fusionPotionSize];
        }

        /// <summary>
        /// use this item
        /// </summary>
        protected override void Use(object sender, UseEventArgs eventArgs)
        {
            if (PlayerHandler.Player.CurrentFusionValue != PlayerHandler.Player.MaxFusionValue)
            {
                PlayerHandler.Player.HealFusionFor(recoveryValue);
                IsAlive = false;
            }
        }

        /// <summary>
        /// clone this item
        /// </summary>
        /// <returns></returns>
        public override AbstractItem Clone()
        {
            AbstractItem clonedItem = new FusionPotion(Size);
            clonedItem.Position = this.Position;
            return clonedItem;
        }
    }
}
