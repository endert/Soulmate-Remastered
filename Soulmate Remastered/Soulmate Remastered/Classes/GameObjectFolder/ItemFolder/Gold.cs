using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder
{
    class Gold : AbstractItem
    {
        public String type { get { return base.type + ".Gold"; } }

        public Gold()
        {
            textureList.Add(new Texture("Picture/Items/Gold.png"));
            sprite = new Sprite(textureList[0]);
            visible = false;
            walkable = true;
            position = new Vector2f();
            hitBox = new HitBox(position, textureList[0].Size.X, textureList[0].Size.Y);
        }

        public override void cloneAndDrop(Vector2f dropPosition)
        {
            (new Gold()).drop(dropPosition);
        }
    }
}
