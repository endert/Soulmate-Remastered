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

        protected Texture backGroundTexture;
        protected Sprite backGround;

        protected Texture backSelected;
        protected Texture backNotSelected;
        protected Sprite back;

        public Vector2f getBackPostion()
        {
            return new Vector2f(Game.windowSizeX - back.Texture.Size.X - 10, 650);
        }
        
        public virtual void initialize()
        {
            backGround = new Sprite(backGroundTexture);
            backGround.Position = new Vector2f(0, 0);
        }

        public virtual void loadContent()
        {

        }

        public abstract EnumGameStates update(GameTime gameTime);

        public void gameUpdate(GameTime gameTime)
        {

        }

        public virtual void draw(RenderWindow window)
        {

        }
    }
}
