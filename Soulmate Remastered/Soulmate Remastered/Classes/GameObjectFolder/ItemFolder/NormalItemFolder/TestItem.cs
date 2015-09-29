using SFML.Graphics;
using Soulmate_Remastered.Core;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.NormalItemFolder
{
    /// <summary>
    /// item for testing (may be later an quest item ;) )
    /// </summary>
    class TestItem : AbstractNormalItem
    {
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

        protected override void LoadTextures()
        {
            TextureList.Add(new Texture("Pictures/Items/TestItem(Pete).png"));
        }

        /// <summary>
        /// initialize a new instance
        /// </summary>
        public TestItem()
        {
            Position = new Vector2();
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
