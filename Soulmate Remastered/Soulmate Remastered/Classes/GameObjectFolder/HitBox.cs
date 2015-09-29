using SFML.Graphics;
using Soulmate_Remastered.Core;

namespace Soulmate_Remastered.Classes.GameObjectFolder
{
    /// <summary>
    /// Hitbox class für GameObjects
    /// </summary>
    class HitBox : BaseHitBox
    {
        RectangleShape r;
        /// <summary>
        /// x size of this Hitbox
        /// </summary>
        public float Width { get { return Size.X; } }
        /// <summary>
        /// y size of this hitbox
        /// </summary>
        public float Height { get { return Size.Y; } }

        /// <summary>
        /// initialize this
        /// </summary>
        /// <param name="pos">the position</param>
        /// <param name="size">the size</param>
        public HitBox(Vector2 pos, Vector2 size)
            : base(pos, size)
        {
            r = new RectangleShape(Size);
            r.FillColor = Color.Transparent;
            r.OutlineThickness = 2;
            r.OutlineColor = Color.Red;
            r.Position = Position;
        }

        /// <summary>
        /// initialize this
        /// </summary>
        /// <param name="pos">the position</param>
        /// <param name="width">x size</param>
        /// <param name="height">y size</param>
        public HitBox(Vector2 pos, float width, float height) : this(pos, new Vector2(width, height)) { }
        /// <summary>
        /// initialize this
        /// </summary>
        /// <param name="x">x position</param>
        /// <param name="y">y position</param>
        /// <param name="size">the size</param>
        public HitBox(float x, float y, Vector2 size) : this(new Vector2(x, y), size) { }
        /// <summary>
        /// initialize this
        /// </summary>
        /// <param name="x">x position</param>
        /// <param name="y">y position</param>
        /// <param name="width">x size</param>
        /// <param name="height">y size</param>
        public HitBox(float x, float y, float width, float height) : this(new Vector2(x, y), new Vector2(width, height)) { }

        /// <summary>
        /// updates this, according to the given sprite
        /// </summary>
        /// <param name="sprite"></param>
        public override void Update(Sprite sprite)
        {
            base.Update(sprite);
            r.Position = Position;
        }

        /// <summary>
        /// debug draw only,
        /// <para>draws a red rectangle where the hitbox is</para>
        /// </summary>
        /// <param name="window"></param>
        public override void Draw(RenderWindow window)
        {
            r.Position = Position;
            window.Draw(r);
        }
    }
}
