using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PetFolder;
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
        private AbstractPet pet;

        public PetItem(AbstractPet _pet)
        {
            textureList.Add(new Texture("Pictures/Items/PetItem.png"));
            sprite = new Sprite(textureList[0]);
            visible = false;
            position = new Vector2f();
            hitBox = new HitBox(position, textureList[0].Size.X, textureList[0].Size.Y);
            dropRate = 100;
            pet = _pet;
        }

        public override void cloneAndDrop(Vector2f dropPosition)
        {
            PetItem petItem = new PetItem(pet);
            ItemHandler.add(petItem);
            petItem.drop(dropPosition);
        }

        public override void use()
        {
            pet.revive();
            PetHandler.pet = pet;
            EntityHandler.add(pet);

            isAlive = false;
        }
    }
}