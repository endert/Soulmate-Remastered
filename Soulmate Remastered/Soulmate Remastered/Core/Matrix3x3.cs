using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Core
{
    /// <summary>
    /// <para>Matrix for 2d transformations</para>
    /// <para>a, b, c</para>
    /// <para>d, e, f</para>
    /// <para>g, h, i</para>
    /// </summary>
    struct Matrix3x3
    {
        public float[,] Matrix;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a">0, 0</param>
        /// <param name="b">0, 1</param>
        /// <param name="c">0, 2</param>
        /// <param name="d">1, 0</param>
        /// <param name="e">1, 1</param>
        /// <param name="f">1, 2</param>
        /// <param name="g">2, 0</param>
        /// <param name="h">2, 1</param>
        /// <param name="i">2, 2</param>
        public Matrix3x3(float a, float b, float c, float d, float e, float f, float g, float h, float i)
        {
            Matrix = new float[3, 3];
            Matrix[0, 0] = a;
            Matrix[0, 1] = b;
            Matrix[0, 2] = c;
            Matrix[1, 0] = d;
            Matrix[1, 1] = e;
            Matrix[1, 2] = f;
            Matrix[2, 0] = g;
            Matrix[2, 1] = h;
            Matrix[2, 2] = i;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adg">X -> [0, 0], Y -> [1,0], Z -> [2, 0]</param>
        /// <param name="beh">X -> [0, 1], Y -> [1,1], Z -> [2, 1]</param>
        /// <param name="cfi">X -> [0, 2], Y -> [1,2], Z -> [2, 2]</param>
        public Matrix3x3(Vector3 adg, Vector3 beh, Vector3 cfi)
        {
            Matrix = new float[3, 3];
            Matrix[0, 0] = adg.X;
            Matrix[1, 0] = adg.Y;
            Matrix[2, 0] = adg.Z;
            Matrix[0, 1] = beh.X;
            Matrix[1, 1] = beh.Y;
            Matrix[2, 1] = beh.Z;
            Matrix[0, 2] = cfi.X;
            Matrix[1, 2] = cfi.Y;
            Matrix[2, 2] = cfi.Z;
        }

        public Vector3 GetADG()
        {
            return new Vector3(Matrix[0, 0], Matrix[1, 0], Matrix[2, 0]);
        }

        public Vector3 GetBEH()
        {
            return new Vector3(Matrix[0, 1], Matrix[1, 1], Matrix[2, 1]);
        }

        public Vector3 GetCFI()
        {
            return new Vector3(Matrix[0, 2], Matrix[1, 2], Matrix[2, 2]);
        }

        public Vector3 GetABC()
        {
            return new Vector3(Matrix[0, 0], Matrix[0, 1], Matrix[0, 2]);
        }

        public Vector3 GetDEF()
        {
            return new Vector3(Matrix[1, 0], Matrix[1, 1], Matrix[1, 2]);
        }

        public Vector3 GetGHI()
        {
            return new Vector3(Matrix[2, 0], Matrix[2, 1], Matrix[2, 1]);
        }

        /// <summary>
        /// evaluates the 3x3 Matrix for rotating for the given angle at the point 0,0
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public static Matrix3x3 RotationMatrix(float angle)
        {
            return new Matrix3x3((float)Math.Cos(angle), -(float)Math.Sin(angle), 0, (float)Math.Sin(angle), (float)Math.Cos(angle), 0, 0, 0, 1);
        }

        /// <summary>
        /// scaling Matrix for scaling around 0, 0 with the given factor
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static Matrix3x3 ScalingMatrix(float scale)
        {
            return new Matrix3x3(scale, 0, 0, 0, scale, 0, 0, 0, scale);
        }

        /// <summary>
        /// scaling Matrix for scaling around 0, 0 with the given factor
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static Matrix3x3 ScalingMatrix(Vector2 scale)
        {
            return new Matrix3x3(scale.X, 0, 0, 0, scale.Y, 0, 0, 0, 1);
        }

        /// <summary>
        /// the Matrix to sheer for fw in x direction and fy in y direction
        /// </summary>
        /// <param name="fx"></param>
        /// <param name="fy"></param>
        /// <returns></returns>
        public static Matrix3x3 Scheering(float fx, float fy)
        {
            return new Matrix3x3(1, fy, 0, fx, 1, 0, 0, 0, 1);
        }

        /// <summary>
        /// the Matrix to translate in the given direction
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static Matrix3x3 Translate(Vector2 direction)
        {
            return new Matrix3x3(1, 0, direction.X, 0, 1, direction.Y, 0, 0, 1);
        }

        //Operators************************************************************************

        public static Matrix3x3 operator *(Matrix3x3 m1, Matrix3x3 m2)
        {
            float a = m1.GetABC().Dot(m2.GetADG());
            float b = m1.GetABC().Dot(m2.GetBEH());
            float c = m1.GetABC().Dot(m2.GetCFI());

            float d = m1.GetDEF().Dot(m2.GetADG());
            float e = m1.GetDEF().Dot(m2.GetBEH());
            float f = m1.GetDEF().Dot(m2.GetCFI());

            float g = m1.GetGHI().Dot(m2.GetADG());
            float h = m1.GetGHI().Dot(m2.GetBEH());
            float i = m1.GetGHI().Dot(m2.GetCFI());

            return new Matrix3x3(a, b, c, d, e, f, g, h, i);
        }

        public static Vector3 operator *(Matrix3x3 m, Vector3 v)
        {
            float x = m.GetABC().Dot(v);
            float y = m.GetDEF().Dot(v);
            float z = m.GetGHI().Dot(v);

            return new Vector3(x, y, z);
        }

        //*********************************************************************************
    }
}
