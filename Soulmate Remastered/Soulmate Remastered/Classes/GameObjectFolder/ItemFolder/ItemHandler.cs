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
        public static PlayerInventory playerInventory { get; set; }
        public static EquipmentHandler equipmentHandler { get; set; }

        public ItemHandler()
        {
            itemList = new List<AbstractItem>();
            playerInventory = new PlayerInventory();
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
                aItem.Kill();
            }
            //playerInventory.Deleate();
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

                if (itemList[i].OnMap && itemList[i].HitBox.DistanceTo(PlayerHandler.Player.HitBox) <= itemList[i].PickUpRange)
                {
                    playerInventory.PickUp(itemList[i]);
                    itemList.RemoveAt(i);
                    i--;
                }

            }
            playerInventory.Update(gameTime);
        }
    }
}
