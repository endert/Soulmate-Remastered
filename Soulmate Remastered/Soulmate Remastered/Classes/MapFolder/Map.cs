using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soulmate_Remastered.Classes.GameObjectFolder;
using Soulmate_Remastered.Core;
using System.Drawing;

namespace Soulmate_Remastered.Classes.MapFolder
{
    /// <summary>
    /// the map of the game where the player moves
    /// </summary>
    class Map
    {

        Sprite backGround = new Sprite(new Texture("Pictures/Map/Village.png"));
        Blocks[,] map;
        Texture blockTex;

        public int objectSize = 32;

        public static String white = "ffffffff"; //Boden
        public static String black = "ff000000"; //Wald

        RectangleShape debugHitbox;

        public Vector2 MapSize
        {
            get
            {
                return new Vector2(map.GetLength(0) * objectSize, map.GetLength(1) * objectSize);
            }
        }

        public bool getWalkable(HitBox hitBox, Vector2 direction)
        {
            Vector2 newPosition = hitBox.Position + direction;

            //Map-Borders, return false if u ran out of the map
            if (newPosition.X < 0 || newPosition.Y < 0 || newPosition.X >= map.GetLength(0) * objectSize || newPosition.Y >= map.GetLength(1) * objectSize
                || newPosition.X + direction.X >= map.GetLength(0) * objectSize || newPosition.Y + direction.Y >= map.GetLength(1) * objectSize)
                return false;

            foreach (Blocks block in map)
            {
                if (block.getBlockHitBox != null && hitBox.WillHit(direction, block.getBlockHitBox))
                    return false;
            }

            return true;
        }

        public Map(Bitmap mask)
        {
            debugHitbox = new RectangleShape(new Vector2(objectSize, objectSize));
            debugHitbox.FillColor = SFML.Graphics.Color.Transparent;
            debugHitbox.OutlineColor = SFML.Graphics.Color.Red;
            debugHitbox.OutlineThickness = 1;


            map = new Blocks[mask.Width, mask.Height];

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    blockTex = new Texture("Pictures/Map/Ground/Sand.png");
                    if (mask.GetPixel(i, j).Name == white)
                        map[i, j] = new Blocks(0, new Vector2f(i * objectSize, j * objectSize), blockTex);
                    if (mask.GetPixel(i, j).Name == black)
                        map[i, j] = new Blocks(1, new Vector2f(i * objectSize, j * objectSize), blockTex);
                }
            }
        }

        public void draw(RenderWindow window)
        {
            window.Draw(backGround);
        }

        public void debugDraw(RenderWindow window)
        {
            foreach (Blocks b in map)
            {
                if (b.getBlockHitBox != null)
                {
                    debugHitbox.Position = b.getBlockHitBox.Position;
                    window.Draw(debugHitbox);
                }
            }
        }
    }
}
