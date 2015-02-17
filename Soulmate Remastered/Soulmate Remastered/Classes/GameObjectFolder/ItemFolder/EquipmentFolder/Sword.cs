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

        public Sword(float _attBonus, float _defBonus, float _hpBonus)
        {
            textureList.Add(new Texture("Pictures/Items/Equipment/Player/Weapon/Sword.png"));
            sprite = new Sprite(textureList[0]);
            visible = false;
            position = new Vector2f();
            hitBox = new HitBox(position, textureList[0].Size.X, textureList[0].Size.Y);
            dropRate = 100;
            attBonus = _attBonus;
            defBonus = _defBonus;
            hpBonus = _hpBonus;
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
