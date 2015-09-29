using SFML.Graphics;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.NPCFolder.ShopFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using System;
using Soulmate_Remastered.Core;

namespace Soulmate_Remastered.Classes.InGameMenuFolder
{
    class InGameMenu
    {
        Texture inGameMenuBackGroundTexture = new Texture("Pictures/Menu/InGameMenu/InGameMenuBackground.png");
        Sprite inGameMenuBackGround;

        Texture ContinueTexture = new Texture("Pictures/Menu/InGameMenu/Continue/ContinueNotSelected.png");
        Sprite Continue;

        Texture SaveTexture = new Texture("Pictures/Menu/InGameMenu/Save/SaveNotSelected.png");
        Sprite Save;

        Texture OptionsTexture = new Texture("Pictures/Menu/MainMenu/Options/OptionsNotSelected.png");
        Sprite Options;

        Texture ExitTexture = new Texture("Pictures/Menu/InGameMenu/Exit/ExitNotSelected.png");
        Sprite Exit;

        /// <summary>
        /// state that contains the seleceted shader
        /// </summary>
        protected RenderStates SelectedState;
        Shader shader;

        /// <summary>
        /// the selected sprite
        /// </summary>
        Selected selected = 0;

        enum Selected
        {
            None = -1,

            Continue,
            Save,
            Options,
            Exit,

            Count
        }

        /// <summary>
        /// the destined save path
        /// </summary>
        readonly string saveFile = "Saves/save.soul";

        /// <summary>
        /// bool if InGameMenu is open
        /// </summary>
        public bool InGameMenuOpen { get; private set; }
        /// <summary>
        /// bool if the game should be closed
        /// </summary>
        public bool CloseGame { get; private set; }
        /// <summary>
        /// bool if options is open
        /// </summary>
        public bool OptionsOpen { get; private set; }

        Vector2 GetInGameMenuBackGroundPosition()
        {
            return new Vector2((Game.WindowSizeX - inGameMenuBackGroundTexture.Size.X) / 2, (Game.WindowSizeY - inGameMenuBackGroundTexture.Size.Y) / 2);
        }

        Vector2 GetContinueGamePosition()
        {
            return new Vector2(inGameMenuBackGround.Position.X + (inGameMenuBackGround.Texture.Size.X / 2) - (Continue.Texture.Size.X / 2), inGameMenuBackGround.Position.Y + 150);
        }

        Vector2 GetSavePosition()
        {
            return new Vector2(GetContinueGamePosition().X, inGameMenuBackGround.Position.Y + 250);
        }

        Vector2 GetOptionsPosition()
        {
            return new Vector2(GetContinueGamePosition().X, inGameMenuBackGround.Position.Y + 350);
        }

        Vector2 GetExitPosition()
        {
            return new Vector2(GetContinueGamePosition().X, inGameMenuBackGround.Position.Y + 450);
        }

        void OnKeyPress(object sender, KeyEventArgs e)
        {
            Console.WriteLine("InGameMenu");

            if (e.Key == Controls.Key.Escape)
                if (!PlayerInventory.IsOpen && !Shop.ShopIsOpen)
                    InGameMenuOpen = !InGameMenuOpen;

            if (InGameMenuOpen)
            {
                if (e.Key == Controls.Key.Return)
                {
                    switch (selected)
                    {
                        case Selected.Continue:
                            InGameMenuOpen = false;
                            break;
                        case Selected.Save:
                            Console.WriteLine("saving Game");
                            SaveGame.SavePath = saveFile;
                            SaveGame.SaveTheGame();
                            Console.WriteLine("successfuly saved Game");
                            break;
                        case Selected.Options:
                            InGameMenuOpen = false;
                            OptionsOpen = true;
                            break;
                        case Selected.Exit:
                            InGameMenuOpen = false;
                            CloseGame = true;
                            break;
                    }
                }

                if (e.Key == Controls.Key.Up)
                    selected = (Selected)(((int)selected + ((int)Selected.Count - 1)) % (int)Selected.Count);

                if (e.Key == Controls.Key.Down)
                    selected = (Selected)(((int)selected + 1) % (int)Selected.Count);
            }
        }
        
        public InGameMenu()
        {
            KeyboardControler.KeyPressed += OnKeyPress;

            inGameMenuBackGround = new Sprite(inGameMenuBackGroundTexture);
            inGameMenuBackGround.Position = GetInGameMenuBackGroundPosition();

            Continue = new Sprite(ContinueTexture);
            Continue.Position = GetContinueGamePosition();

            Save = new Sprite(SaveTexture);
            Save.Position = GetSavePosition();

            Options = new Sprite(OptionsTexture);
            Options.Position = GetOptionsPosition();

            Exit = new Sprite(ExitTexture);
            Exit.Position = GetExitPosition();

            shader = new Shader(null, "Shader/MenuSelectionShader.frag");
            SelectedState = new RenderStates(shader);
        }

        public void Update(GameTime gameTime)
        {
            if (InGameMenuOpen)
                Manage();
        }

        /// <summary>
        /// does what happen when wich sprite is selected and then pressed
        /// </summary>
        public void Manage()
        {
            SetValueToChangeSprite();

            SpritePositionUpdate();
        }

        private void SpritePositionUpdate()
        {
            Continue.Position = GetContinueGamePosition();
            Save.Position = GetSavePosition();
            Options.Position = GetOptionsPosition();
            Exit.Position = GetExitPosition();
        }

        private void SetValueToChangeSprite()
        {
            if (MouseControler.MouseIn(Continue)) //Continue
                selected = Selected.Continue;

            if (MouseControler.MouseIn(Save)) //Save
                selected = Selected.Save;

            if (MouseControler.MouseIn(Options)) //Options
                selected = Selected.Options;

            if (MouseControler.MouseIn(Exit)) //Exit
                selected = Selected.Exit;
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(inGameMenuBackGround);

            if (selected == Selected.Continue)
                window.Draw(Continue, SelectedState);
            else
                window.Draw(Continue);

            if (selected == Selected.Save)
                window.Draw(Save, SelectedState);
            else
                window.Draw(Save);

            if (selected == Selected.Options)
                window.Draw(Options, SelectedState);
            else
                window.Draw(Options);

            if (selected == Selected.Exit)
                window.Draw(Exit, SelectedState);
            else
                window.Draw(Exit);
        }
    }
}
