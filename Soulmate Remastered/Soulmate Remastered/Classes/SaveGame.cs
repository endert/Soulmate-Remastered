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
        public static String savePath { get; set; }
        public static String loadPath { get; set; }

        public static void saveGame()
        {
            StreamWriter writer = new StreamWriter(savePath);
            writer.WriteLine(PlayerHandler.player.toStringForSave());
            if (PetHandler.pet == null)
                writer.WriteLine("null");
            else
                writer.WriteLine(PetHandler.pet.toStringForSave());
            writer.WriteLine(GameObjectHandler.lvl);
            writer.WriteLine(ItemHandler.playerInventory.toStringForSave());

            writer.Flush();
            writer.Close();
            //File.Encrypt(savePath);
        }

        public static void loadGame()
        {
            if (File.Exists(loadPath))
            {
                StreamReader reader = new StreamReader(loadPath);

                if (PlayerHandler.player != null)
                {
                    PlayerHandler.player.load(reader.ReadLine());
                }
                else
                    reader.ReadLine();

                if (PetHandler.pet != null)
                {
                    PetHandler.load(reader.ReadLine());
                }
                else
                    reader.ReadLine();

                GameObjectHandler.lvl = Convert.ToInt32(reader.ReadLine());

                if (ItemHandler.playerInventory != null)
                {
                    ItemHandler.playerInventory.load(reader.ReadLine());
                }
                else
                    reader.ReadLine();

                reader.Close();
            }
        }

        public static void loadMapChange()
        {
            StreamReader reader = new StreamReader(loadPath);

            PlayerHandler.player.loadMapChange(reader.ReadLine());
            PetHandler.load(reader.ReadLine());
            reader.ReadLine();
            ItemHandler.playerInventory.load(reader.ReadLine());

            reader.Close();
        }
    }
     /* player + position
     * pet + position
     * lvl
     * Inventory
     * //questlog
     */
}
