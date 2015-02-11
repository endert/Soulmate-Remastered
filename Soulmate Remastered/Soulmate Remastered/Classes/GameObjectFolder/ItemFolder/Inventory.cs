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
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.EquipmentFolder;

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

        public Vector2f inventoryMatrixPosition { get { return new Vector2f(inventory.Position.X + 306, inventory.Position.Y + 101); } }

        public bool isPressed = false;
        bool isMouseKlicked;
        int xInInventory = 0, yInInventory = 0; //Inventarsteurung
        int yInEquipmentSlots = 0;
        int xInTabs = 0;

        public static bool inventoryOpen { get; set; }

        uint inventoryWidth;
        uint inventoryLength;

        public AbstractItem[,] inventoryMatrix { get; set; }
        public Equipment[] equipment { get; set; }

        AbstractItem selectedItem;
        bool itemIsSelected = false;
        int selectedItemX;
        int selectedItemY;

        bool inInventory = true;
        bool inTab = false;
        bool inEquipmentSlots = false;

        Vector2f equipmentSlotsBase { get { return new Vector2f(inventory.Position.X + 306 - 99, 
            (inventory.Position.Y + 101 - 5)); } } // copied x and y coordinates from inventoryMatrixPosition


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
            xInInventory = 0;
            yInInventory = 0;
            inInventory = true;
            inTab = false;
            inEquipmentSlots = false;
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
            inInventory = true;

            equipment = new Equipment[6];
        }

        public void spriteAndTextPositionUpdate()
        {
            displayedPlayer = new Sprite(displayedPlayerTexture);
            displayedPlayer.Position = new Vector2f(inventory.Position.X + 67, inventory.Position.Y + 82);

            attack.DisplayedString = "Attack: " + PlayerHandler.player.getAtt;
            attack.Position = new Vector2f(inventory.Position.X + 65, inventoryMatrixPosition.Y + 7 * FIELDSIZE + 10);

            defense.DisplayedString = "Defense: " + PlayerHandler.player.getDef;
            defense.Position = new Vector2f(attack.Position.X, attack.Position.Y + attack.CharacterSize);

            exp.DisplayedString = "EXP: " + PlayerHandler.player.getCurrentEXP + "/" + PlayerHandler.player.getMaxEXP;
            exp.Position = new Vector2f(defense.Position.X, defense.Position.Y + defense.CharacterSize);

            lvl.DisplayedString = "Lvl: " + PlayerHandler.player.getLvl;
            if (PlayerHandler.player.getLvl == PlayerHandler.player.MaxLvl)
                lvl.DisplayedString += "°";
            lvl.Position = new Vector2f(exp.Position.X, exp.Position.Y + exp.CharacterSize);

            gold.DisplayedString = "Gold: " + PlayerHandler.player.gold;
            goldSprite.Position = new Vector2f(lvl.Position.X - 10, lvl.Position.Y + 45);
            gold.Position = new Vector2f(goldSprite.Position.X + (goldSprite.Texture.Size.X / 2), goldSprite.Position.Y);
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

        public bool getInventoryOpen()
        {
            if (Keyboard.IsKeyPressed(Controls.OpenInventar) && !isPressed && !inventoryOpen)
            {
                isPressed = true;
                setOpen();
                inventoryOpen = true;
            }

            if (!NavigationHelp.isAnyKeyPressed())
                isPressed = false;

            return inventoryOpen;
        }

        public void managment()
        {
            if ((Keyboard.IsKeyPressed(Controls.OpenInventar) || Keyboard.IsKeyPressed(Controls.Escape)) && !isPressed)
            {
                isPressed = true;
                inventoryOpen = false;
            }
            
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

            if (Keyboard.IsKeyPressed(Controls.Up) && !isPressed)
            {
                if (yInInventory == 0 && inInventory)  // enter Tabs
                {
                    inInventory = false;
                    inTab = true;
                    xInTabs = xInInventory % 2;
                }
                else if (inEquipmentSlots)
                {
                    yInEquipmentSlots = (yInEquipmentSlots + (equipment.Length - 1)) % equipment.Length;
                }
                else
                {
                    yInInventory = (yInInventory + (inventoryMatrix.GetLength(0) - 1)) % inventoryMatrix.GetLength(0);
                    inTab = false;
                    inInventory = true;
                }
                isPressed = true;
            }

            if (Keyboard.IsKeyPressed(Controls.Down) && !isPressed)
            {
                if (inTab)
                {
                    inInventory = true;
                    inTab = false;
                }
                else if(inInventory)
                {
                    yInInventory = (yInInventory + 1) % inventoryMatrix.GetLength(0);
                }
                else
                {
                    yInEquipmentSlots = (yInEquipmentSlots + 1) % equipment.Length;
                }
                isPressed = true;
            }

            if (Keyboard.IsKeyPressed(Controls.Right) && !isPressed)
            {
                if (inEquipmentSlots)
                {
                    inInventory = true;
                    inEquipmentSlots = false;
                }
                else if(inInventory)
                {
                    xInInventory = (xInInventory + 1) % inventoryMatrix.GetLength(1);
                }
                else
                {
                    xInTabs = (xInTabs + 1) % 2;
                }
                isPressed = true;
            }

            if (Keyboard.IsKeyPressed(Controls.Left) && !isPressed)
            {
                if (xInInventory == 0 && inInventory)  //enter equipmentSlots
                {
                    inInventory = false;
                    inEquipmentSlots = true;
                    yInEquipmentSlots = yInInventory % equipment.Length;
                }
                else if(inTab)
                {
                    xInTabs = (xInTabs + 1) % 2;
                }
                else
                {
                    xInInventory = (xInInventory + (inventoryMatrix.GetLength(1) - 1)) % inventoryMatrix.GetLength(1);
                    inInventory = true;
                    inEquipmentSlots = false;
                }
                isPressed = true;
            }

//inInventory management (Matrix[x,y]) =============================================================================
            if (inInventory)    
            {
                if (Keyboard.IsKeyPressed(Controls.ButtonForAttack) && !isPressed)    //Item Swaps
                {
                    if (!itemIsSelected)
                    {
                        selectedItem = inventoryMatrix[yInInventory, xInInventory];
                        itemIsSelected = true;
                        selectedItemX = xInInventory;
                        selectedItemY = yInInventory;
                    }
                    else
                    {
                        inventoryMatrix[selectedItemY, selectedItemX] = inventoryMatrix[yInInventory, xInInventory];
                        if (inventoryMatrix[selectedItemY, selectedItemX] != null)
                        {
                            inventoryMatrix[selectedItemY, selectedItemX].setPositionMatrix(selectedItemX, selectedItemY);
                        }

                        inventoryMatrix[yInInventory, xInInventory] = selectedItem;

                        if (inventoryMatrix[yInInventory, xInInventory] != null)
                        {
                            inventoryMatrix[yInInventory, xInInventory].setPositionMatrix(xInInventory, yInInventory);
                        }
                        selectedItem = null;
                        itemIsSelected = false;
                    }
                    isPressed = true;
                }

                if (Keyboard.IsKeyPressed(Controls.UseItem) && !isPressed)
                {
                    if (inventoryMatrix[yInInventory, xInInventory] != null)
                    {
                        inventoryMatrix[yInInventory, xInInventory].use();
                    }
                }

                if (inventoryMatrix[yInInventory, xInInventory] != null && !inventoryMatrix[yInInventory, xInInventory].isAlive)
                {
                    inventoryMatrix[yInInventory, xInInventory] = null;
                }
            }
//=====================================================================================================================

            if (!NavigationHelp.isAnyKeyPressed() && !Mouse.IsButtonPressed(Mouse.Button.Left))
                isPressed = false;

            if (inInventory)
            {
                selected.Position = new Vector2f(xInInventory * FIELDSIZE + inventoryMatrixPosition.X, yInInventory * FIELDSIZE + inventoryMatrixPosition.Y);
            }
            else if (inEquipmentSlots)
            {
                if (yInEquipmentSlots<2)
                {
                    selected.Position = new Vector2f(equipmentSlotsBase.X, equipmentSlotsBase.Y + yInEquipmentSlots * (FIELDSIZE + 5));
                }
                else if (yInEquipmentSlots < 4)
                {
                    selected.Position = new Vector2f(equipmentSlotsBase.X + 15, (equipmentSlotsBase.Y + 6) + yInEquipmentSlots * (FIELDSIZE + 5));
                }
                else
                {
                    selected.Position = new Vector2f(equipmentSlotsBase.X - 4, (equipmentSlotsBase.Y + 12) + yInEquipmentSlots * (FIELDSIZE + 5));
                }
            }

            setTabs();
        }

        public void deleate()
        {
            inventoryMatrix = new AbstractItem[inventoryLength, inventoryWidth];
        }

        public void update(GameTime gameTime)
        {
            getInventoryOpen();
            if (inventoryOpen)
            {
                spriteAndTextPositionUpdate();
                PlayerHandler.player.cheatUpdate();
                ItemHandler.updateInventoryMatrix(gameTime);
                managment();
            }
        }

        public void setTabs()
        {
            setInventoryTab();
            setPetTab();
        }

        public void setInventoryTab()
        {
            if (xInTabs == 0 && inTab)
            {
                inventoryTab.Texture = inventoryTabSelected;
            }
            else
            {
                inventoryTab.Texture = inventoryTabNotSelected;
            }
        }

        public void setPetTab()
        {
            if (xInTabs == 1 && inTab)
            {
                petTab.Texture = petTabSelected;
            }
            else
            {
                petTab.Texture = petTabNotSelected;
            }
        }

        public void drawTexts(RenderWindow window)
        {
            window.Draw(gold);
            window.Draw(goldSprite);
            window.Draw(displayedPlayer);
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
            if (!inTab)
            {
                window.Draw(selected);
            }
        }
    }
}
