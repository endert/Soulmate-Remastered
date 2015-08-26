using SFML.Graphics;
using Soulmate_Remastered.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder
{
    class AbstractHitBox
    {
        /// <summary>
        /// Position in world coordinates
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// size as a vector x = width y = height
        /// </summary>
        public Vector2 Size { get; protected set; }
        /// <summary>
        /// width as a vector
        /// </summary>
        public Vector2 SizeX { get { return new Vector2(Size.X, 0); } }
        /// <summary>
        /// height as a vector
        /// </summary>
        public Vector2 SizeY { get { return new Vector2(0, Size.Y); } }


        //Constructors*****************************************************************************************


        /// <summary>
        /// <para>constructor is the same as for all child classes</para>
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="size"></param>
        public AbstractHitBox(Vector2 pos, Vector2 size) 
        {
            Position = pos;
            Size = size;
        }
        /// <summary>
        /// <para>constructor is the same as for all child classes</para>
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="size"></param>
        public AbstractHitBox(Vector2 pos, float width, float height) : this(pos, new Vector2(width, height)) { }
        /// <summary>
        /// <para>constructor is the same as for all child classes</para>
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="size"></param>
        public AbstractHitBox(float x, float y, Vector2 size) : this(new Vector2(x, y), size) { }
        /// <summary>
        /// <para>constructor is the same as for all child classes</para>
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="size"></param>
        public AbstractHitBox(float x, float y, float width, float height) : this(new Vector2(x, y), new Vector2(width, height)) { }


        //****************************************************************************************************
        //Methods*********************************************************************************************

        public bool Hit(AbstractHitBox h)
        {
            //creating an Vector that will help us, it point toward the position of h
            Vector2 v = h.Position - Position;

            /*
             * x coordinate:
             * we take the abs of v.x and subtract the size.x of the hitbox with
             * the lowest x value. If the resulting value is smaller or equals zero
             * then both hitboxes have points within them wich have the same x coordinates.
             * 
             * y coordinate:
             * same as for x coordinates.
             * 
             * so if xCheck <= 0 and yCheck <= 0 the points exit in both hitboxes wich have the same x and y coordinates
             * so the hitboxes must colide in some way.
             */
            float xCheck = Math.Abs(v.X) - CompareX(h).Size.X;
            float yCheck = Math.Abs(v.Y) - CompareY(h).Size.Y;

            return xCheck <= 0 && yCheck <= 0;
        }

        /// <summary>
        /// the hitbox with the smaller x coordinate
        /// </summary>
        private AbstractHitBox CompareX(AbstractHitBox h)
        {
            return (Position.X <= h.Position.X) ? (this) : (h);
        }

        /// <summary>
        /// the hitbox with the smaller y coordinate
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        private AbstractHitBox CompareY(AbstractHitBox h)
        {
            return (Position.Y <= h.Position.Y) ? (this) : (h);
        }

        /// <summary>
        /// returns a bool, if the hitbox will hit the other hitbox when it is moved by the direction
        /// </summary>
        /// <param name="direction">moving Direction</param>
        /// <param name="h">testet hitbox for collision</param>
        /// <returns>it will hit</returns>
        public bool WillHit(Vector2 direction, AbstractHitBox h)
        {
            AbstractHitBox test = new AbstractHitBox(Position + direction, Size);
            return test.Hit(h);
        }

        /// <summary>
        /// returns direction to the given Hitbox
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        public Vector2 hitFrom(AbstractHitBox h)
        {
            return h.Position - Position;
        }

        /// <summary>
        /// evaluates the shortest distance between this and the given hitbox
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        public float DistanceTo(AbstractHitBox h)
        {
            if (Hit(h))
                return 0;

            Vector2 res = new Vector2();

            //closest point in this hitbox
            Vector2 h1Pos = Position;
            Vector2[] h1 = new Vector2[]{new Vector2(Size.X, 0), new Vector2(0, Size.Y)};

            //closest point in h
            Vector2 h2Pos = h.Position;
            Vector2[] h2 = new Vector2[]{new Vector2(h.Size.X, 0), new Vector2(0, h.Size.Y)};

            float t;

            //h1
            foreach (Vector2 v in h1)
            {
                res = h2Pos - h1Pos;

                t = res.Dot(v) / v.Dot(v);
                if (t < 0)
                    t = 0;
                if (t > 1)
                    t = 1;

                h1Pos += t * v;

            }

            //h2
            foreach (Vector2 v in h2)
            {
                res = h1Pos - h2Pos;

                t = res.Dot(v) / v.Dot(v);
                if (t < 0)
                    t = 0;
                if (t > 1)
                    t = 1;

                h2Pos += t * v;
            }

            //to be safe if it is realy evaluated
            res = h2Pos - h1Pos;

            return res.Length;
        }

        //****************************************************************************************************

        public virtual void update(Sprite sprite)
        {
            Position = sprite.Position;
        }

        public virtual void draw(RenderWindow window)
        {

        }
    }
}
