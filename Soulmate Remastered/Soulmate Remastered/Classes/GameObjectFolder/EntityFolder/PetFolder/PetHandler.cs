using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.NPCFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PetFolder
{
    class PetHandler
    {
        /// <summary>
        /// players pet
        /// </summary>
        public static AbstractPet Pet { get; set; }
        /// <summary>
        /// List of all inizialazed Pets
        /// </summary>
        public static List<AbstractPet> PetList { get; set; }

        /// <summary>
        /// initialize the players pet
        /// </summary>
        public PetHandler()
        {
            PetList = new List<AbstractPet>();

            Pet = new PetWolf(PlayerHandler.player);
            EntityHandler.add(Pet);
            PetList.Add(Pet);
        }

        /// <summary>
        /// loads the pet from a String
        /// </summary>
        /// <param name="petString"></param>
        public static void Load(String petString)
        {
            if (petString.Equals("null"))
            {
                Pet = null;
                return;
            }

            //if loaded pet differs from the current one
            if (!petString.Split(AbstractPet.LineBreak)[4].Equals(Pet.Type.Split('.')[Pet.Type.Split('.').Length-1]))
            {
                Deleate();
                // ToDo: initialize new Pet
                // pet = ...
            }

            Pet.Load(petString);
        }

        /// <summary>
        /// adds a pet to the petList
        /// </summary>
        /// <param name="pet"></param>
        public static void Add_(AbstractPet pet)
        {
            PetList.Add(pet);
            EntityHandler.add(pet);
        }

        /// <summary>
        /// set the players pet null
        /// </summary>
        static public void Deleate()
        {
            EntityHandler.deleateType(Pet.Type);
            Pet = null;
        }

        /// <summary>
        /// currently not needed because there is nothing to update
        /// </summary>
        public void Update()
        {
            
        }
    }
}
