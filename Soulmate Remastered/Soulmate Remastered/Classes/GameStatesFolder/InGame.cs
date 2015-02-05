﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.MapFolder;
using Soulmate_Remastered.Classes.GameObjectFolder;
using Soulmate_Remastered.Classes.HUDFolder;
using System.Drawing;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.InGameMenuFolder;
using Soulmate_Remastered.Classes.DialogeBoxFolder;
using Soulmate_Remastered.Classes.GameStatesFolder;
using System.IO;

namespace Soulmate_Remastered.Classes.GameStatesFolder
{
    class InGame : AbstractGamePlay
    {
        public override void loadContent()
        {
            map = new Map(new Bitmap("Pictures/Map/Map2.bmp"));            
            GameObjectHandler.lvl = 1;
            GameObjectHandler.lvlMap = map;
            base.loadContent();
        }

        public override EnumGameStates update(GameTime gameTime)
        {
            GameUpdate(gameTime);

            if (Keyboard.IsKeyPressed(Keyboard.Key.L) && !isKlicked)
            {
                isKlicked = true;
                Console.WriteLine("change to village");
                SaveGame.saveGame(savePlayer);
                return EnumGameStates.village;
            }

            if (!Keyboard.IsKeyPressed(Keyboard.Key.L))
            {
                isKlicked = false;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Return) && inGameMenu.getX() == 2) //if exit clicked
            {
                SaveGame.saveGame(savePlayer);
                gameObjectHandler.deleate();
                return EnumGameStates.mainMenu;
            }

            if (returnValue == 1)
            {
                return EnumGameStates.mainMenu;
            }

            return EnumGameStates.inGame;
        }        
    }
}
