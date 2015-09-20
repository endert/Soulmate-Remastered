using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.NPCFolder.ShopFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        /// <summary>
        /// sets InGameMenuOpen == true, if escape is pressed  and no other stuff is open
        /// </summary>
        public void SetInGameMenuOpen()
        {
            if (Keyboard.IsKeyPressed(Controls.Escape) && !Game.IsPressed && !InGameMenuOpen && !PlayerInventory.IsOpen && !Shop.ShopIsOpen)
            {
                Game.IsPressed = true;
                InGameMenuOpen = true;
            }
        }
        
        public InGameMenu()
        {
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
            SetInGameMenuOpen();
            if (InGameMenuOpen)
                Manage();
        }

        /// <summary>
        /// does what happen when wich sprite is selected and then pressed
        /// </summary>
        public void Manage()
        {
            SetValueToChangeSprite();

            if (!Game.IsPressed && Keyboard.IsKeyPressed(Controls.Return))
            {
                Game.IsPressed = true;

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

            if (Keyboard.IsKeyPressed(Controls.Up) && !Game.IsPressed)
            {
                selected = (Selected)(((int)selected + ((int)Selected.Count - 1)) % (int)Selected.Count);
                Game.IsPressed = true;
            }

            if (Keyboard.IsKeyPressed(Controls.Down) && !Game.IsPressed)
            {
                selected = (Selected)(((int)selected + 1) % (int)Selected.Count);
                Game.IsPressed = true;
            }
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
