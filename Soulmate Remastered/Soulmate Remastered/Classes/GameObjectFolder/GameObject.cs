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
        public float yCoordinate { get { return position.Y + sprite.Texture.Size.Y; } }  // for sorting the list
        //Parameters for gameObjects
        public  HitBox hitBox { get; set; }
        public  Sprite sprite { get; set; }
        protected List<Texture> textureList = new List<Texture>();
        public Texture currentTexture { get { return sprite.Texture; } }
        public Vector2 position { get; set; }
        public virtual String type { get { return "Object"; } }
        protected String customName = "";
        public String name { get { if (!customName.Equals("")) return customName; else return type.Split('.')[type.Split('.').Length - 1]; } }
        protected bool _isAlive = true;
        public bool isAlive { get { return _isAlive; } set { _isAlive = value; } }
        public int indexObjectList { get; set; }
        public virtual bool walkable { get { return false; } }
        protected bool visible = true;  //standart is visible
        public bool isVisible { get { return true; } }

        public static Char lineBreak { get { return ';'; } }

        public int CompareTo(GameObject gameObject) //for sort in the ObjectList
        {
            return (int)(this.yCoordinate - gameObject.yCoordinate);
        }

        public void kill()
        {
            isAlive = false;
        }

        public List<GameObject> getTouchedObject()
        {
            List<GameObject> gObjList = new List<GameObject>();
            for (int i = 0; i < GameObjectHandler.gameObjectList.Count; i++)
            {
                if ((i != indexObjectList) && (hitBox.hit(GameObjectHandler.gameObjectList[i].hitBox)))
                {
                    gObjList.Add(GameObjectHandler.gameObjectList[i]);
                    Console.WriteLine(GameObjectHandler.gameObjectList[i].type);
                }
            }
            return gObjList;
        }

        public abstract void update(GameTime gameTime);

        public virtual void draw(RenderWindow window)
        {
            window.Draw(sprite);
        }

        public void debugDraw(RenderWindow window)
        {
            hitBox.draw(window);
        }
    }
}
