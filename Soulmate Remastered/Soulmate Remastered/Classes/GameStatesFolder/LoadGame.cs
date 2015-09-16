using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder;
using Soulmate_Remastered.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameStatesFolder
{
    class LoadGame : AbstractMainMenu
    {
        private readonly string loadFile = "Saves/save.soul";

        Texture loadSelected;
        Texture loadNotSelected;
        Sprite load;

        Texture newGameSelected;
        Texture newGameNotSelected;
        Sprite newGame;

        int CountOffset = (int)Eselected.LoadOffset + 1;
        int SpriteCount = Eselected.LoadSpriteCount - Eselected.LoadOffset - 1;

        public override void Initialize()
        {
            base.Initialize();
            
            load = new Sprite(loadNotSelected);
            load.Position = new Vector2f(300, 300);

            newGame = new Sprite(newGameNotSelected);
            newGame.Position = new Vector2f(load.Position.X, load.Position.Y + 150);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            
            loadSelected = new Texture("Pictures/Menu/MainMenu/Load/LoadSelected.png");
            loadNotSelected = new Texture("Pictures/Menu/MainMenu/Load/LoadNotSelected.png");

            newGameSelected = new Texture("Pictures/Menu/MainMenu/NewGame/NewGameSelected.png");
            newGameNotSelected = new Texture("Pictures/Menu/MainMenu/NewGame/NewGameNotSelected.png");
        }

        public override EnumGameStates Update(GameTime gameTime)
        {
            GameUpdate(gameTime);

            if (MouseControler.MouseIn(load))
                selectedSprite = Eselected.LoadGame;

            if (MouseControler.MouseIn(newGame))
                selectedSprite = Eselected.NewGame;

            if (Keyboard.IsKeyPressed(Controls.Up) && !Game.isPressed)
            {
                selectedSprite = (Eselected)(((int)((selectedSprite - CountOffset) + SpriteCount - 1) % (int)SpriteCount) + CountOffset);
                Game.isPressed = true;
            }

            if (Keyboard.IsKeyPressed(Controls.Down) && !Game.isPressed)
            {
                selectedSprite = (Eselected)(((int)((selectedSprite - CountOffset) + 1) % (int)SpriteCount) + CountOffset);
                Game.isPressed = true;
            }

            if (selectedSprite == Eselected.LoadGame)
            {
                load.Texture = loadSelected;
                newGame.Texture = newGameNotSelected;

                if(!Game.isPressed && (MouseControler.IsPressed(load, Mouse.Button.Left) || Keyboard.IsKeyPressed(Controls.Return)))
                {
                    Game.isPressed = true;
                    AbstractGamePlay.loading = true;
                    SaveGame.loadPath = loadFile;
                    SaveGame.loadGame();

                    switch (GameObjectHandler.lvl)
                    {
                        case 0:
                            return EnumGameStates.Village;
                        case 1:
                            return EnumGameStates.InGame;
                        default:
                            return EnumGameStates.Village;
                    }
                }

            }

            if (selectedSprite == Eselected.NewGame)
            {
                load.Texture = loadNotSelected;
                newGame.Texture = newGameSelected;


            }

            if (NavigationHelp.isSpriteKlicked((int)selectedSprite, 1, newGame, Controls.Return))
            {
                Game.isPressed = true;
                Console.WriteLine("new Game");
                AbstractGamePlay.startNewGame = true;
                ReturnState = EnumGameStates.Village;
            }           

            return ReturnState;
        }

        public override void Draw(RenderWindow window)
        {
            base.Draw(window);

            window.Draw(load);
            window.Draw(newGame);
            window.Draw(Back);
        }
    }
}
