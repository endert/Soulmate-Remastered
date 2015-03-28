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
    class FusionPotion : AbstractPotion
    {
        public override string type { get { return base.type + ".FusionPotion"; } }
        public override float ID
        {
            get
            {
                return base.ID * 10 + 1;
            }
        }

        public enum fusionPotionSize
        {
            small,
            middle,
            big
        }

        public FusionPotion(fusionPotionSize fusionPotionSize)
        {
            visible = false;
            position = new Vector2f();
            dropRate = 100;
            size = (int)fusionPotionSize;

            switch (fusionPotionSize)
            {
                case fusionPotionSize.small:
                    recoveryValue = 20;
                    textureList.Add(new Texture("Pictures/Items/Potion/FusionPotion/FusionPotionSmall.png"));
                    break;
                case fusionPotionSize.middle:
                    recoveryValue = 50;
                    break;
                case fusionPotionSize.big:
                    recoveryValue = 100;
                    break;
                default:
                    throw new NotFiniteNumberException();
            }
            sprite = new Sprite(textureList[0]);
            hitBox = new HitBox(position, textureList[0].Size.X, textureList[0].Size.Y);
        }

        public override void use()
        {
            if (PlayerHandler.player.currentFusionValue != PlayerHandler.player.getMaxFusionValue)
            {
                PlayerHandler.player.healFusionFor(recoveryValue);
                isAlive = false;
            }
        }

        public override AbstractItem clone()
        {
            AbstractItem clonedItem = new FusionPotion((fusionPotionSize)size);
            clonedItem.position = this.position;
            return clonedItem;
        }
    }
}
