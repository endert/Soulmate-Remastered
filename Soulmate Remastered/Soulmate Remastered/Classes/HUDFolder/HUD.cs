using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.HUDFolder
{
    class HUD
    {
        LifePlayer lifePlayer;
        BarFusionPlayerPet barFusionPlayerPet;

        public HUD()
        {
            lifePlayer = new LifePlayer();
            barFusionPlayerPet = new BarFusionPlayerPet();
        }

        public void update(GameTime gameTime)
        {
            lifePlayer.update(gameTime);
            barFusionPlayerPet.update(lifePlayer.getLastLifeHeartSpritePositionBottomY());
        }

        public void draw(RenderWindow window)
        {
            lifePlayer.draw(window);
            barFusionPlayerPet.draw(window);
        }
    }
}
