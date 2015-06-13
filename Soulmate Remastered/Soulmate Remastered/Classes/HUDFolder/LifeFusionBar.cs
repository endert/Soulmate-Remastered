using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.GameStatesFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Sprite scale()
        {
            if (barStyle.Equals("Fusion"))
            {
                barBackground.Scale = new Vector2f(-1 + (PlayerHandler.player.currentFusionValue / PlayerHandler.player.maxFusionValue), 1);
                inNumber.DisplayedString = "FP: " + PlayerHandler.player.currentFusionValue + "/" + PlayerHandler.player.maxFusionValue;
            }

            else if (barStyle.Equals("Life"))
            {
                barBackground.Scale = new Vector2f(-1 + (PlayerHandler.player.CurrentHP / PlayerHandler.player.MaxHP), 1);
                inNumber.DisplayedString = "HP: " + PlayerHandler.player.CurrentHP + "/" + PlayerHandler.player.MaxHP;
            }

            return barBackground;
        }

        /// <summary>
        /// sets the position of the bar and the text of it
        /// </summary>
        public void setPosition()
        {
            if(barStyle.Equals("Fusion"))
            {
                fusionBar.Position = new Vector2f((AbstractGamePlay.VIEW.Center.X - (Game.windowSizeX / 2) + 5), (AbstractGamePlay.VIEW.Center.Y - (Game.windowSizeY / 2) + lifeBarTexture.Size.Y + 10));
                barBackground.Position = new Vector2f(fusionBar.Position.X + fusionBar.Texture.Size.X, fusionBar.Position.Y);
                inNumber.Position = new Vector2f(fusionBar.Position.X + 10, fusionBar.Position.Y + 2);
            }

            else if(barStyle.Equals("Life"))
            {
                lifeBar.Position = new Vector2f((AbstractGamePlay.VIEW.Center.X - (Game.windowSizeX / 2) + 5), (AbstractGamePlay.VIEW.Center.Y - (Game.windowSizeY / 2) + 5));
                barBackground.Position = new Vector2f(lifeBar.Position.X + lifeBar.Texture.Size.X, lifeBar.Position.Y);
                inNumber.Position = new Vector2f(lifeBar.Position.X + 10, lifeBar.Position.Y + 2);
            }
        }

        public void update()
        {
            setPosition();
            scale();
        }

        public void draw(RenderWindow window)
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
