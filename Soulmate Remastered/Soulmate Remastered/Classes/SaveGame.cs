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
            writer.WriteLine(PlayerHandler.Player.ToStringForSave());
            if (PetHandler.Pet == null)
                writer.WriteLine("null");
            else
                writer.WriteLine(PetHandler.Pet.ToStringForSave());
            writer.WriteLine(GameObjectHandler.lvl);
            writer.WriteLine(ItemHandler.playerInventory.ToStringForSave());

            writer.Flush();
            writer.Close();
            //File.Encrypt(savePath);
        }

        public static void loadGame()
        {
            if (File.Exists(loadPath))
            {
                StreamReader reader = new StreamReader(loadPath);


                if (PlayerHandler.Player != null)
                {
                    Console.WriteLine("loading: Player");
                    PlayerHandler.Player.Load(reader.ReadLine());
                }
                else
                    reader.ReadLine();

                if (PetHandler.Pet != null)
                {
                    Console.WriteLine("loading: Pet");
                    PetHandler.Load(reader.ReadLine());
                }
                else
                    reader.ReadLine();

                Console.WriteLine("loading: lvlCount");
                GameObjectHandler.lvl = Convert.ToInt32(reader.ReadLine());

                if (ItemHandler.playerInventory != null)
                {
                    Console.WriteLine("loading: Inventar");
                    ItemHandler.playerInventory.Load(reader.ReadLine());
                }
                else
                    reader.ReadLine();

                reader.Close();
            }
            else
                Console.WriteLine("File don't exist");
        }

        public static void loadMapChange()
        {
            StreamReader reader = new StreamReader(loadPath);

            PlayerHandler.Player.LoadMapChange(reader.ReadLine());
            PetHandler.Load(reader.ReadLine());
            reader.ReadLine();
            ItemHandler.playerInventory.Load(reader.ReadLine());

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
