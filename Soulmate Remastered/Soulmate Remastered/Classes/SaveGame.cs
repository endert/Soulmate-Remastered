using Soulmate_Remastered.Classes.GameObjectFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PetFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using System;
using System.IO;

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
            string[] saveSplit = SavePath.Split('/');
            string directory = "";

            for(int i = 0; i < saveSplit.Length -1; ++i)
            {
                directory += saveSplit[i];
                if (i + 1 < saveSplit.Length - 1)
                    directory += "/";
            }

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (StreamWriter writer = new StreamWriter(SavePath))
            {

                writer.WriteLine(GameObjectHandler.Lvl);

                writer.WriteLine(PlayerHandler.Player.ToStringForSave());

                if (PetHandler.Pet == null)
                    writer.WriteLine("null");
                else
                    writer.WriteLine(PetHandler.Pet.ToStringForSave());

                writer.WriteLine(ItemHandler.ṔlayerInventory.ToStringForSave());
            }

            Encrypt(SavePath);
        }

        public static void LoadMapLvl()
        {
            if (File.Exists(LoadPath))
            {
                Decrypt(LoadPath);

                using (StreamReader reader = new StreamReader(LoadPath))
                { 
                    Console.WriteLine("loading: lvl");
                    GameObjectHandler.Lvl = Convert.ToInt32(reader.ReadLine());
                }

                Encrypt(LoadPath);
            }
            else
            {
                Console.WriteLine("File Don't exist");
                GameObjectHandler.Lvl = 0;
            }
        }

        /// <summary>
        /// loads the game from a file, defined in the loadpath
        /// </summary>
        public static void LoadGame()
        {
            if (File.Exists(LoadPath))
            {
                Decrypt(LoadPath);

                using (StreamReader reader = new StreamReader(LoadPath))
                {

                    //skip the first line (lvl is loaded already)
                    reader.ReadLine();


                    Console.WriteLine("loading: Player");
                    PlayerHandler.Player.Load(reader.ReadLine());


                    Console.WriteLine("loading: Pet");
                    PetHandler.Load(reader.ReadLine());


                    if (ItemHandler.ṔlayerInventory != null)
                    {
                        Console.WriteLine("loading: Inventar");
                        ItemHandler.ṔlayerInventory.Load(reader.ReadLine());
                    }
                    else
                        reader.ReadLine();
                }

                Encrypt(LoadPath);
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

        static void Encrypt(string path)
        {
            string s = path;

            int encrytvalue = 0;
            foreach (char c in s)
                encrytvalue += c;

            string[] FileContext = File.ReadAllLines(path);

            for(int i = 0; i< FileContext.Length; ++i)
            {
                string res = "";
                char[] c = FileContext[i].ToCharArray();

                for(int j = 0; j<c.Length; ++j)
                {
                    c[j] = (char)(c[j] + encrytvalue);
                    res += c[j].ToString();
                }

                FileContext[i] = res;
            }

            File.WriteAllLines(path, FileContext);
        }

        static void Decrypt(string path)
        {
            string s = path;

            int encrytvalue = 0;
            foreach (char c in s)
                encrytvalue += c;

            string[] FileContext = File.ReadAllLines(path);

            for (int i = 0; i < FileContext.Length; ++i)
            {
                string res = "";
                char[] c = FileContext[i].ToCharArray();

                for (int j = 0; j < c.Length; ++j)
                {
                    c[j] = (char)(c[j] - encrytvalue);
                    res += c[j].ToString();
                }

                FileContext[i] = res;
            }

            File.WriteAllLines(path, FileContext);
        }
    }
}
