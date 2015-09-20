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
        /// <summary>
        /// the path to the save file
        /// </summary>
        public static string SavePath { get; set; }
        /// <summary>
        /// the path to the file, that shall be loaded
        /// </summary>
        public static string LoadPath { get; set; }

        /// <summary>
        /// saves the game in a text file
        /// </summary>
        public static void SaveTheGame()
        {
            StreamWriter writer = new StreamWriter(SavePath);

            writer.WriteLine(PlayerHandler.Player.ToStringForSave());

            if (PetHandler.Pet == null)
                writer.WriteLine("null");
            else
                writer.WriteLine(PetHandler.Pet.ToStringForSave());

            writer.WriteLine(GameObjectHandler.Lvl);

            writer.WriteLine(ItemHandler.ṔlayerInventory.ToStringForSave());

            writer.Flush();
            writer.Close();
            //File.Encrypt(savePath);
        }

        /// <summary>
        /// loads the game from a file, defined in the loadpath
        /// </summary>
        public static void LoadGame()
        {
            if (File.Exists(LoadPath))
            {
                StreamReader reader = new StreamReader(LoadPath);


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
                GameObjectHandler.Lvl = Convert.ToInt32(reader.ReadLine());

                if (ItemHandler.ṔlayerInventory != null)
                {
                    Console.WriteLine("loading: Inventar");
                    ItemHandler.ṔlayerInventory.Load(reader.ReadLine());
                }
                else
                    reader.ReadLine();

                reader.Close();
            }
            else
                Console.WriteLine("File don't exist");
        }

        /// <summary>
        /// loads the temp file for the map change
        /// </summary>
        public static void LoadMapChange()
        {
            StreamReader reader = new StreamReader(LoadPath);

            PlayerHandler.Player.LoadMapChange(reader.ReadLine());
            PetHandler.Load(reader.ReadLine());
            reader.ReadLine();
            ItemHandler.ṔlayerInventory.Load(reader.ReadLine());

            reader.Close();
        }
    }
}
