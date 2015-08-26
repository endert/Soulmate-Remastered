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
using Soulmate_Remastered.Core;

namespace Soulmate_Remastered.Classes.ItemFolder
{
    class Inventory
    {
        Char lineBreak = ':';
        Char InventoryMatrixSplit = '_';

        Texture goldTexture = new Texture("Pictures/Items/Money/Gold.png");
        Sprite goldSprite;

        Texture characterMenu = new Texture("Pictures/Inventory/Inventory.png");
        Texture petMenu = new Texture("Pictures/Inventory/PetMenu.png");
        Texture questLog = new Texture("Pictures/Inventory/QuestLog.png");
        Sprite character_pet_questSprite;

        Texture displayedPlayerTexture = new Texture("Pictures/Inventory/CharakterSprite/PlayerFrontInventory.png");
        Sprite displayedPlayer;

        Texture characterTabNotSelected = new Texture("Pictures/Inventory/Tabs/CharacterTabNotSelected.png");
        Texture characterTabSelected = new Texture("Pictures/Inventory/Tabs/CharacterTabSelected.png");
        Sprite characterTab;

        Texture petTabNotSelected = new Texture("Pictures/Inventory/Tabs/PetTabNotSelected.png");
        Texture petTabSelected = new Texture("Pictures/Inventory/Tabs/PetTabSelected.png");
        Sprite petTab;

        Texture questTabNotSelected = new Texture("Pictures/Inventory/Tabs/QuestTabNotSelected.png");
        Texture questTabSelected = new Texture("Pictures/Inventory/Tabs/QuestTabSelected.png");
        Sprite questTab;

        Texture scrollArrowBottomNotSelected = new Texture("Pictures/Inventory/ScrollArrow/ScrollArrowBottomNotSelected.png");
        Texture scrollArrowBottomSelected = new Texture("Pictures/Inventory/ScrollArrow/ScrollArrowBottomSelected.png");
        Sprite scrollArrowBottom;

        Texture scrollArrowTopNotSelected = new Texture("Pictures/Inventory/ScrollArrow/ScrollArrowTopNotSelected.png");
        Texture scrollArrowTopSelected = new Texture("Pictures/Inventory/ScrollArrow/ScrollArrowTopSelected.png");
        Sprite scrollArrowTop;

        Texture closeButtonNotSelected = new Texture("Pictures/Inventory/CloseButton/CloseButtonNotSelected.png");
        Texture closeButtonSelected = new Texture("Pictures/Inventory/CloseButton/CloseButtonSelected.png");
        Sprite closeButton;

        Texture sortNotSelected = new Texture("Pictures/Inventory/Sort/SortNotSelected.png");
        Texture sortSelected = new Texture("Pictures/Inventory/Sort/SortSelected.png");
        Sprite sort;

        Texture selectedMarkerTexture = new Texture("Pictures/Inventory/SelectedMarker.png");
        Sprite selectedMarker;

        Vector2 scrollArrowScaleValue = new Vector2(0.43f, 0.43f);
        Vector2 selectedMarkerPosition;

        /// <summary>
        /// to show which item is currently selected
        /// </summary>
        RectangleShape selected;
        /// <summary>
        /// outLineThickness of the selected rectangle
        /// </summary>
        float outLineThickness = 1;

        bool characterMenuActivated;
        bool petMenuActivated;
        bool questLogActivated;

        Font font = new Font("FontFolder/arial_narrow_7.ttf");
        Text gold;
        Text attack;
        Text defense;
        Text exp;
        Text lvl;

        /// <summary>
        /// name and description of the item
        /// </summary>
        Text itemDescription;

        /// <summary>
        /// <para> numbers of items in the inventory; </para>
        /// <para> contains the name and the description of the item; </para>
        /// don't know it gets Text and not the item
        /// </summary>
        List<Text> StackCount = new List<Text>();
        
        /// <summary>
        /// size of the field of the places in the inventory
        /// </summary>
        public float FIELDSIZE { get { return 49f; } }
        
        /// <summary>
        /// <para> number in which tab of the inventory we are </para>
        /// <para> 0 = player inventory </para>
        /// <para> 1 = pet inventory </para>
        /// <para> 2 = quest log </para>
        /// </summary>
        int selectedTab;

        /// <summary>
        /// max numbers of items on one stack
        /// </summary>
        public static int MaxStackCount { get { return 99; } }

