using SFML.Graphics;
using Soulmate_Remastered.Core;

namespace Soulmate_Remastered.Classes.GameStatesFolder
{
    class ControlsSetting : AbstractMenu
    {
        Texture controlsTexture;
        Sprite controls;

        public override void Initialize()
        {
            base.Initialize();

            controls = new Sprite(controlsTexture);
            controls.Position = new Vector2(0, 0);
        }

        public override void LoadContent()
        {
            base.LoadContent();

            controlsTexture = new Texture("Pictures/Menu/MainMenu/Controls/ControlsMenu.png");
        }

        public override EnumGameStates Update(GameTime gameTime)
        {
            GameUpdate(gameTime);

            return ReturnState;
        }

        public override void Draw(RenderWindow window)
        {
            base.Draw(window);

            window.Draw(controls);
            window.Draw(Back);
        }
    }
}
