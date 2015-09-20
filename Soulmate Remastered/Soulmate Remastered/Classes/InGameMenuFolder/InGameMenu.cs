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
        }

        public void Update(GameTime gameTime)
        {
            SetInGameMenuOpen();
            if (InGameMenuOpen)
            {
                Manage();
            }
        }

        public void Manage()
        {
            SetValueToChangeSprite();

            if (NavigationHelp.isSpriteKlicked(selected, 0, Continue, Controls.Return) || (Keyboard.IsKeyPressed(Controls.Escape) && !Game.IsPressed)) //checking if the continue button or escape was pressed
            {
                Game.IsPressed = true;
                InGameMenuOpen = false;
            }

            if (NavigationHelp.isSpriteKlicked(selected, 1, Save, Controls.Return)) //checking if the save button was pressed
            {
                Game.IsPressed = true;
                Console.WriteLine("saving Game");
                SaveGame.SavePath = saveFile;
                SaveGame.SaveTheGame();
                Console.WriteLine("successfuly saved Game");
            }

            //optionsOpen = false; //WHY?!?!?!?!!?!?!!??
            //it does nothing

            if (NavigationHelp.isSpriteKlicked(selected, 2, Options, Controls.Return)) //checking if the options button was pressed
            {
                Game.IsPressed = true;
                InGameMenuOpen = false;
                OptionsOpen = true;
            }

            //closeGame = false; //WHY!?!?!?!?!!?!?!?
            //it does nothing

            if (NavigationHelp.isSpriteKlicked(selected, 3, Exit, Controls.Return)) //checking if the exit button was pressed
            {
                Game.IsPressed = true;
                InGameMenuOpen = false;
                CloseGame = true;
            }

            changeSprites();
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
            if (NavigationHelp.isMouseInSprite(Continue)) //Continue
            {
                selected = Selected.Continue;
            }

            if (NavigationHelp.isMouseInSprite(Save)) //Save
            {
                selected = Selected.Save;
            }

            if (NavigationHelp.isMouseInSprite(Options)) //Options
            {
                selected = Selected.Options;
            }

            if (NavigationHelp.isMouseInSprite(Exit)) //Exit
            {
                selected = Selected.Exit;
            }

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

        /// <summary>
        /// choose which button is selected
        /// </summary>
        private void changeSprites()
        {
            if (selected == Selected.Continue)
            {
                Continue = new Sprite(continueSelected);
                Save = new Sprite(SaveTexture);
                Options = new Sprite(OptionsTexture);
                Exit = new Sprite(ExitTexture);
            }

            if (selected == 1)
            {
                Continue = new Sprite(ContinueTexture);
                Save = new Sprite(saveSelected);
                Options = new Sprite(OptionsTexture);
                Exit = new Sprite(ExitTexture);
            }

            if (selected == 2)
            {
                Continue = new Sprite(ContinueTexture);
                Save = new Sprite(SaveTexture);
                Options = new Sprite(optionsSelected);
                Exit = new Sprite(ExitTexture);
            }

            if (selected == 3)
            {
                Continue = new Sprite(ContinueTexture);
                Save = new Sprite(SaveTexture);
                Options = new Sprite(OptionsTexture);
                Exit = new Sprite(exitSelected);
            }
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(inGameMenuBackGround);
            window.Draw(Continue);
            window.Draw(Save);
            window.Draw(Options);
            window.Draw(Exit);
        }
    }
}
