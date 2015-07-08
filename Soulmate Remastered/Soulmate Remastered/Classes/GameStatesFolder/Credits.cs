using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace Soulmate_Remastered.Classes.GameStatesFolder
{
    class Credits : AbstractMainMenu
    {
        Texture creditsTexture;
        Sprite credits;
        
        public override void initialize()
        {
            base.initialize();
            
            credits = new Sprite(creditsTexture);
            credits.Position = new Vector2f(0, 0);
        }

        public override void loadContent()
        {
            base.loadContent();
            
            creditsTexture = new Texture("Pictures/Menu/MainMenu/Credits/CreditsMenu.png");
        }

        public override EnumGameStates update(GameTime gameTime)
        {
            gameUpdate(gameTime);
            
            if (backValueSelected == 1)
                return EnumGameStates.mainMenu;

            return EnumGameStates.credits;
        }

        public override void draw(RenderWindow window)
        {
            base.draw(window);
            
            window.Draw(credits);
            window.Draw(back);
        }
    }
}
