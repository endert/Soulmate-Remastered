using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.NPCFolder.ShopFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.EquipmentFolder;
using Soulmate_Remastered.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.NPCFolder
{
    /// <summary>
    /// don't mess with the shopkeeper
    /// </summary>
    class Shopkeeper : AbstractNPC
    {
        /// <summary>
        /// the type of this instance
        /// </summary>
        public override string type { get { return base.type + ".Shopkeeper"; } }
        /// <summary>
        /// path to the dialoge File
        /// </summary>
        protected override string dialogePath { get { return base.dialogePath; } }
        /// <summary>
        /// the shop instance of this instance
        /// </summary>
        Shop shop;
        /// <summary>
        /// the Item list wich can be sold
        /// </summary>
        List<Stack<AbstractItem>> itemsForSell;

        /// <summary>
        /// create a new instance of this class
        /// </summary>
        /// <param name="_position"></param>
        public Shopkeeper(Vector2 _position)
        {
            //Insitialize Game Object data**************************************************************************

            textureList.Add(new Texture("Pictures/Entities/NPC/PetStorageGuy/PetStorageGuyFrontTest.png"));
            textureList.Add(new Texture("Pictures/Entities/NPC/PetStorageGuy/PetStorageGuyBackTest.png"));
            textureList.Add(new Texture("Pictures/Entities/NPC/PetStorageGuy/PetStorageGuyRightTest.png"));
            textureList.Add(new Texture("Pictures/Entities/NPC/PetStorageGuy/PetStorageGuyLeftTest.png"));

            sprite = new Sprite(textureList[0]);
            itemsForSell = new List<Stack<AbstractItem>>();
            position = _position;
            sprite.Position = position;
            hitBox = new HitBox(position, sprite.Texture.Size.X, sprite.Texture.Size.Y);

            //******************************************************************************************************
            //Insitialize Entity data*******************************************************************************

            FacingDirection = Vector2.FRONT;

            //******************************************************************************************************
            //Insitialize NPC data**********************************************************************************

            Interacting = false;
            NPCHandler.shop = shop;
            NPCHandler.add(this);
            addItemForSell(new Sword(10, 10, 10), 10);

            //******************************************************************************************************
        }

        /// <summary>
        /// xhröü //written by alex/chaz
        /// </summary>
        public void addItemForSell(AbstractItem item, int count)
        {
            Stack<AbstractItem> stack = new Stack<AbstractItem>();

            for (int i = 0; i < count; i++)
            {
                stack.Push(item.clone());
            }

            addItemsForSell(stack);
        }

        /// <summary>
        /// add an item stack to the sellable items
        /// </summary>
        /// <param name="item"></param>
        void addItemsForSell(Stack<AbstractItem> item)
        {
            itemsForSell.Add(item);
        }

        /// <summary>
        /// interaction with the player
        /// </summary>
        public override void interact()
        {
            Interacting = true;
            shop = new Shop(itemsForSell);
            NPCHandler.shop = shop;
        }

        /// <summary>
        /// stops interaction with player
        /// </summary>
        public override void stopIteraction()
        {
            shop.closeShop();
            shop = null;
            Interacting = false;
        }
    }
}
