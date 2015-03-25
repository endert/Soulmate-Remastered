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
        Texture shopTexture = new Texture("Pictures/Entities/NPC/Shop/ShopInterface.png");
        Sprite sprite;
        const int collumCount = 2; //number of collums (sell, buy, maybe later more)
        const int lineCount = 10; //number of Items that can be shown at the same Time within one colum
        int selectedCollum = 0; //0 = sell Items / 1 = buy Items
        int selectedLine = 0;
        int smallestDisplayedItem0 = 0;
        int smallestDisplayedItem1 = 0;

        Text[] sellableItemsText = new Text[lineCount];
        Text[] buyableItemsText = new Text[lineCount];

        int smallestDisplayedItem
        {
            get
            {
                switch (selectedCollum)
                {
                    case 0:
                        return smallestDisplayedItem0;
                    case 1:
                        return smallestDisplayedItem1;
                    default:
                        return 0;
                }
            }

            set
            {
                switch (selectedCollum)
                {
                    case 0:
                        smallestDisplayedItem0 = value;
                        break;
                    case 1:
                        smallestDisplayedItem1 = value;
                        break;
                    default:
                        Console.WriteLine("ERROR!! MISSION ABORD MISION ABORED!!!!!!");
                        break;
                }
            }
        }

        public Shop(List<Stack<AbstractItem>> itemsToBuy)
        {
            sprite = new Sprite(shopTexture);
            sprite.Position = new Vector2f((Game.windowSizeX - sprite.Texture.Size.X) / 2, (Game.windowSizeY - sprite.Texture.Size.Y) / 2);

            sellableItems = new List<Stack<AbstractItem>>();
            buyableItems = itemsToBuy;

            for (int i = 0; i < sellableItemsText.Length; i++)
            {
                sellableItemsText[i] = new Text("", Game.font, 20);
                sellableItemsText[i].Color = Color.Black;
                sellableItemsText[i].Position = new Vector2f(sprite.Position.X + 20, sprite.Position.Y + 110 + i * 50);
            }

            for (int i = 0; i < buyableItemsText.Length; i++)
            {
                buyableItemsText[i] = new Text("", Game.font, 20);
                buyableItemsText[i].Color = Color.Black;
                buyableItemsText[i].Position = new Vector2f(sprite.Position.X + 420, sprite.Position.Y + 110 + i * 50);
            }

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
                if (smallestDisplayedItem + selectedLine < getListLength())
                {
                    if (selectedLine < lineCount)
                        selectedLine += 1;
                    else
                    {
                        smallestDisplayedItem += 1;
                    }
                }
                else
                {
                    selectedLine = 0;
                    smallestDisplayedItem = 0;
                }
            }

            if (!Game.isPressed && Keyboard.IsKeyPressed(Controls.Up))
            {
                Game.isPressed = true;

                if (smallestDisplayedItem > 0)
                {
                    if (selectedLine > 0)
                        selectedLine -= 1;
                    else
                    {
                        smallestDisplayedItem -= 1;
                    }
                }
                else
                {
                    selectedLine = lineCount;
                    smallestDisplayedItem = evaluateIntSmallestDisplayedItem();
                }
            }

            textUpdate();
        }

        private void textUpdate()
        {
            for (int i = 0; /*i < sellableItems.Count &&*/ i < sellableItemsText.Length; i++)
            {
                //sellableItemsText[i] = new Text(sellableItems[smallestDisplayedItem0 + i].Peek().type, Game.font, 20);
                sellableItemsText[i].DisplayedString = "Test";
            }

            for (int i = 0; i < buyableItems.Count && i < buyableItemsText.Length; i++)
            {
                buyableItemsText[i].DisplayedString = buyableItems[smallestDisplayedItem0 + i].Peek().type;
            }

        }

        private int evaluateIntSmallestDisplayedItem()
        {
            switch (selectedCollum)
            {
                case 0:
                    if (smallestDisplayedItem0 - 10 >= 0)
                        return smallestDisplayedItem0 - 10;
                    else
                        return 0;
                case 1:
                    if (smallestDisplayedItem1 - 10 >= 0)
                        return smallestDisplayedItem1 - 10;
                    else
                        return 0;
                default:
                    return 0;
            }
        }

        public void closeShop()
        {
            shopIsOpen = false;
            sprite = null;
        }

        public void draw(RenderWindow window)
        {
            if (sprite != null)
            {
                window.Draw(sprite);

                foreach (Text txt in sellableItemsText)
                    if (txt != null)
                        window.Draw(txt);

                foreach (Text txt in buyableItemsText)
                    if (txt != null)
                        window.Draw(txt);
            }
        }
    }
}
