using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.EquipmentFolder
{
    abstract class AbstractWeapon : Equipment
    {
        public override string type { get { return base.type + ".Weapon"; } }
        public override float ID { get { return base.ID * 10 + 2; } }
        protected String discription = "";
        public override string ItemDiscription
        {
            get
            {
                String itemDiscription = "";

                itemDiscription += name + "\n";
                if (!discription.Equals(""))
                    itemDiscription += '"'.ToString() + discription + '"'.ToString() + "\n";
                itemDiscription += "AttBonus: " + attBonus + "      ";
                itemDiscription += "DefBonus: " + defBonus + "\n";
                itemDiscription += "HpBonus: " + hpBonus + "      ";

                return itemDiscription;
            }
        }
    }
}
