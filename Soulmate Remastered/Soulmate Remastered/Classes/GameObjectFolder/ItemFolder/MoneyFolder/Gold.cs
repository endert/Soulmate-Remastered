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
        public override String type { get { return base.type + ".Gold"; } }
        public override bool sellable { get { return false; } }

        public Gold()
        {
            textureList.Add(new Texture("Pictures/Items/Money/Gold.png"));
            sprite = new Sprite(textureList[0]);
            visible = false;
            position = new Vector2f();
            hitBox = new HitBox(position, textureList[0].Size.X, textureList[0].Size.Y);
            dropRate = 80;
        }

        public override void cloneAndDrop(Vector2f dropPosition)
        {
            Gold gold = new Gold();
            ItemHandler.add(gold);
            gold.drop(dropPosition);
        }

        public override AbstractItem clone()
        {
            return new Gold();
        }
    }
}
