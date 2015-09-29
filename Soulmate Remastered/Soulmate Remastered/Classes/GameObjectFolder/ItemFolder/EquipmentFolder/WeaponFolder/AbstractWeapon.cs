namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.EquipmentFolder
{
    /// <summary>
    /// base class for all weapons
    /// </summary>
    abstract class AbstractWeapon : Equipment
    {
        /// <summary>
        /// the ID = 132x
        /// </summary>
        public override float ID { get { return base.ID * 10 + 2; } }
        /// <summary>
        /// determines the index of this item in the equipment slot
        /// </summary>
        public override EType EquipmentType { get { return EType.Weapon; } }
        /// <summary>
        /// discription of this weapon
        /// </summary>
        protected string discription = "";
        /// <summary>
        /// discription of this item
        /// </summary>
        public override string ItemDiscription
        {
            get
            {
                string itemDiscription = "";

                itemDiscription += Name + "\n";
                if (!discription.Equals(""))
                    itemDiscription += '"'.ToString() + discription + '"'.ToString() + "\n";
                itemDiscription += "AttBonus: " + AttBonus + "      ";
                itemDiscription += "DefBonus: " + DefBonus + "\n";
                itemDiscription += "HpBonus: " + HpBonus + "      ";

                return itemDiscription;
            }
        }
    }
}
