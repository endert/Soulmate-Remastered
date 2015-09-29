using SFML.Graphics;

namespace Soulmate_Remastered.Classes
{
    public enum EnumGameStates
    {
        None = -1,

        MainMenu,
        InGame,
        Credits,
        GameWon,
        Village,
        ControlsSetting,
        Options,
        TitleScreen,
        LoadGame, 

        GameStateCount
    }

    interface GameState
    {
        void Initialize();

        void LoadContent();

        EnumGameStates Update(GameTime time);

        void Draw(RenderWindow window);
    }
}