        public Vector2 inventoryMatrixPosition { get { return new Vector2(character_pet_questSprite.Position.X + 306, character_pet_questSprite.Position.Y + 106) + outLineThickness; } }

        int xInInventory = 0, yInInventory = 0; //Inventarsteurung
        int yInEquipmentSlots = 0;
        int xInTabs = 0;

        static Vector2f firstEquipmentSlotPostion = new Vector2f(447, 161);
        Vector2f secondEquipmentSlotPostion = new Vector2f(firstEquipmentSlotPostion.X, firstEquipmentSlotPostion.Y + 54);
        Vector2f thirdEquipmentSlotPostion = new Vector2f(firstEquipmentSlotPostion.X + 15, firstEquipmentSlotPostion.Y + 114);
        Vector2f fourthEquipmentSlotPostion = new Vector2f(firstEquipmentSlotPostion.X + 15, firstEquipmentSlotPostion.Y + 168);
        Vector2f fifthEquipmentSlotPostion = new Vector2f(firstEquipmentSlotPostion.X - 4, firstEquipmentSlotPostion.Y + 228);
        Vector2f sixthEquipmentSlotPostion = new Vector2f(firstEquipmentSlotPostion.X -4 , firstEquipmentSlotPostion.Y + 282);

        public static Vector2[] equipmentPosition
        {
            get
            {
                return new Vector2[]
                {
                    new Vector2(firstEquipmentSlotPostion.X, firstEquipmentSlotPostion.Y),
                    new Vector2(firstEquipmentSlotPostion.X, firstEquipmentSlotPostion.Y + 54),
                    new Vector2(firstEquipmentSlotPostion.X + 15, firstEquipmentSlotPostion.Y + 114), 
                    new Vector2(firstEquipmentSlotPostion.X + 15, firstEquipmentSlotPostion.Y + 168), 
                    new Vector2(firstEquipmentSlotPostion.X - 4, firstEquipmentSlotPostion.Y + 228), 
                    new Vector2(firstEquipmentSlotPostion.X -4 , firstEquipmentSlotPostion.Y + 282)
                };
            }
        }

        RenderWindow window = Game.window;

        public static bool inventoryOpen { get; set; }

        uint inventoryWidth;
        uint inventoryLength;

        public Stack<AbstractItem>[,] inventoryMatrix { get; set; }
        public Equipment[] equipment { get; set; }

        Stack<AbstractItem> selectedItemStack;
        bool itemIsSelected = false;
        int selectedItemStackX;
        int selectedItemStackY;

        bool inInventory = true;
        bool inTab = false;
        bool inEquipmentSlots = false;

        Vector2f equipmentSlotsBase { get { return new Vector2f(character_pet_questSprite.Position.X + 306 - 99, (character_pet_questSprite.Position.Y + 101)); } } // copied x and y coordinates from inventoryMatrixPosition

