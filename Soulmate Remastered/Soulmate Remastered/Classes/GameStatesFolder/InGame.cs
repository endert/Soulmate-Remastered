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
using System.IO;

namespace Soulmate_Remastered.Classes.GameStatesFolder
{
    class InGame : AbstractGamePlay
    {
        /// <summary>
        /// load the content of the level
        /// </summary>
        public override void loadContent()
        {
            map = new Map(new Bitmap("Pictures/Map/Bitmap/Map2.bmp"));            
            GameObjectHandler.lvl = 1;
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
                    Console.WriteLine("change to village");
                    return EnumGameStates.village;

                default:
                    return EnumGameStates.inGame;
            }            
        }        
    }
}
