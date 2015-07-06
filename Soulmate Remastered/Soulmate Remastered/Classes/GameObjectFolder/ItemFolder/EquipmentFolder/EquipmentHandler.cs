using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.EquipmentFolder
{
    class EquipmentHandler
    {
        public static List<Equipment> equipmentList { get; set; }

        public EquipmentHandler()
        {
            equipmentList = new List<Equipment>();
        }

        public static void add(Equipment equip)
        {
            equipmentList.Add(equip);
            ItemHandler.add(equip);
        }

        public static void add(List<Equipment> equipment)
        {
            foreach (Equipment equip in equipment)
            {
                equipmentList.Add(equip);
                ItemHandler.add(equip);
            }
        }

        public void deleate()
        {
            for (int i = 0; i < equipmentList.Count; i++)
            {
                equipmentList.RemoveAt(i);
                i--;
            }
        }

        public void update(GameTime gameTime)
        {
            for (int i = 0; i < equipmentList.Count; i++)
            {
                if (equipmentList[i].IsAlive)
                {
                    equipmentList.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
