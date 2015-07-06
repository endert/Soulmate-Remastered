using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder
{
    abstract class GameObject : IComparable<GameObject>
    {
        /// <summary>
        /// for sorting the List
        /// </summary>
        public float YCoordinate { get { return Position.Y + Sprite.Texture.Size.Y; } } 
        //Parameters for gameObjects
        public  HitBox HitBox { get; set; }
        public  Sprite Sprite { get; set; }
        protected List<Texture> TextureList = new List<Texture>();
        public Texture CurrentTexture { get { return Sprite.Texture; } }
        public Vector2 Position { get; set; }
        /// <summary>
        /// the type of this gameobject
        /// </summary>
        public virtual string Type { get { return "Object"; } }
        protected string CustomName = "";
        public string Name { get { if (!CustomName.Equals("")) return CustomName; else return Type.Split('.')[Type.Split('.').Length - 1]; } }
        public bool IsAlive { get; protected set; }
        public int IndexObjectList { get; set; }
        public virtual bool Walkable { get { return false; } }
        public bool IsVisible { get; protected set; }

        public static char LineBreak { get { return ';'; } }

        public int CompareTo(GameObject gameObject) //for sort in the ObjectList
        {
            return (int)(this.YCoordinate - gameObject.YCoordinate);
        }

        public void Kill()
        {
            IsAlive = false;
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(RenderWindow window)
        {
            window.Draw(Sprite);
        }

        public virtual void DebugDraw(RenderWindow window)
        {
            HitBox.draw(window);
        }
    }
}
