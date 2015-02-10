using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.GameObjectFolder;
using System.IO;

namespace Soulmate_Remastered.Classes.ItemFolder
{
    class Inventory
    {
        Char lineBreak = ':';

        Texture goldTexture = new Texture("Pictures/Items/Money/Gold.png");
        Sprite goldSprite;

        Texture displayedPlayerTexture = new Texture("Pictures/Inventory/PlayerFrontInventory.png");
        Sprite displayedPlayer;

        Texture inventoryTabNotSelected = new Texture("Pictures/Inventory/InventoryTabNotSelected.png");
        Texture inventoryTabSelected = new Texture("Pictures/Inventory/InventoryTabSelected.png");
        Sprite inventoryTab;

        Texture petTabNotSelected = new Texture("Pictures/Inventory/petTabNotSelected.png");
        Texture petTabSelected = new Texture("Pictures/Inventory/petTabSelected.png");
        Sprite petTab;


        Font font = new Font("FontFolder/arial_narrow_7.ttf");
        Text gold;
        Text attack;
        Text defense;
        Text exp;
        Text lvl;

        Texture inventoryTexture = new Texture("Pictures/Inventory/Inventory.PNG");
        public Sprite inventory { get; set; }
        public float FIELDSIZE { get { return 49f; } }
        Texture selectedTexture = new Texture("Pictures/Inventory/Selected.png");
        Sprite selected;

        public Vector2f inventoryMatrixPosition { get { return new Vector2f(inventory.Position.X + 6 * selected.Texture.Size.X+6, inventory.Position.Y + 2 * selected.Texture.Size.Y+1); } }

        bool isPressed = false;
        bool isMouseKlicked;
        int x = 0, y = 0; //Inventarsteurung

        public static bool inventoryOpen { get; set; }

        uint inventoryWidth;
        uint inventoryLength;

        public AbstractItem[,] inventoryMatrix { get; set; }

        AbstractItem selectedItem;
        bool itemIsSelected = false;
        int selectedItemX;
        int selectedItemY;

        public String toStringForSave()
        {
            String inventoryForSave = "inv" + lineBreak.ToString();

            foreach  (AbstractItem item in inventoryMatrix)
            {
                try
                {
                    inventoryForSave += item.toStringForSave() + lineBreak.ToString();
                }
                catch (NullReferenceException)
                {
                    inventoryForSave += " " + lineBreak.ToString();
                }
            }

            return inventoryForSave;
        }

        public void setOpen()
        {
            x = 0;
            y = 0;
        }

        public void load(String inventoryString)
        {
            String[] splitInventoryString = inventoryString.Split(lineBreak);

            for (int i = 0; i < inventoryMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < inventoryMatrix.GetLength(1); j++)
                {
                    inventoryMatrix[i, j] = ItemHandler.load(splitInventoryString[i * inventoryMatrix.GetLength(1) + j + 1]);
                    if (inventoryMatrix[i, j] != null)
                        inventoryMatrix[i, j].position = new Vector2f((j * FIELDSIZE+1 + inventoryMatrixPosition.X), (i * FIELDSIZE+1 + inventoryMatrixPosition.Y));
                }
            }
        }

        public Inventory()
        {
            gold = new Text("Gold: ", font, 20);
            goldSprite = new Sprite(goldTexture);
            goldSprite.Scale = new Vector2f(0.5f, 0.5f);

            attack = new Text("Attack Damage: ", font, 20);
            defense = new Text("Defense: ", font, 20);
            exp = new Text("Exp: ", font, 20);
            lvl = new Text("Lvl: ", font, 20);


            inventory = new Sprite(inventoryTexture);
            inventory.Position = new Vector2f((Game.windowSizeX - inventoryTexture.Size.X) / 2, (Game.windowSizeY - inventoryTexture.Size.Y) / 2);

            selected = new Sprite(selectedTexture);
            selected.Position = inventoryMatrixPosition;

            inventoryTab = new Sprite(inventoryTabNotSelected);
            inventoryTab.Position = new Vector2f(inventoryMatrixPosition.X + 5, inventoryMatrixPosition.Y - 25);
            petTab = new Sprite(petTabNotSelected);
            petTab.Position = new Vector2f(inventoryTab.Position.X + 110, inventoryTab.Position.Y);

            inventoryWidth = 9;
            inventoryLength = 7;

            inventoryMatrix = new AbstractItem[inventoryLength, inventoryWidth];

            spriteAndTextPositionUpdate();
            goldSprite.Position = new Vector2f(inventory.Position.X + (inventory.Texture.Size.X - (goldSprite.Texture.Size.X - 20)), gold.Position.Y);
        }

