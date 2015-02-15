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
        public override string type { get { return base.type + ".HealPotion"; } }
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
            visible = false;
            position = new Vector2f();
            dropRate = 100;
            size = (int)healPotionSize;

            switch (healPotionSize)
            {
                case healPotionSize.small:
                    recoveryValue = 20;
                    textureList.Add(new Texture("Pictures/Items/Potion/HealPotion/PotionSmall.png"));
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
            sprite = new Sprite(textureList[0]);
            hitBox = new HitBox(position, textureList[0].Size.X, textureList[0].Size.Y);
        }

        public override void cloneAndDrop(Vector2f dropPosition)
        {
            HealPotion healPotion = new HealPotion((healPotionSize)size);
            ItemHandler.add(healPotion);
            healPotion.drop(dropPosition);
        }

        public override void use()
        {
            PlayerHandler.player.HealFor(recoveryValue);
            isAlive = false;
        }

        public override AbstractItem clone()
        {
            AbstractItem clonedItem = new HealPotion((healPotionSize)size);
            clonedItem.position = this.position;
            return clonedItem;
        }
    }
}
