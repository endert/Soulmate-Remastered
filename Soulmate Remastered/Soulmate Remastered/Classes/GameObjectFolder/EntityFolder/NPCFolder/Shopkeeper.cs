using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.NPCFolder.ShopFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.NPCFolder
{
    class Shopkeeper : AbstractNPC
    {
        public override string type { get { return base.type + ".Shopkeeper"; } }
        protected override string dialogePath { get { return base.dialogePath; } }
        Shop shop;
        List<Stack<AbstractItem>> itemsForSell;

        public Shopkeeper(Vector2f _position)
        {
            textureList.Add(new Texture("Pictures/Entities/NPC/PetStorageGuy/PetStorageGuyFrontTest.png"));
            textureList.Add(new Texture("Pictures/Entities/NPC/PetStorageGuy/PetStorageGuyBackTest.png"));
            textureList.Add(new Texture("Pictures/Entities/NPC/PetStorageGuy/PetStorageGuyRightTest.png"));
            textureList.Add(new Texture("Pictures/Entities/NPC/PetStorageGuy/PetStorageGuyLeftTest.png"));

            facingDirection = new Vector2f(0, 1);

            sprite = new Sprite(textureList[getNumFacingDirection]);

            position = _position;
            sprite.Position = position;
            hitBox = new HitBox(position, sprite.Texture.Size.X, sprite.Texture.Size.Y);
            interacted = false;
            NPCHandler.add(this);
        }

        public override void interact()
        {
            shop = new Shop(itemsForSell);
        }

        public override void stopIteraction()
        {
            shop = null;
        }
    }
}
