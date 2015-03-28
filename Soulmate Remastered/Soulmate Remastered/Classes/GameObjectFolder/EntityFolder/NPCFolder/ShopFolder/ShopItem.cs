using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.NPCFolder.ShopFolder
{
    class ShopItem
    {
        public Vector2f position { get; set; }
        public bool visible { get; set; }
        public bool isAlive { get { if (itemStack.Count > 0)return true; else return false; } }

        Stack<AbstractItem> itemStack;
        Sprite itemSprite;
        Text displayedName;
        Text goldValue;

        public ShopItem(Stack<AbstractItem> item)
        {
            itemStack = item;

            itemSprite = new Sprite(itemStack.Peek().currentTexture);
            displayedName = new Text("", Game.font, 20);
            goldValue = new Text("", Game.font, 20);

            positionUpdate();
        }

        private void positionUpdate()
        {
            itemSprite.Position = new Vector2f(position.X + 5, position.Y);
            displayedName.Position = new Vector2f(itemSprite.Position.X + itemSprite.Texture.Size.X + 5, position.Y + 5);
            goldValue.Position = new Vector2f(position.X + 390 - (goldValue.DisplayedString.Length * goldValue.CharacterSize) / 2, position.Y + 5);
            
        }

        private void textUpdate()
        {
            displayedName.DisplayedString = itemStack.Count + "x " + itemStack.Peek().name;
            goldValue.DisplayedString = itemStack.Peek().sellPrize.ToString();
        }

        public static List<ShopItem> ToShopItemList(List<Stack<AbstractItem>> list)
        {
            List<ShopItem> res = new List<ShopItem>();

            foreach (Stack<AbstractItem> item in list)
                res.Add(new ShopItem(item));

            return res;
        }

        public void update()
        {
            positionUpdate();
            textUpdate();
        }

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
