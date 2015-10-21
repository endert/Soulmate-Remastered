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

<<<<<<< HEAD
        void OnKeyPress(object sender, KeyEventArgs e)
=======
        public void SetInGameMenuOpen()
>>>>>>> parent of 40ea887... load game does now function correctly
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
        }

        public void Update(GameTime gameTime)
        {
            if (InGameMenuOpen)
            {
                Manage();
            }
        }

        public void Manage()
        {
            SetValueToChangeSprite();

<<<<<<< HEAD
=======
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
>>>>>>> parent of 40ea887... load game does now function correctly
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
<<<<<<< HEAD
=======
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
>>>>>>> parent of 40ea887... load game does now function correctly
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
