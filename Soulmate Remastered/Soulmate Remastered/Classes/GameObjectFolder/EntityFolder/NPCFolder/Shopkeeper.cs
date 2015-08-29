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
        public override string Type { get { return base.Type + ".Shopkeeper"; } }
        /// <summary>
        /// path to the dialoge File
        /// </summary>
        protected override string DialogePath { get { return base.DialogePath; } }
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

            TextureList.Add(new Texture("Pictures/Entities/NPC/PetStorageGuy/PetStorageGuyFrontTest.png"));
            TextureList.Add(new Texture("Pictures/Entities/NPC/PetStorageGuy/PetStorageGuyBackTest.png"));
            TextureList.Add(new Texture("Pictures/Entities/NPC/PetStorageGuy/PetStorageGuyRightTest.png"));
            TextureList.Add(new Texture("Pictures/Entities/NPC/PetStorageGuy/PetStorageGuyLeftTest.png"));

            IsAlive = true;
            IsVisible = true;
            Sprite = new Sprite(TextureList[0]);
            itemsForSell = new List<Stack<AbstractItem>>();
            Position = _position;
            Sprite.Position = Position;
            HitBox = new HitBox(Position, Sprite.Texture.Size.X, Sprite.Texture.Size.Y);

            //******************************************************************************************************
            //Insitialize Entity data*******************************************************************************

            FacingDirection = Vector2.FRONT;

            //******************************************************************************************************
            //Insitialize NPC data**********************************************************************************

            Interacting = false;
            NPCHandler.Shop_ = shop;
            NPCHandler.Add_(this);
            AddItemForSell(new Sword(10, 10, 10), 10);

            //******************************************************************************************************
        }

        /// <summary>
        /// xhröü //written by alex/chaz
        /// </summary>
        public void AddItemForSell(AbstractItem item, int count)
        {
            Stack<AbstractItem> stack = new Stack<AbstractItem>();

            for (int i = 0; i < count; i++)
            {
                stack.Push(item.Clone());
            }

            AddItemsForSell(stack);
        }

        /// <summary>
        /// add an item stack to the sellable items
        /// </summary>
        /// <param name="item"></param>
        void AddItemsForSell(Stack<AbstractItem> item)
        {
            itemsForSell.Add(item);
        }

        /// <summary>
        /// interaction with the player
        /// </summary>
        public override void Interact()
        {
            Interacting = true;
            shop = new Shop(itemsForSell);
            NPCHandler.Shop_ = shop;
        }

        /// <summary>
        /// stops interaction with player
        /// </summary>
        public override void StopIteraction()
        {
            shop.CloseShop();
            shop = null;
            Interacting = false;
        }
    }
}
