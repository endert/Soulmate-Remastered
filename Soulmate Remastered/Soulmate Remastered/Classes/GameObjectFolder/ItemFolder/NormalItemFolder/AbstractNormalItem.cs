using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.NormalItemFolder
{
    abstract class AbstractNormalItem : AbstractItem
    {
        public override string type
        {
            get
            {
                return base.type + ".NormalItem";
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
