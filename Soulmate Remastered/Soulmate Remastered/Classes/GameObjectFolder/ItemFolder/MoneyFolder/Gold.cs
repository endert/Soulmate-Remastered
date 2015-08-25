using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.MoneyFolder
{
    class Gold : Money //should extend another instans (money?)
    {
        public override String Type { get { return base.Type + ".Gold"; } }
        public override bool sellable { get { return false; } }

        public Gold()
        {
            TextureList.Add(new Texture("Pictures/Items/Money/Gold.png"));
            Sprite = new Sprite(TextureList[0]);
            IsVisible = false;
            Position = new Vector2f();
            HitBox = new HitBox(Position, TextureList[0].Size.X, TextureList[0].Size.Y);
            dropRate = 80;
        }

        public override AbstractItem clone()
        {
            return new Gold();
        }
    }
}
