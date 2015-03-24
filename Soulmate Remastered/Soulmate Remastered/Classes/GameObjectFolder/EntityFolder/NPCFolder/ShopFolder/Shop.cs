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
        List<Stack<AbstractItem>> buayableItems;
        static Texture shopTexture = new Texture("Pictures/Entities/NPC/Shop/ShopInterface.png");
        static Sprite sprite;

        public Shop(List<Stack<AbstractItem>> itemsToBuy)
        {
            sprite = new Sprite(shopTexture);
            sprite.Position = new Vector2f((Game.windowSizeX - sprite.Texture.Size.X) / 2, (Game.windowSizeY - sprite.Texture.Size.Y) / 2);

            sellableItems = new List<Stack<AbstractItem>>();
            buayableItems = itemsToBuy;

            foreach (Stack<AbstractItem> item in ItemHandler.playerInventory.inventoryMatrix)
            {
                sellableItems.Add(item);
            }

            shopIsOpen = true;
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
