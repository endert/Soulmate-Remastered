using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using System.Diagnostics;
using Soulmate_Remastered.Core;

namespace Soulmate_Remastered.Classes.GameStatesFolder
{
    class TitleScreen : GameState
    {
        Texture titleScreenTexture;
        Sprite titleScreen;

        Texture pressEnter;
        Sprite enter;

        RenderStates SelectedState;
        Shader shader;

        View view;

        public void Initialize()
        {
            titleScreen = new Sprite(titleScreenTexture);
           
            enter = new Sprite(pressEnter);
            enter.Position = new Vector2f((Game.WindowSizeX / 2) - (pressEnter.Size.X / 2), (Game.WindowSizeY - pressEnter.Size.Y) - 50);

            shader = new Shader(null, "Shader/MenuSelectionShader.frag");
            SelectedState = new RenderStates(shader);

            view = new View(new FloatRect(0, 0, Game.WindowSizeX, Game.WindowSizeY));
        }

        public void LoadContent()
        {
            titleScreenTexture = new Texture("Pictures/Menu/MainMenu/Background.png");
            pressEnter = new Texture("Pictures/Menu/MainMenu/AnyKey/AnyKeyNotSelected.png");
        }

        public EnumGameStates Update(GameTime gameTime)
        {
            shader.SetParameter("time", gameTime.TotalTime.Seconds * (float)Math.PI);
               
            if (!Game.IsPressed && (MouseControler.IsPressed(Mouse.Button.Left) || NavigationHelp.isAnyKeyPressed()))
            {
                Game.IsPressed = true;
                return EnumGameStates.MainMenu;
            }

            return EnumGameStates.TitleScreen;
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(titleScreen);
            window.Draw(enter, SelectedState);
        }
    }
}
