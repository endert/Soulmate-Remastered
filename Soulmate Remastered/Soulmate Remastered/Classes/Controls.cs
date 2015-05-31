using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes
{
    /// <summary>
    /// a collection of all controls.
    /// </summary>
    static class Controls
    {
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
        public static Keyboard.Key debugging = Keyboard.Key.F3;

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
        public static Keyboard.Key ButtonForAttack = Keyboard.Key.Q;
        /// <summary>
        /// Shoot Button
        /// </summary>
        public static Keyboard.Key ButtonForShoot = Keyboard.Key.E;

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
        public static Keyboard.Key CheatConsoleOpen = Keyboard.Key.T;
    }
}
