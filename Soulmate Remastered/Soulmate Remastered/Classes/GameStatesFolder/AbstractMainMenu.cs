﻿using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        /// Enum that indicates which Sprite is selected
        /// </summary>
        protected Eselected selectedSprite;

        /// <summary>
        /// important: the sprites must have the exact same name as they have in the enum
        /// </summary>
        protected enum Eselected
        {
            None = -1,

            Back,

            MainMenuOffset,

            StartSprite,
            OptionsButton,
            Controls,
            Credits,
            End,

            MainMenuCount,

            LoadOffset,

            NewGame,
            LoadGame,

            LoadSpriteCount

        }

        /// <summary>
        /// the Game State that is returned
        /// </summary>
        protected EnumGameStates ReturnState;

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
        protected Sprite Back;

        /// <summary>
        /// equals to a camera
        /// </summary>
        protected View view;

        void MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                if (selectedSprite == Eselected.None)
                    return;

                Game.isPressed = true;
                //get the selected sprite++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                string selected = selectedSprite.ToString().Split('.').Last();

                FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

                Sprite selectedSpr = null;

                foreach(FieldInfo info in fields)
                {
                    if (info.Name.Equals(selected))
                        selectedSpr = (Sprite)info.GetValue(this);
                }

                //if u got it+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                if(selectedSpr != null)
                {
                    if (MouseControler.MouseIn(selectedSpr))
                    {
                        MouseControler.ButtonPressed -= MouseButtonPressed;

                        switch (selectedSprite)
                        {
                            case Eselected.Back:
                                SetBackReturnState();
                                break;
                            case Eselected.StartSprite:
                                ReturnState = EnumGameStates.LoadGame;
                                break;
                            case Eselected.OptionsButton:
                                ReturnState = EnumGameStates.Options;
                                break;
                            case Eselected.Controls:
                                ReturnState = EnumGameStates.ControlsSetting;
                                break;
                            case Eselected.Credits:
                                ReturnState = EnumGameStates.Credits;
                                break;
                            case Eselected.End:
                                ReturnState = EnumGameStates.None;
                                break;

                        }
                    }
                }
            }
        }

        /// <summary>
        /// evaluates which GameState is entered by pressing back
        /// </summary>
        void SetBackReturnState()
        {
            if (GetType().Equals(typeof(MainMenu)))
            {
                ReturnState = EnumGameStates.TitleScreen;
                selectedSprite = Eselected.None;
            }
            else
                ReturnState = EnumGameStates.MainMenu;
        }

        /// <summary>
        /// position of the back-button
        /// </summary>
        /// <returns>vector</returns>
        public Vector2 GetBackPostion()
        {
            return new Vector2(Game.windowSizeX - Back.Texture.Size.X - 10, 650);
        }
 
        /// <summary>
        /// loads everything what is needded for the scene
        /// </summary>
        public virtual void LoadContent()
        {
            MouseControler.ButtonPressed += MouseButtonPressed;

            selectedSprite = Eselected.None;

            backGroundTexture = new Texture("Pictures/Menu/MainMenu/Background.png");
            backGround = new Sprite(backGroundTexture);
            backGround.Position = new Vector2(0, 0);
            
            backNotSelected = new Texture("Pictures/Menu/MainMenu/Back/BackNotSelected.png");
            backSelected = new Texture("Pictures/Menu/MainMenu/Back/BackSelected.png");
            Back = new Sprite(backNotSelected);
            Back.Position = GetBackPostion();

            ReturnState = GetThisAsEnum();

            view = new View(new FloatRect(0, 0, Game.windowSizeX, Game.windowSizeY));
        }

        public abstract EnumGameStates Update(GameTime gameTime);

        /// <summary>
        /// takes the class name and interprets it as the fitting EnumGameState
        /// </summary>
        /// <returns></returns>
        EnumGameStates GetThisAsEnum()
        {
            string className = GetType().Name;

            for(int i = 0; i<(int)EnumGameStates.GameStateCount; ++i)
            {
                if (((EnumGameStates)i).ToString().Split('.').Last().Equals(className))
                    return (EnumGameStates)i;
            }

            return EnumGameStates.None;
        }

        /// <summary>
        /// updates if the back button was pressed …
        /// </summary>
        /// <param name="gameTime"></param>
        public void GameUpdate(GameTime gameTime)
        {
            if (MouseControler.MouseIn(Back))
            {
                Back.Texture = backSelected;
                selectedSprite = Eselected.Back;
            }
            else
                Back.Texture = backNotSelected;

            if (!Game.isPressed && Keyboard.IsKeyPressed(Controls.Escape))
            {
                Game.isPressed = true;
                SetBackReturnState();
            }
        }

        /// <summary>
        /// draws the sprites
        /// </summary>
        /// <param name="window">window where it should be drawn</param>
        public virtual void Draw(RenderWindow window)
        {
            window.Draw(backGround);
            window.SetView(view);
            window.Draw(Back);
        }

        public virtual void Initialize() { }
    }
}
