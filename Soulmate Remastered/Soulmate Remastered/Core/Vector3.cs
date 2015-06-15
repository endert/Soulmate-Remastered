using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Core
{
    /// <summary>
    /// 3Dimensional vector
    /// </summary>
    struct Vector3
    {
        /// <summary>
        /// X coordinate
        /// </summary>
        public float X;
        /// <summary>
        /// Y coordinate
        /// </summary>
        public float Y;
        /// <summary>
        /// Z coordinate
        /// </summary>
        public float Z;
        /// <summary>
        /// Length of this vector
        /// </summary>
        public float Length { get { return (float)Math.Sqrt(X * X + Y * Y + Z * Z); } }

        //Constructors**************************************************************************

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Lift 2d vector in 3d by adding 1 as the Z coordinate
        /// </summary>
        /// <param name="v2"></param>
        public Vector3(Vector2 v2d)
        {
            X = v2d.X;
            Y = v2d.Y;
            Z = 1;
        }

        /// <summary>
        /// adding z as the third coordinate
        /// </summary>
        /// <param name="v2d"></param>
        /// <param name="z"></param>
        public Vector3(Vector2 v2d, float z)
        {
            X = v2d.X;
            Y = v2d.Y;
            Z = z;
        }

        //**************************************************************************************

        //Casts*********************************************************************************

        /// <summary>
        /// <para>casts 3d vector to 2d vector by dividing x and y by z</para>
        /// <para>and ignoring than z</para>
        /// <para>in other words project the point back to the plane the 2d vecs lay in</para>
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static implicit operator Vector2(Vector3 v)
        {
            return new Vector2(v.X / v.Z, v.Y / v.Z);
        }

        //**************************************************************************************

        //Operators*****************************************************************************

        /// <summary>
        /// evaluate sum of two vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        /// <summary>
        /// substract v2 from v1
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        /// <summary>
        /// multply component wise
        /// </summary>
        /// <param name="v"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Vector3 operator *(Vector3 v, float f)
        {
            return new Vector3(v.X * f, v.Y * f, v.Z * f);
        }

        /// <summary>
        /// multiply component wise
        /// </summary>
        /// <param name="f"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3 operator *(float f, Vector3 v)
        {
            return new Vector3(v.X * f, v.Y * f, v.Z * f);
        }

        /// <summary>
        /// divides component wise
        /// </summary>
        /// <param name="v"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Vector3 operator /(Vector3 v, float f)
        {
            return new Vector3(v.X / f, v.Y / f, v.Z / f);
        }

        /// <summary>
        /// returns true if all components equal the coressponding 
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static bool operator ==(Vector3 v1, Vector3 v2)
        {
            return v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z;
        }

        /// <summary>
        /// returns true if at least one coordinate is diferent than the coressponding one in the other vec
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static bool operator !=(Vector3 v1, Vector3 v2)
        {
            return !(v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z);
        }

        //**************************************************************************************

        //Methods*******************************************************************************

        /// <summary>
        /// cross product
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public Vector3 Cross(Vector3 v)
        {
            float x = Y * v.Z - Z * v.Y;
            float y = Z * v.X - X * v.Z;
            float z = X * v.Y - Y * v.X;

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Dot product (cos of the angle bewtween two normalized vecs)
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public float Dot(Vector3 v)
        {
            return X * v.X + Y * v.Y + Z * v.Z;
        }

        /// <summary>
        /// bool if the two vecs are orthogonal (angle = 90 degree)
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public bool IsOrthognoal(Vector3 v)
        {
            return this.Dot(v) == 0;
        }

        /// <summary>
        /// shortens the vector so that it has the length 1 without changing the direction
        /// </summary>
        public void Normalize()
        {
            X /= Length;
            Y /= Length;
            Z /= Length;
        }

        /// <summary>
        /// returns the normalized vec without changing this
        /// </summary>
        /// <returns></returns>
        public Vector3 GetNormalized()
        {
            return this / Length;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "[X = " + X + ", Y = " + Y + ", Z = " + Z + "]";
        }

        //**************************************************************************************

    }
}
