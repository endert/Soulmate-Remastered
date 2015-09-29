using SFML.Graphics;
using Soulmate_Remastered.Core;

namespace Soulmate_Remastered.Classes.GameStatesFolder
{
    class Credits : AbstractMenu
    {
        Texture creditsTexture;
        Sprite credits;
        
        public override void Initialize()
        {
            base.Initialize();
            
            credits = new Sprite(creditsTexture);
            credits.Position = new Vector2(0, 0);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            
            creditsTexture = new Texture("Pictures/Menu/MainMenu/Credits/CreditsMenu.png");
        }

        public override EnumGameStates Update(GameTime gameTime)
        {
            GameUpdate(gameTime);

            return ReturnState;
        }

        public override void Draw(RenderWindow window)
        {
            base.Draw(window);
            
            window.Draw(credits);
            window.Draw(Back);
        }
    }
}
