using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameStatesFolder
{
    abstract class AbstractMainMenu : GameState
    {
        protected int x; //für Menüsteuerung
        protected int backValueSelected;

        protected Texture backGroundTexture;
        protected Sprite backGround;

        protected Texture backSelected;
        protected Texture backNotSelected;
        protected Sprite back;

        protected View view;

        public Vector2f getBackPostion()
        {
            return new Vector2f(Game.windowSizeX - back.Texture.Size.X - 10, 650);
        }
        
        public virtual void initialize()
        {
            x = 0;
            backValueSelected = 0;
            
            backGround = new Sprite(backGroundTexture);
            backGround.Position = new Vector2f(0, 0);

            back = new Sprite(backNotSelected);
            back.Position = getBackPostion();

            view = new View(new FloatRect(0, 0, Game.windowSizeX, Game.windowSizeY));
        }

        public virtual void loadContent()
        {
            backGroundTexture = new Texture("Pictures/Menu/MainMenu/Background.png");
            
            backSelected = new Texture("Pictures/Menu/MainMenu/Back/BackSelected.png");
            backNotSelected = new Texture("Pictures/Menu/MainMenu/Back/BackNotSelected.png");
        }

        public abstract EnumGameStates update(GameTime gameTime);

        public void gameUpdate(GameTime gameTime)
        {
            if (NavigationHelp.isMouseInSprite(back))
                back.Texture = backSelected;
            else
                back.Texture = backNotSelected;

            backValueSelected = 0;

            if (NavigationHelp.isSpriteKlicked(0, 0, Game.isPressed, back, Controls.Escape))
            {
                Game.isPressed = true;
                backValueSelected = 1;
            }
        }

        public virtual void draw(RenderWindow window)
        {
            window.Draw(backGround);
            window.SetView(view);
            window.Draw(back);
        }
    }
}
