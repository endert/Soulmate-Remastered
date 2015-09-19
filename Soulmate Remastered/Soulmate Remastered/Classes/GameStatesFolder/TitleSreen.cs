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
            enter.Position = new Vector2f((Game.windowSizeX / 2) - (pressEnter.Size.X / 2), (Game.windowSizeY - pressEnter.Size.Y) - 50);

            shader = new Shader(null, "Shader/MenuSelectionShader.frag");
            SelectedState = new RenderStates(shader);

            view = new View(new FloatRect(0, 0, Game.windowSizeX, Game.windowSizeY));
        }

        public void LoadContent()
        {
            titleScreenTexture = new Texture("Pictures/Menu/MainMenu/Background.png");
            pressEnter = new Texture("Pictures/Menu/MainMenu/AnyKey/AnyKeyNotSelected.png");
        }

        public EnumGameStates Update(GameTime gameTime)
        {
            shader.SetParameter("time", gameTime.TotalTime.Seconds * (float)Math.PI);
               
            if (!Game.isPressed && (MouseControler.IsPressed(Mouse.Button.Left) || NavigationHelp.isAnyKeyPressed()))
            {
                Game.isPressed = true;
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
