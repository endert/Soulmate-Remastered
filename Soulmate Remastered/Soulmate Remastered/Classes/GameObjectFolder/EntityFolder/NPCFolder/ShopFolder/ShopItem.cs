using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using Soulmate_Remastered.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.NPCFolder.ShopFolder
{
    class ShopItem
    {
        /// <summary>
        /// Position in view coordinates
        /// </summary>
        public Vector2 position { get; set; }
        /// <summary>
        /// bool if the item should be drawn
        /// </summary>
        public bool visible { get; set; }
        /// <summary>
        /// bool if the item is alive
        /// </summary>
        public bool isAlive { get { if (itemStack != null && itemStack.Count > 0)return true; else return false; } }
        /// <summary>
        /// single chained list of abstaract items after the lilo princip
        /// </summary>
        Stack<AbstractItem> itemStack;
        /// <summary>
        /// the sprite of the item
        /// </summary>
        Sprite itemSprite;
        /// <summary>
        /// the name of the item
        /// </summary>
        Text displayedName;
        /// <summary>
        /// the price for sell/buy
        /// </summary>
        Text goldValue;

        /// <summary>
        /// creates a shop item out of a item stack
        /// </summary>
        /// <param name="item"></param>
        public ShopItem(Stack<AbstractItem> item)
        {
            itemStack = item;
            if (itemStack.Count > 0)
            {
                itemSprite = new Sprite(itemStack.Peek().currentTexture);
                displayedName = new Text("", Game.font, 20);
                goldValue = new Text("", Game.font, 20);

                positionUpdate();
            }
        }

        /// <summary>
        /// updates the position
        /// </summary>
        private void positionUpdate()
        {
            itemSprite.Position = new Vector2f(position.X + 5, position.Y);
            displayedName.Position = new Vector2f(itemSprite.Position.X + itemSprite.Texture.Size.X + 5, position.Y + 5);
            goldValue.Position = new Vector2f(position.X + 390 - (goldValue.DisplayedString.Length * goldValue.CharacterSize) / 2, position.Y + 5);
            
        }

        /// <summary>
        /// buys or sells the item
        /// </summary>
        /// <param name="selectedCollum">the selected collum</param>
        /// <returns>the gold that is added to Players gold</returns>
        public float BuySell(Shop.Collum selectedCollum)
        {
            if (isAlive)
            {
                switch (selectedCollum)
                {
                    case Shop.Collum.Sell:
                        return itemStack.Pop().sellPrize;
                    case Shop.Collum.Buy:
                        if (PlayerHandler.player.gold >= itemStack.Peek().sellPrize)
                        {
                            float prize = -itemStack.Peek().sellPrize;
                            itemStack.Pop().cloneAndDrop(PlayerHandler.player.hitBox.Position);
                            return prize;
                        }
                        else
                            return 0;
                    default:
                        return 0;
                }
            }
            else
                return 0;
        }

        /// <summary>
        /// updates texts should only be called once
        /// </summary>
        private void textUpdate()
        {
            displayedName.DisplayedString = itemStack.Count + "x " + itemStack.Peek().name;
            goldValue.DisplayedString = itemStack.Peek().sellPrize.ToString();
        }

        /// <summary>
        /// converts a list of stacks of Abstract Items into a list of shop items, without changing the first list
        /// </summary>
        /// <param name="list">list that shall be converted</param>
        /// <returns>returns a new List</returns>
        public static List<ShopItem> ToShopItemList(List<Stack<AbstractItem>> list)
        {
            List<ShopItem> res = new List<ShopItem>();

            foreach (Stack<AbstractItem> item in list)
                res.Add(new ShopItem(item));

            return res;
        }

        /// <summary>
        /// if the item is alive calls position update and text update
        /// </summary>
        public void update()
        {
            if (isAlive)
            {
                positionUpdate();
                textUpdate();
            }
            else
                itemStack = null;
        }

        /// <summary>
        /// draws the item if it is visible
        /// </summary>
        /// <param name="window"></param>
        public void draw(RenderWindow window)
        {
            if (visible)
            {
                window.Draw(itemSprite);
                window.Draw(displayedName);
                window.Draw(goldValue);
            }
        }
    }
}
