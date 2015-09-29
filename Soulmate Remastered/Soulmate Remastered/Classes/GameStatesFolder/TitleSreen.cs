using System;
using SFML.Graphics;
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
        EnumGameStates ReturnState;

        void OnKeyPress(object sender, KeyEventArgs e)
        {
            ReturnState = EnumGameStates.MainMenu;
            KeyboardControler.KeyPressed -= OnKeyPress;
        }

        void OnButtonPress(object sender, MouseButtonEventArgs e)
        {
            ReturnState = EnumGameStates.MainMenu;
            MouseControler.ButtonPressed -= OnButtonPress;
        }

        public void Initialize()
        {
            KeyboardControler.KeyPressed += OnKeyPress;
            MouseControler.ButtonPressed += OnButtonPress;

            ReturnState = EnumGameStates.TitleScreen;
            titleScreen = new Sprite(titleScreenTexture);
           
            enter = new Sprite(pressEnter);
            enter.Position = new Vector2((Game.WindowSizeX / 2) - (pressEnter.Size.X / 2), (Game.WindowSizeY - pressEnter.Size.Y) - 50);

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

            return ReturnState;
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(titleScreen);
            window.Draw(enter, SelectedState);
        }
    }
}
