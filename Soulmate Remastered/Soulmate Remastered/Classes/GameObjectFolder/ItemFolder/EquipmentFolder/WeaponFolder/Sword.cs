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
        public override string Type { get { return base.Type + ".Sword"; } }
        public override float ID { get { return base.ID*10 + 0; } }
        

        public Sword(float _attBonus, float _defBonus, float _hpBonus)
        {
            TextureList.Add(new Texture("Pictures/Items/Equipment/Player/Weapon/Sword.png"));
            Sprite = new Sprite(TextureList[0]);
            IsVisible = false;
            Position = new Vector2f();
            HitBox = new HitBox(Position, TextureList[0].Size.X, TextureList[0].Size.Y);
            dropRate = 30 * (100 / (_attBonus + _defBonus + _hpBonus));
            AttBonus = _attBonus;
            DefBonus = _defBonus;
            HpBonus = _hpBonus;
        }

        public Sword(float _attBonus, float _defBonus, float _hpBonus, String name)
            : this(_attBonus, _defBonus, _hpBonus)
        {
            CustomName = name;
        }

        public Sword() : this(1, 0, 0,"Broken Sword") { }

        public override AbstractItem clone()
        {
            AbstractItem clonedItem = new Sword(AttBonus, DefBonus, HpBonus, Name);
            clonedItem.Position = this.Position;
            return clonedItem;
        }
    }
}
