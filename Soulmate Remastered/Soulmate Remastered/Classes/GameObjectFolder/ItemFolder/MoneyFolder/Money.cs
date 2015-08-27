using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.MoneyFolder
{
    abstract class Money : AbstractItem
    {
        public override String Type { get { return base.Type + ".Money"; } }
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
        public override void pickUp()
        {
            onMap = false;
            GameObjectHandler.removeAt(IndexObjectList);
            PlayerHandler.Player.Gold += 1;
        }
    }
}
