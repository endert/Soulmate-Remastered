using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.NPCFolder.ShopFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using Soulmate_Remastered.Classes.ItemFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.InGameMenuFolder
{
    class InGameMenu
    {
        Texture inGameMenuBackGroundTexture = new Texture("Pictures/Menu/InGameMenu/InGameMenuBackground.png");
        Sprite inGameMenuBackGround;

        Texture continueNotSelected = new Texture("Pictures/Menu/InGameMenu/Continue/ContinueNotSelected.png");
        Texture continueSelected = new Texture("Pictures/Menu/InGameMenu/Continue/ContinueSelected.png");
        Sprite continueGame;

        Texture saveNotSelected = new Texture("Pictures/Menu/InGameMenu/Save/SaveNotSelected.png");
        Texture saveSelected = new Texture("Pictures/Menu/InGameMenu/Save/SaveSelected.png");
        Sprite save;

        Texture optionsNotSelected = new Texture("Pictures/Menu/MainMenu/Options/OptionsNotSelected.png");
        Texture optionsSelected = new Texture("Pictures/Menu/MainMenu/Options/OptionsSelected.png");
        Sprite options;

        Texture exitNotSelected = new Texture("Pictures/Menu/InGameMenu/Exit/ExitNotSelected.png");
        Texture exitSelected = new Texture("Pictures/Menu/InGameMenu/Exit/ExitSelected.png");
        Sprite exit;

        /// <summary>
        /// Inventory-value which button is selected;
        /// x = 0 Continue;
        /// x = 1 Save;
        /// x = 2 Options;
        /// x = 3 Exit
        /// </summary>
        int x = 0; 

        readonly String saveFile = "Saves/save.soul";
        
        public bool inGameMenuOpen { get; set; }
        public bool closeGame { get; set; }
        public bool optionsOpen { get; set; }

        public Vector2f getInGameMenuBackGroundPosition()
        {
            return new Vector2f((Game.windowSizeX - inGameMenuBackGroundTexture.Size.X) / 2, (Game.windowSizeY - inGameMenuBackGroundTexture.Size.Y) / 2);
        }

        public Vector2f getContinueGamePosition()
        {
            return new Vector2f(inGameMenuBackGround.Position.X + (inGameMenuBackGround.Texture.Size.X / 2) - (continueGame.Texture.Size.X / 2), inGameMenuBackGround.Position.Y + 150);
        }

        public Vector2f getSavePosition()
        {
            return new Vector2f(getContinueGamePosition().X, inGameMenuBackGround.Position.Y + 250);
        }

        public Vector2f getOptionsPosition()
        {
            return new Vector2f(getContinueGamePosition().X, inGameMenuBackGround.Position.Y + 350);
        }

        public Vector2f getExitPosition()
        {
            return new Vector2f(getContinueGamePosition().X, inGameMenuBackGround.Position.Y + 450);
        }

        public bool getInGameMenuOpen()
        {
            if (Keyboard.IsKeyPressed(Controls.Escape) && !Game.isPressed && !inGameMenuOpen && !Inventory.inventoryOpen && !Shop.shopIsOpen)
            {
                Game.isPressed = true;
                inGameMenuOpen = true;
            }
            return inGameMenuOpen;
        }
        
        public InGameMenu()
        {
            inGameMenuBackGround = new Sprite(inGameMenuBackGroundTexture);
            inGameMenuBackGround.Position = getInGameMenuBackGroundPosition();

            continueGame = new Sprite(continueSelected);
            continueGame.Position = getContinueGamePosition();

            save = new Sprite(saveNotSelected);
            save.Position = getSavePosition();

            options = new Sprite(optionsNotSelected);
            options.Position = getOptionsPosition();

            exit = new Sprite(exitNotSelected);
            exit.Position = getExitPosition();
        }

        public void update(GameTime gameTime)
        {
            getInGameMenuOpen();
            if (inGameMenuOpen)
            {
                manage();
            }
        }

        public void manage()
        {
            setValueToChangeSprite();

            if (NavigationHelp.isSpriteKlicked(x, 0, continueGame, Controls.Return) || (Keyboard.IsKeyPressed(Controls.Escape) && !Game.isPressed)) //checking if the continue button or escape was pressed
            {
                Game.isPressed = true;
                inGameMenuOpen = false;
            }

            if (NavigationHelp.isSpriteKlicked(x, 1, save, Controls.Return)) //checking if the save button was pressed
            {
                Game.isPressed = true;
                Console.WriteLine("saving Game");
                SaveGame.savePath = saveFile;
                SaveGame.saveGame();
                Console.WriteLine("successfuly saved Game");
            }

            //optionsOpen = false; //WHY?!?!?!?!!?!?!!??
            //it does nothing
            
            if (NavigationHelp.isSpriteKlicked(x, 2, options, Controls.Return)) //checking if the options button was pressed
            {
                Game.isPressed = true;
                inGameMenuOpen = false;
                optionsOpen = true;
            }

            //closeGame = false; //WHY!?!?!?!?!!?!?!?
            //it does nothing
            
            if (NavigationHelp.isSpriteKlicked(x, 3, exit, Controls.Return)) //checking if the exit button was pressed
            {
                Game.isPressed = true;
                inGameMenuOpen = false;
                closeGame = true;
            }

            changeSprites();
            spritePositionUpdate();
        }

        private void spritePositionUpdate()
        {
            continueGame.Position = getContinueGamePosition();
            save.Position = getSavePosition();
            options.Position = getOptionsPosition();
            exit.Position = getExitPosition();
        }

        private void setValueToChangeSprite()
        {
            if (NavigationHelp.isMouseInSprite(continueGame)) //Continue
            {
                x = 0;
            }

            if (NavigationHelp.isMouseInSprite(save)) //Save
            {
                x = 1;
            }

            if (NavigationHelp.isMouseInSprite(options)) //Options
            {
                x = 2;
            }

            if (NavigationHelp.isMouseInSprite(exit)) //Exit
            {
                x = 3;
            }

            if (Keyboard.IsKeyPressed(Controls.Up) && !Game.isPressed)
            {
                x = (x + 3) % 4;
                Game.isPressed = true;
            }

            if (Keyboard.IsKeyPressed(Controls.Down) && !Game.isPressed)
            {
                x = (x + 1) % 4;
                Game.isPressed = true;
            }
        }

        /// <summary>
        /// choose which button is selected
        /// </summary>
        private void changeSprites()
        {
            if (x == 0)
            {
                continueGame = new Sprite(continueSelected);
                save = new Sprite(saveNotSelected);
                options = new Sprite(optionsNotSelected);
                exit = new Sprite(exitNotSelected);
            }

            if (x == 1)
            {
                continueGame = new Sprite(continueNotSelected);
                save = new Sprite(saveSelected);
                options = new Sprite(optionsNotSelected);
                exit = new Sprite(exitNotSelected);
            }

            if (x == 2)
            {
                continueGame = new Sprite(continueNotSelected);
                save = new Sprite(saveNotSelected);
                options = new Sprite(optionsSelected);
                exit = new Sprite(exitNotSelected);
            }

            if (x == 3)
            {
                continueGame = new Sprite(continueNotSelected);
                save = new Sprite(saveNotSelected);
                options = new Sprite(optionsNotSelected);
                exit = new Sprite(exitSelected);
            }
        }

        public void draw(RenderWindow window)
        {
            window.Draw(inGameMenuBackGround);
            window.Draw(continueGame);
            window.Draw(save);
            window.Draw(options);
            window.Draw(exit);
        }
    }
}
