using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soulmate_Remastered.Classes.MapFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.NPCFolder.ShopFolder;

namespace Soulmate_Remastered.Classes.GameObjectFolder
{
    class GameObjectHandler
    {
        public static List<GameObject> gameObjectList { get; set; }
        public static EntityHandler entityHandler { get; set; }
        public static ItemHandler itemHandler { get; set; }

        public static Map lvlMap { get; set; }
        public static int lvl { get; set; }
        
        public GameObjectHandler(Map _lvlMap, int _lvl)
        {
            lvlMap = _lvlMap;
            lvl = _lvl;
            
            gameObjectList = new List<GameObject>();
            entityHandler = new EntityHandler();
            itemHandler = new ItemHandler();
        }

        public static void add(GameObject obj)
        {
            gameObjectList.Add(obj);
        }

        public static void removeAt(int index)
        {
            if (index < gameObjectList.Count)
            {
                gameObjectList.RemoveAt(index);
            }
            for (int i = 0; i < gameObjectList.Count; i++)
            {
                gameObjectList[i].indexObjectList = i;
            }
        }

        public static void deleateType(String _type)
        {
            for (int i = 0; i < gameObjectList.Count; i++)
            {
                if (gameObjectList[i].type.Equals(_type))
                {
                    gameObjectList.RemoveAt(i);
                    i--;
                }
            }
        }

        public void deleate()
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
            gameObjectList.Sort();

            for (int i = 0; i < gameObjectList.Count; ++i)
            {
                if (!gameObjectList[i].isAlive)
                {
                    gameObjectList.RemoveAt(i);
                    i--;
                }
                else
                {
                    gameObjectList[i].indexObjectList = i;
                    gameObjectList[i].update(gameTime);
                }
            }

            entityHandler.update(gameTime);
            itemHandler.update(gameTime);
        }

        public void draw(RenderWindow window)
        {
            foreach (GameObject gObj in gameObjectList)
            {
                if (gObj.isVisible)
                {
                    gObj.draw(window);
                }
            }
        }

        public void debugDraw(RenderWindow window)
        {
            foreach (GameObject obj in gameObjectList)
                obj.debugDraw(window);
        }
    }
}