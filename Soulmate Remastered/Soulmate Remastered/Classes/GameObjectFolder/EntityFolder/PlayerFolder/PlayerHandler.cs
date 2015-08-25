using SFML.Window;
using Soulmate_Remastered.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder
{
    /// <summary>
    /// handles the player
    /// </summary>
    class PlayerHandler
    {
        /// <summary>
        /// the player
        /// </summary>
        public static AbstractPlayer player { get; set; }

        public PlayerHandler()
        {
            switch(GameObjectHandler.lvl)
            { 
                case 0:
                    player = new HumanPlayer(new Vector2f(32 * 15, 32 * 10 - 219), Vector2.RIGHT);
                    EntityHandler.add(player);
                    break;

                case 1:
                    player = new HumanPlayer(new Vector2f(32 * 15, 32 * 10 - 219), Vector2.RIGHT);
                    EntityHandler.add(player);
                    break;

                default:
                    break;
            }
        }

        public static void deleate()
        {
            player = null;
        }
    }
}
