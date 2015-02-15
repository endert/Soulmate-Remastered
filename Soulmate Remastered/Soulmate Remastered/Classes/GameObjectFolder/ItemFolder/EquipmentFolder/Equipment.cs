using SFML.Window;
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
            Equipment swapedItem = ItemHandler.playerInventory.equipment[(int)(ID) % 10];
            ItemHandler.playerInventory.equipment[(int)(ID) % 10] = this;
            position = new Vector2f(447 + 15, 161 + 114);
            sprite.Position = position;
            if (swapedItem != null)
            {
                ItemHandler.playerInventory.inventoryMatrix[inventoryMatrixPositionY, inventoryMatrixPositionX].Pop();
                ItemHandler.playerInventory.inventoryMatrix[inventoryMatrixPositionY, inventoryMatrixPositionX].Push(swapedItem);
            }
            else
            {
                ItemHandler.playerInventory.inventoryMatrix[inventoryMatrixPositionY, inventoryMatrixPositionX] = null;
            }
        }
    }
}
