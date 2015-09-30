using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.CheatConsoleFolder.CheatConsoleThreadFolder
{
    class Input
    {
        /// <summary>
        /// invoked when a key is pressed
        /// </summary>
        public static EventHandler<KeyEventArgs> KeyPressed;

        /// <summary>
        /// invokes the event
        /// </summary>
        /// <param name="e"></param>
        static void OnKeyPress(KeyEventArgs e)
        {
            EventHandler<KeyEventArgs> handler = KeyPressed;
            if (handler != null)
                handler(null, e);
        }

        /// <summary>
        /// bool array for is pressed values
        /// </summary>
        static bool[] isPressed;
        /// <summary>
        /// hilfs bool
        /// </summary>
        static bool ReturnPressed = false;

        /// <summary>
        /// initializes the input stuff
        /// </summary>
        public static void Initialize()
        {
            isPressed = new bool[(int)Keyboard.Key.KeyCount];
            for (int i = 0; i < isPressed.Length; ++i)
                isPressed[i] = false;
            KeyPressed += StringZeugs;
        }

        /// <summary>
        /// changes the cheatconsole text according to what key is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void StringZeugs(object sender, KeyEventArgs e)
        {
            string res = "";

            string code = e.Code.ToString();

            if (code.Length == 1)
            {
                res += e.Code.ToString();
                if (!e.Shift)
                    res = res.ToLower();
            }

            if (e.Code == Keyboard.Key.Space)
                res = " ";

            if (code.Contains("Num"))
            {
                res = code.Last().ToString();
                if (res.Equals("7") && e.Shift)
                {
                    res = "/";
                }
            }

            if (e.Code == Keyboard.Key.Return)
                ReturnPressed = true;


            if (e.Code == Keyboard.Key.Back)
            {
                res = "";
                CheatConsole.Text_.DisplayedString = CheatConsole.Text_.DisplayedString.Remove(CheatConsole.Text_.DisplayedString.Length - 1);
            }

            CheatConsole.Text_.DisplayedString += res;
        }

        /// <summary>
        /// set the given Key as pressed
        /// </summary>
        /// <param name="k">Keyboard.Key</param>
        public static void SetKeyPressed(Keyboard.Key k)
        {
            isPressed[(int)k] = true;
        }

        /// <summary>
        /// bool if return was pressed
        /// </summary>
        /// <returns></returns>
        public static bool ReturnIsPressed()
        {
            bool ispressed = ReturnPressed;
            ReturnPressed = false;

            return ispressed;
        }

        /// <summary>
        /// updates the input
        /// </summary>
        public static void Update()
        {
            for (int i = 0; i < isPressed.Length; ++i)
            {
                if (!isPressed[i] && Keyboard.IsKeyPressed((Keyboard.Key)i))
                {
                    KeyEventArgs e = new KeyEventArgs(new KeyEvent());
                    e.Code = (Keyboard.Key)i;
                    e.Shift = (Keyboard.IsKeyPressed(Keyboard.Key.LShift) || Keyboard.IsKeyPressed(Keyboard.Key.RShift)) ? (true) : (false);
                    OnKeyPress(e);
                }
                isPressed[i] = Keyboard.IsKeyPressed((Keyboard.Key)i);
            }
        }
    }
}
