using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Core;

namespace Soulmate_Remastered.Classes.GameStatesFolder
{
    class Options : AbstractMainMenu
    {
        protected Texture optionsTexture;
        protected Sprite options;

        Text Controls = new Text("Controls", Game.font, 100);
        
        public override void Initialize()
        {
            base.Initialize();

            options = new Sprite(optionsTexture);
            options.Position = new Vector2(0, 0);

            Controls.Position = new Vector2(200, 200);
        }

        public override void LoadContent()
        {
            base.LoadContent();

            optionsTexture = new Texture("Pictures/Menu/MainMenu/Options/OptionsMenu.png");
        }

        public override EnumGameStates Update(GameTime gameTime)
        {
            GameUpdate(gameTime);

            return ReturnState;
        }

        public override void Draw(RenderWindow window)
        {
            base.Draw(window);

            window.Draw(options);
            window.Draw(Controls);
            window.Draw(Back);
        }
    }
}
