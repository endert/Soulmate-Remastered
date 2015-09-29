using System;
using System.Linq;
using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes;

namespace Soulmate_Remastered.Core
{
    class MouseButtonEventArgs
    {
        public MouseButton Button;
        public Vector2 Position;
    }

    enum MouseButton
    {
        None = -1,

        Left,
        Right,

        Count
    }

    static class MouseControler
    {
        /// <summary>
        /// event that fires when a button is relized
        /// </summary>
        public static event EventHandler<MouseButtonEventArgs> Relessed;

        /// <summary>
        /// is triggered once when a button is pressed
        /// </summary>
        public static event EventHandler<MouseButtonEventArgs> ButtonPressed;

        public static Vector2 MousePosition { get; private set; }

        /// <summary>
        /// triggers the relize event
        /// </summary>
        /// <param name="b"></param>
        static void OnRealize(MouseButtonEventArgs e)
        {
            EventHandler<MouseButtonEventArgs> handler = Relessed;
            if (Relessed != null)
                handler(null, e);
        }

        /// <summary>
        /// triggers the button press event
        /// </summary>
        /// <param name="e"></param>
        static void OnButtonPress(MouseButtonEventArgs e)
        {
            EventHandler<MouseButtonEventArgs> handler = ButtonPressed;
            if (handler != null)
                handler(null, e);
        }

        static bool[] isPressed;
        static bool[] wasPressed;

        /// <summary>
        /// returns if the given sprite is pressed with the given Button
        /// </summary>
        /// <param name="sprite">the sprite that shall be pressed</param>
        /// <param name="b">the button that shall be checked</param>
        public static bool IsPressed(Sprite sprite, MouseButton b)
        {
            return MouseIn(sprite) && isPressed[(int)b];
        }

        public static bool IsPressed(MouseButton b)
        {
            return isPressed[(int)b];
        }

        /// <summary>
        /// initialize the controller
        /// </summary>
        public static void Initialize()
        {
            DebugInitialize();

            isPressed = new bool[(int)MouseButton.Count];
            for (int i = 0; i < isPressed.Length; ++i)
                isPressed[i] = false;

            wasPressed = new bool[(int)MouseButton.Count];
            for (int i = 0; i < wasPressed.Length; ++i)
                wasPressed[i] = false;
        }

        private static void DebugInitialize()
        {
            Relessed += (sender, ev) => { Console.WriteLine("Released " + ev.Button); };
            ButtonPressed += (sender, ev) => { Console.WriteLine("Pressed " + ev.Button); };
        }

        /// <summary>
        /// checks if the mouse is in the rectangle
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool MouseIn(Rectangle rect)
        {
            Vector2 v = MousePosition;

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

        static Mouse.Button Cast(MouseButton b)
        {
            string name = b.ToString().Split('.').Last();

            string[] names = typeof(Mouse).GetNestedType("Button").GetEnumNames();

            for (int i = 0; i < names.Length; ++i)
                if (names[i].Equals(name))
                    return (Mouse.Button)i;

            return Mouse.Button.ButtonCount;
        }

        /// <summary>
        /// updates the mouse controller
        /// </summary>
        public static void Update()
        {
            Vector2i v = Mouse.GetPosition(AbstractGame.window);
            MousePosition = new Vector2(v.X, v.Y);

            for (int i = 0; i < isPressed.Length; ++i)
            { 

                if (!isPressed[i] && Mouse.IsButtonPressed(Cast((MouseButton)i)))
                {
                    isPressed[i] = Mouse.IsButtonPressed((Mouse.Button)i);
                    MouseButtonEventArgs e = new MouseButtonEventArgs();
                    e.Button = (MouseButton)i;
                    e.Position.X = Mouse.GetPosition(AbstractGame.window).X;
                    e.Position.Y = Mouse.GetPosition(AbstractGame.window).Y;
                    OnButtonPress(e);
                }

                isPressed[i] = Mouse.IsButtonPressed(Cast((MouseButton)i));

                if(wasPressed[i] && !isPressed[i])
                {
                    MouseButtonEventArgs e = new MouseButtonEventArgs();
                    e.Button = (MouseButton)i;
                    e.Position.X = Mouse.GetPosition(AbstractGame.window).X;
                    e.Position.Y = Mouse.GetPosition(AbstractGame.window).Y;
                    OnRealize(e);
                }

                wasPressed[i] = isPressed[i];
            }
        }
    }
}
