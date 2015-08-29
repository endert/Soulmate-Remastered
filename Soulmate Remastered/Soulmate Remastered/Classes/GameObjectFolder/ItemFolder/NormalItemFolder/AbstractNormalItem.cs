using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.NormalItemFolder
{
    /// <summary>
    /// base class for "normal" items, in generell items that cannot be used for example: quest items
    /// </summary>
    abstract class AbstractNormalItem : AbstractItem
    {
        /// <summary>
        /// the type of this instance
        /// </summary>
        public override string Type
        {
            get
            {
                return base.Type + ".NormalItem";
            }
        }

        /// <summary>
        /// the ID = 11x
        /// </summary>
        public override float ID
        {
            get
            {
                return base.ID * 10 + 1;
            }
        }
    }
}
