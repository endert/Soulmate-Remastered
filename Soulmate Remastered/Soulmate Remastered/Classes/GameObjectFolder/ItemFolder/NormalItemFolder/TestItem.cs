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
        public override bool sellable { get { return false; } }

        public override float ID
        {
            get
            {
                return base.ID * 10 + 0;
            }
        }
        public override String ItemDiscription
        {
            get
            {
                String itemDiscription = "";

                itemDiscription += "Pete \n";
                itemDiscription += "A totaly useless Item";

                return itemDiscription;
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

        public override AbstractItem clone()
        {
            AbstractItem clonedItem = new TestItem();
            clonedItem.position = this.position;
            return clonedItem;
        }
    }
}
