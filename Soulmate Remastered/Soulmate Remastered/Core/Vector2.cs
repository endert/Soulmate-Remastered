﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Core
{
    /// <summary>
    /// 2 dimensional vector
    /// </summary>
    struct Vector2
    {
        /// <summary>
        /// x coordinate
        /// </summary>
        public float X;
        /// <summary>
        /// y coordinate
        /// </summary>
        public float Y;

        /// <summary>
        /// Length of the Vector
        /// </summary>
        public float Length { get { return (float)Math.Sqrt(X * X + Y * Y); } }

        //constants

        /// <summary>
        /// zero vector
        /// </summary>
        public static Vector2 ZERO { get { return new Vector2(0, 0); } }
        /// <summary>
        /// Vector that points to the back
        /// </summary>
        public static Vector2 BACK { get { return new Vector2(0, -1); } }
        /// <summary>
        /// Vector that points to the front
        /// </summary>
        public static Vector2 FRONT { get { return new Vector2(0, 1); } }
        /// <summary>
        /// Vector that points right
        /// </summary>
        public static Vector2 RIGHT { get { return new Vector2(1, 0); } }
        /// <summary>
        /// Vector that points left
        /// </summary>
        public static Vector2 LEFT { get { return new Vector2(-1, 0); } }
        /// <summary>
        /// smallest float vector != 0 in right direction
        /// </summary>
        public static Vector2 OFFSETŖ { get { return RIGHT * 0.1f; } }
        /// <summary>
        /// smallest float vector != 0 in left direction
        /// </summary>
        public static Vector2 OFFSETL { get { return LEFT * 0.1f; } }
        /// <summary>
        /// smallest float vector != 0 in front direction
        /// </summary>
        public static Vector2 OFFSETF { get { return FRONT * 0.1f; } }
        /// <summary>
        /// smallest float vector != 0 in back direction
        /// </summary>
        public static Vector2 OFFSETB { get { return BACK * 0.1f; } }


        //Constructors*********************************************************

        /// <summary>
        /// 2 dimensional vector
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// 2 dimensional vector
        /// </summary>
        /// <param name="v">coppied vector</param>
        public Vector2(Vector2 v) : this(v.X, v.Y) { }

        //*********************************************************************
        //Operators************************************************************

        /// <summary>
        /// cast into a float array
        /// </summary>
        /// <param name="v">the vector to cast</param>
        /// <returns>the float array</returns>
        public static implicit operator float[](Vector2 v)
        {
            return new float[] { v.X, v.Y };
        }

        /// <summary>
        /// cast Vector2 to SFML.Window.Vector2f
        /// </summary>
        /// <param name="v">vector to cast</param>
        /// <returns>the casted vector</returns>
        public static implicit operator SFML.Window.Vector2f(Vector2 v)
        {
            return new SFML.Window.Vector2f(v.X, v.Y);
        }

        /// <summary>
        /// cast SFML.Window.Vector2f to Vector2
        /// </summary>
        /// <param name="v">vector to cast</param>
        /// <returns>casted vector</returns>
        public static implicit operator Vector2(SFML.Window.Vector2f v)
        {
            return new Vector2(v.X, v.Y);
        }

        /// <summary>
        /// compare coordinate wise
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static bool operator ==(Vector2 v1, Vector2 v2)
        {
            return v1.X == v2.X && v1.Y == v2.Y;
        }

        /// <summary>
        /// compares coordinate wise
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static bool operator !=(Vector2 v1, Vector2 v2)
        {
            return !(v1.X == v2.X && v1.Y == v2.Y);
        }

        /// <summary>
        /// multiplies with -1
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector2 operator -(Vector2 v)
        {
            return (-1) * v;
        }

        /// <summary>
        /// sum up two vectors
        /// </summary>
        /// <param name="v1">first vector</param>
        /// <param name="v2">second vector</param>
        /// <returns>new vector with X = v1.X + v2.X und Y= v1.Y + v2.Y</returns>
        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }

        /// <summary>
        /// add component wise
        /// </summary>
        /// <param name="v"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Vector2 operator +(Vector2 v, float f)
        {
            return new Vector2(v.X + f, v.Y + f);
        }

        /// <summary>
        /// add component wise
        /// </summary>
        /// <param name="v"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Vector2 operator +(float f, Vector2 v)
        {
            return new Vector2(v.X + f, v.Y + f);
        }

        /// <summary>
        /// substract two vectors component wise
        /// </summary>
        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        }

        /// <summary>
        /// multiply a Vector with a scalar from the left side
        /// </summary>
        public static Vector2 operator *(float f, Vector2 v)
        {
            return new Vector2(v.X * f, v.Y * f);
        }

        /// <summary>
        /// multiply a vector with a scalar from the right side
        /// </summary>
        public static Vector2 operator *(Vector2 v, float f)
        {
            return new Vector2(v.X * f, v.Y * f);
        }

        /// <summary>
        /// multiply component wise
        /// </summary>
        /// <param name="v1">Vector2 v1 </param>
        /// <param name="v2">Vector2 v2 </param>
        /// <returns> new Vector2(v1.x * v2.x, v1.y * v2.y) </returns>
        public static Vector2 operator *(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X * v2.X, v1.Y * v2.Y);
        }

        /// <summary>
        /// divide a vector by a float value
        /// </summary>
        /// <param name="v">Vector which should be divided</param>
        /// <param name="f">value</param>
        /// <returns>new Vector</returns>
        public static Vector2 operator /(Vector2 v, float f)
        {
            return new Vector2(v.X / f, v.Y / f);
        }

        /// <summary>
        /// divides component wise 
        /// <para>divided by zero => result = 0</para>
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vector2 operator /(Vector2 v1, Vector2 v2)
        {
            return new Vector2((v2.X != 0) ? (v1.X / v2.X) : (0), (v2.Y != 0) ? (v1.Y / v2.Y) : (0));
        }

        /// <summary>
        /// modulo component wise also casts x and y to int
        /// </summary>
        /// <returns>the result as vector</returns>
        public static Vector2 operator %(Vector2 v1, Vector2 v2)
        {
            if (v2.X <= 0 || v2.Y <= 0)
                return new Vector2();

            int x = 0;
            int y = 0;

            if (v1.X < 0)
                while (v1.X < 0)
                    v1.X += v2.X;

            x = (int)v1.X % (int)v2.X;

            if (v1.Y < 0)
                while (v1.Y < 0)
                    v1.Y += v2.Y;

            y = (int)v1.Y % (int)v2.Y;

            return new Vector2(x, y);
        }

        //*********************************************************************
        //Methods**************************************************************

        /// <summary>
        /// multiply componets and sum it up
        /// </summary>
        /// <param name="v">Vector2 v</param>
        /// <returns>dot product of this and v</returns>
        public float Dot(Vector2 v)
        {
            return X * v.X + Y * v.Y;
        }

        /// <summary>
        /// evaluats the vector with the abs values of each component
        /// </summary>
        /// <returns></returns>
        public Vector2 Abs()
        {
            return new Vector2(Math.Abs(X), Math.Abs(Y));
        }

        /// <summary>
        /// turn the direction by 180 degree
        /// </summary>
        public void Invert()
        {
            X = -X;
            Y = -Y;
        }

        /// <summary>
        /// changes the vector, so that it has the length 1
        /// </summary>
        public void Normalize()
        {
            X /= Length;
            Y /= Length;
        }

        /// <summary>
        /// returns the Vector as normalized
        /// </summary>
        /// <returns>normalized Vector</returns>
        public Vector2 GetNormalized()
        {
            return new Vector2(this / Length);
        }

        /// <summary>
        /// evaluate the euclidean distance between this and the given point
        /// </summary>
        /// <param name="point">position vector of a point</param>
        /// <returns>the euclidean distance between this and the point</returns>
        public float Distance(Vector2 point)
        {
            return (point - this).Length;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// create a valid String
        /// </summary>
        /// <returns>vector as String</returns>
        public override string ToString()
        {
            return "[" + X + ", " + Y + " ]";
        }
    }
}
