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
        Texture barBackgroundTexture = new Texture("Pictures/BarBackground.png");
        Texture fusionBarTexture = new Texture("Pictures/FusionBar.png");
        Texture lifeBarTexture = new Texture("Pictures/LifeBar.png");

        Sprite barBackground;
        Sprite fusionBar;
        Sprite lifeBar;

        public LifeFusionBar()
        {
            barBackground = new Sprite(barBackgroundTexture);
            fusionBar = new Sprite(fusionBarTexture);
            lifeBar = new Sprite(lifeBarTexture);
        }

        public Sprite scale(String barStyle)
        {
            if (barStyle.Equals("Fusion"))
            {
                barBackground.Scale = new Vector2f(-1 + ((float)PlayerHandler.player.currentFusionValue / (float)PlayerHandler.player.getMaxFusionValue), 1);

                return barBackground;
            }

            else
            {
                barBackground.Scale = new Vector2f(-1 + ((float)PlayerHandler.player.getCurrentHP / (float)PlayerHandler.player.getMaxHP), 1);

                return barBackground;
            }
            
        }

        public void setPosition(String barStyle)
        {
            if(barStyle.Equals("Fusion"))
            {
                fusionBar.Position = new Vector2f((InGame.VIEW.Center.X - (Game.windowSizeX / 2) + 5),
                                                  (InGame.VIEW.Center.Y - (Game.windowSizeY / 2) + lifeBarTexture.Size.Y + 5));
                barBackground.Position = new Vector2f(fusionBar.Position.X + fusionBar.Texture.Size.X, fusionBar.Position.Y);
            }

            else
            {
                lifeBar.Position = new Vector2f((InGame.VIEW.Center.X - (Game.windowSizeX / 2) + 5),
                                                (InGame.VIEW.Center.Y - (Game.windowSizeY / 2) + 5));
                barBackground.Position = new Vector2f(lifeBar.Position.X + lifeBar.Texture.Size.X, lifeBar.Position.Y);
            }
            
        }

        public void update(String barStyle)
        {
            setPosition(barStyle);
            scale(barStyle);
        }

        public void draw(RenderWindow window)
        {
            window.Draw(lifeBar);
            window.Draw(fusionBar);
            window.Draw(barBackground);
        }
    }
}
