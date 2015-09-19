using System;
using System.Collections.Generic;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.EquipmentFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder
{
    class ItemHandler
    {
        /// <summary>
        /// all items, that are not in an inventory
        /// </summary>
        public static List<AbstractItem> ItemList { get; set; }
        /// <summary>
        /// the inventory of the player
        /// </summary>
        public static PlayerInventory ṔlayerInventory { get; set; }

        /// <summary>
        /// initializes the Itemlist and the PlayerInventory, also calls EquipmentHandler.Initialize()
        /// </summary>
        public static void Initialize()
        {
            ItemList = new List<AbstractItem>();
            ṔlayerInventory = new PlayerInventory();
            EquipmentHandler.Initialize();
        }

        /// <summary>
        /// Adds the given item to the list and calls all other needed add methods
        /// </summary>
        /// <param name="aItem"></param>
        public static void Add(AbstractItem aItem)
        {
            ItemList.Add(aItem);
            GameObjectHandler.Add(aItem);
        }

        /// <summary>
        /// deletes all items, wich match the given type
        /// </summary>
        /// <param name="_type"></param>
        public static void DeleateType(Type _type)
        {
            for(int i = 0; i<ItemList.Count; ++i)
            {
                if(ItemList[i].GetType().Equals(_type))
                {
                    ItemList.RemoveAt(i);
                    --i;
                }
            }
        }

        /// <summary>
        /// deletes all items and calls EquipmentHandler.Deleate()
        /// </summary>
        public static void Deleate()
        {
            ItemList = null;
            //playerInventory.Deleate();
            ṔlayerInventory = null;
            EquipmentHandler.Deleate();
        }

        /// <summary>
        /// Updates the list and updates all items within the list
        /// </summary>
        /// <param name="gameTime"></param>
        public static void Update(GameTime gameTime)
        {
            for (int i = 0; i < ItemList.Count; i++)
            {
                if (!ItemList[i].IsAlive)
                {
                    ItemList.RemoveAt(i);
                    i--;
                    continue;
                }

                if (ItemList[i].OnMap && ItemList[i].HitBox.DistanceTo(PlayerHandler.Player.HitBox) <= ItemList[i].PickUpRange)
                {
                    ṔlayerInventory.PickUp(ItemList[i]);
                    ItemList.RemoveAt(i);
                    i--;
                }

            }
            ṔlayerInventory.Update(gameTime);
        }
    }
}
