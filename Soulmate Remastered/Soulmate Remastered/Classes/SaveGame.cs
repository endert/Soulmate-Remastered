using SFML.Graphics;
using Soulmate_Remastered.Classes.GameObjectFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PetFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using Soulmate_Remastered.Classes.HUDFolder;
using Soulmate_Remastered.Classes.InGameMenuFolder;
using Soulmate_Remastered.Classes.MapFolder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes
{
    class SaveGame
    {
        public static void saveGame(String path)
        {
            StreamWriter writer = new StreamWriter(path);
            writer.WriteLine(PlayerHandler.player.toStringForSave());
            writer.WriteLine(PetHandler.pet.toStringForSave());
            writer.WriteLine(GameObjectHandler.lvl);
            writer.WriteLine(ItemHandler.playerInventory.toStringForSave());

            writer.Flush();
            writer.Close();
        }

        public static void loadGame(String path)
        {

        }
    }
     /* player + position
     * pet + position
     * lvl
     * Inventory
     * //questlog
     */
}
