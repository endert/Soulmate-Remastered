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
        public override void pickUp(GameTime gameTime)
        {
            onMap = false;
            GameObjectHandler.removeAt(indexObjectList);
            PlayerHandler.player.gold += 1;
        }
    }
}
