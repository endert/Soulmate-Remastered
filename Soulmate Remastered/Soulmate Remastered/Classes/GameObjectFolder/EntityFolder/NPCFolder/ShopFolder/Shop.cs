using SFML.Graphics;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using Soulmate_Remastered.Core;
using System;
using System.Collections.Generic;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.NPCFolder.ShopFolder
{
    class Shop
    {
        /// <summary>
        /// bool if the Shop is open or not
        /// </summary>
        public static bool ShopIsOpen { get; protected set; }
        /// <summary>
        /// sellable items
        /// </summary>
        List<ShopItem> sellableItems;
        /// <summary>
        /// buyable items
        /// </summary>
        List<ShopItem> buyableItems;
        /// <summary>
        /// Texture that is used fpr the Shops background
        /// </summary>
        Texture shopTexture = new Texture("Pictures/Entities/NPC/Shop/ShopInterface.png");
        /// <summary>
        /// the shops sprite
        /// </summary>
        Sprite sprite;
        /// <summary>
        /// Size of the selected Marker
        /// </summary>
        Vector2 selectedSize = new Vector2(400, 50);
        /// <summary>
        /// the thickness of the border
        /// </summary>
        float outlineThickness = 2;
        /// <summary>
        /// the selected Marker
        /// </summary>
        RectangleShape selected;
        /// <summary>
        /// the number wich shows the amount of gold the players own
        /// </summary>
        Text playerGoldText;
        /// <summary>
        /// number of Items that can be shown at the same Time within one collum
        /// </summary>
        int LineCount { get { return 10; } }
        /// <summary>
        /// the collum the marker is in
        /// </summary>
        Collum selectedCollum = Collum.Sell;
        /// <summary>
        /// index of the selected Line
        /// </summary>
        int selectedLine = 0;
        /// <summary>
        /// the index of the first shown Item in the sell List
        /// </summary>
        int smallestDisplayedItem0 = 0;
        /// <summary>
        /// the index ot the first shown Item in the buy List
        /// </summary>
        int smallestDisplayedItem1 = 0;
        /// <summary>
        /// the position where the List starts in the sell Collum
        /// </summary>
        Vector2 StartSellPosition { get { return new Vector2(sprite.Position.X, sprite.Position.Y + 100); } }
        /// <summary>
        /// the position where the List starts in the buy Collum
        /// </summary>
        Vector2 StartBuyPosition { get { return new Vector2(sprite.Position.X + 400, sprite.Position.Y + 100); } }
        /// <summary>
        /// the list in wich the cursor is at the Moment
        /// </summary>
        List<ShopItem> SelectedList
        {
            get
            {
                switch (selectedCollum)
                {
                    case Collum.Sell:
                        return sellableItems;
                    case Collum.Buy:
                        return buyableItems;
                    default:
                        throw new NotImplementedException();
                }
            }
        }
        /// <summary>
        /// the needed startposition for the Marker
        /// </summary>
        Vector2 StartPos
        {
            get
            {
                switch (selectedCollum)
                {
                    case Collum.Sell:
                        return StartSellPosition + outlineThickness;
                    case Collum.Buy:
                        return StartBuyPosition + outlineThickness;
                    default:
                        return new Vector2();
                }
            }
        }
        /// <summary>
        /// the index of the first shown item in the selected Collum
        /// </summary>
        int SmallestDisplayedItem
        {
            get
            {
                switch (selectedCollum)
                {
                    case Collum.Sell:
                        return smallestDisplayedItem0;
                    case Collum.Buy:
                        return smallestDisplayedItem1;
                    default:
                        return 0;
                }
            }

            set
            {
                switch (selectedCollum)
                {
                    case Collum.Sell:
                        smallestDisplayedItem0 = value;
                        break;
                    case Collum.Buy:
                        smallestDisplayedItem1 = value;
                        break;
                    default:
                        Console.WriteLine("ERROR!! MISSION ABORD MISION ABORED!!!!!!");
                        break;
                }
            }
        }
        /// <summary>
        /// the number of Items inside the selected List
        /// </summary>
        int SelectedListCount
        {
            get
            {
                switch (selectedCollum)
                {
                    case Collum.Sell:
                        return sellableItems.Count;
                    case Collum.Buy:
                        return buyableItems.Count;
                    default:
                        return 0;
                }
            }
        }

        /// <summary>
        /// Collums
        /// </summary>
        public enum Collum
        {
            Sell,
            Buy,

            CollumCount
        }

        public Shop(List<Stack<AbstractItem>> itemsToBuy)
        {
            KeyboardControler.KeyPressed += OnKeyPress;

            //initialize sprite
            sprite = new Sprite(shopTexture);
            sprite.Position = new Vector2((Game.WindowSizeX - sprite.Texture.Size.X) / 2, (Game.WindowSizeY - sprite.Texture.Size.Y) / 2);

            //initialize Marker
            selected = new RectangleShape(selectedSize+(-2*outlineThickness));
            selected.Position = StartPos;
            selected.FillColor = Color.Transparent;
            selected.OutlineThickness = outlineThickness;
            selected.OutlineColor = Color.Red;

            

            playerGoldText = new Text(PlayerHandler.Player.Gold.ToString(), Game.font, 20);
            playerGoldText.Position = new Vector2(sprite.Position.X, sprite.Position.Y);

            //initialize ItemLists**************************************************************************

            //Buyable Items
            buyableItems = ShopItem.ToShopItemList(itemsToBuy);
            for (int i = 0; i < buyableItems.Count; i++)
            {
                buyableItems[i].Position = new Vector2(StartBuyPosition.X, StartBuyPosition.Y + i * 50);

                if (i >= LineCount)
                    buyableItems[i].Visible = false;
            }

            //sellable Items
            sellableItems = new List<ShopItem>();
            //foreach (Stack<AbstractItem> itemStack in ItemHandler.playerInventory.inventoryMatrix)
            //{
            //    if (itemStack != null && itemStack.Count > 0)
            //        sellableItems.Add(new ShopItem(itemStack));
            //}
            for (int i = 0; i < sellableItems.Count; i++)
            {
                sellableItems[i].Position = new Vector2(StartSellPosition.X, StartSellPosition.Y + i * 50);

                if (i >= LineCount)
                    sellableItems[i].Visible = false;
            }

            //**********************************************************************************************

            //open Shop
            ShopIsOpen = true;
        }

        void OnKeyPress(object sender, Core.KeyEventArgs e)
        {
            if(e.Key == Controls.Key.Right)
                selectedCollum = (Collum)((int)++selectedCollum % (int)Collum.CollumCount);

            if(e.Key == Controls.Key.Left)
            {
                selectedCollum += (int)Collum.CollumCount - 1;
                selectedCollum = (Collum)((int)selectedCollum % (int)Collum.CollumCount);
            }

            if(e.Key == Controls.Key.Down)
            {
                /*
                 * if the index of the first shown item + index of the selected Line = selectedLineCount - 1 then the last item of the List is selected
                 * because then we got the index of the last item in this List
                 */

                //checking if we can still scroll on
                if (SmallestDisplayedItem + selectedLine < SelectedListCount - 1)
                {
                    //if we are not at the last shown line set line++
                    if (selectedLine < LineCount - 1)
                        selectedLine += 1;
                    //else rise the index of the first shown Item
                    else
                        SmallestDisplayedItem += 1;
                }
                //if the selected item is indeed the last one then jump to the first item of the list and select the first line
                else
                {
                    selectedLine = 0;
                    SmallestDisplayedItem = 0;
                }
            }

            if(e.Key == Controls.Key.Up)
            {
                //if the first item of the list is not the selected one
                if (SmallestDisplayedItem > 0 || selectedLine > 0)
                {
                    //we can select an item with a smaller index or move the selected Line up
                    if (selectedLine > 0)
                        selectedLine -= 1;
                    else
                        SmallestDisplayedItem -= 1;
                }
                //else we select the last last item by moving the first shown item at the right index and selecting the line with the last item
                else
                {
                    if (SelectedListCount >= LineCount)
                        selectedLine = LineCount - 1;
                    else
                        selectedLine = SelectedListCount - 1;

                    SmallestDisplayedItem = EvaluateIntSmallestDisplayedItem();
                }
            }

            if(e.Key == Controls.Key.Return)
            {
                BuyOrSell();
            }
        }

        /// <summary>
        /// manages the controls in the Shop
        /// </summary>
        public void Shopmanagement()
        {
            //manage Lines*****************************************************************

            //fixing eventual undefined lines
            if (selectedLine >= SelectedListCount)
                selectedLine = SelectedListCount - 1;
            if (selectedLine < 0)
                selectedLine = 0;

            //*****************************************************************************

            ShopItemUpdate();
            //set Position
            selected.Position = new Vector2(StartPos.X, StartPos.Y + selectedLine * 50);
        }

        /// <summary>
        /// evaluates the first item that should be shown in the selected list when we have selected the last one
        /// </summary>
        /// <returns>index of the first shown item</returns>
        private int EvaluateIntSmallestDisplayedItem()
        {
            switch (selectedCollum)
            {
                case Collum.Sell:
                    if (sellableItems.Count - 10 >= 0)
                        return sellableItems.Count - 10;
                    else
                        return 0;
                case Collum.Buy:
                    if (buyableItems.Count - 10 >= 0)
                        return buyableItems.Count - 10;
                    else
                        return 0;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// update the Players Gold by subtracting or adding the price of the bought/ selled item
        /// </summary>
        public void BuyOrSell()
        {
            if (SmallestDisplayedItem + selectedLine < SelectedListCount)
                PlayerHandler.Player.Gold += SelectedList[SmallestDisplayedItem + selectedLine].BuySell(selectedCollum);
        }

        /// <summary>
        /// close the shop
        /// </summary>
        public void CloseShop()
        {
            ShopIsOpen = false;
            sprite = null;
        }

        /// <summary>
        /// updates the shop items means reducing their number or caling the buy sell method etc. should only be called once in update
        /// </summary>
        private void ShopItemUpdate()
        {
            //update sellable Items
            for (int i = 0; i < sellableItems.Count - smallestDisplayedItem0 && i < LineCount; i++)
            {
                sellableItems[smallestDisplayedItem0 + i].Position = StartSellPosition + new Vector2(0, i * 50);
                sellableItems[smallestDisplayedItem0 + i].Update();
                sellableItems[smallestDisplayedItem0 + i].Visible = true;
            }
            for (int i = 0; i < sellableItems.Count; i++)
            {
                if (i < smallestDisplayedItem0 || i >= smallestDisplayedItem0 + LineCount)
                    sellableItems[i].Visible = false;

                if (!sellableItems[i].IsAlive)
                {
                    sellableItems.RemoveAt(i);
                    i--;
                }
            }

            //update buyable Items
            for (int i = 0; i < buyableItems.Count - smallestDisplayedItem1 && i < LineCount; i++)
            {
                buyableItems[smallestDisplayedItem1 + i].Position = StartBuyPosition + new Vector2(0, i * 50);
                buyableItems[smallestDisplayedItem1 + i].Update();
                buyableItems[smallestDisplayedItem1 + i].Visible = true;
            }
            for (int i = 0; i < buyableItems.Count; i++)
            {
                if (i < smallestDisplayedItem1 || i >= smallestDisplayedItem1 + LineCount)
                    buyableItems[i].Visible = false;

                if (!buyableItems[i].IsAlive)
                {
                    buyableItems.RemoveAt(i);
                    i--;
                }
            }

            //updating the displayed Gold value
            playerGoldText.DisplayedString = PlayerHandler.Player.Gold.ToString();
        }

        /// <summary>
        /// draw all shop content that is visible
        /// </summary>
        /// <param name="window"></param>
        public void Draw(RenderWindow window)
        {
            if (sprite != null)
            {
                window.Draw(sprite);
                window.Draw(selected);
                window.Draw(playerGoldText);

                foreach (ShopItem item in sellableItems)
                    item.Draw(window);

                foreach (ShopItem item in buyableItems)
                    item.Draw(window);
            }
        }
    }
}
