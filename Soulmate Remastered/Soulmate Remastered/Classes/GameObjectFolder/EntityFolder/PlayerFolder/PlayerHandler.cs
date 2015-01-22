﻿using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder
{
    class PlayerHandler
    {
        public static AbstractPlayer player { get; set; }

        public PlayerHandler()
        {
            switch(GameObjectHandler.lvl)
            { 
                case 0:
                    player = new HumanPlayer(new Vector2f(32 * 15, 32 * 10 - 219), 2);
                    break;

                default:
                    break;
            }
        }

        public static void deleate()
        {
            player = null;

            EntityHandler.deleateType("Player");
        }
    }
}
