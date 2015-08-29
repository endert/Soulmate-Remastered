using SFML.Window;
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
        /// the type of this instance
        /// </summary>
        public override String Type { get { return base.Type + ".Currency"; } }
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

        /// <summary>
        /// method for picking this up, should only be called on the pick up event (to do)
        /// </summary>
        public override void PickUp()
        {
            OnMap = false;
            GameObjectHandler.removeAt(IndexObjectList);
            PlayerHandler.Player.Gold += 1;
        }
    }
}
