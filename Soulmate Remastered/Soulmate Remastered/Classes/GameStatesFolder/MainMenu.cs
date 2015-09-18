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
    class MainMenu : AbstractMainMenu
    {
        /// <summary>
        /// number of sprites that can be selected
        /// </summary>
        int CountSprites = Eselected.MainMenuCount - Eselected.MainMenuOffset - 1;

        Texture StartSpriteTexture;
        Texture OptionsSpriteTexture;
        Texture controlsNotSelected;
        Texture creditsNotSelected;
        Texture endNotSelected;

        Sprite StartSprite;
        Sprite OptionsSprite;
        Sprite ControlsSprite;
        Sprite CreditsSprite;
        Sprite EndSprite;
       
        /// <summary>
        /// initialize the sprites
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            StartSprite = new Sprite(StartSpriteTexture);
            StartSprite.Position = new Vector2(300, 300);

            OptionsSprite = new Sprite(OptionsSpriteTexture);
            OptionsSprite.Position = new Vector2(StartSprite.Position.X, StartSprite.Position.Y + 75 * 1);

            ControlsSprite = new Sprite(controlsNotSelected);
            ControlsSprite.Position = new Vector2(StartSprite.Position.X, StartSprite.Position.Y + 75 * 2);

            CreditsSprite = new Sprite(creditsNotSelected);
            CreditsSprite.Position = new Vector2(StartSprite.Position.X, StartSprite.Position.Y + 75 * 3);

            EndSprite = new Sprite(endNotSelected);
            EndSprite.Position = new Vector2(StartSprite.Position.X, StartSprite.Position.Y + 75 * 4);
        }

        /// <summary>
        /// Loads all textures needed for initialization
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();

            StartSpriteTexture = new Texture("Pictures/Menu/MainMenu/Start/StartNotSelected.png");

            OptionsSpriteTexture = new Texture("Pictures/Menu/MainMenu/Options/OptionsNotSelected.png");

            creditsNotSelected = new Texture("Pictures/Menu/MainMenu/Credits/CreditsNotSelected.png");

            controlsNotSelected = new Texture("Pictures/Menu/MainMenu/Controls/ControlsNotSelected.png");

            endNotSelected = new Texture("Pictures/Menu/MainMenu/End/EndNotSelected.png");   
        }

        /// <summary>
        /// updates the current gamestate
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public override EnumGameStates Update(GameTime gameTime)
        {
            GameUpdate(gameTime);

            if (MouseControler.MouseIn(StartSprite))
                selectedSprite = Eselected.StartSprite;

            if (MouseControler.MouseIn(OptionsSprite))
                selectedSprite = Eselected.OptionsSprite;

            if (MouseControler.MouseIn(ControlsSprite))
                selectedSprite = Eselected.ControlsSprite;

            if (MouseControler.MouseIn(CreditsSprite))
                selectedSprite = Eselected.CreditsSprite;

            if (MouseControler.MouseIn(EndSprite))
                selectedSprite = Eselected.EndSprite;

            if (Keyboard.IsKeyPressed(Controls.Up) && !Game.isPressed)
            {
                if (selectedSprite == Eselected.None)
                    selectedSprite = Eselected.StartSprite;
                else
                    selectedSprite = (Eselected)((((int)(selectedSprite - Eselected.MainMenuOffset - 1) + CountSprites - 1) % CountSprites) + Eselected.MainMenuOffset + 1);
                Game.isPressed = true;
            }

            if (Keyboard.IsKeyPressed(Controls.Down) && !Game.isPressed)
            {
                selectedSprite = (Eselected)((((int)(selectedSprite - Eselected.MainMenuOffset - 1) + 1) % CountSprites) + (Eselected.MainMenuOffset + 1));
                Game.isPressed = true;
            }

            if (!Game.isPressed && Keyboard.IsKeyPressed(Controls.Return))
            {
                Game.isPressed = true;

                switch (selectedSprite) {
                    case Eselected.StartSprite:
                        ReturnState = EnumGameStates.LoadGame;
                        break;
                    case Eselected.OptionsSprite:
                        Console.WriteLine("load Options");
                        ReturnState = EnumGameStates.Options;
                        break;
                    case Eselected.ControlsSprite:
                        Console.WriteLine("load Controls");
                        ReturnState = EnumGameStates.ControlsSetting;
                        break;
                    case Eselected.CreditsSprite:
                        Console.WriteLine("load Credits");
                        ReturnState = EnumGameStates.Credits;
                        break;
                    case Eselected.EndSprite:
                        ReturnState = EnumGameStates.None;
                        break;
                }
            }

            return ReturnState;
        }

        /// <summary>
        /// draws all sprites
        /// </summary>
        /// <param name="window"></param>
        public override void Draw(RenderWindow window)
        {
            base.Draw(window);

            if (selectedSprite == Eselected.StartSprite)
                window.Draw(StartSprite, SelectedState);
            else
                window.Draw(StartSprite);

            if (selectedSprite == Eselected.OptionsSprite)
                window.Draw(OptionsSprite, SelectedState);
            else
                window.Draw(OptionsSprite);

            if (selectedSprite == Eselected.ControlsSprite)
                window.Draw(ControlsSprite, SelectedState);
            else
                window.Draw(ControlsSprite);

            if (selectedSprite == Eselected.CreditsSprite)
                window.Draw(CreditsSprite, SelectedState);
            else
                window.Draw(CreditsSprite);

            if (selectedSprite == Eselected.EndSprite)
                window.Draw(EndSprite, SelectedState);
            else
                window.Draw(EndSprite);
        }
    }
}
