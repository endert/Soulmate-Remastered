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
        public static AbstractPet pet { get; set; }
        public static List<AbstractPet> petList { get; set; }

        public PetHandler()
        {
            petList = new List<AbstractPet>();

            pet = new PetWolf(PlayerHandler.player);
            EntityHandler.add(pet);
            petList.Add(pet);
        }

        public static void load(String petString)
        {
            if (petString.Equals("null"))
            {
                EntityHandler.deleateType(pet.type);
                pet = null;
                return;
            }

            if (!petString.Split(AbstractPet.lineBreak)[4].Equals(pet.type.Split('.')[pet.type.Split('.').Length-1]))
            {
                deleate();
                // pet = ...
            }

            pet.load(petString);
        }

        public static void add(AbstractPet pet)
        {
            petList.Add(pet);
            EntityHandler.add(pet);
        }


        static public void deleate()
        {
            EntityHandler.deleateType(pet.type);
            pet = null;
        }

        public void update()
        {
            
        }
    }
}
