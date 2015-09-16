using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using Soulmate_Remastered.Classes.GameStatesFolder;

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
