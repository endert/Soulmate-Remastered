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
                gameObjectList[i].IndexObjectList = i;
            }
        }

        public static void deleateType(String _type)
        {
            for (int i = 0; i < gameObjectList.Count; i++)
            {
                if (gameObjectList[i].Type.Equals(_type))
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
                gObj.Kill();
            }
            EntityHandler.deleate();
            ItemHandler.deleate();
        }

        public void update(GameTime gameTime)
        {
            gameObjectList.Sort();

            for (int i = 0; i < gameObjectList.Count; ++i)
            {
                if (!gameObjectList[i].IsAlive)
                {
                    gameObjectList.RemoveAt(i);
                    i--;
                }
                else
                {
                    gameObjectList[i].IndexObjectList = i;
                    gameObjectList[i].Update(gameTime);
                }
            }

            entityHandler.update(gameTime);
            itemHandler.update(gameTime);
        }

        public void draw(RenderWindow window)
        {
            foreach (GameObject gObj in gameObjectList)
            {
                if (gObj.IsVisible)
                {
                    gObj.Draw(window);
                }
            }
        }

        public void debugDraw(RenderWindow window)
        {
            foreach (GameObject obj in gameObjectList)
                obj.DebugDraw(window);
        }
    }
}