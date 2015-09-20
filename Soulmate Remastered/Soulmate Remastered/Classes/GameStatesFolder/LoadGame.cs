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

        public override void Initialize()
        {
            base.Initialize();
            
            Load = new Sprite(loadNotSelected);
            Load.Position = new Vector2f(300, 300);

            NewGame = new Sprite(newGameNotSelected);
            NewGame.Position = new Vector2f(Load.Position.X, Load.Position.Y + 150);
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

            if (MouseControler.MouseIn(Load))
                selectedSprite = Eselected.Load;

            if (MouseControler.MouseIn(NewGame))
                selectedSprite = Eselected.NewGame;

            if (Keyboard.IsKeyPressed(Controls.Up) && !Game.IsPressed)
            {
                selectedSprite = (Eselected)(((int)((selectedSprite - CountOffset) + SpriteCount - 1) % (int)SpriteCount) + CountOffset);
                Game.IsPressed = true;
            }

            if (Keyboard.IsKeyPressed(Controls.Down) && !Game.IsPressed)
            {
                selectedSprite = (Eselected)(((int)((selectedSprite - CountOffset) + 1) % (int)SpriteCount) + CountOffset);
                Game.IsPressed = true;
            }

            if (selectedSprite == Eselected.Load)
            {
                Load.Texture = loadSelected;
                NewGame.Texture = newGameNotSelected;

                if(!Game.IsPressed && Keyboard.IsKeyPressed(Controls.Return))
                {
                    Game.IsPressed = true;
                    AbstractGamePlay.loading = true;
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
                }

            }

            if (selectedSprite == Eselected.NewGame)
            {
                Load.Texture = loadNotSelected;
                NewGame.Texture = newGameSelected;

                if (!Game.IsPressed && Keyboard.IsKeyPressed(Controls.Return))
                {
                    Game.IsPressed = true;
                    Console.WriteLine("new Game");
                    AbstractGamePlay.startNewGame = true;
                    ReturnState = EnumGameStates.Village;
                }
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
