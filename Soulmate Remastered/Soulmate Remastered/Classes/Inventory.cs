using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;

namespace Soulmate_Remastered.Classes
{
    class Inventory
    {
        Texture inventoryTexture = new Texture("Pictures/Inventory/inventory.png");
        public Sprite inventory { get; set; }
        public float FIELDSIZE { get { return 50f; } }
        Texture selectedTexture = new Texture("Pictures/Inventory/Selected.png");
        Sprite selected;

        bool isPressed = false;
        int x = 0, y = 0; //Inventarsteurung

        uint inventoryWidth;
        uint inventoryLength;

        public AbstractItem[,] inventoryMatrix { get; set; }

        AbstractItem selectedItem;
        bool itemIsSelected = false;
        int selectedItemX;
        int selectedItemY;


        public Inventory()
        {
            inventory = new Sprite(inventoryTexture);
            inventory.Position = new Vector2f((Game.windowSizeX - inventoryTexture.Size.X) / 2, (Game.windowSizeY - inventoryTexture.Size.Y) / 2);

            selected = new Sprite(selectedTexture);
            selected.Position = inventory.Position;

            inventoryWidth = inventoryTexture.Size.X / selectedTexture.Size.X;
            inventoryLength = inventoryTexture.Size.Y / selectedTexture.Size.Y;

            inventoryMatrix = new AbstractItem[inventoryLength, inventoryWidth];
        }

        

        public bool isFull()
        {
            foreach (AbstractItem item in inventoryMatrix)
            {
                if (item == null)
                {
                    return false;
                }
            }
            return true;
        }

        public void managment()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && !isPressed)
            {
                y = (y + (inventoryMatrix.GetLength(0) - 1)) % inventoryMatrix.GetLength(0);
                isPressed = true;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && !isPressed)
            {
                y = (y + 1) % inventoryMatrix.GetLength(0);
                isPressed = true;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Right) && !isPressed)
            {
                x = (x + 1) % inventoryMatrix.GetLength(1);
                isPressed = true;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Left) && !isPressed)
            {
                x = (x + (inventoryMatrix.GetLength(1) - 1)) % inventoryMatrix.GetLength(1);
                isPressed = true;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && !isPressed)    //Item Swaps
            {
                if (!itemIsSelected)
                {
                    selectedItem = inventoryMatrix[y, x];
                    itemIsSelected = true;
                    selectedItemX = x;
                    selectedItemY = y;
                }
                else
                {
                    inventoryMatrix[selectedItemY, selectedItemX] = inventoryMatrix[y, x];
                    if (inventoryMatrix[selectedItemY, selectedItemX] != null)
                    {
                        inventoryMatrix[selectedItemY, selectedItemX].setPositionMatrix(selectedItemX, selectedItemY);
                    }

                    inventoryMatrix[y, x] = selectedItem;

                    if (inventoryMatrix[y, x] != null)
                    {
                        inventoryMatrix[y, x].setPositionMatrix(x, y);
                    }
                    selectedItem = null;
                    itemIsSelected = false;
                }
                isPressed = true;
            }

            if (!Keyboard.IsKeyPressed(Keyboard.Key.Down) && !Keyboard.IsKeyPressed(Keyboard.Key.Up) && !Keyboard.IsKeyPressed(Keyboard.Key.Right) && !Keyboard.IsKeyPressed(Keyboard.Key.Left) && !Keyboard.IsKeyPressed(Keyboard.Key.A))
                isPressed = false;


            selected.Position = new Vector2f(x * FIELDSIZE + inventory.Position.X, y * FIELDSIZE + inventory.Position.Y);
        }

        public void deleate()
        {
            inventoryMatrix = new AbstractItem[inventoryLength, inventoryWidth];
        }

        public void update(GameTime gameTime)
        {
            ItemHandler.updateInventoryMatrix(gameTime);
            managment();
        }

        public void draw(RenderWindow window)
        {
            window.Draw(inventory);
            ItemHandler.drawInventoryItems(window);
            window.Draw(selected);
        }
    }
}
