﻿using SFML.Graphics;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.GameStatesFolder;
using Soulmate_Remastered.Core;
using System;

namespace Soulmate_Remastered.Classes.HUDFolder
{
    class LifeFusionBar
    {
        Font font = new Font("FontFolder/arial_narrow_7.ttf");
        /// <summary>
        /// text for the value of life or fusion bar
        /// </summary>
        Text inNumber;

        Texture barBackgroundTexture = new Texture("Pictures/Bars/BarBackground.png");
        Texture fusionBarTexture = new Texture("Pictures/Bars/FusionBar.png");
        Texture lifeBarTexture = new Texture("Pictures/Bars/LifeBar.png");

        /// <summary>
        /// not really the background because we scale this to make the bars smaller;
        /// means that this lays in the front not in the back
        /// </summary>
        Sprite barBackground;
        Sprite fusionBar;
        Sprite lifeBar;

        /// <summary>
        /// to differentiate if it is a lifebar or a fusionbar
        /// </summary>
        String barStyle;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="_barStyle">bar which should be constructed</param>
        public LifeFusionBar(String _barStyle)
        {
            inNumber = new Text("", font, 20);
            barBackground = new Sprite(barBackgroundTexture);
            fusionBar = new Sprite(fusionBarTexture);
            lifeBar = new Sprite(lifeBarTexture);
            barStyle = _barStyle;
        }

        /// <summary>
        /// scales the backGround for the bars and defines the text for the bars
        /// </summary>
        /// <returns>the sprite of the background</returns>
        public Sprite Scale()
        {
            if (barStyle.Equals("Fusion"))
            {
                barBackground.Scale = new Vector2(-1 + (PlayerHandler.Player.CurrentFusionValue / PlayerHandler.Player.MaxFusionValue), 1);
                inNumber.DisplayedString = "FP: " + PlayerHandler.Player.CurrentFusionValue + "/" + PlayerHandler.Player.MaxFusionValue;
            }

            else if (barStyle.Equals("Life"))
            {
                barBackground.Scale = new Vector2(-1 + (PlayerHandler.Player.CurrentHP / PlayerHandler.Player.MaxHP), 1);
                inNumber.DisplayedString = "HP: " + PlayerHandler.Player.CurrentHP + "/" + PlayerHandler.Player.MaxHP;
            }

            return barBackground;
        }

        /// <summary>
        /// sets the position of the bar and the text of it
        /// </summary>
        public void SetPosition()
        {
            if(barStyle.Equals("Fusion"))
            {
                fusionBar.Position = new Vector2((AbstractGamePlay.View.Center.X - (Game.WindowSizeX / 2) + 5), (AbstractGamePlay.View.Center.Y - (Game.WindowSizeY / 2) + lifeBarTexture.Size.Y + 10));
                barBackground.Position = new Vector2(fusionBar.Position.X + fusionBar.Texture.Size.X, fusionBar.Position.Y);
                inNumber.Position = new Vector2(fusionBar.Position.X + 10, fusionBar.Position.Y + 2);
            }

            else if(barStyle.Equals("Life"))
            {
                lifeBar.Position = new Vector2((AbstractGamePlay.View.Center.X - (Game.WindowSizeX / 2) + 5), (AbstractGamePlay.View.Center.Y - (Game.WindowSizeY / 2) + 5));
                barBackground.Position = new Vector2(lifeBar.Position.X + lifeBar.Texture.Size.X, lifeBar.Position.Y);
                inNumber.Position = new Vector2(lifeBar.Position.X + 10, lifeBar.Position.Y + 2);
            }
        }

        public void Update()
        {
            SetPosition();
            Scale();
        }

        public void Draw(RenderWindow window)
        {
            if (barStyle.Equals("Fusion"))
                window.Draw(fusionBar);
            if (barStyle.Equals("Life"))
                window.Draw(lifeBar);
            window.Draw(barBackground);
            window.Draw(inNumber);
        }
    }
}
