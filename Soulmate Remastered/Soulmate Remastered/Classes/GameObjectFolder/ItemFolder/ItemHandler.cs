using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.EquipmentFolder;
using SFML.Graphics;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.InGameMenuFolder;
using Soulmate_Remastered.Classes.ItemFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PetFolder;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.NormalItemFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.PotionFolder;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder
{
    class ItemHandler
    {
        public static List<AbstractItem> itemList { get; set; }
        public static Inventory playerInventory { get; set; }
        public static EquipmentHandler equipmentHandler { get; set; }

        static String[] loadableItems = new String[]
        {
            "Pete",
            "HealPotion"
        };

        private static AbstractItem evaluateLoadedItem(int index, int size)
        {
            switch (index)
            {
                case 0:
                    return new TestItem();
                case 1:
                    return new HealPotion((HealPotion.healPotionSize)size);
                default:
                    return null;
            }
        } 

        public ItemHandler()
        {
            itemList = new List<AbstractItem>();
            playerInventory = new Inventory();
            equipmentHandler = new EquipmentHandler();
        }

        public static Stack<AbstractItem> load(String itemString)
        {
            Stack<AbstractItem> loadedStack = new Stack<AbstractItem>();
            try
            {
                for (int i = 0; i < loadableItems.Length; i++)
                {
                    if (itemString.Split(AbstractItem.lineBreak)[1].Equals(loadableItems[i]))
                    {
                        AbstractItem loadedItem = null;
                        switch (i)
                        {
                            case 0:
                                loadedItem = evaluateLoadedItem(i, 0);
                                break;
                            case 1:
                                loadedItem = evaluateLoadedItem(i, Convert.ToInt32(itemString.Split(AbstractItem.lineBreak)[4]));
                                break;
                            default:
                                break;
                        }
                        loadedItem.position = new Vector2f(Convert.ToSingle(itemString.Split(AbstractItem.lineBreak)[2]),
                                                           Convert.ToSingle(itemString.Split(AbstractItem.lineBreak)[3]));

                        for (int j = 0; j < Convert.ToInt32(itemString.Split(AbstractItem.lineBreak)[itemString.Split(AbstractItem.lineBreak).Length - 1]); j++)
                        {
                            loadedStack.Push(loadedItem);
                        }

                        break;
                    }
                }
                return loadedStack;
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
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
                    break;
                }
                
                if (itemList[i].onMap && itemList[i].hitBox.distanceTo(PlayerHandler.player.hitBox) <= itemList[i].pickUpRange && !playerInventory.isFullWith(itemList[i]))
                {
                    itemList[i].pickUp(gameTime);
                    itemList.RemoveAt(i);
                    i--;
                }
                
            }
            playerInventory.update(gameTime);
        }

        static public void drawInventoryItems(RenderWindow window)
        {
            foreach (Stack<AbstractItem> itemStack in playerInventory.inventoryMatrix)
            {
                if (itemStack != null && itemStack.Count != 0 && itemStack.Peek() != null)
                {
                    itemStack.Peek().draw(window);
                }
            }
        }
    }
}
