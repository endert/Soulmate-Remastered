using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameStatesFolder
{
    /// <summary>
    /// similar content of all menu points
    /// </summary>
    abstract class AbstractMainMenu : GameState
    {
        /// <summary>
        /// <para>value to check if the back-sprite was klicked</para>
        /// <para>0=>not klicked</para>
        /// <para>1=>klicked</para>
        /// </summary>
        protected int backValueSelected;

        /// <summary>
        /// texture of the background
        /// </summary>
        protected Texture backGroundTexture;
        /// <summary>
        /// sprite of the background
        /// </summary>
        protected Sprite backGround;

        /// <summary>
        /// texture of the back-button when selected
        /// </summary>
        protected Texture backSelected;
        /// <summary>
        /// texture of the back-button when not selected
        /// </summary>
        protected Texture backNotSelected;
        /// <summary>
        /// sprite of the back-button
        /// </summary>
        protected Sprite back;

        /// <summary>
        /// equals to a camera
        /// </summary>
        protected View view;

        /// <summary>
        /// position of the back-button
        /// </summary>
        /// <returns>vector</returns>
        public Vector2 getBackPostion()
        {
            return new Vector2(Game.windowSizeX - back.Texture.Size.X - 10, 650);
        }
 
        /// <summary>
        /// loads everything what is needded for the scene
        /// </summary>
        public virtual void loadContent()
        {
            backValueSelected = 0;

            backGroundTexture = new Texture("Pictures/Menu/MainMenu/Background.png");
            backGround = new Sprite(backGroundTexture);
            backGround.Position = new Vector2f(0, 0);
            
            backNotSelected = new Texture("Pictures/Menu/MainMenu/Back/BackNotSelected.png");
            backSelected = new Texture("Pictures/Menu/MainMenu/Back/BackSelected.png");
            back = new Sprite(backNotSelected);
            back.Position = getBackPostion();

            view = new View(new FloatRect(0, 0, Game.windowSizeX, Game.windowSizeY));
        }

        public abstract EnumGameStates update(GameTime gameTime);

        public void gameUpdate(GameTime gameTime)
        {
            if (NavigationHelp.isMouseInSprite(back))
                back.Texture = backSelected;
            else
                back.Texture = backNotSelected;

            backValueSelected = 0;

            if (NavigationHelp.isSpriteKlicked(0, 0, back, Controls.Escape))
            {
                Game.isPressed = true;
                backValueSelected = 1;
            }
        }

        /// <summary>
        /// draws the sprites
        /// </summary>
        /// <param name="window">window where it should be drawed</param>
        public virtual void draw(RenderWindow window)
        {
            window.Draw(backGround);
            window.SetView(view);
            window.Draw(back);
        }

        public virtual void initialize() { }
    }
}
