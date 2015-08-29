using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.MoneyFolder
{
    /// <summary>
    /// gold, the main currency for the player
    /// </summary>
    class Gold : Currency
    {
        /// <summary>
        /// the type of this instance
        /// </summary>
        public override String Type { get { return base.Type + ".Gold"; } }
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

        /// <summary>
        /// initialize a new gold
        /// </summary>
        public Gold()
        {
            TextureList.Add(new Texture("Pictures/Items/Money/Gold.png"));
            Sprite = new Sprite(TextureList[0]);
            IsVisible = false;
            Position = new Vector2f();
            HitBox = new HitBox(Position, TextureList[0].Size.X, TextureList[0].Size.Y);
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
