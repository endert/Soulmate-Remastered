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
            "HealPotion",
            "Sword",
            "FusionPotion"
        };

        private static AbstractItem evaluateLoadedItem(int index, List<String> parameter)
        {
            switch (index)
            {
                case 0:
                    return new TestItem();
                case 1:
                    return new HealPotion((HealPotion.PotionSize)Convert.ToInt32(parameter[0]));
                case 2:
                    return new Sword(Convert.ToSingle(parameter[0]), Convert.ToSingle(parameter[1]), Convert.ToSingle(parameter[2]), parameter[3]);
                case 3:
                    return new FusionPotion((FusionPotion.PotionSize)Convert.ToInt32(parameter[0]));
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
                    if (itemString.Split(AbstractItem.LineBreak)[1].Equals(loadableItems[i]))
                    {
                        AbstractItem loadedItem = null;
                        List<String> parameter = new List<String>();

                        if(itemString.Split(AbstractItem.LineBreak).Length > 4)
                            for (int j = 4; j < itemString.Split(AbstractItem.LineBreak).Length - 1; j++)
                            {
                                parameter.Add(itemString.Split(AbstractItem.LineBreak)[j]);
                            }

                        loadedItem = evaluateLoadedItem(i, parameter);
                        loadedItem.Position = new Vector2f(Convert.ToSingle(itemString.Split(AbstractItem.LineBreak)[2]),
                                                           Convert.ToSingle(itemString.Split(AbstractItem.LineBreak)[3]));

                        for (int j = 0; j < Convert.ToInt32(itemString.Split(AbstractItem.LineBreak)[itemString.Split(AbstractItem.LineBreak).Length - 1]); j++)
                        {
                            loadedStack.Push(loadedItem.Clone());
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

        public static Equipment loadEquip(String EquipmentString)
        {
            Equipment loadedEquip = null;
            try
            {
                for (int i = 0; i < loadableItems.Length; i++)
                {
                    if (EquipmentString.Split(AbstractItem.LineBreak)[1].Equals(loadableItems[i]))
                    {
                        List<String> parameter = new List<String>();

                        if (EquipmentString.Split(AbstractItem.LineBreak).Length > 4)
                            for (int j = 4; j < EquipmentString.Split(AbstractItem.LineBreak).Length - 1; j++)
                            {
                                parameter.Add(EquipmentString.Split(AbstractItem.LineBreak)[j]);
                            }

                        switch (i)
                        {
                            case 0:
                                loadedEquip = (Equipment)evaluateLoadedItem(i, parameter);
                                break;
                            case 1:
                                loadedEquip = (Equipment)evaluateLoadedItem(i, parameter);
                                break;
                            case 2:
                                loadedEquip = (Equipment)evaluateLoadedItem(i, parameter);
                                break;
                            default:
                                break;
                        }
                        loadedEquip.Position = new Vector2f(Convert.ToSingle(EquipmentString.Split(AbstractItem.LineBreak)[2]),
                                                           Convert.ToSingle(EquipmentString.Split(AbstractItem.LineBreak)[3]));
                        loadedEquip.Sprite.Position = loadedEquip.Position;
                        loadedEquip.setEquiped();
                        break;
                    }
                }
                return loadedEquip;
            }
            catch (Exception)
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
                aItem.Kill();
            }
            playerInventory.deleate();
            playerInventory = null;
            equipmentHandler.Deleate();
        }

        public void update(GameTime gameTime)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (!itemList[i].IsAlive)
                {
                    itemList.RemoveAt(i);
                    i--;
                    break;
                }
                
                if (itemList[i].OnMap && itemList[i].HitBox.DistanceTo(PlayerHandler.Player.HitBox) <= itemList[i].PickUpRange && !playerInventory.isFullWith(itemList[i]))
                {
                    itemList[i].PickUp();
                    itemList.RemoveAt(i);
                    i--;
                }
                
            }
            //playerInventory.update(gameTime);
        }

        static public void drawInventoryItems(RenderWindow window)
        {
            foreach (Stack<AbstractItem> itemStack in playerInventory.inventoryMatrix)
            {
                if (itemStack != null && itemStack.Count != 0 && itemStack.Peek() != null)
                {
                    itemStack.Peek().Draw(window);
                }
            }

            foreach (Equipment equipment in playerInventory.equipment)
            {
                if (equipment != null)
                {
                    equipment.Draw(window);
                }
            }
        }
    }
}
