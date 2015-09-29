using SFML.Window;
using System.Linq;
using System.Reflection;

namespace Soulmate_Remastered.Core
{
    /// <summary>
    /// a collection of all controls.
    /// </summary>
    static class Controls
    {
        public enum Key
        {
            Undefined = -1,

            Escape,
            Return,
            Back,
            Debugging,
            Up,
            Down,
            Right,
            Left,
            Attack,
            Shoot,
            OpenInventar,
            UseItem,
            Interact,
            CheatConsole,
            DebugMapChange,
            Fuse,

            Count
        }

        /// <summary>
        /// cast the Controls.Key to Keyboard.Key
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public static Keyboard.Key Cast(Key k)
        {
            Keyboard.Key res = Keyboard.Key.Unknown;

            string name = k.ToString().Split('.').Last();

            FieldInfo[] controls = typeof(Controls).GetFields();

            foreach(FieldInfo info in controls)
            {
                if (info.Name.Equals(name))
                    res = (Keyboard.Key)info.GetValue(null);
            }

            return res;
        }

        public static Key Cast(Keyboard.Key k)
        {
            Key res = Key.Undefined;

            FieldInfo[] controls = typeof(Controls).GetFields();

            foreach (FieldInfo info in controls)
            {
                if (k.Equals(info.GetValue(null)))
                {
                    res = (Key)System.Enum.Parse(typeof(Key), info.Name);
                }
            }

            return res;
        }

        //readonly***************************************************************

        /// <summary>
        /// Escape button (readonly)
        /// </summary>
        public static readonly Keyboard.Key Escape = Keyboard.Key.Escape;
        /// <summary>
        /// return button (readonly)
        /// </summary>
        public static readonly Keyboard.Key Return = Keyboard.Key.Return;
        /// <summary>
        /// back button (readonly)
        /// </summary>
        public static readonly Keyboard.Key Back = Keyboard.Key.Back;
        /// <summary>
        /// debug mode readonly (F3)
        /// </summary>
        public static readonly Keyboard.Key Debugging = Keyboard.Key.F3;

        //***********************************************************************
        //able to change*********************************************************

        /// <summary>
        /// Up Button for navigation
        /// </summary>
        public static Keyboard.Key Up = Keyboard.Key.W;
        /// <summary>
        /// Down button for navigation
        /// </summary>
        public static Keyboard.Key Down = Keyboard.Key.S;
        /// <summary>
        /// Right button for navigation
        /// </summary>
        public static Keyboard.Key Right = Keyboard.Key.D;
        /// <summary>
        /// Left button for Navigation
        /// </summary>
        public static Keyboard.Key Left = Keyboard.Key.A;

        /// <summary>
        /// Attack Button
        /// </summary>
        public static Keyboard.Key Attack = Keyboard.Key.Q;
        /// <summary>
        /// Shoot Button
        /// </summary>
        public static Keyboard.Key Shoot = Keyboard.Key.E;

        /// <summary>
        /// Open Inventory
        /// </summary>
        public static Keyboard.Key OpenInventar = Keyboard.Key.I;
        /// <summary>
        /// use Item
        /// </summary>
        public static Keyboard.Key UseItem = Keyboard.Key.U;
        /// <summary>
        /// interact with surrounding
        /// </summary>
        public static Keyboard.Key Interact = Keyboard.Key.P;
        /// <summary>
        /// Open console
        /// </summary>
        public static Keyboard.Key CheatConsole = Keyboard.Key.T;
        /// <summary>
        /// debug map change
        /// </summary>
        public static Keyboard.Key DebugMapChange = Keyboard.Key.L;
        /// <summary>
        /// the key for fusing player and pet
        /// </summary>
        public static Keyboard.Key Fuse = Keyboard.Key.Space;
    }
}
