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
        public override string Type { get { return base.Type + ".FusionPotion"; } }
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
            IsVisible = false;
            Position = new Vector2f();
            dropRate = 100;
            size = (int)fusionPotionSize;

            switch (fusionPotionSize)
            {
                case fusionPotionSize.small:
                    recoveryValue = 20;
                    TextureList.Add(new Texture("Pictures/Items/Potion/FusionPotion/FusionPotionSmall.png"));
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
            Sprite = new Sprite(TextureList[0]);
            HitBox = new HitBox(Position, TextureList[0].Size.X, TextureList[0].Size.Y);
        }

        public override void use()
        {
            if (PlayerHandler.player.CurrentFusionValue != PlayerHandler.player.MaxFusionValue)
            {
                PlayerHandler.player.HealFusionFor(recoveryValue);
                IsAlive = false;
            }
        }

        public override AbstractItem clone()
        {
            AbstractItem clonedItem = new FusionPotion((fusionPotionSize)size);
            clonedItem.Position = this.Position;
            return clonedItem;
        }
    }
}
