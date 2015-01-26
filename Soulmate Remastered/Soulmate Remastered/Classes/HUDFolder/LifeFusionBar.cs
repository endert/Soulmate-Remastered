using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.GameStatesFolders;
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
        Text inNumber;

        Texture barBackgroundTexture = new Texture("Pictures/BarBackground.png");
        Texture fusionBarTexture = new Texture("Pictures/FusionBar.png");
        Texture lifeBarTexture = new Texture("Pictures/LifeBar.png");

        Sprite barBackground;
        Sprite fusionBar;
        Sprite lifeBar;

        String barStyle;

        public LifeFusionBar(String _barStyle)
        {
            inNumber = new Text("",font,20);
            barBackground = new Sprite(barBackgroundTexture);
            fusionBar = new Sprite(fusionBarTexture);
            lifeBar = new Sprite(lifeBarTexture);
            barStyle = _barStyle;
        }

        public Sprite scale()
        {
            if (barStyle.Equals("Fusion"))
            {
                barBackground.Scale = new Vector2f(-1 + (PlayerHandler.player.currentFusionValue / PlayerHandler.player.getMaxFusionValue), 1);
                inNumber.DisplayedString = "FP: " + PlayerHandler.player.currentFusionValue + "/" + PlayerHandler.player.getMaxFusionValue;
            }

            else if (barStyle.Equals("Life"))
            {
                barBackground.Scale = new Vector2f(-1 + (PlayerHandler.player.getCurrentHP / PlayerHandler.player.getMaxHP), 1);
                inNumber.DisplayedString = "HP: " + PlayerHandler.player.getCurrentHP + "/" + PlayerHandler.player.getMaxHP;
            }

            return barBackground;
        }

        public void setPosition()
        {
            if(barStyle.Equals("Fusion"))
            {
                fusionBar.Position = new Vector2f((InGame.VIEW.Center.X - (Game.windowSizeX / 2) + 5), (InGame.VIEW.Center.Y - (Game.windowSizeY / 2) + lifeBarTexture.Size.Y + 10));

                barBackground.Position = new Vector2f(fusionBar.Position.X + fusionBar.Texture.Size.X, fusionBar.Position.Y);
                inNumber.Position = new Vector2f(fusionBar.Position.X + 10, fusionBar.Position.Y + 2);
            }

            else if(barStyle.Equals("Life"))
            {
                lifeBar.Position = new Vector2f((InGame.VIEW.Center.X - (Game.windowSizeX / 2) + 5), (InGame.VIEW.Center.Y - (Game.windowSizeY / 2) + 5));
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
