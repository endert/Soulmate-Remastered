using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.EquipmentFolder;
using Soulmate_Remastered.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.TreasureChestFolder
{
    /// <summary>
    /// handles the treasure chests
    /// </summary>
    class TreasureChestHandler
    {
        /// <summary>
        /// all treasure chests
        /// </summary>
        public static List<AbstractTreasureChest> treasureChestList { get; set; }

        public TreasureChestHandler()
        {
            treasureChestList = new List<AbstractTreasureChest>();

            switch (GameObjectHandler.lvl)
            {
                case 0:
                    new TreasureChest(new Vector2(1000, 500), new Sword(100, 100, 100, "Super Awesome Mega Sword of DOOOOOOM!!!!"));
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// adds the treasure chest to the list
        /// </summary>
        /// <param name="treasureChest"></param>
        public static void Add(AbstractTreasureChest treasureChest)
        {
            treasureChestList.Add(treasureChest);
            EntityHandler.Add(treasureChest);
        }

        /// <summary>
        /// clears the list -> deletes all treasure chests
        /// </summary>
        static public void Deleate()
        {
            treasureChestList.Clear();
        }

        /// <summary>
        /// updates all treasure chests
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < treasureChestList.Count; i++)
            {
                treasureChestList[i].Update(gameTime);
            }
        }
    }
}
