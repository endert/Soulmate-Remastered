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
        LifeFusionBar fusionBar;
        LifeFusionBar lifeBar;

        public HUD()
        {
            fusionBar = new LifeFusionBar("Fusion");
            lifeBar = new LifeFusionBar("Life");
        }

        public void update(GameTime gameTime)
        {
            fusionBar.update();
            lifeBar.update();
        }

        public void draw(RenderWindow window)
        {
            fusionBar.draw(window);
            lifeBar.draw(window);
        }
    }
}
