using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder
{
    abstract class GameObject : IComparable<GameObject>
    {
        public float yCordinate { get { return position.Y + sprite.Texture.Size.Y; } }  // for sorting the list
        //Parameters for gameObjects
        public  HitBox hitBox { get; set; }
        public  Sprite sprite { get; set; }
        protected List<Texture> textureList = new List<Texture>();
        public Vector2f position { get; set; }
        public virtual String type { get { return "Object"; } }
        protected bool _isAlive = true;
        public bool isAlive { get { return _isAlive; } set { _isAlive = value; } }
        public int indexObjectList { get; set; }
        public virtual bool walkable { get { return false; } }
        protected bool visible = true;  //standart is visible
        public bool isVisible { get { return true; } }

        public int CompareTo(GameObject gameObject) //for sort in the ObjectList
        {
            return (int)(this.yCordinate - gameObject.yCordinate);
        }

        public void kill()
        {
            isAlive = false;
        }

        public abstract void update(GameTime gameTime);

        public virtual void draw(RenderWindow window)
        {
            window.Draw(sprite);
        }
    }
}