        public String toStringForSave()
        {
            String inventoryForSave = "inv" + lineBreak.ToString();

            foreach  (Stack<AbstractItem> itemStack in inventoryMatrix)
            {
                try
                {
                    if (itemStack.Count == 0)
                    {
                        throw new NullReferenceException(); //deleating Stacks witch a count of 0;
                    }
                    inventoryForSave += itemStack.Peek().toStringForSave() + itemStack.Count + lineBreak.ToString();
                }
                catch (NullReferenceException)
                {
                    inventoryForSave += " " + lineBreak.ToString();
                }
            }
            inventoryForSave += InventoryMatrixSplit.ToString();
            foreach (Equipment equip in equipment)
            {
                try
                {
                    inventoryForSave += equip.toStringForSave() + lineBreak.ToString();
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
            String[] inventoryStringSplit = inventoryString.Split(InventoryMatrixSplit);
            String[] MatrixString = inventoryStringSplit[0].Split(lineBreak);
            String[] EquipmentString = inventoryStringSplit[1].Split(lineBreak);

            for (int i = 0; i < inventoryMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < inventoryMatrix.GetLength(1); j++)
                {
                    inventoryMatrix[i, j] = ItemHandler.load(MatrixString[i * inventoryMatrix.GetLength(1) + j + 1]);
                }
            }

            for (int i = 0; i < equipment.Length; i++)
            {
                equipment[i] = ItemHandler.loadEquip(EquipmentString[i]);
            }

            PlayerHandler.Player.StatsUpdate();
        }

        public Inventory()
        {
            selectedTab = 0;
            gold = new Text("Gold: ", font, 20);
            goldSprite = new Sprite(goldTexture);
            goldSprite.Scale = new Vector2f(0.25f, 0.25f);

            attack = new Text("Attack Damage: ", font, 20);
            defense = new Text("Defense: ", font, 20);
            exp = new Text("Exp: ", font, 20);
            lvl = new Text("Lvl: ", font, 20);

            character_pet_questSprite = new Sprite(characterMenu);
            character_pet_questSprite.Position = new Vector2f((Game.windowSizeX - characterMenu.Size.X) / 2, (Game.windowSizeY - characterMenu.Size.Y) / 2);

            selected = new RectangleShape(new Vector2(FIELDSIZE, FIELDSIZE) + (-2 * outLineThickness));
            selected.FillColor = Color.Transparent;
            selected.OutlineThickness = outLineThickness;
            selected.OutlineColor = Color.Red;
            selected.Position = inventoryMatrixPosition;

            characterTab = new Sprite(characterTabNotSelected);
            characterTab.Position = new Vector2f(character_pet_questSprite.Position.X + 63, character_pet_questSprite.Position.Y + 37);

            petTab = new Sprite(petTabNotSelected);
            petTab.Position = new Vector2f(characterTab.Position.X + 131, characterTab.Position.Y);

            questTab = new Sprite(questTabNotSelected);
            questTab.Position = new Vector2f(characterTab.Position.X + 210, characterTab.Position.Y);

            scrollArrowBottom = new Sprite(scrollArrowBottomNotSelected);
            scrollArrowBottom.Scale = scrollArrowScaleValue;
            scrollArrowBottom.Position = new Vector2f(character_pet_questSprite.Position.X + 727, character_pet_questSprite.Position.Y + 420);

            scrollArrowTop = new Sprite(scrollArrowTopNotSelected);
            scrollArrowTop.Scale = scrollArrowScaleValue;
            scrollArrowTop.Position = new Vector2f(scrollArrowBottom.Position.X - 19, scrollArrowBottom.Position.Y);

            closeButton = new Sprite(closeButtonNotSelected);
            closeButton.Position = new Vector2f(character_pet_questSprite.Position.X + character_pet_questSprite.Texture.Size.X - closeButton.Texture.Size.X - 3, character_pet_questSprite.Position.Y + 3);

            sort = new Sprite(sortNotSelected);
            sort.Position = new Vector2f(character_pet_questSprite.Position.X + 710, character_pet_questSprite.Position.Y + 460);

            selectedMarker = new Sprite(selectedMarkerTexture);
            selectedMarkerPosition = inventoryMatrixPosition;

            inventoryWidth = 9;
            inventoryLength = 7;

            inventoryMatrix = new Stack<AbstractItem>[inventoryLength, inventoryWidth];

            itemDescription = new Text("", Game.font, 20);

            spriteAndTextPositionUpdate();
            inInventory = true;

            equipment = new Equipment[6];

            characterMenuActivated = true;
            petMenuActivated = false;
            questLogActivated = false;

            foreach (Stack<AbstractItem> itemStack in inventoryMatrix)
            {
                StackCount.Add(new Text("", Game.font, 15));
                StackCount[StackCount.Count - 1].Color = Color.Black;
                StackCount[StackCount.Count - 1].Style = Text.Styles.Bold;
            }
        }

        public void spriteAndTextPositionUpdate()
        {
            displayedPlayer = new Sprite(displayedPlayerTexture);
            displayedPlayer.Position = new Vector2f(character_pet_questSprite.Position.X + 67, character_pet_questSprite.Position.Y + 82);

            attack.DisplayedString = "Attack: " + PlayerHandler.Player.Att;
            attack.Position = new Vector2f(character_pet_questSprite.Position.X + 65, inventoryMatrixPosition.Y + 7 * FIELDSIZE + 10);

            defense.DisplayedString = "Defense: " + PlayerHandler.Player.Def;
            defense.Position = new Vector2f(attack.Position.X, attack.Position.Y + attack.CharacterSize);

            exp.DisplayedString = "EXP: " + PlayerHandler.Player.CurrentEXP + "/" + PlayerHandler.Player.MaxEXP;
            exp.Position = new Vector2f(defense.Position.X, defense.Position.Y + defense.CharacterSize);

            lvl.DisplayedString = "Lvl: " + PlayerHandler.Player.Lvl;
            if (PlayerHandler.Player.Lvl == PlayerHandler.Player.MaxLvl)
                lvl.DisplayedString += "°";
            lvl.Position = new Vector2f(exp.Position.X, exp.Position.Y + exp.CharacterSize);

            gold.DisplayedString = "Gold: " + PlayerHandler.Player.Gold;
            goldSprite.Position = new Vector2f(lvl.Position.X - 10, lvl.Position.Y + 50);
            gold.Position = new Vector2f(goldSprite.Position.X + (goldSprite.Texture.Size.X / 2), goldSprite.Position.Y - 5);

            for (int i = 0; i < StackCount.Count; i++)
            {
                StackCount[i].Position = new Vector2f(inventoryMatrixPosition.X + ((i % inventoryWidth) * FIELDSIZE) + 35, 
                                                      inventoryMatrixPosition.Y + ((i / inventoryWidth) * FIELDSIZE) + FIELDSIZE - StackCount[i].CharacterSize);
            }

            itemDescription.Position = new Vector2f(inventoryMatrixPosition.X + 5, inventoryMatrixPosition.Y + inventoryMatrix.GetLength(0) * FIELDSIZE + 5);


            selectedMarker.Position = selectedMarkerPosition;
        }

        public bool isFullWith(AbstractItem item)
        {
            if (item.Type.Equals("Object.Item.Money.Gold"))
            {
                return false;
            }

            foreach (Stack<AbstractItem> itemStack in inventoryMatrix)
            {
                try
                {
                    if (itemStack.Count == 0 || itemStack.Count < MaxStackCount && item.CompareTo(itemStack.Peek()) == 0)
                    {
                        return false;
                    }
                }
                catch (NullReferenceException)
                {
                    return false;
                }
            }
            return true;
        }

        public void sortManagment()
        {
            if (NavigationHelp.isMouseInSprite(sort))
            {
                sort.Texture = sortSelected;

                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    Game.isPressed = true;

                    sortItem();
                    //updateMatrix();
                }
            }
            else
                sort.Texture = sortNotSelected;
        }

