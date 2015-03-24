using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using Soulmate_Remastered.Classes.ItemFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.NPCFolder.ShopFolder
{
    class Shop
    {
        public static bool shopIsOpen { get; set; }
        List<Stack<AbstractItem>> sellableItems;
        List<Stack<AbstractItem>> buyableItems;
        static Texture shopTexture = new Texture("Pictures/Entities/NPC/Shop/ShopInterface.png");
        static Sprite sprite;
        const int collumCount = 2; //number of collums (sell, buy, maybe later more)
        const int lineCount = 10; //number of Items that can be shown at the same Time within one colum
        int selectedCollum = 0; //0 = sell Items / 1 = buy Items
        int selectedLine = 0;

        public Shop(List<Stack<AbstractItem>> itemsToBuy)
        {
            sprite = new Sprite(shopTexture);
            sprite.Position = new Vector2f((Game.windowSizeX - sprite.Texture.Size.X) / 2, (Game.windowSizeY - sprite.Texture.Size.Y) / 2);

            sellableItems = new List<Stack<AbstractItem>>();
            buyableItems = itemsToBuy;

            foreach (Stack<AbstractItem> item in ItemHandler.playerInventory.inventoryMatrix)
            {
                sellableItems.Add(item);
            }

            shopIsOpen = true;
        }

        private int getListLength() //return length of the selected List
        {
            switch (selectedCollum)
            {
                case 0:
                    return sellableItems.Count;
                case 1:
                    return buyableItems.Count;
                default:
                    return 0;
            }
        }

        public void Shopmanagement()
        {
            if (!Game.isPressed && Keyboard.IsKeyPressed(Controls.Right))
            {
                Game.isPressed = true;
                selectedCollum += 1;
                selectedCollum %= collumCount;
            }
            if (!Game.isPressed && Keyboard.IsKeyPressed(Controls.Left))
            {
                Game.isPressed = true;
                selectedCollum += collumCount - 1;
                selectedCollum %= collumCount;
            }
            if (!Game.isPressed && Keyboard.IsKeyPressed(Controls.Down))
            {
                Game.isPressed = true;
                selectedLine += 1;
                selectedLine %= getListLength();
            }
            if (!Game.isPressed && Keyboard.IsKeyPressed(Controls.Up))
            {
                Game.isPressed = true;
                selectedLine += getListLength() - 1;
                selectedLine %= getListLength();
            }
        }

        public void closeShop()
        {
            shopIsOpen = false;
            sprite = null;
        }

        public static void draw(RenderWindow window)
        {
            if (sprite != null)
            {
                window.Draw(sprite);
            }
        }
    }
}
