using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.PotionFolder
{
    /// <summary>
    /// a heal potion
    /// </summary>
    class HealPotion : AbstractPotion
    {
        /// <summary>
        /// the type of this instance
        /// </summary>
        public override string Type { get { return base.Type + ".HealPotion"; } }
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

        /// <summary>
        /// initialize a new heal potion
        /// </summary>
        /// <param name="healPotionSize">determines how much this potion will heal</param>
        public HealPotion(PotionSize healPotionSize)
        {
            IsVisible = false;
            Position = new Vector2f();
            DropRate = 100;
            Size = healPotionSize;

            switch (healPotionSize)
            {
                case PotionSize.small:
                    recoveryValue = 20;
                    TextureList.Add(new Texture("Pictures/Items/Potion/HealPotion/PotionSmall.png"));
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
