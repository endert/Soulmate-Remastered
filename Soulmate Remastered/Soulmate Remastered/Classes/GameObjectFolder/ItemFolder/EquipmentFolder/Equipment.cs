using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.EquipmentFolder
{
    /// <summary>
    /// base class for equipment
    /// </summary>
    abstract class Equipment : AbstractItem
    {
        /// <summary>
        /// the ID = 13x
        /// </summary>
        public override float ID { get { return base.ID * 10 + 3; } }
        /// <summary>
        /// the amount of gold the player gets by selling this item
        /// </summary>
        public override float SellPrize { get { return (AttBonus + DefBonus + HpBonus) * 10 / ((ID % 10) + 1); } }
        /// <summary>
        /// the attack bonus granted by this equipment
        /// </summary>
        public float AttBonus { get; protected set; }
        /// <summary>
        /// the def bonus granted by this equipment
        /// </summary>
        public float DefBonus { get; protected set; }
        /// <summary>
        /// the hp bonus granted by this equipment
        /// </summary>
        public float HpBonus { get; protected set; }

        public override int MaxStackCount { get { return 1; } }

        /// <summary>
        /// enum for the types of equip
        /// </summary>
        public enum EType
        {
            None = -1,
            Helmet,
            Chest,
            Weapon,
            Arms,
            Legs,
            Shoe,

            Count
        }

        /// <summary>
        /// determines the index of this item in the equipment slot
        /// </summary>
        public virtual EType EquipmentType { get { return EType.None; } }

        /// <summary>
        /// creates a string out of the attributes of this instance
        /// <para>only needed for saving</para>
        /// </summary>
        /// <returns></returns>
        public override string ToStringForSave()
        {
            String save = base.ToStringForSave();

            save += AttBonus + LineBreak.ToString();
            save += DefBonus + LineBreak.ToString();
            save += HpBonus + LineBreak.ToString();
            save += Name + LineBreak.ToString();

            return save;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        protected override void Use(object sender, UseEventArgs eventArgs)
        {
            PlayerInventory.OnEquip(sender, eventArgs);
        }
    }
}
