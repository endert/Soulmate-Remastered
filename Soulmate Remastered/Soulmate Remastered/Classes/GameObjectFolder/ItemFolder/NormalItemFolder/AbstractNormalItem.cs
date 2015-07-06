﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.NormalItemFolder
{
    abstract class AbstractNormalItem : AbstractItem
    {
        public override string Type
        {
            get
            {
                return base.Type + ".NormalItem";
            }
        }
        public override float ID
        {
            get
            {
                return base.ID * 10 + 0;
            }
        }
    }
}
