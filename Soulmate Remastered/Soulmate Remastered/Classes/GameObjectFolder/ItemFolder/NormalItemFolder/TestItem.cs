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
    /// <summary>
    /// item for testing (may be later an quest item ;) )
    /// </summary>
    class TestItem : AbstractNormalItem
    {
        /// <summary>
        /// the type of this instance
        /// </summary>
        public override string Type { get { return base.Type + ".Pete"; } }
        /// <summary>
        /// a bool if this item is sellable
        /// </summary>
        public override bool Sellable { get { return false; } }

        /// <summary>
        /// the ID = 110
        /// </summary>
        public override float ID
        {
            get
            {
                return base.ID * 10 + 0;
            }
        }

        /// <summary>
        /// the item discription
        /// </summary>
        public override string ItemDiscription
        {
            get
            {
                string itemDiscription = "";

                itemDiscription += "Pete \n";
                itemDiscription += "A totaly useless Item";

                return itemDiscription;
            }
        }

        /// <summary>
        /// initialize a new instance
        /// </summary>
        public TestItem()
        {
            TextureList.Add(new Texture("Pictures/Items/TestItem(Pete).png"));
            Sprite = new Sprite(TextureList[0]);
            IsVisible = false;
            Position = new Vector2f();
            HitBox = new HitBox(Position, TextureList[0].Size.X, TextureList[0].Size.Y);
            DropRate = 100;
        }

        /// <summary>
        /// clones this
        /// </summary>
        /// <returns></returns>
        public override AbstractItem Clone()
        {
            AbstractItem clonedItem = new TestItem();
            clonedItem.Position = this.Position;
            return clonedItem;
        }
    }
}
