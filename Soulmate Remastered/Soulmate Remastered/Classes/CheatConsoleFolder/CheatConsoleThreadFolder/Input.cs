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
        /// German standard Keyboard without special Keys like Ü, Ä, Ö, + etc
        /// contains [a, b, ..., z] + [A, B, ..., Z] + [" ", "/", "(", ")", ","]
        /// </summary>
        public enum Key
        {
            Unknown = -1,
            Num0 = 0, Num1 = 1, Num2 = 2, Num3 = 3, Num4 = 4, Num5 = 5, Num6 = 6, Num7 = 7, Num8 = 8, Num9 = 9,
            a = 10, b = 11, c = 12, d = 13, e = 14, f = 15, g = 16, h = 17, i = 18, j = 19, k = 20, l = 21, m = 22, n = 23, o = 24, p = 25, q = 26, r = 27, s = 28, t = 29, u = 30, v = 31, w = 32, x = 33, y = 34, z = 35,
            A = 36, B = 37, C = 38, D = 39, E = 40, F = 41, G = 42, H = 43, I = 44, J = 45, K = 46, L = 47, M = 48, N = 49, O = 50, P = 51, Q = 52, R = 53, S = 54, T = 55, U = 56, V = 57, W = 58, X = 59, Y = 60, Z = 61,
            Slash = 62, LBracket = 63, RBracket = 64, Space = 65, Comma = 66, Point = 67, Back, Return, KeyCount
        }

        /// <summary>
        /// bool array if the button was pressed
        /// </summary>
        static bool[] isPressed = new bool[(int)Key.KeyCount];

        /// <summary>
        /// the bool value if any Shift Key is pressed
        /// </summary>
        public static bool IsShiftPressed { get { return Keyboard.IsKeyPressed(Keyboard.Key.LShift) || Keyboard.IsKeyPressed(Keyboard.Key.RShift); } }

        /// <summary>
        /// set the given Key as pressed
        /// </summary>
        /// <param name="_k">Keyboard.Key</param>
        public static void SetKeyPressed(Keyboard.Key _k)
        {
            Initialize();
            Key k = Cast(_k);
            isPressed[(int)k] = true;
            if ((int)k >= 10 && (int)k <= 35)
                isPressed[((int)k) + 26] = true;
        }

        /// <summary>
        /// cast Keyboard.Key to Input.Key
        /// </summary>
        /// <param name="k">Keyboard.Key k</param>
        /// <returns>Input.Key</returns>
        private static Key Cast(Keyboard.Key k)
        {
            if (IsShiftPressed)   //shift is pressed means we use upper case letter
                switch (k)
                {
                    case Keyboard.Key.A: return Key.A;
                    case Keyboard.Key.B: return Key.B;
                    case Keyboard.Key.C: return Key.C;
                    case Keyboard.Key.D: return Key.D;
                    case Keyboard.Key.E: return Key.E;
                    case Keyboard.Key.F: return Key.F;
                    case Keyboard.Key.G: return Key.G;
                    case Keyboard.Key.H: return Key.H;
                    case Keyboard.Key.I: return Key.I;
                    case Keyboard.Key.J: return Key.J;
                    case Keyboard.Key.K: return Key.K;
                    case Keyboard.Key.L: return Key.L;
                    case Keyboard.Key.M: return Key.M;
                    case Keyboard.Key.N: return Key.N;
                    case Keyboard.Key.O: return Key.O;
                    case Keyboard.Key.P: return Key.P;
                    case Keyboard.Key.Q: return Key.Q;
                    case Keyboard.Key.R: return Key.R;
                    case Keyboard.Key.S: return Key.S;
                    case Keyboard.Key.T: return Key.T;
                    case Keyboard.Key.U: return Key.U;
                    case Keyboard.Key.V: return Key.V;
                    case Keyboard.Key.W: return Key.W;
                    case Keyboard.Key.X: return Key.X;
                    case Keyboard.Key.Y: return Key.Y;
                    case Keyboard.Key.Z: return Key.Z;
                    case Keyboard.Key.Space: return Key.Space;
                    case Keyboard.Key.Num7: return Key.Slash;
                    case Keyboard.Key.Num8: return Key.LBracket;
                    case Keyboard.Key.Num9: return Key.RBracket;
                    case Keyboard.Key.Return: return Key.Return;
                    default: return Key.Unknown;
                }
            else                                                        //else we use lower case Letter
                switch (k)
                {
                    case Keyboard.Key.Num0: return Key.Num0;
                    case Keyboard.Key.Num1: return Key.Num1;
                    case Keyboard.Key.Num2: return Key.Num2;
                    case Keyboard.Key.Num3: return Key.Num3;
                    case Keyboard.Key.Num4: return Key.Num4;
                    case Keyboard.Key.Num5: return Key.Num5;
                    case Keyboard.Key.Num6: return Key.Num6;
                    case Keyboard.Key.Num7: return Key.Num7;
                    case Keyboard.Key.Num8: return Key.Num8;
                    case Keyboard.Key.Num9: return Key.Num9;
                    case Keyboard.Key.A: return Key.a;
                    case Keyboard.Key.B: return Key.b;
                    case Keyboard.Key.C: return Key.c;
                    case Keyboard.Key.D: return Key.d;
                    case Keyboard.Key.E: return Key.e;
                    case Keyboard.Key.F: return Key.f;
                    case Keyboard.Key.G: return Key.g;
                    case Keyboard.Key.H: return Key.h;
                    case Keyboard.Key.I: return Key.i;
                    case Keyboard.Key.J: return Key.j;
                    case Keyboard.Key.K: return Key.k;
                    case Keyboard.Key.L: return Key.l;
                    case Keyboard.Key.M: return Key.m;
                    case Keyboard.Key.N: return Key.n;
                    case Keyboard.Key.O: return Key.o;
                    case Keyboard.Key.P: return Key.p;
                    case Keyboard.Key.Q: return Key.q;
                    case Keyboard.Key.R: return Key.r;
                    case Keyboard.Key.S: return Key.s;
                    case Keyboard.Key.T: return Key.t;
                    case Keyboard.Key.U: return Key.u;
                    case Keyboard.Key.V: return Key.v;
                    case Keyboard.Key.W: return Key.w;
                    case Keyboard.Key.X: return Key.x;
                    case Keyboard.Key.Y: return Key.y;
                    case Keyboard.Key.Z: return Key.z;
                    case Keyboard.Key.Space: return Key.Space;
                    case Keyboard.Key.Comma: return Key.Comma;
                    case Keyboard.Key.Return: return Key.Return;
                    case Keyboard.Key.LBracket: return Key.LBracket;
                    case Keyboard.Key.RBracket: return Key.RBracket;
                    case Keyboard.Key.Slash: return Key.Slash;
                    case Keyboard.Key.Period: return Key.Point;
                    case Keyboard.Key.Back: return Key.Back;
                    default: return Key.Unknown;
                }
        }

        /// <summary>
        /// cast Input.Key to Keyboard.Key
        /// </summary>
        /// <param name="k">Input.Key</param>
        /// <returns>Keyboard.Key</returns>
        private static Keyboard.Key Cast(Key k) // (Keyboard.Key) Input.Key
        {
            switch (k)                      //a lot of switch cases 
            {
                case Key.Num0: return Keyboard.Key.Num0;
                case Key.Num1: return Keyboard.Key.Num1;
                case Key.Num2: return Keyboard.Key.Num2;
                case Key.Num3: return Keyboard.Key.Num3;
                case Key.Num4: return Keyboard.Key.Num4;
                case Key.Num5: return Keyboard.Key.Num5;
                case Key.Num6: return Keyboard.Key.Num6;
                case Key.Num7: return Keyboard.Key.Num7;
                case Key.Num8: return Keyboard.Key.Num8;
                case Key.Num9: return Keyboard.Key.Num9;
                case Key.a: return Keyboard.Key.A;
                case Key.b: return Keyboard.Key.B;
                case Key.c: return Keyboard.Key.C;
                case Key.d: return Keyboard.Key.D;
                case Key.e: return Keyboard.Key.E;
                case Key.f: return Keyboard.Key.F;
                case Key.g: return Keyboard.Key.G;
                case Key.h: return Keyboard.Key.H;
                case Key.i: return Keyboard.Key.I;
                case Key.j: return Keyboard.Key.J;
                case Key.k: return Keyboard.Key.K;
                case Key.l: return Keyboard.Key.L;
                case Key.m: return Keyboard.Key.M;
                case Key.n: return Keyboard.Key.N;
                case Key.o: return Keyboard.Key.O;
                case Key.p: return Keyboard.Key.P;
                case Key.q: return Keyboard.Key.Q;
                case Key.r: return Keyboard.Key.R;
                case Key.s: return Keyboard.Key.S;
                case Key.t: return Keyboard.Key.T;
                case Key.u: return Keyboard.Key.U;
                case Key.v: return Keyboard.Key.V;
                case Key.w: return Keyboard.Key.W;
                case Key.x: return Keyboard.Key.X;
                case Key.y: return Keyboard.Key.Y;
                case Key.z: return Keyboard.Key.Z;
                case Key.A: return Keyboard.Key.A;
                case Key.B: return Keyboard.Key.B;
                case Key.C: return Keyboard.Key.C;
                case Key.D: return Keyboard.Key.D;
                case Key.E: return Keyboard.Key.E;
                case Key.F: return Keyboard.Key.F;
                case Key.G: return Keyboard.Key.G;
                case Key.H: return Keyboard.Key.H;
                case Key.I: return Keyboard.Key.I;
                case Key.J: return Keyboard.Key.J;
                case Key.K: return Keyboard.Key.K;
                case Key.L: return Keyboard.Key.L;
                case Key.M: return Keyboard.Key.M;
                case Key.N: return Keyboard.Key.N;
                case Key.O: return Keyboard.Key.O;
                case Key.P: return Keyboard.Key.P;
                case Key.Q: return Keyboard.Key.Q;
                case Key.R: return Keyboard.Key.R;
                case Key.S: return Keyboard.Key.S;
                case Key.T: return Keyboard.Key.T;
                case Key.U: return Keyboard.Key.U;
                case Key.V: return Keyboard.Key.V;
                case Key.W: return Keyboard.Key.W;
                case Key.X: return Keyboard.Key.X;
                case Key.Y: return Keyboard.Key.Y;
                case Key.Z: return Keyboard.Key.Z;
                case Key.Space: return Keyboard.Key.Space;
                case Key.Comma: return Keyboard.Key.Comma;
                case Key.LBracket: return Keyboard.Key.Num8;    //to convert it to a german keyboard we set the brackets to the Number slots
                case Key.RBracket: return Keyboard.Key.Num9;
                case Key.Return: return Keyboard.Key.Return;
                case Key.Slash: return Keyboard.Key.Num7;
                case Key.Point: return Keyboard.Key.Period;
                case Key.Back: return Keyboard.Key.Back;
                default: return Keyboard.Key.Unknown;
            }
        }

        /// <summary>
        /// cast Key to String
        /// </summary>
        /// <param name="k">Input.Key</param>
        /// <returns>String</returns>
        public static String ButtonString(Key k)
        {
            switch (k)
            {
                case Key.Num0: return "0";
                case Key.Num1: return "1";
                case Key.Num2: return "2";
                case Key.Num3: return "3";
                case Key.Num4: return "4";
                case Key.Num5: return "5";
                case Key.Num6: return "6";
                case Key.Num7: return "7";
                case Key.Num8: return "8";
                case Key.Num9: return "9";
                case Key.a: return "a";
                case Key.b: return "b";
                case Key.c: return "c";
                case Key.d: return "d";
                case Key.e: return "e";
                case Key.f: return "f";
                case Key.g: return "g";
                case Key.h: return "h";
                case Key.i: return "i";
                case Key.j: return "j";
                case Key.k: return "k";
                case Key.l: return "l";
                case Key.m: return "m";
                case Key.n: return "n";
                case Key.o: return "o";
                case Key.p: return "p";
                case Key.q: return "q";
                case Key.r: return "r";
                case Key.s: return "s";
                case Key.t: return "t";
                case Key.u: return "u";
                case Key.v: return "v";
                case Key.w: return "w";
                case Key.x: return "x";
                case Key.y: return "y";
                case Key.z: return "z";
                case Key.A: return "A";
                case Key.B: return "B";
                case Key.C: return "C";
                case Key.D: return "D";
                case Key.E: return "E";
                case Key.F: return "F";
                case Key.G: return "G";
                case Key.H: return "H";
                case Key.I: return "I";
                case Key.J: return "J";
                case Key.K: return "K";
                case Key.L: return "L";
                case Key.M: return "M";
                case Key.N: return "N";
                case Key.O: return "O";
                case Key.P: return "P";
                case Key.Q: return "Q";
                case Key.R: return "R";
                case Key.S: return "S";
                case Key.T: return "T";
                case Key.U: return "U";
                case Key.V: return "V";
                case Key.W: return "W";
                case Key.X: return "X";
                case Key.Y: return "Y";
                case Key.Z: return "Z";
                case Key.Space: return " ";
                case Key.Comma: return ",";
                case Key.LBracket: return "(";
                case Key.RBracket: return ")";
                case Key.Slash: return "/";
                case Key.Point: return ".";
                default: return "";
            }
        }

        /// <summary>
        /// initialize the bool array of the pressed Keys with false
        /// </summary>
        public static void Initialize()
        {
            for (int i = 0; i < isPressed.Length; i++)
            {
                isPressed[i] = false;
            }
        }

        /// <summary>
        /// is Return pressed
        /// </summary>
        /// <returns>bool</returns>
        public static bool ReturnIsPressed()
        {
            bool res = !isPressed[(int)Key.Return] && Keyboard.IsKeyPressed(Keyboard.Key.Return);
            isPressed[(int)Key.Return] = true;
            return res;
        }

        /// <summary>
        /// updating the bool array and enter the pressed Keys to the shown String in the Console
        /// </summary>
        public static void Update()
        {
            for (int i = 0; i < isPressed.Length; ++i)
            {
                if (!isPressed[i] && Keyboard.IsKeyPressed(Cast((Key)i)) && (i < 36 || i > 64) && !IsShiftPressed) //excluding upperLetter if Shift is not pressed
                {
                    CheatConsole.Text_.DisplayedString += ButtonString((Key)i);
                    isPressed[i] = true;

                    if (6 < i && i < 10)    //7 -> Slash 8 -> LBracket 9 -> RBracket
                        isPressed[i + 55] = true;

                    if (9 < i && i < 36)    //if its a Letter
                        isPressed[i + 26] = true;   //the UpperLetter
                }

                if (i > 35 && i < 65 && !isPressed[i] && Keyboard.IsKeyPressed(Cast((Key)i)) && IsShiftPressed) //only upperLetter if a Shift button is pressed
                {
                    CheatConsole.Text_.DisplayedString += ButtonString((Key)i);
                    isPressed[i] = true;
                    if (i < 62)
                        isPressed[i - 26] = true;   //the LowerLetter
                    else
                        isPressed[i - 55] = true;   //Slash -> 7 LBracket -> 8 RBracket -> 9
                }

                if (CheatConsole.Text_.DisplayedString.Length>0 && !isPressed[(int)Key.Back] && Keyboard.IsKeyPressed(Keyboard.Key.Back))
                {
                    CheatConsole.Text_.DisplayedString = CheatConsole.Text_.DisplayedString.Remove(CheatConsole.Text_.DisplayedString.Length - 1);
                    isPressed[(int)Key.Back] = true;
                }

                if (isPressed[i] && !Keyboard.IsKeyPressed(Cast((Key)i)))
                {
                    isPressed[i] = false;
                }
            }
        }
    }
}
