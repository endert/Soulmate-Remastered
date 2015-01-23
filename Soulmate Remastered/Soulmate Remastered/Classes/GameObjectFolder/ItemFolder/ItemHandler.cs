using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.EquipmentFolder;
using SFML.Graphics;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.InGameMenuFolder;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder
{
    class ItemHandler
    {
        public static List<AbstractItem> itemList { get; set; }
        public static Inventory playerInventory { get; set; }
        public static EquipmentHandler equipmentHandler { get; set; }

        public ItemHandler()
        {
            itemList = new List<AbstractItem>();
            playerInventory = new Inventory();
            equipmentHandler = new EquipmentHandler();
        }

        static public void add(AbstractItem aItem)
        {
            itemList.Add(aItem);
            GameObjectHandler.add(aItem);
        }

        static public void add(List<AbstractItem> aItemList)
        {
            foreach (AbstractItem aItem in aItemList)
            {
                itemList.Add(aItem);
                GameObjectHandler.add(aItem);
            }
        }

        public static void deleateType(String _type)
        {

        }

        public static void deleate()
        {
            foreach (AbstractItem aItem in itemList)
            {
                aItem.kill();
            }
            playerInventory.deleate();
            equipmentHandler.deleate();
        }

        public void update(GameTime gameTime)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (!itemList[i].isAlive)
                {
                    itemList.RemoveAt(i);
                    i--;
                }
                
                if (!playerInventory.isFull() && itemList[i].hitBox.distanceTo(PlayerHandler.player.hitBox) <= itemList[i].pickUpRange && itemList[i].onMap)
                {
                    itemList[i].pickUp(gameTime);
                    itemList.RemoveAt(i);
                    i--;
                }
                
            }
            playerInventory.update(gameTime);
        }

        static public void updateInventoryMatrix(GameTime gameTime)
        {
            foreach (AbstractItem item in playerInventory.inventoryMatrix)
            {
                if (item != null)
                {
                    item.sprite.Position = item.position;
                    item.setVisible(true);
                }
            }
        }

        static public void drawInventoryItems(RenderWindow window)
        {
            foreach (AbstractItem item in playerInventory.inventoryMatrix)
            {
                if (item != null)
                {
                    item.draw(window);
                }
            }
        }
    }
}
