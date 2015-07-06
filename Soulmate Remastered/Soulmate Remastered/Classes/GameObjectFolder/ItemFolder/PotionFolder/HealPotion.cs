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
    class HealPotion : AbstractPotion
    {
        public override string Type { get { return base.Type + ".HealPotion"; } }
        public override float ID
        {
            get
            {
                return base.ID * 10 + 0;
            }
        }

        public enum healPotionSize
        {
            small,
            middle,
            big
        }

        public HealPotion(healPotionSize healPotionSize)
        {
            IsVisible = false;
            Position = new Vector2f();
            dropRate = 100;
            size = (int)healPotionSize;

            switch (healPotionSize)
            {
                case healPotionSize.small:
                    recoveryValue = 20;
                    TextureList.Add(new Texture("Pictures/Items/Potion/HealPotion/PotionSmall.png"));
                    break;
                case healPotionSize.middle:
                    recoveryValue = 50;
                    break;
                case healPotionSize.big:
                    recoveryValue = 100;
                    break;
                default:
                    throw new NotFiniteNumberException();
            }
            Sprite = new Sprite(TextureList[0]);
            HitBox = new HitBox(Position, TextureList[0].Size.X, TextureList[0].Size.Y);
        }

        public override void use()
        {
            if (PlayerHandler.player.CurrentHP != PlayerHandler.player.MaxHP)
            {
                PlayerHandler.player.HealFor(recoveryValue);
                IsAlive = false;
            }
        }

        public override AbstractItem clone()
        {
            AbstractItem clonedItem = new HealPotion((healPotionSize)size);
            clonedItem.Position = this.Position;
            return clonedItem;
        }
    }
}
