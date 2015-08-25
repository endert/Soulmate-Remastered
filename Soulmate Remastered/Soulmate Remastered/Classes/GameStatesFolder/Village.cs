using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using System.Drawing;
using Soulmate_Remastered.Classes.MapFolder;
using Soulmate_Remastered.Classes.GameObjectFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.GameStatesFolder;

namespace Soulmate_Remastered.Classes.GameStatesFolder
{
    class Village : AbstractGamePlay
    {
        /// <summary>
        /// load the content of the level
        /// </summary>
        public override void loadContent()
        {
            map = new Map(new Bitmap("Pictures/Map/Bitmap/Village.bmp"));
            GameObjectHandler.lvl = 0;
            base.loadContent();
        }

        /// <summary>
        /// updates the current state of the game
        /// </summary>
        /// <param name="gameTime">time of the game</param>
        /// <returns>enumGameStates</returns>
        public override EnumGameStates update(GameTime gameTime)
        {
            GameUpdate(gameTime);

            switch (returnValue)
            {
                case 1:
                    Console.WriteLine("change to mainMenu");
                    return EnumGameStates.mainMenu;

                case 2:
                    Console.WriteLine("change to instance");
                    return EnumGameStates.inGame;

                default:
                    return EnumGameStates.village;
            }          
        }
    }
}
