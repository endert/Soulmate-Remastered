using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder
{
    class PetItem : AbstractItem
    {
        public override string type { get { return base.type + ".PetItem"; } }

        public PetItem()
        {
            textureList.Add(new Texture("Pictures/Items/PetItem.png"));
            sprite = new Sprite(textureList[0]);
            visible = false;
            position = new Vector2f();
            hitBox = new HitBox(position, textureList[0].Size.X, textureList[0].Size.Y);
            dropRate = 100;
        }

        public override void cloneAndDrop(Vector2f dropPosition)
        {
            PetItem petItem = new PetItem();
            ItemHandler.add(petItem);
            petItem.drop(dropPosition);
        }
    }
}