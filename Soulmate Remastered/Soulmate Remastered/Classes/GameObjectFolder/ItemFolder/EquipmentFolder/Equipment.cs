using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.ItemFolder;
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
        /// the type of this instance
        /// </summary>
        public override String Type { get { return base.Type + ".Equipment"; } }
        /// <summary>
        /// the ID = 13x
        /// </summary>
        public override float ID { get { return base.ID * 10 + 3; } }
        /// <summary>
        /// bool if this item can be stacked
        /// </summary>
        public override bool Stackable { get { return false; } }
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
        /// <summary>
        /// bool if it is equiped or not
        /// <para>default false</para>
        /// </summary>
        protected bool equiped = false;

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
        /// use this item -> equip equipment
        /// </summary>
        public override void Use()
        {
            if (equiped)
                unequip();
            else
                equip();
        }

        /// <summary>
        /// equiped = true
        /// </summary>
        public void setEquiped()
        {
            equiped = true;
        }

        /// <summary>
        /// equips this
        /// </summary>
        public void equip()
        {
            if (ItemHandler.playerInventory.equipment[(int)((ID) / 10) % 10] != null)
            {
                ItemHandler.playerInventory.equipment[(int)((ID) / 10) % 10].unequip();
            }
            ItemHandler.playerInventory.equipment[(int)((ID) / 10) % 10] = this;
            ItemHandler.playerInventory.inventoryMatrix[(int)InventoryMatrixPosition.Y, (int)InventoryMatrixPosition.X] = null;
            Position = Inventory.equipmentPosition[(int)((ID) / 10) % 10];
            Sprite.Position = Position;
            equiped = true;
        }

        /// <summary>
        /// unequips this
        /// </summary>
        public void unequip()
        {
            if (!ItemHandler.playerInventory.isFullWith(this))
            {
                bool _break = false;
                for (int i = 0; i < ItemHandler.playerInventory.inventoryMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < ItemHandler.playerInventory.inventoryMatrix.GetLength(1); j++)
                    {
                        if (ItemHandler.playerInventory.inventoryMatrix[i,j] == null)
                        {
                            ItemHandler.playerInventory.inventoryMatrix[i, j] = new Stack<AbstractItem>();
                            ItemHandler.playerInventory.inventoryMatrix[i, j].Push(this);
                            Position = new Vector2f(ItemHandler.playerInventory.inventoryMatrixPosition.X + j * ItemHandler.playerInventory.FIELDSIZE + 1,
                                                    ItemHandler.playerInventory.inventoryMatrixPosition.Y + i * ItemHandler.playerInventory.FIELDSIZE + 1);

                            _break = true;
                            break;
                        }
                    }
                    if (_break)
                    {
                        break;
                    }
                }
                ItemHandler.playerInventory.equipment[(int)(ID) % 10] = null;
                equiped = false;
            }
            else
            {
                Console.WriteLine("Fuck you!! Inventar voll!!");
            }
        }
    }
}
