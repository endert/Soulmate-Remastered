using SFML.Graphics;
using Soulmate_Remastered.Core;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.MoneyFolder
{
    /// <summary>
    /// gold, the main currency for the player
    /// </summary>
    class Gold : Currency
    {
        /// <summary>
        /// bool if this item is sellable, for currencies default = false
        /// </summary>
        public override bool Sellable { get { return false; } }
        /// <summary>
        /// the ID = 100
        /// </summary>
        public override float ID
        {
            get
            {
                return base.ID * 10 + 0;
            }
        }

        protected override void LoadTextures()
        {
            TextureList.Add(new Texture("Pictures/Items/Money/Gold.png"));
        }

        /// <summary>
        /// initialize a new gold
        /// </summary>
        public Gold()
        {
            Position = new Vector2();
            Sprite.Position = Position;
            DropRate = 80;
        }

        /// <summary>
        /// clone this item
        /// </summary>
        /// <returns></returns>
        public override AbstractItem Clone()
        {
            return new Gold();
        }
    }
}
