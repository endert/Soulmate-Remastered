using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.NormalItemFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.NormalItemFolder
{
    class TestItem : AbstractNormalItem
    {
        public override string type { get { return base.type + ".Pete"; } }
        public override float ID
        {
            get
            {
                return base.ID * 10 + 0;
            }
        }

        public TestItem()
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
            TestItem test = new TestItem();
            ItemHandler.add(test);
            test.drop(dropPosition);
        }

        public override AbstractItem clone()
        {
            AbstractItem clonedItem = new TestItem();
            clonedItem.position = this.position;
            return clonedItem;
        }
    }
}
