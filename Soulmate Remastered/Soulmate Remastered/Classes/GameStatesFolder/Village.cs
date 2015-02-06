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
        public override void loadContent()
        {
            map = new Map(new Bitmap("Pictures/Map/Map.bmp"));
            GameObjectHandler.lvl = 0;
            base.loadContent();
        }

        public override EnumGameStates update(GameTime gameTime)
        {
            returnValue = 0;
            GameUpdate(gameTime);

            if (returnValue == 2)
            {
                Console.WriteLine("change to instance");
                return EnumGameStates.inGame;
            }

            if (returnValue == 1)
            {
                return EnumGameStates.mainMenu;
            }

            return EnumGameStates.village;
        }
    }
}
