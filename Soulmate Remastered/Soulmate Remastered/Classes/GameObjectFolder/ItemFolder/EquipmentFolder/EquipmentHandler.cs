using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.EquipmentFolder
{
    /// <summary>
    /// handles the equipment
    /// </summary>
    class EquipmentHandler
    {
        /// <summary>
        /// every piece of equipment
        /// </summary>
        public static List<Equipment> EquipmentList { get; set; }

        /// <summary>
        /// initialize the equipment list
        /// </summary>
        public EquipmentHandler()
        {
            EquipmentList = new List<Equipment>();
        }

        /// <summary>
        /// adds the equip to the list
        /// </summary>
        /// <param name="equip"></param>
        public static void Add(Equipment equip)
        {
            EquipmentList.Add(equip);
            ItemHandler.add(equip);
        }

        /// <summary>
        /// clears the list => deleates all equipment
        /// </summary>
        public void Deleate()
        {
            EquipmentList.Clear();
        }

        /// <summary>
        /// updates the equipment list
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < EquipmentList.Count; i++)
            {
                if (EquipmentList[i].IsAlive)
                {
                    EquipmentList.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
