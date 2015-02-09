using SFML.Graphics;
using SFML.Window;
using SFML.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soulmate_Remastered.Classes;
using Soulmate_Remastered.Classes.GameObjectFolder;

namespace Soulmate_Remastered.Classes
{
    abstract class AbstractGame
    {
        public static RenderWindow window;
        GameTime gameTime;

        public AbstractGame(uint width, uint height, String title)
        {
            window = new RenderWindow(new VideoMode(width, height), title);
            window.Closed += window_Close;

            gameTime = new GameTime();
        }

        public void run()
        {
            gameTime.Start();

            while(window.IsOpen())
            {
                window.Clear(new Color(101, 156, 239)); //CornFlowerBlue
                window.DispatchEvents();
                gameTime.Update();
                update(gameTime);
                draw(window);
                window.Display();
            }
        }

        public abstract void draw(RenderWindow window);

        public abstract void update(GameTime gameTime);

        private void window_Close(Object sender, EventArgs e)
        {
            ((RenderWindow)sender).Close();
        }
    }
}
