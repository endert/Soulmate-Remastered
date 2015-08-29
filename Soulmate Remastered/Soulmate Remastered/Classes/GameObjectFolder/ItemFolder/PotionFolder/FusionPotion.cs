using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
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
        /// the type of this instance
        /// </summary>
        public override string Type { get { return base.Type + ".FusionPotion"; } }
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

        /// <summary>
        /// initialize a new fusion potion
        /// </summary>
        /// <param name="fusionPotionSize">determines how much it will raise the fusion value</param>
        public FusionPotion(PotionSize fusionPotionSize)
        {
            IsVisible = false;
            Position = new Vector2();
            DropRate = 100;
            Size = fusionPotionSize;

            switch (fusionPotionSize)
            {
                case PotionSize.small:
                    recoveryValue = 20;
                    TextureList.Add(new Texture("Pictures/Items/Potion/FusionPotion/FusionPotionSmall.png"));
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
            Sprite = new Sprite(TextureList[0]);
            HitBox = new HitBox(Position, TextureList[0].Size.X, TextureList[0].Size.Y);
        }

        /// <summary>
        /// use this item
        /// </summary>
        public override void Use()
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
