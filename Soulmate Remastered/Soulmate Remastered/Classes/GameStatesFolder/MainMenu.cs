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
        int CountSprites = Eselected.MainMenuCount - Eselected.MainMenuOffset - 1;

        Texture startSelected;
        Texture startNotSelected;
                        
        Texture optionsSelected;
        Texture optionsNotSelected;

        Texture controlsSelected;
        Texture controlsNotSelected;

        Texture creditsSelected;
        Texture creditsNotSelected;

        Texture endSelected;
        Texture endNotSelected;

        Sprite StartSprite;
        Sprite OptionsButton;
        Sprite Controls;
        Sprite Credits;
        Sprite End;
       
        public override void Initialize()
        {
            base.Initialize();

            StartSprite = new Sprite(startNotSelected);
            StartSprite.Position = new Vector2f(300, 300);

            OptionsButton = new Sprite(optionsNotSelected);
            OptionsButton.Position = new Vector2f(StartSprite.Position.X, StartSprite.Position.Y + 75 * 1);

            Controls = new Sprite(controlsNotSelected);
            Controls.Position = new Vector2f(StartSprite.Position.X, StartSprite.Position.Y + 75 * 2);

            Credits = new Sprite(creditsNotSelected);
            Credits.Position = new Vector2f(StartSprite.Position.X, StartSprite.Position.Y + 75 * 3);

            End = new Sprite(endNotSelected);
            End.Position = new Vector2f(StartSprite.Position.X, StartSprite.Position.Y + 75 * 4);

            selectedSprite = Eselected.StartSprite;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            startSelected = new Texture("Pictures/Menu/MainMenu/Start/StartSelected.png");
            startNotSelected = new Texture("Pictures/Menu/MainMenu/Start/StartNotSelected.png");

            optionsSelected = new Texture("Pictures/Menu/MainMenu/Options/OptionsSelected.png");
            optionsNotSelected = new Texture("Pictures/Menu/MainMenu/Options/OptionsNotSelected.png");

            creditsSelected = new Texture("Pictures/Menu/MainMenu/Credits/CreditsSelected.png");
            creditsNotSelected = new Texture("Pictures/Menu/MainMenu/Credits/CreditsNotSelected.png");

            controlsSelected = new Texture("Pictures/Menu/MainMenu/Controls/ControlsSelected.png");
            controlsNotSelected = new Texture("Pictures/Menu/MainMenu/Controls/ControlsNotSelected.png");

            endSelected = new Texture("Pictures/Menu/MainMenu/End/EndSelected.png");
            endNotSelected = new Texture("Pictures/Menu/MainMenu/End/EndNotSelected.png");   
        }

        public override EnumGameStates Update(GameTime gameTime)
        {
            GameUpdate(gameTime);

            if (MouseControler.MouseIn(StartSprite))
                selectedSprite = Eselected.StartSprite;

            if (MouseControler.MouseIn(OptionsButton))
                selectedSprite = Eselected.OptionsButton;

            if (MouseControler.MouseIn(Controls))
                selectedSprite = Eselected.Controls;

            if (MouseControler.MouseIn(Credits))
                selectedSprite = Eselected.Credits;

            if (MouseControler.MouseIn(End))
                selectedSprite = Eselected.End;

            if (Keyboard.IsKeyPressed(Classes.Controls.Up) && !Game.isPressed)
            {
                selectedSprite = (Eselected)((((int)(selectedSprite - Eselected.MainMenuOffset - 1) + CountSprites - 1) % CountSprites) + Eselected.MainMenuOffset + 1);
                Game.isPressed = true;
            }

            if (Keyboard.IsKeyPressed(Classes.Controls.Down) && !Game.isPressed)
            {
                selectedSprite = (Eselected)((((int)(selectedSprite - Eselected.MainMenuOffset - 1) + 1) % CountSprites) + (Eselected.MainMenuOffset + 1));
                Game.isPressed = true;
            }

            if (selectedSprite == Eselected.StartSprite)
            {
                StartSprite.Texture = startSelected;
                OptionsButton.Texture = optionsNotSelected;
                Controls.Texture = controlsNotSelected;
                Credits.Texture = creditsNotSelected;
                End.Texture = endNotSelected;
            }

            if (selectedSprite == Eselected.OptionsButton)
            {
                StartSprite.Texture = startNotSelected;
                OptionsButton.Texture = optionsSelected;
                Controls.Texture = controlsNotSelected;
                Credits.Texture = creditsNotSelected;
                End.Texture = endNotSelected;
            }

            if (selectedSprite == Eselected.Controls)
            {
                StartSprite.Texture = startNotSelected;
                OptionsButton.Texture = optionsNotSelected;
                Controls.Texture = controlsSelected;
                Credits.Texture = creditsNotSelected;
                End.Texture = endNotSelected;
            }

            if (selectedSprite == Eselected.Credits)
            {
                StartSprite.Texture = startNotSelected;
                OptionsButton.Texture = optionsNotSelected;
                Controls.Texture = controlsNotSelected;
                Credits.Texture = creditsSelected;
                End.Texture = endNotSelected;
            }

            if (selectedSprite == Eselected.End)
            {
                StartSprite.Texture = startNotSelected;
                OptionsButton.Texture = optionsNotSelected;
                Controls.Texture = controlsNotSelected;
                Credits.Texture = creditsNotSelected;
                End.Texture = endSelected;
            }

            if (NavigationHelp.isSpriteKlicked((int)selectedSprite, 0, StartSprite, Classes.Controls.Return))
            {
                Game.isPressed = true;
                ReturnState = EnumGameStates.LoadGame;
            }
            if (NavigationHelp.isSpriteKlicked((int)selectedSprite, 1, OptionsButton, Classes.Controls.Return))
            {
                Game.isPressed = true;
                Console.WriteLine("load Options");
                ReturnState = EnumGameStates.Options;
            }
            if (NavigationHelp.isSpriteKlicked((int)selectedSprite, 2, Controls, Classes.Controls.Return))
            {
                Game.isPressed = true;
                Console.WriteLine("load Controls");
                ReturnState = EnumGameStates.ControlsSetting;
            }
            if (NavigationHelp.isSpriteKlicked((int)selectedSprite, 3, Credits, Classes.Controls.Return))
            {
                Game.isPressed = true;
                Console.WriteLine("load Credits");
                ReturnState = EnumGameStates.Credits;
            }
            if (NavigationHelp.isSpriteKlicked((int)selectedSprite, 4, End, Classes.Controls.Return))
            {
                Game.isPressed = true;
                ReturnState = EnumGameStates.None;
            }

            return ReturnState;
        }

        public override void Draw(RenderWindow window)
        {
            base.Draw(window);

            window.Draw(StartSprite);
            window.Draw(OptionsButton);
            window.Draw(Controls);
            window.Draw(Credits);
            window.Draw(End);
        }
    }
}
