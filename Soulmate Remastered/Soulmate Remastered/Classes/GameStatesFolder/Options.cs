﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace Soulmate_Remastered.Classes.GameStatesFolder
{
    class Options : AbstractMainMenu
    {
        protected Texture optionsTexture;
        protected Sprite options;

        Text Controls = new Text("Controls", Game.font, 100);
        
        public override void initialize()
        {
            base.initialize();

            options = new Sprite(optionsTexture);
            options.Position = new Vector2f(0, 0);

            Controls.Position = new Vector2f(200, 200);
        }

        public override void loadContent()
        {
            base.loadContent();

            optionsTexture = new Texture("Pictures/Menu/MainMenu/Options/OptionsMenu.png");
        }

        public override EnumGameStates update(GameTime gameTime)
        {
            gameUpdate(gameTime);

            if (backValueSelected == 1)
                return EnumGameStates.mainMenu;

            return EnumGameStates.options;
        }

        public override void draw(RenderWindow window)
        {
            base.draw(window);

            window.Draw(options);
            window.Draw(Controls);
            window.Draw(back);
        }
    }
}
