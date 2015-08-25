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
        public override string Type { get { return base.Type + ".Pete"; } }
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
            TextureList.Add(new Texture("Pictures/Items/TestItem(Pete).png"));
            Sprite = new Sprite(TextureList[0]);
            IsVisible = false;
            Position = new Vector2f();
            HitBox = new HitBox(Position, TextureList[0].Size.X, TextureList[0].Size.Y);
            dropRate = 100;
        }

        public override AbstractItem clone()
        {
            AbstractItem clonedItem = new TestItem();
            clonedItem.Position = this.Position;
            return clonedItem;
        }
    }
}
