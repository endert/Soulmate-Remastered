using SFML.Graphics;
using Soulmate_Remastered.Classes.GameObjectFolder;
using Soulmate_Remastered.Core;
using System;
using System.Linq;
using System.Reflection;

namespace Soulmate_Remastered.Classes.GameStatesFolder
{
    /// <summary>
    /// <para>base class for the menus</para>
    /// similar content of all menu points
    /// </summary>
    abstract class AbstractMenu : GameState
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
            OptionsSprite,
            ControlsSprite,
            CreditsSprite,
            EndSprite,

            MainMenuCount,

            LoadOffset,

            NewGame,
            Load,

            LoadSpriteCount

        }

        /// <summary>
        /// the Game State that is returned
        /// </summary>
        protected EnumGameStates ReturnState;

        /// <summary>
        /// state that contains the seleceted shader
        /// </summary>
        protected RenderStates SelectedState;
        Shader shader;

        /// <summary>
        /// texture of the background
        /// </summary>
        protected Texture backGroundTexture;
        /// <summary>
        /// sprite of the background
        /// </summary>
        protected Sprite backGround;

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
            if (e.Button == MouseButton.Left)
            {
                if (selectedSprite == Eselected.None)
                    return;

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
                            case Eselected.OptionsSprite:
                                ReturnState = EnumGameStates.Options;
                                break;
                            case Eselected.ControlsSprite:
                                ReturnState = EnumGameStates.ControlsSetting;
                                break;
                            case Eselected.CreditsSprite:
                                ReturnState = EnumGameStates.Credits;
                                break;
                            case Eselected.EndSprite:
                                ReturnState = EnumGameStates.None;
                                break;
                            case Eselected.NewGame:
                                Console.WriteLine("new Game");
                                AbstractGamePlay.startNewGame = true;
                                ReturnState = EnumGameStates.Village;
                                break;
                            case Eselected.Load:
                                AbstractGamePlay.loading = true;

                                string loadFile = (string)GetType().GetField("loadFile", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);

                                SaveGame.LoadPath = loadFile;
                                SaveGame.LoadGame();

                                switch (GameObjectHandler.Lvl)
                                {
                                    case 0:
                                        ReturnState = EnumGameStates.Village;
                                        break;
                                    case 1:
                                        ReturnState = EnumGameStates.InGame;
                                        break;
                                    default:
                                        ReturnState = EnumGameStates.Village;
                                        break;
                                }
                                break;

                        }
                    }
                }
            }
        }

        protected virtual void OnKeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key == Controls.Key.Escape)
            {
                KeyboardControler.KeyPressed -= OnKeyPress;
                SetBackReturnState();
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
            return new Vector2(Game.WindowSizeX - Back.Texture.Size.X - 10, 650);
        }
 
        /// <summary>
        /// loads everything what is needded for the scene
        /// </summary>
        public virtual void LoadContent()
        {
            MouseControler.ButtonPressed += MouseButtonPressed;
            KeyboardControler.KeyPressed += OnKeyPress;

            selectedSprite = Eselected.None;

            backGroundTexture = new Texture("Pictures/Menu/MainMenu/Background.png");
           
            backNotSelected = new Texture("Pictures/Menu/MainMenu/Back/BackNotSelected.png");

            ReturnState = GetThisAsEnum();

            view = new View(new FloatRect(0, 0, Game.WindowSizeX, Game.WindowSizeY));
        }

        /// <summary>
        /// initialize sprites
        /// </summary>
        public virtual void Initialize()
        {
            backGround = new Sprite(backGroundTexture);
            backGround.Position = new Vector2(0, 0);

            Back = new Sprite(backNotSelected);
            Back.Position = GetBackPostion();

            shader = new Shader(null, "Shader/MenuSelectionShader.frag");
            SelectedState = new RenderStates(shader);

            shader.SetParameter("time", 0);
        }

        /// <summary>
        /// updates this gamest
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
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
                selectedSprite = Eselected.Back;
        }

        /// <summary>
        /// draws the sprites
        /// </summary>
        /// <param name="window">window where it should be drawn</param>
        public virtual void Draw(RenderWindow window)
        {
            window.Draw(backGround);
            window.SetView(view);

            if (selectedSprite == Eselected.Back)
                window.Draw(Back, SelectedState);
            else
                window.Draw(Back);
        }
    }
}
