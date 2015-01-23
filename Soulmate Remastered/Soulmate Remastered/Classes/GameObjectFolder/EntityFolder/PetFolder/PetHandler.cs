using SFML.Window;
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
        }

        public static void add(AbstractPet pet)
        {
            petList.Add(pet);
            EntityHandler.add(pet);
        }


        static public void deleate()
        {
            pet = null;

            EntityHandler.deleateType("Pet");
        }
    }
}
