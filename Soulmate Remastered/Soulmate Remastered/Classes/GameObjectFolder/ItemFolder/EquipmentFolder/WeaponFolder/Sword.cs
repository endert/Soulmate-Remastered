using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.EquipmentFolder
{
    /// <summary>
    /// a sword
    /// </summary>
    class Sword : AbstractWeapon
    {
        /// <summary>
        /// the type of this instance
        /// </summary>
        public override string Type { get { return base.Type + ".Sword"; } }
        /// <summary>
        /// the ID = 1320
        /// </summary>
        public override float ID { get { return base.ID*10 + 0; } }
        
        /// <summary>
        /// initialize a sword
        /// </summary>
        /// <param name="_attBonus">the attack gained through wielding this sword</param>
        /// <param name="_defBonus">the def gained through wielding this sword</param>
        /// <param name="_hpBonus">the hp gained trough wielding this sword</param>
        /// <param name="name">a costum name (optional); default = ""</param>
        public Sword(float _attBonus, float _defBonus, float _hpBonus, string name = "")
        {
            TextureList.Add(new Texture("Pictures/Items/Equipment/Player/Weapon/Sword.png"));
            Sprite = new Sprite(TextureList[0]);
            IsVisible = false;
            Position = new Vector2f();
            HitBox = new HitBox(Position, TextureList[0].Size.X, TextureList[0].Size.Y);
            DropRate = 30 * (100 / (_attBonus + _defBonus + _hpBonus));
            AttBonus = _attBonus;
            DefBonus = _defBonus;
            HpBonus = _hpBonus;
            CustomName = name;

            //EquipmentHandler.Add(this);
        }

        /// <summary>
        /// initialize the broken sword
        /// </summary>
        public Sword() : this(1, 0, 0, "broken Sword") { }

        /// <summary>
        /// clones this sword
        /// </summary>
        /// <returns></returns>
        public override AbstractItem Clone()
        {
            AbstractItem clonedItem = new Sword(AttBonus, DefBonus, HpBonus, Name);
            clonedItem.Position = this.Position;
            return clonedItem;
        }
    }
}
