﻿using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.MoneyFolder
{
    /// <summary>
    /// the base class for all currencies
    /// </summary>
    abstract class Currency : AbstractItem
    {
        /// <summary>
        /// the ID 10x
        /// </summary>
        public override float ID
        {
            get
            {
                return base.ID* 10 + 0;
            }
        }
    }
}
