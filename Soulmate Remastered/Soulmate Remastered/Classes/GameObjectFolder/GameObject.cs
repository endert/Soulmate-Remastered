using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder
{
    abstract class GameObject
    {
        //Parameters for gameObjects
        public  HitBox hitBox { get; set; }
        public  Sprite sprite { get; set; }
        protected List<Texture> textureList = new List<Texture>();
        public Vector2f position { get; set; }
        public String type { get { return "Object"; } }
        public bool isAlive { get; set; }
        public int indexObjectList { get; set; }
        protected bool _walkable = false;
        public bool walkable { get { return _walkable; } set { _walkable = value; } }
        protected bool visible = true;  //standart is visible
        public bool isVisible { get { return visible; } }


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