        private List<Stack<AbstractItem>> InventoryCastToList()
        {
            List<Stack<AbstractItem>> list = new List<Stack<AbstractItem>>();

            for (int i = 0; i < inventoryMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < inventoryMatrix.GetLength(1); j++)
                {
                    list.Add(inventoryMatrix[i, j]);
                    inventoryMatrix[i, j] = null;
                }
            }

            return list;
        }

        public void sortItem()
        {
            List<Stack<AbstractItem>> inventoryList = InventoryCastToList();

            NavigationHelp.deleteNullObjectFromList(inventoryList);
            inventoryList.Sort(new SortByID());
            
            for (int i = 0; i < inventoryList.Count; i++)
            {
                inventoryMatrix[i / inventoryWidth, i % inventoryWidth] = inventoryList[i];
            }
        }

        public bool getInventoryOpen()
        {
            if (Keyboard.IsKeyPressed(Controls.OpenInventar) && !Game.isPressed && !inventoryOpen)
            {
                Game.isPressed = true;
                setOpen();
                inventoryOpen = true;
            }

            return inventoryOpen;
        }

        public void characterMenuManagment()
        {
            mouseCharacterManagment();
            
            if (Keyboard.IsKeyPressed(Controls.Up) && !Game.isPressed)
            {
                if (yInInventory == 0 && inInventory)  //enter Tabs
                {
                    inInventory = false;
                    inTab = true;
                    xInTabs = xInInventory % 2;
                    if (xInTabs == selectedTab)
                    {
                        xInTabs++;
                    }
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
                Game.isPressed = true;
            }

            if (Keyboard.IsKeyPressed(Controls.Down) && !Game.isPressed)
            {
                if (inTab)
                {
                    inInventory = true;
                    inTab = false;
                }
                else if (inInventory)
                {
                    yInInventory = (yInInventory + 1) % inventoryMatrix.GetLength(0);
                }
                else
                {
                    yInEquipmentSlots = (yInEquipmentSlots + 1) % equipment.Length;
                }
                Game.isPressed = true;
            }

            if (Keyboard.IsKeyPressed(Controls.Right) && !Game.isPressed)
            {
                if (inEquipmentSlots)
                {
                    inInventory = true;
                    inEquipmentSlots = false;
                }
                else if (inInventory)
                {
                    xInInventory = (xInInventory + 1) % inventoryMatrix.GetLength(1);
                }
                else
                {
                    do
                    {
                        xInTabs = (xInTabs + 1) % 3;
                    } while (xInTabs == selectedTab);
                }
                Game.isPressed = true;
            }

            if (Keyboard.IsKeyPressed(Controls.Left) && !Game.isPressed)
            {
                if (xInInventory == 0 && inInventory)  //enter equipmentSlots
                {
                    inInventory = false;
                    inEquipmentSlots = true;
                    yInEquipmentSlots = yInInventory % equipment.Length;
                }
                else if (inTab)
                {
                    do
                    {
                        xInTabs = (xInTabs + 2) % 3;
                    } while (xInTabs == selectedTab);
                }
                else
                {
                    xInInventory = (xInInventory + (inventoryMatrix.GetLength(1) - 1)) % inventoryMatrix.GetLength(1);
                    inInventory = true;
                    inEquipmentSlots = false;
                }
                Game.isPressed = true;
            }

            if (inInventory)
            {
                inInventoryManagement();
            }
            else if (inEquipmentSlots)
            {
                equipmentManagement();
            }

            if (inInventory)
            {
                selected.Position = getSelectedPosition();
            }
            else if (inEquipmentSlots)
            {
                if (yInEquipmentSlots < 2)
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
        }

        public void petMenuManagment()
        {

        }

        public void questLogManagment()
        {
            mouseQuestLogManagment();
        }

        public void managment()
        {
            if ((Keyboard.IsKeyPressed(Controls.OpenInventar) || Keyboard.IsKeyPressed(Controls.Escape)) && !Game.isPressed)
            {
                Game.isPressed = true;
                inventoryOpen = false;
            }

            if (Mouse.IsButtonPressed(Mouse.Button.Left) && NavigationHelp.isMouseInSprite(closeButton) && !Game.isPressed)
            {
                Game.isPressed = true;
                inventoryOpen = false;
            }

            if (NavigationHelp.isMouseInSprite(closeButton))
                closeButton.Texture = closeButtonSelected;
            else
                closeButton.Texture = closeButtonNotSelected;

            if (characterMenuActivated)
            {
                characterMenuManagment();
                sortManagment();
            }

            if (petMenuActivated)
                petMenuManagment();

            if (questLogActivated)
                questLogManagment();

            setTabs();
        }

        public void inInventoryManagement()
        {
            if ((Keyboard.IsKeyPressed(Controls.ButtonForAttack) || Mouse.IsButtonPressed(Mouse.Button.Left)) && !Game.isPressed)    //Item Swaps
            {
                Game.isPressed = true;

                selectedMarkerPosition = getSelectedPosition();

                if (!itemIsSelected)
                {
                    selectedItemStack = inventoryMatrix[yInInventory, xInInventory];
                    itemIsSelected = true;
                    selectedItemStackX = xInInventory;
                    selectedItemStackY = yInInventory;
                }
                else
                {
                    inventoryMatrix[selectedItemStackY, selectedItemStackX] = inventoryMatrix[yInInventory, xInInventory];

                    if (inventoryMatrix[selectedItemStackY, selectedItemStackX] != null && inventoryMatrix[selectedItemStackY, selectedItemStackX].Peek() != null)
                    {
                        inventoryMatrix[selectedItemStackY, selectedItemStackX].Peek().setPositionMatrix(selectedItemStackX, selectedItemStackY);
                    }

                    inventoryMatrix[yInInventory, xInInventory] = selectedItemStack;

                    if (inventoryMatrix[yInInventory, xInInventory] != null)
                    {
                        inventoryMatrix[yInInventory, xInInventory].Peek().setPositionMatrix(xInInventory, yInInventory);
                    }
                    selectedItemStack = null;
                    itemIsSelected = false;
                }
            }

            if ((Keyboard.IsKeyPressed(Controls.UseItem) || Mouse.IsButtonPressed(Mouse.Button.Right)) && !Game.isPressed)
            {
                Game.isPressed = true;
                if (inventoryMatrix[yInInventory, xInInventory] != null && inventoryMatrix[yInInventory, xInInventory].Count != 0 && inventoryMatrix[yInInventory, xInInventory].Peek() != null)
                {
                    inventoryMatrix[yInInventory, xInInventory].Peek().giveMatrixPosition(yInInventory, xInInventory);
                    inventoryMatrix[yInInventory, xInInventory].Peek().use();
                    PlayerHandler.Player.StatsUpdate();
                }
            }

            if (inventoryMatrix[yInInventory, xInInventory] != null && inventoryMatrix[yInInventory, xInInventory].Count != 0)
            {
                itemDescription.DisplayedString = inventoryMatrix[yInInventory, xInInventory].Peek().ItemDiscription;
            }

            if (inventoryMatrix[yInInventory, xInInventory] != null && inventoryMatrix[yInInventory, xInInventory].Count != 0 && !inventoryMatrix[yInInventory, xInInventory].Peek().IsAlive)
            {
                inventoryMatrix[yInInventory, xInInventory].Pop();
                if (inventoryMatrix[yInInventory, xInInventory].Count != 0)
                {
                    inventoryMatrix[yInInventory, xInInventory].Peek().Position = getSelectedPosition();
                    inventoryMatrix[yInInventory, xInInventory].Peek().setVisible(true);
                }
                else
                {
                    inventoryMatrix[yInInventory, xInInventory] = null;
                }
            }

        }

        public void equipmentManagement()
        {
            if ((Keyboard.IsKeyPressed(Controls.UseItem) || Mouse.IsButtonPressed(Mouse.Button.Right)) && !Game.isPressed)
            {
                Game.isPressed = true;
                for (int i = 0; i < equipmentPosition.Length; i++)
                {
                    if (equipment[i] != null && selected.Position.Equals(equipmentPosition[i]))
                    {
                        equipment[i].use();
                        PlayerHandler.Player.StatsUpdate();
                    }
                }
            }
        }

        public Vector2f getSelectedPosition()
        {
            return new Vector2f(xInInventory * FIELDSIZE + inventoryMatrixPosition.X, yInInventory * FIELDSIZE + inventoryMatrixPosition.Y);
        }

        public Vector2f mousePositionInInventoryMatrix()
        {
            return new Vector2f(((Mouse.GetPosition(window).X - inventoryMatrixPosition.X) / FIELDSIZE), (Mouse.GetPosition(window).Y - inventoryMatrixPosition.Y) / FIELDSIZE);
        }

        public bool mouseInInventoryMatrix()
        {            
            return (0 <= (mousePositionInInventoryMatrix().X) && inventoryWidth > (mousePositionInInventoryMatrix().X)
                 && 0 <= (mousePositionInInventoryMatrix().Y) && inventoryLength > (mousePositionInInventoryMatrix().Y));
        }

        public bool mouseInFirstEquipmentSlot()
        {
            return (Mouse.GetPosition(window).X >= firstEquipmentSlotPostion.X && Mouse.GetPosition(window).X <= (firstEquipmentSlotPostion.X + FIELDSIZE)
                 && Mouse.GetPosition(window).Y >= firstEquipmentSlotPostion.Y && Mouse.GetPosition(window).Y <= (firstEquipmentSlotPostion.Y + FIELDSIZE));
        }

        public bool mouseInSecondEquipmentSlot()
        {
            return (Mouse.GetPosition(window).X >= secondEquipmentSlotPostion.X && Mouse.GetPosition(window).X <= (secondEquipmentSlotPostion.X + FIELDSIZE)
                 && Mouse.GetPosition(window).Y >= secondEquipmentSlotPostion.Y && Mouse.GetPosition(window).Y <= (secondEquipmentSlotPostion.Y + FIELDSIZE));
        }

        public bool mouseInThirdEquipmentSlot()
        {
            return (Mouse.GetPosition(window).X >= thirdEquipmentSlotPostion.X && Mouse.GetPosition(window).X <= (thirdEquipmentSlotPostion.X + FIELDSIZE)
                 && Mouse.GetPosition(window).Y >= thirdEquipmentSlotPostion.Y && Mouse.GetPosition(window).Y <= (thirdEquipmentSlotPostion.Y + FIELDSIZE));
        }

        public bool mouseInFourthEquipmentSlot()
        {
            return (Mouse.GetPosition(window).X >= fourthEquipmentSlotPostion.X && Mouse.GetPosition(window).X <= (fourthEquipmentSlotPostion.X + FIELDSIZE)
                 && Mouse.GetPosition(window).Y >= fourthEquipmentSlotPostion.Y && Mouse.GetPosition(window).Y <= (fourthEquipmentSlotPostion.Y + FIELDSIZE));
        }

        public bool mouseInFifthEquipmentSlot()
        {
            return (Mouse.GetPosition(window).X >= fifthEquipmentSlotPostion.X && Mouse.GetPosition(window).X <= (fifthEquipmentSlotPostion.X + FIELDSIZE)
                 && Mouse.GetPosition(window).Y >= fifthEquipmentSlotPostion.Y && Mouse.GetPosition(window).Y <= (fifthEquipmentSlotPostion.Y + FIELDSIZE));
        }

        public bool mouseInSixthEquipmentSlot()
        {
            return (Mouse.GetPosition(window).X >= sixthEquipmentSlotPostion.X && Mouse.GetPosition(window).X <= (sixthEquipmentSlotPostion.X + FIELDSIZE)
                 && Mouse.GetPosition(window).Y >= sixthEquipmentSlotPostion.Y && Mouse.GetPosition(window).Y <= (sixthEquipmentSlotPostion.Y + FIELDSIZE));
        }

        public bool mouseInEquipmentSlots()
        {
            return (mouseInFirstEquipmentSlot() || mouseInSecondEquipmentSlot()
                 || mouseInThirdEquipmentSlot() || mouseInFourthEquipmentSlot()
                 || mouseInFifthEquipmentSlot() || mouseInSixthEquipmentSlot());
        }

        public void mouseTabManagment()
        {
            if (NavigationHelp.isMouseInSprite(characterTab))
            {
                inTab = true;
                inInventory = false;
                inEquipmentSlots = false;

                xInTabs = 0;
            }

            if (NavigationHelp.isMouseInSprite(petTab))
            {
                inTab = true;
                inInventory = false;
                inEquipmentSlots = false;

                xInTabs = 1;
            }

            if (NavigationHelp.isMouseInSprite(questTab))
            {
                inTab = true;
                inInventory = false;
                inEquipmentSlots = false;

                xInTabs = 2;
            }
        }

        public void mouseCharacterManagment()
        {            
            if (mouseInInventoryMatrix())
            {
                inInventory = true;
                inTab = false;
                inEquipmentSlots = false;

                xInInventory = (int)mousePositionInInventoryMatrix().X;
                yInInventory = (int)mousePositionInInventoryMatrix().Y;
            }

            if(mouseInEquipmentSlots())
            {
                inEquipmentSlots = true;
                inTab = false;
                inInventory = false;

                if (mouseInFirstEquipmentSlot())
                    yInEquipmentSlots = 0;
                if (mouseInSecondEquipmentSlot())
                    yInEquipmentSlots = 1;
                if (mouseInThirdEquipmentSlot())
                    yInEquipmentSlots = 2;
                if (mouseInFourthEquipmentSlot())
                    yInEquipmentSlots = 3;
                if (mouseInFifthEquipmentSlot())
                    yInEquipmentSlots = 4;
                if (mouseInSixthEquipmentSlot())
                    yInEquipmentSlots = 5;
            }
        }

        public void mouseQuestLogManagment()
        {
            if (NavigationHelp.isMouseInSprite(scrollArrowBottom))
                scrollArrowBottom.Texture = scrollArrowBottomSelected;
            else
                scrollArrowBottom.Texture = scrollArrowBottomNotSelected;

            if (NavigationHelp.isMouseInSprite(scrollArrowTop))
                scrollArrowTop.Texture = scrollArrowTopSelected;
            else
                scrollArrowTop.Texture = scrollArrowTopNotSelected;
        }

        public void changeTab()
        {
            if (NavigationHelp.isSpriteKlicked(xInTabs, 0, characterTab, Controls.Return))
            {
                Game.isPressed = true;
                characterMenuActivated = true;
                petMenuActivated = false;
                questLogActivated = false;
                character_pet_questSprite.Texture = characterMenu;
                selectedTab = 0;
            }

            if (NavigationHelp.isSpriteKlicked(xInTabs, 1, petTab, Controls.Return))
            {
                Game.isPressed = true;
                characterMenuActivated = false;
                petMenuActivated = true;
                questLogActivated = false;
                character_pet_questSprite.Texture = petMenu;
                selectedTab = 1;
            }

            if (NavigationHelp.isSpriteKlicked(xInTabs, 2, questTab, Controls.Return))
            {
                Game.isPressed = true;
                characterMenuActivated = false;
                petMenuActivated = false;
                questLogActivated = true;
                character_pet_questSprite.Texture = questLog;
                selectedTab = 2;
            }
        }

        public void updateMatrix()
        {
            int k = 0;
            for (int i = 0; i < inventoryMatrix.GetLength(0); i++ )
            {
                for (int j = 0; j < inventoryMatrix.GetLength(1); j++)
                {
                    if (inventoryMatrix[i,j] != null && inventoryMatrix[i,j].Count == 0)
                    {
                        inventoryMatrix[i, j] = null;
                    }

                    if (inventoryMatrix[i, j] != null && inventoryMatrix[i, j].Count != 0 && inventoryMatrix[i, j].Peek() != null)
                    {
                        inventoryMatrix[i, j].Peek().setPositionMatrix(j, i);
                        inventoryMatrix[i, j].Peek().Sprite.Position = inventoryMatrix[i, j].Peek().Position;
                        inventoryMatrix[i, j].Peek().setVisible(true);
                    }

                    if (inventoryMatrix[i,j] != null && inventoryMatrix[i,j].Count != 0)
                    {
                        if (inventoryMatrix[i,j].Count < 10)
                        {
                            if (inventoryMatrix[i, j].Peek().stackable)
                                StackCount[k].DisplayedString = "0" + Convert.ToString(inventoryMatrix[i, j].Count);
                            else
                                StackCount[k].DisplayedString = "";
                        }
                        else
                        {
                            StackCount[k].DisplayedString = Convert.ToString(inventoryMatrix[i,j].Count);
                        }
                    }
                    else
                    {
                        StackCount[k].DisplayedString = "";
                    }
                    k++;
                }
            }
        }

        public float getAttBonus()
        {
            float attBonus = 0;
            foreach (Equipment equip in equipment)
            {
                if (equip != null)
                {
                    attBonus += equip.bonusAtt;
                }
            }
            return attBonus;
        }

        public float getDefBonus()
        {
            float defBonus = 0;
            foreach (Equipment equip in equipment)
            {
                if (equip != null)
                {
                    defBonus += equip.bonusDef;
                }
            }
            return defBonus;
        }

        public float getHpBonus()
        {
            float hpBonus = 0;
            foreach (Equipment equip in equipment)
            {
                if (equip != null)
                {
                    hpBonus += equip.bonusHp;
                }
            }
            return hpBonus;
        }

        public void deleate()
        {
            inventoryMatrix = new Stack<AbstractItem>[inventoryLength, inventoryWidth];
        }

        public void setTabs()
        {
            setCharacterTab();
            setPetTab();
            setQuestTab();
        }

        public void setCharacterTab()
        {
            if (inTab && xInTabs != selectedTab && xInTabs == 0)
            {
                characterTab.Texture = characterTabSelected;
            }
            else
            {
                characterTab.Texture = characterTabNotSelected;
            }
        }

        public void setPetTab()
        {
            if (inTab && xInTabs != selectedTab && xInTabs == 1)
            {
                petTab.Texture = petTabSelected;
            }
            else
            {
                petTab.Texture = petTabNotSelected;
            }
        }

        public void setQuestTab()
        {
            if (inTab && xInTabs != selectedTab && xInTabs == 2)
            {
                questTab.Texture = questTabSelected;
            }
            else
            {
                questTab.Texture = questTabNotSelected;
            }
        }

        public void update(GameTime gameTime)
        {
            getInventoryOpen();
            if (inventoryOpen)
            {
                spriteAndTextPositionUpdate();
                updateMatrix();
                managment();
                mouseTabManagment();
                changeTab();
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
            foreach (Text txt in StackCount)
            {
                window.Draw(txt);
            }
            window.Draw(itemDescription);
        }

        public void draw(RenderWindow window)
        {
            window.Draw(character_pet_questSprite);

            if (characterMenuActivated)
            {
                ItemHandler.drawInventoryItems(window);
                if (!inTab)
                {
                    window.Draw(selected);
                }
                drawTexts(window);
                window.Draw(sort);
                
                window.Draw(selectedMarker);
            }

            if(questLogActivated)
            {
                window.Draw(scrollArrowBottom);
                window.Draw(scrollArrowTop);
            }

            window.Draw(characterTab);
            window.Draw(petTab);
            window.Draw(questTab);
            window.Draw(closeButton);
        }
    }
}
