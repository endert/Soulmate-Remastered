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
        /// <summary>
        /// the list, that contains all gameObjects, sorted according to their highest y-Coordinate
        /// </summary>
        public static List<GameObject> GameObjectList { get; set; }

        /// <summary>
        /// the map for the current lvl
        /// </summary>
        public static Map LvlMap { get; set; }
        /// <summary>
        /// the current lvl
        /// </summary>
        public static int Lvl { get; set; }

        public static void Initialize(Map _lvlMap, int _lvl)
        {
            LvlMap = _lvlMap;
            Lvl = _lvl;

            GameObjectList = new List<GameObject>();
            ItemHandler.Initialize();
            EntityHandler.Initialize();
        }

        /// <summary>
        /// adds the gameObject to the list
        /// </summary>
        /// <param name="obj"></param>
        public static void Add(GameObject obj)
        {
            GameObjectList.Add(obj);
        }

        /// <summary>
        /// deleates all objects, wich match the given type
        /// </summary>
        /// <param name="_type"></param>
        public static void DeleateType(Type _type)
        {
            for (int i = 0; i < GameObjectList.Count; i++)
            {
                if (GameObjectList[i].GetType().Equals(_type))
                {
                    GameObjectList.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <summary>
        /// deleates the gameobject list and calls the Deleate Methode of the other handler
        /// </summary>
        public static void Deleate()
        {
            foreach (GameObject gObj in GameObjectList)
            {
                gObj.Kill();
            }
            EntityHandler.Deleate();
            ItemHandler.Deleate();
        }

        /// <summary>
        /// update the list and all gameobjects
        /// </summary>
        /// <param name="gameTime"></param>
        public static void Update(GameTime gameTime)
        {
            GameObjectList.Sort();

            for (int i = 0; i < GameObjectList.Count; ++i)
            {
                if (!GameObjectList[i].IsAlive)
                {
                    GameObjectList.RemoveAt(i);
                    i--;
                }
                else
                {
                    GameObjectList[i].IndexObjectList = i;
                    GameObjectList[i].Update(gameTime);
                }
            }

            EntityHandler.Update(gameTime);
            ItemHandler.Update(gameTime);
        }

        /// <summary>
        /// draws every gameobject, that is visible
        /// </summary>
        /// <param name="window"></param>
        public static void Draw(RenderWindow window)
        {
            foreach (GameObject gObj in GameObjectList)
            {
                if (gObj.IsVisible)
                {
                    gObj.Draw(window);
                }
            }
        }

        /// <summary>
        /// draws the hitboxes
        /// </summary>
        /// <param name="window"></param>
        public static void DebugDraw(RenderWindow window)
        {
            foreach (GameObject obj in GameObjectList)
                obj.DebugDraw(window);
        }
    }
}