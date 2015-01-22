using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder
{
    public abstract class GameObject
    {
        //Parameters for gameObjects
        public  HitBox hitBox { get; set; }
        public  Sprite sprite { get; set; }
        protected List<Texture> textureList = new List<Texture>();
        public Vector2f position { get; set; }
        protected String type = "Object";
        public String TYPE { get { return type; } }
        public bool isAlive { get; set; }
        public int indexObjectList { get; set; }
        public bool walkable { get; set; }


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
