﻿using Soulmate_Remastered.Core;

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
        public static AbstractPlayer Player { get; set; }

        /// <summary>
        /// initialize the playerhandler and the player
        /// </summary>
        public PlayerHandler()
        {
            switch(GameObjectHandler.Lvl)
            { 
                case 0:
                    Player = new HumanPlayer(new Vector2(32 * 15, 32 * 10 - 219), Vector2.RIGHT);
                    EntityHandler.Add(Player);
                    break;

                case 1:
                    Player = new HumanPlayer(new Vector2(32 * 15, 32 * 10 - 219), Vector2.RIGHT);
                    EntityHandler.Add(Player);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// deletes the player, should only be called when the game is closed
        /// </summary>
        public static void Deleate()
        {
            Player = null;
        }
    }
}
