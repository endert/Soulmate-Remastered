using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered;
using Soulmate_Remastered.Classes;

namespace Soulmate_Remastered.Core
{
    class MouseControler
    {
        /// <summary>
        /// event that fires when a button is relized
        /// </summary>
        public static event EventHandler<MouseButtonEventArgs> Relize;

        /// <summary>
        /// is triggered once when a button is pressed
        /// </summary>
        public static event EventHandler<MouseButtonEventArgs> ButtonPressed;

        static void OnRealize(MouseButtonEvent b)
        {
            EventHandler<MouseButtonEventArgs> handler = Relize;
            if (Relize != null)
                handler(null, new MouseButtonEventArgs(b));
        }

        static void OnButtonPress(MouseButtonEvent e)
        {
            EventHandler <MouseButtonEventArgs> handler = ButtonPressed;
            if (handler != null)
                handler(null, new MouseButtonEventArgs(e));
        }

        static bool[] isPressed;
        static bool[] wasPressed;


        public static bool IsPressed(Mouse.Button b)
        {
            return isPressed[(int)b];
        }

        /// <summary>
        /// initialize the controller
        /// </summary>
        public static void Initialize()
        {
            DebugInitialize();

            isPressed = new bool[(int)Mouse.Button.ButtonCount];
            for (int i = 0; i < isPressed.Length; ++i)
                isPressed[i] = false;

            wasPressed = new bool[(int)Mouse.Button.ButtonCount];
            for (int i = 0; i < wasPressed.Length; ++i)
                wasPressed[i] = false;
        }

        private static void DebugInitialize()
        {
            Relize += (sender, ev) => { Console.WriteLine("Relized " + ev.Button); };
            ButtonPressed += (sender, ev) => { Console.WriteLine("Pressed " + ev.Button); };
        }

        /// <summary>
        /// checks if the mouse is in the rectangle
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool MouseIn(Rectangle rect)
        {
            Vector2 v = new Vector2(Mouse.GetPosition(Game.window).X, Mouse.GetPosition(Game.window).Y);

            if (v.X < rect.Position.X || v.X > rect.Position.X + rect.Size.X)
                return false;

            if (v.Y < rect.Position.Y || v.Y > rect.Position.Y + rect.Size.Y)
                return false;

            return true;
        }

        /// <summary>
        /// gets the position in the given Rectangle
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector2 PositionIn(Rectangle r)
        {
            return new Vector2(Mouse.GetPosition(Game.window).X, Mouse.GetPosition(Game.window).Y) - r.Position;
        }

        /// <summary>
        /// checks if the mouse is within the sprite
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool MouseIn(Sprite sprite)
        {
            Vector2 v = new Vector2(Mouse.GetPosition(Game.window).X, Mouse.GetPosition(Game.window).Y);

            if (v.X < sprite.Position.X || v.X > sprite.Position.X + sprite.Texture.Size.X)
                return false;

            if (v.Y < sprite.Position.Y || v.Y > sprite.Position.Y + sprite.Texture.Size.Y)
                return false;

            return true;
        }

        /// <summary>
        /// updates the mouse controller
        /// </summary>
        public static void Update()
        {
            for(int i = 0; i< isPressed.Length; ++i)
            {
                if (!isPressed[i] && Mouse.IsButtonPressed((Mouse.Button)i))
                {
                    isPressed[i] = Mouse.IsButtonPressed((Mouse.Button)i);
                    MouseButtonEvent e = new MouseButtonEvent();
                    e.Button = (Mouse.Button)i;
                    e.X = Mouse.GetPosition(Game.window).X;
                    e.Y = Mouse.GetPosition(Game.window).Y;
                    OnButtonPress(e);
                }

                isPressed[i] = Mouse.IsButtonPressed((Mouse.Button)i);

                if(wasPressed[i] && !isPressed[i])
                {
                    MouseButtonEvent e = new MouseButtonEvent();
                    e.Button = (Mouse.Button)i;
                    e.X = Mouse.GetPosition(Game.window).X;
                    e.Y = Mouse.GetPosition(Game.window).Y;
                    OnRealize(e);
                }

                wasPressed[i] = isPressed[i];
            }
        }
    }
}
