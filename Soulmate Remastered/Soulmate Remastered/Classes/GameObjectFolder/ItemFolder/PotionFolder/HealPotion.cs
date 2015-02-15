using SFML.Graphics;
using SFML.Window;
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

        public HealPotion()
        {
            textureList.Add(new Texture("Pictures/Items/TestItem(Pete).png"));
            sprite = new Sprite(textureList[0]);
            visible = false;
            position = new Vector2f();
            hitBox = new HitBox(position, textureList[0].Size.X, textureList[0].Size.Y);
            dropRate = 100;
        }

        public override void cloneAndDrop(Vector2f dropPosition)
        {
            HealPotion test = new HealPotion();
            ItemHandler.add(test);
            test.drop(dropPosition);
        }

        public override void use()
        {
            
        }
    }
}
