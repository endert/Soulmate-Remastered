using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.MapFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.ProjectileFolder;

namespace Soulmate_Remastered.Classes.GameObjectFolder
{
    class HitBox
    {
        Texture testTexture;
        Sprite testSprite;
        static bool visible = false;
        public static bool VISIBLE { get { return visible; } set { visible = value; } }
        public Vector2f Position { get; set; }
        public float width { get; set; }
        public float height { get; set; }
        private float spriteHeight;

        Vector2f unionPos;
        float unionWidth;
        float unionHeight;

        public void setPosition(Vector2f _pos)
        {
            Position = new Vector2f(_pos.X, _pos.Y + (2 * spriteHeight) / 5);
        }

        public HitBox(Blocks block)
        {
            Position = block.getSprite.Position;
            width = block.getSprite.Texture.Size.X;
            height = block.getSprite.Texture.Size.Y;
            spriteHeight = height;
        }

        public HitBox(AbstractProjectile projectile)
        {
            Position = projectile.position;
            width = projectile.sprite.Texture.Size.X;
            height = projectile.sprite.Texture.Size.Y;
            spriteHeight = height;
            testTexture = new Texture("Pictures/Inventory/Selected.png");
            testSprite = new Sprite(testTexture);
            testSprite.Scale = new Vector2f(width / testTexture.Size.X, height / testTexture.Size.Y);
            testSprite.Position = Position;
        }

        public HitBox(Vector2f pos, float _width, float _height)
        {
            Position = new Vector2f(pos.X, pos.Y + (2 * _height) / 5);
            width = _width;
            height = (_height * 3) / 5;
            spriteHeight = _height;
            testTexture = new Texture("Pictures/Inventory/Selected.png");
            testSprite = new Sprite(testTexture);
            testSprite.Scale = new Vector2f(width / testTexture.Size.X, height / testTexture.Size.Y);
            testSprite.Position = Position;
        }

        public Vector2f hitFrom(HitBox h)
        {
            Vector2f hitFrom = new Vector2f(0, 0);

            if (hitWithoutInsection(h))
            {
                if ((Position.X + width) < h.Position.X)
                {
                    hitFrom.X = 1;
                }
                else
                {
                    if ((h.Position.X + h.width) < Position.X)
                    {
                        hitFrom.X = -1;
                    }
                    else
                    {
                        hitFrom.X = 0;
                    }
                }

                if ((Position.Y + height) <= h.Position.Y)
                {
                    hitFrom.Y = 1;
                }
                else
                {
                    if ((h.Position.Y + h.height) <= Position.Y)
                    {
                        hitFrom.Y = -1;
                    }
                    else
                    {
                        hitFrom.Y = 0;
                    }
                }
            }

            if (hit(h) && !hitWithoutInsection(h))
            {
                if ((Position.X) <= h.Position.X)
                {
                    hitFrom.X = 1;
                }
                else
                {
                    hitFrom.X = -1;
                }

                if ((Position.Y) <= h.Position.Y)
                {
                    hitFrom.Y = 1;
                }
                else
                {
                    hitFrom.Y = -1;
                }
            }

            return hitFrom;
        }

        public float distanceTo(HitBox h)
        {
            float distanceX = 0;
            float distanceY = 0;

            union(h);
            if (unionWidth > (this.width + h.width))
            {
                distanceX = unionWidth - (this.width + h.width);
            }

            if (unionHeight > (this.height + h.height))
            {
                distanceY = unionHeight - (this.height + h.height);
            }

            return (float)Math.Sqrt(Math.Pow(distanceX, 2) + Math.Pow(distanceY, 2));
        }

        public bool hit(HitBox h)
        {
            if (testSprite != null)
            {
                testSprite.Position = Position;
            }
            if (h != null)
            {
                union(h);
                if ((unionWidth <= (this.width + h.width)) && (unionHeight <= (this.height + h.height)))
                    return true;
                else
                    return false;
            }

            return false;
        }

        public bool hitWithoutInsection(HitBox h)
        {
            union(h);
            if (((unionWidth <= (this.width + h.width)) && (unionHeight == (this.height + h.height))) ||
                ((unionWidth == (this.width + h.width)) && (unionHeight <= (this.height + h.height))))
                return true;
            else
                return false;
        }

        private void union(HitBox h)
        {
            Vector2f thisBottomRight = new Vector2f(this.Position.X + this.width, this.Position.Y + this.height);
            Vector2f hBottomRight = new Vector2f(h.Position.X + h.width, h.Position.Y + h.height);

            unionPos = new Vector2f((this.Position.X <= h.Position.X) ? (this.Position.X) : (h.Position.X),
                (this.Position.Y <= h.Position.Y) ? (this.Position.Y) : (h.Position.Y));

            unionWidth = ((thisBottomRight.X >= hBottomRight.X) ? (thisBottomRight.X) : (hBottomRight.X)) - unionPos.X;
            unionHeight = ((thisBottomRight.Y >= hBottomRight.Y) ? (thisBottomRight.Y) : (hBottomRight.Y)) - unionPos.Y;
        }

        public void update(Sprite sprite)
        {
            Position = new Vector2f(sprite.Position.X, sprite.Position.Y + (2 * spriteHeight) / 5);
            width = sprite.Texture.Size.X;
            height = sprite.Texture.Size.Y * 3 / 5;
            testSprite.Scale = new Vector2f(width / testTexture.Size.X, height / testTexture.Size.Y);
            testSprite.Position = Position;
        }

        public void draw(RenderWindow window)
        {
            if (testSprite != null)
            {
                window.Draw(testSprite);
            }
        }
    }
}
