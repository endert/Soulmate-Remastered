using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.EquipmentFolder
{
    class Sword : Equipment
    {
        public override string type { get { return base.type + ".Sword"; } }
        public override float ID { get { return base.ID*10 + 2; } }
        public override string ItemDiscription
        {
            get
            {
                String itemDiscription = "";

                itemDiscription += "sword \n";
                itemDiscription += '"'.ToString() + "an old used sword." + '"'.ToString() + "\n";
                itemDiscription += "AttBonus: " + attBonus + "      ";
                itemDiscription += "DefBonus: " + defBonus + "\n";
                itemDiscription += "HpBonus: " + hpBonus + "      ";

                return itemDiscription;
            }
        }


        public Sword(float _attBonus, float _defBonus, float _hpBonus)
        {
            textureList.Add(new Texture("Pictures/Items/Equipment/Player/Weapon/Sword.png"));
            sprite = new Sprite(textureList[0]);
            visible = false;
            position = new Vector2f();
            hitBox = new HitBox(position, textureList[0].Size.X, textureList[0].Size.Y);
            dropRate = 30 * (100 / (_attBonus + _defBonus + _hpBonus));
            attBonus = (int)_attBonus;
            defBonus = (int)_defBonus;
            hpBonus = (int)_hpBonus;
        }

        public override AbstractItem clone()
        {
            AbstractItem clonedItem = new Sword(attBonus, defBonus, hpBonus);
            clonedItem.position = this.position;
            return clonedItem;
        }

        public override void cloneAndDrop(Vector2f dropPosition)
        {
            AbstractItem dropedItem = this.clone();
            ItemHandler.add(dropedItem);
            dropedItem.drop(dropPosition);
        }
    }
}
