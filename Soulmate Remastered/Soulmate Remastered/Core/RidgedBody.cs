using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Core
{
    /// <summary>
    /// Transformable Body
    /// </summary>
    class RidgedBody
    {
        List<Vertex> vertecies;
        bool getMatrix = false;
        public Vector2 Position { get; protected set; }

        public RidgedBody(Vector2 pos)
        {
            Position = pos;
            vertecies = new List<Vertex>();
            vertecies.Add(new Vertex(pos));
        }

        /// <summary>
        /// add a vertex at the given position
        /// </summary>
        /// <param name="pos"></param>
        public void AddVertex(Vector2 pos)
        {
            vertecies.Add(new Vertex(pos));
        }

        /// <summary>
        /// translate the ridged body in the given direction
        /// </summary>
        /// <param name="direction"></param>
        public Matrix3x3 Translate(Vector2 direction)
        {
            Matrix3x3 translate = Matrix3x3.Translate(direction);

            if (getMatrix)
                return translate;

            //multiply all vertecies with translation matrix
            foreach (Vertex v in vertecies)
                v.Transform(translate);

            return translate;
        }

        /// <summary>
        /// rotate the ridged body for the angle at the position
        /// </summary>
        /// <param name="angle"></param>
        private Matrix3x3 Rotate(float angle)
        {
            Matrix3x3 toOrigin = Matrix3x3.Translate(-Position);
            Matrix3x3 rotate = Matrix3x3.RotationMatrix(angle);
            Matrix3x3 backToPosition = Matrix3x3.Translate(Position);

            Matrix3x3 transform = backToPosition * rotate * toOrigin;

            if (getMatrix)
                return transform;

            foreach (Vertex v in vertecies)
                v.Transform(transform);

            return transform;
        }

        /// <summary>
        /// rotate the ridgedBody for the x Coordinate
        /// </summary>
        /// <param name="angle"></param>
        public Matrix3x3 Rotate(Vector2 angle)
        {
            return Rotate(angle.X);
        }

        /// <summary>
        /// scale with the given Vec
        /// </summary>
        /// <param name="scale"></param>
        public Matrix3x3 Scale(Vector2 scale)
        {
            Matrix3x3 toOrigin = Matrix3x3.Translate(-Position);
            Matrix3x3 scaleM = Matrix3x3.ScalingMatrix(scale);
            Matrix3x3 backToPosition = Matrix3x3.Translate(Position);

            Matrix3x3 transform = toOrigin * scaleM * backToPosition;

            if (getMatrix)
                return transform;

            foreach (Vertex v in vertecies)
                v.Transform(transform);

            return transform;
        }

        public delegate Matrix3x3 TransformMethods(Vector2 parm);

        public void Transform(TransformMethods[] list, Vector2[] args)
        {
            if (list.Length != args.Length)
                throw new IndexOutOfRangeException();

            getMatrix = true;
            Matrix3x3 transform = new Matrix3x3(1, 0, 0, 0, 1, 0, 0, 0, 1);

            for (int i = 0; i < list.Length; ++i)
                transform *= list[i](args[i]);

            foreach (Vertex v in vertecies)
                v.Transform(transform);

            getMatrix = false;
        }
    }
}
