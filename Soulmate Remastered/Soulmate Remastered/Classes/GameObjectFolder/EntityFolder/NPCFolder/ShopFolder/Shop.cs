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
        List<ShopItem> sellableItems;
        List<ShopItem> buyableItems;
        Texture shopTexture = new Texture("Pictures/Entities/NPC/Shop/ShopInterface.png");
        Sprite sprite;
        Texture selectedTexture = new Texture("Pictures/Entities/NPC/Shop/Selected.png");
        Sprite selectedSprite;
        const int collumCount = 2; //number of collums (sell, buy, maybe later more)
        const int lineCount = 10; //number of Items that can be shown at the same Time within one colum
        int selectedCollum = 0; //0 = sell Items / 1 = buy Items
        int selectedLine = 0;
        int smallestDisplayedItem0 = 0;
        int smallestDisplayedItem1 = 0;

        Text[] sellableItemsText = new Text[lineCount];
        Text[] buyableItemsText = new Text[lineCount];

        Vector2f startSellPosition { get { return new Vector2f(sprite.Position.X, sprite.Position.Y + 100); } }
        Vector2f startBuyPosition { get { return new Vector2f(sprite.Position.X + 400, sprite.Position.Y + 100); } }
        Vector2f startPos
        {
            get
            {
                switch (selectedCollum)
                {
                    case 0:
                        return startSellPosition;
                    case 1:
                        return startBuyPosition;
                    default:
                        return new Vector2f();
                }
            }
        }

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
        int selectedListCount
        {
            get
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
        }

        public Shop(List<Stack<AbstractItem>> itemsToBuy)
        {
            sprite = new Sprite(shopTexture);
            sprite.Position = new Vector2f((Game.windowSizeX - sprite.Texture.Size.X) / 2, (Game.windowSizeY - sprite.Texture.Size.Y) / 2);

            selectedSprite = new Sprite(selectedTexture);
            selectedSprite.Position = new Vector2f(startSellPosition.X, startSellPosition.Y);

            sellableItems = new List<ShopItem>();
            buyableItems = ShopItem.ToShopItemList(itemsToBuy);

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

            foreach (Stack<AbstractItem> itemStack in ItemHandler.playerInventory.inventoryMatrix)
            {
                if (itemStack != null)
                    sellableItems.Add(new ShopItem(itemStack));
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
                if (smallestDisplayedItem + selectedLine < getListLength() - 1)
                {
                    if (selectedLine < lineCount - 1)
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

                if (smallestDisplayedItem > 0 || selectedLine > 0)
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
                    if (selectedListCount >= lineCount)
                        selectedLine = lineCount - 1;
                    else
                        selectedLine = selectedListCount - 1;

                    smallestDisplayedItem = evaluateIntSmallestDisplayedItem();
                }
            }

            //text_spriteUpdate();
        }

        //private void text_spriteUpdate()
        //{
        //    for (int i = 0; i < sellableItems.Count - smallestDisplayedItem0 && i < sellableItemsText.Length; i++)
        //    {
        //        sellableItemsText[i].DisplayedString = sellableItems[smallestDisplayedItem0 + i].Peek().type + "  " + sellableItems[smallestDisplayedItem0 + i].Count + "x";
        //    }

        //    for (int i = 0; i < buyableItems.Count - smallestDisplayedItem1 && i < buyableItemsText.Length; i++)
        //    {
        //        buyableItemsText[i].DisplayedString = buyableItems[smallestDisplayedItem1 + i].Peek().type + "  " + buyableItems[smallestDisplayedItem1 + i].Count + "x";
        //    }

        //    selectedSprite.Position = new Vector2f(startPos.X, startPos.Y + selectedLine * 50);
        //}

        private int evaluateIntSmallestDisplayedItem()
        {
            switch (selectedCollum)
            {
                case 0:
                    if (sellableItems.Count - 10 >= 0)
                        return sellableItems.Count - 10;
                    else
                        return 0;
                case 1:
                    if (buyableItems.Count - 10 >= 0)
                        return buyableItems.Count - 10;
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
                window.Draw(selectedSprite);

                //foreach (Text txt in sellableItemsText)
                //    if (txt != null)
                //        window.Draw(txt);

                //foreach (Text txt in buyableItemsText)
                //    if (txt != null)
                //        window.Draw(txt);
            }
        }
    }
}
