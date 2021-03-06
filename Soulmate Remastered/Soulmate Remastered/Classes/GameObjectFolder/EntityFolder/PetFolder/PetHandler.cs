﻿using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.GameStatesFolder;
using System;
using System.Collections.Generic;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PetFolder
{
    /// <summary>
    /// handles the pet
    /// </summary>
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

            bool isLoading = (bool)typeof(AbstractGamePlay).GetField("loading").GetValue(null);

            if (!isLoading)
            {
                Pet = new PetWolf(PlayerHandler.Player);
                EntityHandler.Add(Pet);
                PetList.Add(Pet);
            }
        }

        /// <summary>
        /// loads the pet from a String
        /// </summary>
        /// <param name="petString"></param>
        public static void Load(string petString)
        {
            if (petString.Equals("null"))
            {
                Pet = null;
                return;
            }

            //load the pet with a reflection ^^
            Pet = (AbstractPet)Activator.CreateInstance(null, petString.Split(GameObject.LineBreak)[0]).Unwrap();

            EntityHandler.Add(Pet);
            PetList.Add(Pet);

            Pet.Load(petString);
        }

        /// <summary>
        /// adds a pet to the petList
        /// </summary>
        /// <param name="pet"></param>
        public static void Add_(AbstractPet pet)
        {
            PetList.Add(pet);
            EntityHandler.Add(pet);
        }

        /// <summary>
        /// set the players pet null
        /// </summary>
        static public void Deleate()
        {
            EntityHandler.DeleateType(Pet.GetType());
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