        public void spriteAndTextPositionUpdate()
        {
            gold.DisplayedString = "Gold: " + PlayerHandler.player.gold;
            gold.Position = new Vector2f((inventory.Position.X + inventory.Texture.Size.X) - ((gold.CharacterSize / 2) * gold.DisplayedString.Length) - (goldSprite.Texture.Size.X - 20),
                                        (inventory.Position.Y + inventory.Texture.Size.Y) - (gold.CharacterSize + 10));

            displayedPlayer = new Sprite(displayedPlayerTexture);
            displayedPlayer.Position = new Vector2f(inventory.Position.X + 20, inventory.Position.Y + FIELDSIZE);

            attack.DisplayedString = "Attack: " + PlayerHandler.player.getAtt;
            attack.Position = new Vector2f(inventory.Position.X + 20, inventory.Position.Y + 9 * FIELDSIZE + 20);

            defense.DisplayedString = "Defense: " + PlayerHandler.player.getDef;
            defense.Position = new Vector2f(attack.Position.X, attack.Position.Y + attack.CharacterSize);

            exp.DisplayedString = "EXP: " + PlayerHandler.player.getCurrentEXP + "/" + PlayerHandler.player.getMaxEXP;
            exp.Position = new Vector2f(defense.Position.X, defense.Position.Y + defense.CharacterSize);

            lvl.DisplayedString = "Lvl: " + PlayerHandler.player.getLvl;
            if (PlayerHandler.player.getLvl == PlayerHandler.player.MaxLvl)
                lvl.DisplayedString += "°";
            lvl.Position = new Vector2f(exp.Position.X, exp.Position.Y + exp.CharacterSize);
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
            if (!Mouse.IsButtonPressed(Mouse.Button.Left))
                isMouseKlicked = false;

            if(Mouse.IsButtonPressed(Mouse.Button.Left) && NavigationHelp.isMouseInSprite(inventoryTab) && !isMouseKlicked)
            {
                isMouseKlicked = true;
            }

            if (Mouse.IsButtonPressed(Mouse.Button.Left) && NavigationHelp.isMouseInSprite(petTab) && !isMouseKlicked)
            {
                isMouseKlicked = true;
            }
            
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

            if (Keyboard.IsKeyPressed(Keyboard.Key.U) && !isPressed)
            {
                if (inventoryMatrix[y, x] != null)
                {
                    inventoryMatrix[y, x].use();
                }
            }

            if (!Keyboard.IsKeyPressed(Keyboard.Key.Down) && !Keyboard.IsKeyPressed(Keyboard.Key.Up) && !Keyboard.IsKeyPressed(Keyboard.Key.Right) 
                && !Keyboard.IsKeyPressed(Keyboard.Key.Left) && !Keyboard.IsKeyPressed(Keyboard.Key.A) && !Keyboard.IsKeyPressed(Keyboard.Key.U))
                isPressed = false;

            if (inventoryMatrix[y, x] != null && !inventoryMatrix[y, x].isAlive)
            {
                inventoryMatrix[y, x] = null;
            }

            selected.Position = new Vector2f(x * FIELDSIZE + inventoryMatrixPosition.X, y * FIELDSIZE + inventoryMatrixPosition.Y);
        }

        public void deleate()
        {
            inventoryMatrix = new AbstractItem[inventoryLength, inventoryWidth];
        }

        public void update(GameTime gameTime)
        {
            spriteAndTextPositionUpdate();
            PlayerHandler.player.cheatUpdate();
            ItemHandler.updateInventoryMatrix(gameTime);
            managment();
        }

        public void drawTexts(RenderWindow window)
        {
            window.Draw(gold);
            window.Draw(goldSprite);
            //window.Draw(displayedPlayer);
            window.Draw(attack);
            window.Draw(defense);
            window.Draw(exp);
            window.Draw(lvl);
        }

        public void draw(RenderWindow window)
        {
            window.Draw(inventory);
            ItemHandler.drawInventoryItems(window);
            window.Draw(inventoryTab);
            window.Draw(petTab);
            drawTexts(window);
            window.Draw(selected);
        }
    }
}
