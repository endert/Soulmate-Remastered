﻿using System;
using System.Drawing;
using Soulmate_Remastered.Classes.MapFolder;
using Soulmate_Remastered.Classes.GameObjectFolder;

namespace Soulmate_Remastered.Classes.GameStatesFolder
{
    class InGame : AbstractGamePlay
    {
        /// <summary>
        /// load the content of the level
        /// </summary>
        public override void LoadContent()
        {
            map = new Map(new Bitmap("Pictures/Map/Bitmap/Map2.bmp"));            
            GameObjectHandler.Lvl = 1;
            base.LoadContent();
        }

        /// <summary>
        /// updates the current state of the game
        /// </summary>
        /// <param name="gameTime">time of the game</param>
        /// <returns>enumGameStates</returns>
        public override EnumGameStates Update(GameTime gameTime)
        {
            GameUpdate(gameTime);

            switch (returnValue)
            {
                case 1:
                    Console.WriteLine("change to mainMenu");
                    return EnumGameStates.MainMenu;

                case 2:
                    Console.WriteLine("change to village");
                    return EnumGameStates.Village;

                default:
                    return EnumGameStates.InGame;
            }            
        }        
    }
}
