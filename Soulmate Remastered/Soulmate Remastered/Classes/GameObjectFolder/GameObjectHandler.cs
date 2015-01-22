using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soulmate_Remastered.Classes.MapFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;

namespace Soulmate_Remastered.Classes.GameObjectFolder
{
    class GameObjectHandler
    {
        public static List<GameObject> gameObjectList { get; set; }

        public static EntityHandler entityHandler { get; set; }

        public static Map lvlMap { get; set; }
        public static int lvl { get; set; }
        
        public GameObjectHandler(Map _lvlMap, int _lvl)
        {
            lvlMap = _lvlMap;
            lvl = _lvl;
            gameObjectList = new List<GameObject>();
            entityHandler = new EntityHandler();
        }

        public static void add(GameObject obj)
        {
            gameObjectList.Add(obj);
        }

        public static void add(List<GameObject> objs)
        {
            foreach (GameObject obj in objs)
            {
                gameObjectList.Add(obj);
            }
        }

        public static void deleateType(String type)
        {
            for (int i = 0; i < gameObjectList.Count; i++)
            {
                if (gameObjectList[i].TYPE.Equals(type))
                {
                    gameObjectList.RemoveAt(i);
                    i--;
                }
            }
        }

        static public void deleate()
        {
            foreach (GameObject gObj in gameObjectList)
            {
                gObj.kill();
            }
            EntityHandler.deleate();
            ItemHandler.deleate();
        }

        public void update(GameTime gameTime)
        {
            for (int i = 0; i < gameObjectList.Count; i++)
            {
                if (!gameObjectList[i].isAlive)
                {
                    gameObjectList.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < gameObjectList.Count; i++)
            {
                gameObjectList[i].indexObjectList = i;
                gameObjectList[i].update(gameTime);
            }

            entityHandler.update(gameTime);
        }

        public void draw(RenderWindow window)
        {
            foreach (GameObject gObj in gameObjectList)
            {
                gObj.draw(window);
            }
        }
    }
}