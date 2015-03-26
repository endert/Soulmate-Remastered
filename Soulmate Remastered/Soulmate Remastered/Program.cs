using Soulmate_Remastered.Classes;
using Soulmate_Remastered.Classes.GameStatesFolder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.run();

            File.Delete(AbstractGamePlay.savePlayerPath);
        }
    }
}
