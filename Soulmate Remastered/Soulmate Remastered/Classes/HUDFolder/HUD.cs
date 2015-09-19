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

        public void Update(GameTime gameTime)
        {
            fusionBar.Update();
            lifeBar.Update();
        }

        public void Draw(RenderWindow window)
        {
            fusionBar.Draw(window);
            lifeBar.Draw(window);
        }
    }
}
