using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.PotionFolder
{
    abstract class AbstractPotion : AbstractItem
    {
        public override string Type
        {
            get
            {
                return base.Type + ".Potion";
            }
        }
        /// <summary>
        /// the ID 12x
        /// </summary>
        public override float ID
        {
            get
            {
                return base.ID * 10 + 2;
            }
        }

        protected int size;
        public int getSize { get { return size; } }

        public override string ToStringForSave()
        {
            return base.ToStringForSave() + size + LineBreak.ToString();
        }
    }
}
