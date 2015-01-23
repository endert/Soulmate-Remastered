using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.EquipmentFolder
{
    abstract class Equipment : AbstractItem
    {
        public override String type { get { return base.type + ".Equipment"; } }
        
        protected float attBonus;
            public float bonusAtt { get { return attBonus; } }
        protected float defBonus;
            public float bonusDef { get { return defBonus; } }
        protected float hpBonus;
            public float bonusHp { get { return hpBonus; } }

        public override void use()
        {
            equip();
        }

        public void equip()
        {

        }
    }
}
