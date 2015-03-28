using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.EquipmentFolder
{
    class Sword : AbstractWeapon
    {
        public override string type { get { return base.type + ".Sword"; } }
        public override float ID { get { return base.ID*10 + 0; } }
        

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

        public Sword(float _attBonus, float _defBonus, float _hpBonus, String name)
            : this(_attBonus, _defBonus, _hpBonus)
        {
            customName = name;
        }

        public Sword() : this(1, 0, 0,"Broken Sword") { }

        public override AbstractItem clone()
        {
            AbstractItem clonedItem = new Sword(attBonus, defBonus, hpBonus, name);
            clonedItem.position = this.position;
            return clonedItem;
        }
    }
}
