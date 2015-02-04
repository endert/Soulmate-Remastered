using SFML.Window;
using System;
using System.Collections.Generic;
using System.IO;
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
                    if (File.Exists("Saves/player.soul"))
                    {
                        StreamReader reader = new StreamReader("Saves/player.soul");

                        player.toPlayer(reader.ReadLine());

                        reader.Close();
                    }
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
