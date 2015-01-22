using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes
{
    public enum EnumGameStates
    {
        none,
        mainMenu,
        inGame,
        credits,
        gameWon,
        village,
        controls,
        options,
        titleSreen
    }

    interface GameState
    {
        void initialize();

        void loadContent();

        EnumGameStates update(GameTime time);

        void draw(RenderWindow window);
    }
}
