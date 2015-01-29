using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Soulmate_Remastered.Classes.GameObjectFolder;

namespace Soulmate_Remastered.Classes.MapFolder
{
    class Map
    {
        Blocks[,] map;
        Texture blockTex;
        //List<EnemyHandler> EnemyList;

        public int objectSize = 32;

        public static String white = "ffffffff"; //Boden
        public static String black = "ff000000"; //Wald

        public bool getWalkable(HitBox hitBox, Vector2f vector)
        {
            bool walkable = true;
            Vector2f newPosition = new Vector2f(hitBox.Position.X + vector.X, hitBox.Position.Y + vector.Y);

            //Map-Grenzen ;; muss auch abbrechen wenn die newPosition + Vector außerhalb liegt
            //Entschulidgung für die Änderung, aber anders hat es ständig exceptions geworfen ;(
            if (newPosition.X < 0 || newPosition.Y < 0 || newPosition.X >= map.GetLength(0) * objectSize || newPosition.Y >= map.GetLength(1) * objectSize
                || newPosition.X + vector.X >= map.GetLength(0) * objectSize || newPosition.Y + vector.Y >= map.GetLength(1) * objectSize)
                return false;

            try
            {
                //Kollision
                if (!(map[(int)(newPosition.X / objectSize), (int)(newPosition.Y / objectSize)].getWalkable()/*links oben*/
                    && map[(int)(newPosition.X / objectSize), (int)((newPosition.Y + hitBox.height) / objectSize)].getWalkable()/*links unten*/
                    && map[(int)(newPosition.X / objectSize), (int)((newPosition.Y + hitBox.height / 2)) / objectSize].getWalkable()/*links mitte*/

                    && map[(int)((newPosition.X + hitBox.width) / objectSize), (int)(newPosition.Y / objectSize)].getWalkable()/*rechts oben*/
                    && map[(int)((newPosition.X + hitBox.width) / objectSize), (int)((newPosition.Y + hitBox.height) / objectSize)].getWalkable()/*rechts unten*/
                    && map[(int)((newPosition.X + hitBox.width) / objectSize), (int)((newPosition.Y + hitBox.height / 2) / objectSize)].getWalkable()/*rechts mitte*/

                    && map[(int)((newPosition.X + hitBox.width / 2) / objectSize), (int)(newPosition.Y / objectSize)].getWalkable()/*oben mitte*/
                    && map[(int)((newPosition.X + hitBox.width / 2) / objectSize), (int)((newPosition.Y + hitBox.height) / objectSize)].getWalkable()/*unten mitte*/
                    ))
                    return false;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }

            //foreach (Blocks block in map)
            //{
            //    if(block.getBlockHitBox != null)
            //    {
            //        if(block.getBlockHitBox.hit(new HitBox(newPosition, hitBox.width, hitBox.height)))
            //        {
            //            walkable = false;
            //        }
            //    }
            //}

            return walkable;
        }

        public Map(Bitmap mask)
        {
            map = new Blocks[mask.Width, mask.Height];

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    blockTex = new Texture("Pictures/Map/Sand.png");
                    if (mask.GetPixel(i, j).Name == white)
                        map[i, j] = new Blocks(0, new Vector2f(i * objectSize, j * objectSize), blockTex);
                    if (mask.GetPixel(i, j).Name == black)
                        map[i, j] = new Blocks(1, new Vector2f(i * objectSize, j * objectSize), blockTex);
                }
            }
        }

        public void draw(RenderWindow window)
        {
            foreach (Blocks block in map)
            {
                block.draw(window);
            }
        }
    }
}
