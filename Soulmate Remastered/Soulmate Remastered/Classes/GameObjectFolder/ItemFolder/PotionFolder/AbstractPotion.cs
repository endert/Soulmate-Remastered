﻿using System;
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

        public override float ID
        {
            get
            {
                return base.ID * 10 + 2;
            }
        }

        protected int size;
        public int getSize { get { return size; } }

        public override string toStringForSave()
        {
            return base.toStringForSave() + size + LineBreak.ToString();
        }
    }
}
