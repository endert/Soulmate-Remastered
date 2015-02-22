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
    abstract class Equipment : AbstractItem
    {
        public override String type { get { return base.type + ".Equipment"; } }
        public override float ID { get { return base.ID * 10 + 1; } }
        public override bool stackable { get { return false; } }
        public override float sellPrize { get { return (attBonus + bonusDef + bonusHp) * 10 / ((ID % 10) + 1); } }

        protected float attBonus;
            public float bonusAtt { get { return attBonus; } }
        protected float defBonus;
            public float bonusDef { get { return defBonus; } }
        protected float hpBonus;
            public float bonusHp { get { return hpBonus; } }
        protected bool equiped = false;

        public override string toStringForSave()
        {
            String save = base.toStringForSave();

            save += bonusAtt + lineBreak.ToString();
            save += bonusDef + lineBreak.ToString();
            save += bonusHp + lineBreak.ToString();

            return save;
        }

        public override void use()
        {
            if (equiped)
                unequip();
            else
                equip();
        }

        public void equip()
        {
            if (ItemHandler.playerInventory.equipment[(int)(ID) % 10] != null)
            {
                ItemHandler.playerInventory.equipment[(int)(ID) % 10].unequip();
            }
            ItemHandler.playerInventory.equipment[(int)(ID) % 10] = this;
            ItemHandler.playerInventory.inventoryMatrix[inventoryMatrixPositionY, inventoryMatrixPositionX] = null;
            position = Inventory.equipmentPosition[(int)(ID) % 10];
            sprite.Position = position;
            equiped = true;
        }

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
                            position = new Vector2f(ItemHandler.playerInventory.inventoryMatrixPosition.X + j * ItemHandler.playerInventory.FIELDSIZE + 1,
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
