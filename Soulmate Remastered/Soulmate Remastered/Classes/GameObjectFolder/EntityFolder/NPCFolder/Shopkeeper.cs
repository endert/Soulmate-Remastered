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
            itemsForSell = new List<Stack<AbstractItem>>();
            position = _position;
            sprite.Position = position;
            hitBox = new HitBox(position, sprite.Texture.Size.X, sprite.Texture.Size.Y);
            interacted = false;
            NPCHandler.add(this);
        }

        public void addItemForSell(AbstractItem item, int count)
        {
            Stack<AbstractItem> stack = new Stack<AbstractItem>();

            for (int i = 0; i < count; i++)
            {
                stack.Push(item.clone());
            }

            addItemsForSell(stack);
        }

        void addItemsForSell(Stack<AbstractItem> item)
        {
            itemsForSell.Add(item);
        }

        public override void interact()
        {
            interacted = true;
            shop = new Shop(itemsForSell);
        }

        public override void stopIteraction()
        {
            shop.closeShop();
            shop = null;
            interacted = false;
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
        }
    }
}
