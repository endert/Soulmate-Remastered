using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.TreasureChestFolder
{
    class TreasureChestHandler
    {
        public static List<AbstractTreasureChest> treasureChestList { get; set; }

        public TreasureChestHandler()
        {
            treasureChestList = new List<AbstractTreasureChest>();
             if (GameObjectHandler.lvl == 0)
             {
                 new TreasureChest(new Vector2f(1000, 500));
             }
        }

        public static void add(AbstractTreasureChest treasureChest)
        {
            treasureChestList.Add(treasureChest);
            EntityHandler.add(treasureChest);
        }

        static public void deleate()
        {
            treasureChestList.Clear();
        }

        public void update(GameTime gameTime)
        {
            for (int i = 0; i < treasureChestList.Count; i++)
            {
                treasureChestList[i].Update(gameTime);
            }
        }
    }
}
