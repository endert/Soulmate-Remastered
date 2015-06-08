using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.MapFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.ProjectileFolder;
using Soulmate_Remastered.Core;

namespace Soulmate_Remastered.Classes.GameObjectFolder
{
    class HitBox : AbstractHitBox
    {
        RectangleShape r;
        public float width { get { return Size.X; } }
        public float height { get { return Size.Y; } }

        public HitBox(Vector2 pos, Vector2 size)
            : base(pos, size)
        {
            r = new RectangleShape(Size);
            r.FillColor = Color.Transparent;
            r.OutlineThickness = 2;
            r.OutlineColor = Color.Red;
            r.Position = Position;
        }

        public HitBox(Vector2 pos, float width, float height) : this(pos, new Vector2(width, height)) { }
        public HitBox(float x, float y, Vector2 size) : this(new Vector2(x, y), size) { }
        public HitBox(float x, float y, float width, float height) : this(new Vector2(x, y), new Vector2(width, height)) { }

        public override void update(Sprite sprite)
        {
            base.update(sprite);
            r.Position = Position;
        }

        public override void draw(RenderWindow window)
        {
            r.Position = Position;
            window.Draw(r);
        }
    }
}
