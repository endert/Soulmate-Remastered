using SFML.Window;
using System;

namespace Soulmate_Remastered.Core
{
    class KeyEventArgs : EventArgs
    {
        /// <summary>
        /// the Key that was pressed/released
        /// </summary>
        public Controls.Key Key;
        /// <summary>
        /// true if alt was pressed to
        /// </summary>
        public bool Alt;
        /// <summary>
        /// true if control was pressed to
        /// </summary>
        public bool Control;
        /// <summary>
        /// true if shift was pressed to
        /// </summary>
        public bool Shift;
    }

    /// <summary>
    /// class for Keyboard control
    /// </summary>
    static class KeyboardControler
    {
        /// <summary>
        /// event, that is called when a key is pressed
        /// </summary>
        public static event EventHandler<KeyEventArgs> KeyPressed;
        /// <summary>
        /// event, that is called when a key is released
        /// </summary>
        public static event EventHandler<KeyEventArgs> KeyReleased;

        static void OnKeyPressed(KeyEventArgs e)
        {
            EventHandler<KeyEventArgs> handler = KeyPressed;
            if (handler != null)
                handler(null, e);
        }

        static void OnKeyRelease(KeyEventArgs e)
        {
            EventHandler<KeyEventArgs> handler = KeyReleased;
            if (handler != null)
                handler(null, e);
        }

        static bool[] isPressed;
        static bool[] wasPressed;

        /// <summary>
        /// initialize the Keyboardcontroler, only call once
        /// </summary>
        public static void Initialize()
        {
            DebugInitialize();

            isPressed = new bool[(int)Keyboard.Key.KeyCount];
            for (int i = 0; i < isPressed.Length; ++i)
                isPressed[i] = false;

            wasPressed = new bool[isPressed.Length];
            for (int i = 0; i < isPressed.Length; ++i)
                isPressed[i] = false;
        }

        /// <summary>
        /// only call if u want debug infos
        /// </summary>
        static void DebugInitialize()
        {
            KeyPressed += (sender, e) => { Console.WriteLine(e.Key + " pressed"); };
            KeyReleased += (sender, e) => { Console.WriteLine(e.Key + " released"); };
        }

        /// <summary>
        /// only call on map changes/ if the game is initialized again
        /// </summary>
        public static void Deleate()
        {
            KeyPressed = null;
            KeyReleased = null;

            DebugInitialize();
        }

        /// <summary>
        /// updates the KeyboardControler
        /// </summary>
        public static void Update()
        {
            for(int i = 0; i<isPressed.Length; ++i)
            {
                Keyboard.Key key = (Keyboard.Key)i;

                if(!isPressed[i] && Keyboard.IsKeyPressed(key))
                {
                    KeyEventArgs e = new KeyEventArgs();
                    e.Key = Controls.Cast(key);
                    e.Shift = Keyboard.IsKeyPressed(Keyboard.Key.LShift) || Keyboard.IsKeyPressed(Keyboard.Key.RShift);
                    e.Control = Keyboard.IsKeyPressed(Keyboard.Key.LControl) || Keyboard.IsKeyPressed(Keyboard.Key.RControl);
                    e.Alt = Keyboard.IsKeyPressed(Keyboard.Key.LAlt) || Keyboard.IsKeyPressed(Keyboard.Key.RAlt);
                    OnKeyPressed(e);
                }

                isPressed[i] = Keyboard.IsKeyPressed(key);

                if(wasPressed[i] && isPressed[i] != wasPressed[i])
                {
                    KeyEventArgs e = new KeyEventArgs();
                    e.Key = Controls.Cast(key);
                    e.Shift = Keyboard.IsKeyPressed(Keyboard.Key.LShift) || Keyboard.IsKeyPressed(Keyboard.Key.RShift);
                    e.Control = Keyboard.IsKeyPressed(Keyboard.Key.LControl) || Keyboard.IsKeyPressed(Keyboard.Key.RControl);
                    e.Alt = Keyboard.IsKeyPressed(Keyboard.Key.LAlt) || Keyboard.IsKeyPressed(Keyboard.Key.RAlt);
                    OnKeyRelease(e);
                }

                wasPressed[i] = isPressed[i];
            }
        }
    }
}
