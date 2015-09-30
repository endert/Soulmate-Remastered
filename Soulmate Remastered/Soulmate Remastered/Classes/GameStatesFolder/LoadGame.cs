using SFML.Graphics;
using Soulmate_Remastered.Classes.GameObjectFolder;
using Soulmate_Remastered.Core;
using System;

namespace Soulmate_Remastered.Classes.GameStatesFolder
{
    class LoadGame : AbstractMenu
    {
        private readonly string loadFile = "Saves/save.soul";

        Texture loadSelected;
        Texture loadNotSelected;
        Sprite Load;

        Texture newGameSelected;
        Texture newGameNotSelected;
        Sprite NewGame;

        int CountOffset = (int)Eselected.LoadOffset + 1;
        int SpriteCount = Eselected.LoadSpriteCount - Eselected.LoadOffset - 1;

        protected override void OnKeyPress(object sender, Core.KeyEventArgs e)
        {
            base.OnKeyPress(sender, e);

            if (selectedSprite == Eselected.None)
                selectedSprite = Eselected.Load;
            else
            {
                if (e.Key == Controls.Key.Up)
                    selectedSprite = (Eselected)(((int)((selectedSprite - CountOffset) + SpriteCount - 1) % (int)SpriteCount) + CountOffset);

                if (e.Key == Controls.Key.Down)
                    selectedSprite = (Eselected)(((int)((selectedSprite - CountOffset) + 1) % (int)SpriteCount) + CountOffset);

                if (e.Key == Controls.Key.Return)
                {
                    KeyboardControler.KeyPressed -= OnKeyPress;

                    switch (selectedSprite)
                    {
                        case Eselected.Load:
                            Load_();
                            break;
                        case Eselected.NewGame:
                            NewGame_();
                            break;
                    }
                }
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            
            Load = new Sprite(loadNotSelected);
            Load.Position = new Vector2(300, 300);

            NewGame = new Sprite(newGameNotSelected);
            NewGame.Position = new Vector2(Load.Position.X, Load.Position.Y + 150);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            
            loadSelected = new Texture("Pictures/Menu/MainMenu/Load/LoadSelected.png");
            loadNotSelected = new Texture("Pictures/Menu/MainMenu/Load/LoadNotSelected.png");

            newGameSelected = new Texture("Pictures/Menu/MainMenu/NewGame/NewGameSelected.png");
            newGameNotSelected = new Texture("Pictures/Menu/MainMenu/NewGame/NewGameNotSelected.png");
        }

        /// <summary>
        /// Set up a new game
        /// </summary>
        void NewGame_()
        {
            Console.WriteLine("new Game");
            AbstractGamePlay.startNewGame = true;
            ReturnState = EnumGameStates.Village;
        }

        /// <summary>
        /// Set up a loaded Game
        /// </summary>
        void Load_()
        {
            AbstractGamePlay.loading = true;
            SaveGame.LoadPath = loadFile;
            SaveGame.LoadMapLvl();

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
        }

        public override EnumGameStates Update(GameTime gameTime)
        {
            GameUpdate(gameTime);

            if (MouseControler.MouseIn(Load))
                selectedSprite = Eselected.Load;

            if (MouseControler.MouseIn(NewGame))
                selectedSprite = Eselected.NewGame;

            if (selectedSprite == Eselected.Load)
            {
                Load.Texture = loadSelected;
                NewGame.Texture = newGameNotSelected;
            }

            if (selectedSprite == Eselected.NewGame)
            {
                Load.Texture = loadNotSelected;
                NewGame.Texture = newGameSelected;
            }       

            return ReturnState;
        }

        public override void Draw(RenderWindow window)
        {
            base.Draw(window);

            window.Draw(Load);
            window.Draw(NewGame);
            window.Draw(Back);
        }
    }
}
