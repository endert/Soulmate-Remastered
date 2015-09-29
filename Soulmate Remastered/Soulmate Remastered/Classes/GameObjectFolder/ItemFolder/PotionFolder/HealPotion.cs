using SFML.Graphics;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Core;
using System;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.PotionFolder
{
    /// <summary>
    /// a heal potion
    /// </summary>
    class HealPotion : AbstractPotion
    {
        /// <summary>
        /// the ID = 120
        /// </summary>
        public override float ID
        {
            get
            {
                return base.ID * 10 + 0;
            }
        }

        protected override void LoadTextures()
        {
            TextureList.Add(new Texture("Pictures/Items/Potion/HealPotion/PotionSmall.png"));
        }

        /// <summary>
        /// initialize a new heal potion
        /// </summary>
        /// <param name="healPotionSize">determines how much this potion will heal</param>
        public HealPotion(PotionSize healPotionSize)
        {
            Position = new Vector2();
            DropRate = 100;
            Size = healPotionSize;

            switch (healPotionSize)
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
            Sprite.Texture = TextureList[(int)Size];
        }

        /// <summary>
        /// use this item
        /// </summary>
        protected override void Use(object sender, UseEventArgs eventArgs)
        {
            if (PlayerHandler.Player.CurrentHP != PlayerHandler.Player.MaxHP)
            {
                PlayerHandler.Player.HealFor(recoveryValue);
                IsAlive = false;
            }
        }

        /// <summary>
        /// clone this item
        /// </summary>
        /// <returns></returns>
        public override AbstractItem Clone()
        {
            AbstractItem clonedItem = new HealPotion(Size);
            clonedItem.Position = this.Position;
            return clonedItem;
        }
    }
}
