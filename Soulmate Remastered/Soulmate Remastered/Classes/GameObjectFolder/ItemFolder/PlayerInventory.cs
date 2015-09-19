using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.EquipmentFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.MoneyFolder;
using Soulmate_Remastered.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder
{
    /// <summary>
    /// this hole character pet quest management
    /// </summary>
    class PlayerInventory
    {
        //Heading////////////////////////////////////////////////////////////////////////////////////////////////////////////


        //events***********************************************************************************

        /// <summary>
        /// event that fires when the inventory is opened
        /// </summary>
        public event EventHandler Open_CloseInventory;

        /// <summary>
        /// triggert when equipment is used
        /// </summary>
        static event EventHandler<UseEventArgs> EquipEvent;

        /// <summary>
        /// fires Open_CloseInventory event
        /// </summary>
        private void OnOpen_Close()
        {
            EventHandler handler = Open_CloseInventory;
            if (handler != null)
                handler(this, null);
        }

        /// <summary>
        /// triggers the equip event, you may only call it in equipment, and only once
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public static void OnEquip(object sender, UseEventArgs eventArgs)
        {
            EventHandler<UseEventArgs> handler = EquipEvent;
            if (handler != null)
                handler(sender, eventArgs);
        }

        /// <summary>
        /// called by initialization and when somethings changed in the inventory
        /// </summary>
        private void MatrixUpdate(object sender, EventArgs eventargs)
        {
            for (int i = 0; i < inventory.Capacity; ++i)
            {
                AbstractItem item = inventory.At(i);

                if (item != null)
                    item.Position = InventoryMatrixPosition + (new Vector2(i % MatrixSizeX, i / MatrixSizeX) * FIELDSIZE);

                matrix[i / MatrixSizeX, i % MatrixSizeX] = item;
            }
        }

        /// <summary>
        /// equips the equipment
        /// </summary>
        private void Equip(object sender, UseEventArgs e)
        {
            //the equiped equipment
            Equipment eq = (Equipment)sender;

            //make spac in inventory
            inventory.RemoveAt(e.Index);

            //unequip the destined slot and add it to the inventory
            inventory.Add(equip.At((int)eq.EquipmentType));

            //make space for the equipment
            equip.RemoveAt((int)eq.EquipmentType);

            //equips the equipment and brings it to the destined slot
            equip.Swap((int)equip.Add(eq), (int)eq.EquipmentType);

        }

        /// <summary>
        /// when ever the equipment is changed this will update all equipment items
        /// </summary>
        private void EquipmentUpate(object sender, EventArgs e)
        {
            for (int i = 0; i < equip.Capacity; ++i)
            {
                if (equip.At(i) != null)
                {
                    Equipment eq = (Equipment)equip.At(i);

                    eq.Position = EquipmentBase + EquipmentOffsets[(int)eq.EquipmentType];
                }
            }
        }

        //Mouse///////////////////////////////////////////////////////////////////

        /// <summary>
        /// things that should happen when a mouse button is pressed
        /// </summary>
        private void MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            Vector2 mousePosition = new Vector2(e.X, e.Y);

            if (e.Button == Mouse.Button.Left)
                LeftMouseButtonPressed(mousePosition);
            if (e.Button == Mouse.Button.Right)
                RightMouseButtonPressed(mousePosition);
        }

        /// <summary>
        /// things that shal happen when a mouse button is relized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseButtonRelized(object sender, MouseButtonEventArgs e)
        {
            Vector2 mousePosition = new Vector2(e.X, e.Y);

            if (e.Button == Mouse.Button.Left)
                LeftMouseButtonRelized(mousePosition);
        }

        //*****************************************************************************************


        //constants********************************************************************************

        /// <summary>
        /// the end of one part of the inventory
        /// </summary>
        private static char LineBreak { get { return '?'; } }
        /// <summary>
        /// size of the field of the places in the inventory
        /// </summary>
        public static float FIELDSIZE { get { return 49f; } }
        /// <summary>
        /// outLineThickness of the selected rectangle
        /// </summary>
        float outLineThickness { get { return 1; } }
        /// <summary>
        /// size of the matrix
        /// </summary>
        static Vector2 MatrixSize { get { return new Vector2(9, 7); } }
        /// <summary>
        /// the position of the matrix
        /// </summary>
        public Vector2 InventoryMatrixPosition { get { return (Vector2)character_pet_questSprite.Position + new Vector2(306, 106) + outLineThickness; } }
        /// <summary>
        /// float for scrollarrow scaling
        /// </summary>
        float scrollArrowScaleValue { get { return 0.43f; } }

        /// <summary>
        /// the Base position for the equipment
        /// </summary>
        Vector2 EquipmentBase { get{ return (Vector2)character_pet_questSprite.Position + new Vector2(208, 102); } }
        /// <summary>
        /// the offsets from the base of equipment according to wich index u got
        /// </summary>
        Vector2[] EquipmentOffsets
        {
            get
            {
                return new Vector2[]
                {
                    new Vector2(0, 0),
                    new Vector2(0,  54),
                    new Vector2( 15, 114),
                    new Vector2( 15,  168),
                    new Vector2( - 4,  228),
                    new Vector2( -4 , 282)
                };
            }
        }

        /// <summary>
        /// quest base position(upper right corner of the quest list
        /// </summary>
        Vector2 QuestBasePosition { get { return (Vector2)character_pet_questSprite.Position + new Vector2(53, 107); } }

        ///<summary>
        ///the size of the quest lines, the size wich the cursor becomes in this tab 
        ///</summary>
        Vector2 QuestLineSize { get { return new Vector2(398, FIELDSIZE - 1); } }

        /// <summary>
        /// the max number of quest that can be displayed in this tab
        /// </summary>
        int maxQuestOnOnePage { get { return 7; } }

        //*****************************************************************************************


        //properties*******************************************************************************

        /// <summary>
        /// bool if the inventory is open
        /// </summary>
        public static bool IsOpen { get; private set; }
        /// <summary>
        /// the number of slots in x direction
        /// </summary>
        public static int MatrixSizeX { get { return (int)MatrixSize.X; } }
        /// <summary>
        /// the number of slots in y direction
        /// </summary>
        public static int MatrixSizeY { get { return (int)MatrixSize.Y; } }

        //*****************************************************************************************

        //inventories******************************************************************************

        /// <summary>
        /// the inventory for the items
        /// </summary>
        public Inventory inventory;

        /// <summary>
        /// the equiped equip
        /// </summary>
        public Inventory equip;

        /// <summary>
        /// the equiped petEquip
        /// </summary>
        public Inventory petEquip;

        /// <summary>
        /// the shown matrix in wich the items lay
        /// <para>IMPORTANT: matrix[y,x] is the item at the xy position</para>
        /// </summary>
        AbstractItem[,] matrix;

        //sprites + textures***********************************************************************


        Texture characterMenuTexture = new Texture("Pictures/Inventory/Inventory.png");
        Texture petMenuTexture = new Texture("Pictures/Inventory/PetMenu.png");
        Texture questLogTexture = new Texture("Pictures/Inventory/QuestLog.png");
        /// <summary>
        /// the shown sprite for the inventory and its tabs
        /// </summary>
        Sprite character_pet_questSprite;

        Texture characterTabTexture = new Texture("Pictures/Inventory/Tabs/CharacterTabNotSelected.png");
        /// <summary>
        /// sprite for the character tab
        /// </summary>
        Sprite characterTab;

        Texture petTabTexture = new Texture("Pictures/Inventory/Tabs/PetTabNotSelected.png");
        /// <summary>
        /// sprite for the pet tab
        /// </summary>
        Sprite petTab;

        Texture questTabTexture = new Texture("Pictures/Inventory/Tabs/QuestTabNotSelected.png");
        /// <summary>
        /// sprite for the quest tab
        /// </summary>
        Sprite questTab;

        Texture scrollArrowBottomTexture = new Texture("Pictures/Inventory/ScrollArrow/ScrollArrowBottomNotSelected.png");
        /// <summary>
        /// sprite for the quest scroll arrow (bottom)
        /// </summary>
        Sprite scrollArrowBottom;

        Texture scrollArrowTopTexture = new Texture("Pictures/Inventory/ScrollArrow/ScrollArrowTopNotSelected.png");
        /// <summary>
        /// sprite for the quest scroll arrow (top)
        /// </summary>
        Sprite scrollArrowTop;

        Texture closeButtonTexture = new Texture("Pictures/Inventory/CloseButton/CloseButtonNotSelected.png");
        /// <summary>
        /// sprite for the close button
        /// </summary>
        Sprite closeButton;

        Texture sortTexture = new Texture("Pictures/Inventory/Sort/SortNotSelected.png");
        /// <summary>
        /// sprite for the sort button
        /// </summary>
        Sprite sort;

        Texture goldTexture = new Texture("Pictures/Items/Money/Gold.png");
        /// <summary>
        /// the gold icon
        /// </summary>
        Sprite goldSprite;

        Texture displayedPlayerTexture = new Texture("Pictures/Inventory/CharakterSprite/PlayerFrontInventory.png");
        /// <summary>
        /// the inventory charakter
        /// </summary>
        Sprite displayedPlayer;

        //Texts************************************************************************************

        /// <summary>
        /// the amount of gold the player carries
        /// </summary>
        Text gold;
        /// <summary>
        /// the attack of the player
        /// </summary>
        Text attack;
        /// <summary>
        /// the defense of the player
        /// </summary>
        Text defense;
        /// <summary>
        /// the exp of the player
        /// </summary>
        Text exp;
        /// <summary>
        /// the lvl of the player
        /// </summary>
        Text lvl;

        /// <summary>
        /// name and description of the item
        /// </summary>
        Text itemDescription;


        //shader***********************************************************************************

        /// <summary>
        /// the render wich contains the selectedShader
        /// </summary>
        RenderStates SelectedTabState;
        /// <summary>
        /// the shader wich indicates the selected tab
        /// </summary>
        Shader selectedTabShader;
        /// <summary>
        /// renderstate for the selected sort/close button
        /// </summary>
        RenderStates SelectedCloseSortState;
        /// <summary>
        /// shader for the selected sort/close button
        /// </summary>
        Shader selectedCloseSortShader;
        /// <summary>
        /// state wich colors the cursor green
        /// </summary>
        RenderStates SelectedCursorState;
        /// <summary>
        /// shader that colors things green
        /// </summary>
        Shader selectedCursorShader;

        //cursor management************************************************************************

        /// <summary>
        /// the cursor
        /// </summary>
        RectangleShape cursor;
        /// <summary>
        /// should only contain int values
        /// <para>descripes the position within the matrix</para>
        /// </summary>
        Vector2 cursorPositionInMatrix;
        /// <summary>
        /// the position in matrix coordinates of the seleced field
        /// </summary>
        Vector2 positionSelected;
        /// <summary>
        /// bool if something is selected
        /// </summary>
        bool isSeleced;

        /// <summary>
        /// int that equals the index of the equipmentSlot
        /// </summary>
        int positionInEquipment;

        /// <summary>
        /// int that equals the index of the petEquipmentSlot
        /// </summary>
        int positionInPetEquipment;

        /// <summary>
        /// which quest is selected
        /// </summary>
        int positionInQuest;

        /// <summary>
        /// the selected tab
        /// </summary>
        ETab selectedTab;
        /// <summary>
        /// the Tab we are working in at the moment
        /// </summary>
        ETab activeTab;

        /// <summary>
        /// enum for the tabs
        /// </summary>
        enum ETab
        {
            None = -1,
            Character = 0,
            Pet = 1,
            Quest = 2,

            Count
        }

        /// <summary>
        /// in wich section of the management is the cursor (no mouse)
        /// </summary>
        ECursorIn cursorIn;

        /// <summary>
        /// enum for possible section of the management where the cursor (no mouse) could be in
        /// </summary>
        enum ECursorIn
        {
            None = -1,

            Inventory,
            Equipment,
            Tabs,
            Quests,
            PetEquipment,

            Count
        }

        bool staticEventsAreInitialized = false;

        //properties for selection (mouse only)**********************************************************

        bool closeButtonSelected { get { return MouseControler.MouseIn(closeButton); } }
        bool sortSelected { get { return MouseControler.MouseIn(sort); } }
        bool itemIsFollowingMouse { get; set; }
        AbstractItem followingItem { get; set; }

        //methods/////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Save/ Load***********************************************************************
        public string ToStringForSave()
        {
            string res = "";

            res += inventory.ToStringForSave() + LineBreak.ToString();
            res += equip.ToStringForSave() + LineBreak.ToString();
            res += petEquip.ToStringForSave();

            return res;
        }

        public void Load(string loadFrom)
        {
            string[] array = loadFrom.Split(LineBreak);

            if (array.Length != 3)
                throw new IndexOutOfRangeException();

            inventory = new Inventory();
            inventory.Load(array[0]);

            equip = new Inventory();
            equip.Load(array[1]);

            petEquip = new Inventory();
            petEquip.Load(array[2]);

            //adding the matrix update to the changed event
            //meaning that it is allways called when something
            //is changed within the inventory
            inventory.Changed += MatrixUpdate;
            //same as above but this time for the equipment
            equip.Changed += EquipmentUpate;
        }

        //initialize***********************************************************************

        /// <summary>
        /// initialize the player inventory
        /// </summary>
        public PlayerInventory()
        {
            //core++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            matrix = new AbstractItem[MatrixSizeY, MatrixSizeX];

            int capacity = MatrixSizeX * MatrixSizeY;

            inventory = new Inventory(capacity);
            equip = new Inventory(6);
            petEquip = new Inventory(4);

            //events++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            Open_CloseInventory += (sender, eventargs) => { IsOpen = !IsOpen; };

            EquipEvent += Equip;

            //adding the matrix update to the changed event
            //meaning that it is allways called when something
            //is changed within the inventory
            inventory.Changed += MatrixUpdate;
            //same as above but this time for the equipment
            equip.Changed += EquipmentUpate;

            if (!staticEventsAreInitialized)
            {
                staticEventsAreInitialized = true;
                MouseControler.ButtonPressed += MouseButtonPressed;
                MouseControler.Relize += MouseButtonRelized;
            }
            //default values++++++++++++++++++++++++++++++++++++++++++++++++++++++
            IsOpen = false;
            selectedTab = ETab.None;
            activeTab = ETab.Character;
            cursorIn = ECursorIn.Inventory;
            positionInEquipment = 0;
            positionInQuest = 0;
            positionInPetEquipment = 0;
            itemIsFollowingMouse = false;
            followingItem = null;

            //sprites+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            character_pet_questSprite = new Sprite(characterMenuTexture);
            character_pet_questSprite.Position = new Vector2((Game.windowSizeX - characterMenuTexture.Size.X) / 2, (Game.windowSizeY - characterMenuTexture.Size.Y) / 2);

            characterTab = new Sprite(characterTabTexture);
            characterTab.Position = new Vector2f(character_pet_questSprite.Position.X + 63, character_pet_questSprite.Position.Y + 37);

            petTab = new Sprite(petTabTexture);
            petTab.Position = new Vector2f(characterTab.Position.X + 131, characterTab.Position.Y);

            questTab = new Sprite(questTabTexture);
            questTab.Position = new Vector2f(characterTab.Position.X + 210, characterTab.Position.Y);

            scrollArrowBottom = new Sprite(scrollArrowBottomTexture);
            scrollArrowBottom.Scale = new Vector2(scrollArrowScaleValue, scrollArrowScaleValue);
            scrollArrowBottom.Position = new Vector2(727, 420) + (Vector2)character_pet_questSprite.Position;

            scrollArrowTop = new Sprite(scrollArrowTopTexture);
            scrollArrowTop.Scale = new Vector2(scrollArrowScaleValue, scrollArrowScaleValue);
            scrollArrowTop.Position = new Vector2(scrollArrowBottom.Position.X - 19, scrollArrowBottom.Position.Y);

            closeButton = new Sprite(closeButtonTexture);
            closeButton.Position = (Vector2)character_pet_questSprite.Position + new Vector2(character_pet_questSprite.Texture.Size.X - closeButton.Texture.Size.X - 3, 3);

            sort = new Sprite(sortTexture);
            sort.Position = (Vector2)character_pet_questSprite.Position + new Vector2(710, 460);

            goldSprite = new Sprite(goldTexture);
            goldSprite.Scale = new Vector2f(0.25f, 0.25f);

            displayedPlayer = new Sprite(displayedPlayerTexture);
            displayedPlayer.Position = (Vector2)character_pet_questSprite.Position + new Vector2(67, 82);

            //Texts+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            attack = new Text("Attack Damage: ", Game.font, 20);
            attack.Position = new Vector2(character_pet_questSprite.Position.X, InventoryMatrixPosition.Y) + new Vector2(65, 7 * FIELDSIZE + 10);

            defense = new Text("Defense: ", Game.font, 20);
            defense.Position = (Vector2)attack.Position + new Vector2(0, attack.CharacterSize);

            exp = new Text("Exp: ", Game.font, 20);
            exp.Position = (Vector2)defense.Position + new Vector2(0, defense.CharacterSize);

            lvl = new Text("Lvl: ", Game.font, 20);
            lvl.Position = new Vector2(exp.Position.X, exp.Position.Y + exp.CharacterSize);

            gold = new Text("Gold: ", Game.font, 20);
            goldSprite.Position = new Vector2(lvl.Position.X - 10, lvl.Position.Y + 50);
            gold.Position = new Vector2(goldSprite.Position.X + (goldSprite.Texture.Size.X / 2), goldSprite.Position.Y - 5);

            itemDescription = new Text("", Game.font, 20);
            itemDescription.Position = (Vector2)InventoryMatrixPosition + new Vector2(5, MatrixSizeY * FIELDSIZE + 5);

            //shader++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            selectedTabShader = new Shader(null, "Shader/SelectedShader.frag");
            SelectedTabState = new RenderStates(selectedTabShader);

            selectedCloseSortShader = new Shader(null, "Shader/RedSelectedShader.frag");
            SelectedCloseSortState = new RenderStates(selectedCloseSortShader);

            selectedCursorShader = new Shader(null, "Shader/SelectedCursorShader.frag");
            SelectedCursorState = new RenderStates(selectedCursorShader);

            //cursor+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            cursor = new RectangleShape(new Vector2(FIELDSIZE - 1, FIELDSIZE - 1));
            cursor.FillColor = Color.Transparent;
            cursor.OutlineThickness = outLineThickness;
            cursor.OutlineColor = Color.Red;
            cursor.Position = InventoryMatrixPosition;
            cursorPositionInMatrix = new Vector2();

            //finalize initialization++++++++++++++++++++++++++++++++++++++++++++++
            MatrixUpdate(null, null);
        }

        //other//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// returns the sum of the attack bonus gained with the equip
        /// </summary>
        public float getAttBonus()
        {
            float res = 0;

            for (int i = 0; i < equip.Capacity; ++i)
                if (equip.At(i) != null)
                    res += ((Equipment)equip.At(i)).AttBonus;

            return res;
        }

        /// <summary>
        /// returns the sum of the defense bonus gained with the equip
        /// </summary>
        public float getDefBonus()
        {
            float res = 0;

            for (int i = 0; i < equip.Capacity; ++i)
                if (equip.At(i) != null)
                    res += ((Equipment)equip.At(i)).DefBonus;

            return res;
        }

        /// <summary>
        /// returns the sum of the hp bonus gained with the equip
        /// </summary>
        public float getHpBonus()
        {
            float res = 0;

            for (int i = 0; i < equip.Capacity; ++i)
                if (equip.At(i) != null)
                    res += ((Equipment)equip.At(i)).HpBonus;

            return res;
        }

        /// <summary>
        /// Adds the item to the matrix
        /// </summary>
        public void PickUp(AbstractItem item)
        {
            if (item.GetType().Equals(typeof(Gold)))
            {
                PlayerHandler.Player.Gold += 1;
                item.Kill();
                return;
            }

            item.OnPickUp();

            if (!inventory.IsFull)
            {
                if (inventory.Add(item) == null)
                    item.Drop(PlayerHandler.Player.Position + new Vector2(200, 200));
            }
            else
                item.Drop(PlayerHandler.Player.Position + new Vector2(200, 200));
        }

        //cursor management (no mouse)*******************************************************************

        /// <summary>
        /// updates the cursor accordingly
        /// </summary>
        private void EnterExitQuest()
        {
            if(activeTab == ETab.Quest)
            {
                cursor.Size = QuestLineSize;
            }
            else
            {
                cursor.Size = new Vector2(FIELDSIZE - 1, FIELDSIZE - 1);
                positionInQuest = 0;
            }
        }

        /// <summary>
        /// manages the cursor, should only be called when IsOpen == true
        /// </summary>
        private void CursorManagement()
        {
            switch (cursorIn)
            {
                case ECursorIn.Inventory:
                    CursorManagementInventory();
                    cursor.Position = InventoryMatrixPosition + (cursorPositionInMatrix * FIELDSIZE);
                    break;
                case ECursorIn.Equipment:
                    CursorManagementEquipment();
                    cursor.Position = EquipmentBase + EquipmentOffsets[positionInEquipment];
                    break;
                case ECursorIn.Tabs:
                    CursorManagementTabs();
                    break;
                case ECursorIn.Quests:
                    CursorManagementQuest();
                    cursor.Position = QuestBasePosition + new Vector2(0, positionInQuest) * FIELDSIZE;
                    break;
                case ECursorIn.PetEquipment:
                    CursorManagementPetEquip();
                    cursor.Position = EquipmentBase + EquipmentOffsets[positionInPetEquipment + ((positionInPetEquipment > 0) ? (2) : (0))];
                    break;
            }

            
        }

        /// <summary>
        /// manages the cursor within the inventorymatrix
        /// </summary>
        private void CursorManagementInventory()
        {
            //navigation+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            
            if (!Game.isPressed && Keyboard.IsKeyPressed(Keyboard.Key.W))
                cursorPositionInMatrix += Vector2.BACK;

            if (!Game.isPressed && Keyboard.IsKeyPressed(Keyboard.Key.A))
                cursorPositionInMatrix += Vector2.LEFT;

            if (!Game.isPressed && Keyboard.IsKeyPressed(Keyboard.Key.S))
                cursorPositionInMatrix += Vector2.FRONT;

            if (!Game.isPressed && Keyboard.IsKeyPressed(Keyboard.Key.D))
                cursorPositionInMatrix += Vector2.RIGHT;

            //activition (return is pressed)+++++++++++++++++++++++++++++++++++++++++++++

            //selection and swaping
            if (!Game.isPressed && Keyboard.IsKeyPressed(Keyboard.Key.Return) && !isSeleced)
            {
                positionSelected = cursorPositionInMatrix;
                isSeleced = true;
            }
            else if(!Game.isPressed && Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                inventory.Swap((int)positionSelected.Y * MatrixSizeX + (int)positionSelected.X, (int)cursorPositionInMatrix.Y * MatrixSizeX + (int)cursorPositionInMatrix.X);
                isSeleced = false;
            }

            //use item
            if (!Game.isPressed && Keyboard.IsKeyPressed(Keyboard.Key.U))
            {
                int i = (int)cursorPositionInMatrix.Y * MatrixSizeX + (int)cursorPositionInMatrix.X;

                if (inventory.At(i) != null)
                    inventory.At(i).OnUse(i);
            }

            //exit+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            if (cursorPositionInMatrix.Y < 0)
            {
                cursorIn = ECursorIn.Tabs;

                if (activeTab == ETab.Character)
                    selectedTab = ETab.Pet;
                else
                    selectedTab = ETab.Character;

                cursorPositionInMatrix = Vector2.ZERO;
            }

            if(cursorPositionInMatrix.X < 0)
            {
                if (activeTab == ETab.Character)
                    cursorIn = ECursorIn.Equipment;
                else
                    cursorIn = ECursorIn.PetEquipment;
            }

            //special++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            cursorPositionInMatrix %= MatrixSize;
        }

        /// <summary>
        /// manages the cursor within the equipment slots
        /// </summary>
        private void CursorManagementEquipment()
        {
            //navigation+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            if (!Game.isPressed && Keyboard.IsKeyPressed(Keyboard.Key.W))
                positionInEquipment--;

            if (!Game.isPressed && Keyboard.IsKeyPressed(Keyboard.Key.S))
                positionInEquipment++;

            //activition (return is pressed)+++++++++++++++++++++++++++++++++++++++++++++

            //unequip
            if (!Game.isPressed && Keyboard.IsKeyPressed(Keyboard.Key.Return) && equip.At(positionInEquipment) != null)
            {
                inventory.Add(equip.At(positionInEquipment));
                equip.RemoveAt(positionInEquipment);
            }

            //exit+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            if(positionInEquipment < 0)
            {
                cursorIn = ECursorIn.Tabs;

                if (activeTab == ETab.Character)
                    selectedTab = ETab.Pet;
                else
                    selectedTab = ETab.Character;

                positionInEquipment = 0;
            }

            if(!Game.isPressed && (Keyboard.IsKeyPressed(Keyboard.Key.D) || Keyboard.IsKeyPressed(Keyboard.Key.A)))
            {
                cursorIn = ECursorIn.Inventory;

                if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                    cursorPositionInMatrix.X = 0;
            }

            //special++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            positionInEquipment %= equip.Capacity;
        }

        /// <summary>
        /// manages the cursor within the tabs
        /// </summary>
        private void CursorManagementTabs()
        {
            //navigation+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            if (!Game.isPressed && Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                selectedTab = (ETab)(((int)selectedTab + 2) % (int)ETab.Count);

                if (selectedTab == activeTab)
                    selectedTab = (ETab)(((int)selectedTab + 2) % (int)ETab.Count);
            }

            if (!Game.isPressed && Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                selectedTab = (ETab)(((int)selectedTab + 1) % (int)ETab.Count);

                if(selectedTab == activeTab)
                    selectedTab = (ETab)(((int)selectedTab + 1) % (int)ETab.Count);
            }

            //activition (return is pressed)+++++++++++++++++++++++++++++++++++++++++++

            //switching to selected tab
            if (!Game.isPressed && selectedTab != ETab.None && selectedTab != activeTab && Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                activeTab = selectedTab;
                EnterExitQuest();

                switch (activeTab)
                {
                    case ETab.Character:
                        character_pet_questSprite.Texture = characterMenuTexture;
                        cursorIn = ECursorIn.Inventory;
                        break;
                    case ETab.Pet:
                        character_pet_questSprite.Texture = petMenuTexture;
                        cursorIn = ECursorIn.Inventory;
                        break;
                    case ETab.Quest:
                        character_pet_questSprite.Texture = questLogTexture;
                        cursorIn = ECursorIn.Quests;
                        break;
                }
            }

            //exit+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            if(!Game.isPressed && Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                switch (activeTab)
                {
                    case ETab.Quest:
                        cursorIn = ECursorIn.Quests;
                        break;

                    default:
                        cursorIn = ECursorIn.Inventory;
                        break;
                }

                selectedTab = ETab.None;
            }

        }
        
        /// <summary>
        /// manages the cursor within the quests
        /// </summary>
        private void CursorManagementQuest()
        {
            //navigation+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            if (!Game.isPressed && Keyboard.IsKeyPressed(Keyboard.Key.W))
                positionInQuest--;

            if (!Game.isPressed && Keyboard.IsKeyPressed(Keyboard.Key.S))
                positionInQuest++;


            //activition (return is pressed)+++++++++++++++++++++++++++++++++++++++++++++



            //exit+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            if(positionInQuest < 0)
            {
                cursorIn = ECursorIn.Tabs;
                selectedTab = ETab.Character;
                positionInQuest = 0;
            }

            if (positionInQuest >= maxQuestOnOnePage)
                positionInQuest = maxQuestOnOnePage - 1;
        }

        private void CursorManagementPetEquip()
        {
            //navigation+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            if (!Game.isPressed && Keyboard.IsKeyPressed(Keyboard.Key.W))
                    positionInPetEquipment--;

            if (!Game.isPressed && Keyboard.IsKeyPressed(Keyboard.Key.S))
                    positionInPetEquipment++;

            //activition (return is pressed)+++++++++++++++++++++++++++++++++++++++++++++

            //unequip
            if (!Game.isPressed && Keyboard.IsKeyPressed(Keyboard.Key.Return) && petEquip.At(positionInPetEquipment) != null)
            {
                inventory.Add(petEquip.At(positionInPetEquipment));
                petEquip.RemoveAt(positionInPetEquipment);
            }

            //exit+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            if (positionInPetEquipment < 0)
            {
                cursorIn = ECursorIn.Tabs;

                if (activeTab == ETab.Character)
                    selectedTab = ETab.Pet;
                else
                    selectedTab = ETab.Character;

                positionInPetEquipment = 0;
            }

            if (!Game.isPressed && (Keyboard.IsKeyPressed(Keyboard.Key.D) || Keyboard.IsKeyPressed(Keyboard.Key.A)))
            {
                cursorIn = ECursorIn.Inventory;

                if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                    cursorPositionInMatrix.X = 0;
            }

            //special++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            positionInPetEquipment %= petEquip.Capacity;
        }

        //Mouse management*******************************************************************************

        private void MouseManagement()
        {
            //Tabs (doesn't care wich tab is active++++++++++++++++++++++++++++++++++++++++
            if (MouseControler.MouseIn(petTab))
            {
                cursorIn = ECursorIn.Tabs;
                selectedTab = ETab.Pet;
            }

            if (MouseControler.MouseIn(questTab))
            {
                cursorIn = ECursorIn.Tabs;
                selectedTab = ETab.Quest;
            }

            if (MouseControler.MouseIn(characterTab))
            {
                cursorIn = ECursorIn.Tabs;
                selectedTab = ETab.Character;
            }

            switch (activeTab)
            {
                case ETab.Character:
                    MouseManagementCharacter();
                    break;
                case ETab.Pet:
                    MouseManagementPet();
                    break;
                case ETab.Quest:
                    MouseManagementQuest();
                    break;
            }
        }

        private void MouseManagementCharacter()
        {
            //inventar+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            if (MouseControler.MouseIn(new Rectangle(InventoryMatrixPosition, MatrixSize * FIELDSIZE)))
            {
                Vector2 v = MouseControler.PositionIn(new Rectangle(InventoryMatrixPosition, MatrixSize * FIELDSIZE));

                cursorIn = ECursorIn.Inventory;
                selectedTab = ETab.None;
                cursorPositionInMatrix = new Vector2(Math.Min((int)v.X / (int)FIELDSIZE, MatrixSizeX - 1), Math.Min((int)v.Y / (int)FIELDSIZE, MatrixSizeY - 1));
            }

            //Equip+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            for (int i = 0; i < EquipmentOffsets.Length; ++i)
                if (MouseControler.MouseIn(new Rectangle(EquipmentBase + EquipmentOffsets[i], new Vector2(FIELDSIZE, FIELDSIZE))))
                {
                    cursorIn = ECursorIn.Equipment;
                    selectedTab = ETab.None;
                    positionInEquipment = i;
                }
        }

        private void MouseManagementPet()
        {
            //inventar+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            if (MouseControler.MouseIn(new Rectangle(InventoryMatrixPosition, MatrixSize * FIELDSIZE)))
            {
                Vector2 v = MouseControler.PositionIn(new Rectangle(InventoryMatrixPosition, MatrixSize * FIELDSIZE));

                cursorIn = ECursorIn.Inventory;
                selectedTab = ETab.None;
                cursorPositionInMatrix = new Vector2(Math.Min((int)v.X / (int)FIELDSIZE, MatrixSizeX - 1), Math.Min((int)v.Y / (int)FIELDSIZE, MatrixSizeY - 1));
            }

            //Equip+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            for (int i = 0; i < petEquip.Capacity; ++i)
                if (MouseControler.MouseIn(new Rectangle(EquipmentBase + EquipmentOffsets[i + ((i > 0)?(2):(0))], new Vector2(FIELDSIZE, FIELDSIZE))))
                {
                    cursorIn = ECursorIn.PetEquipment;
                    selectedTab = ETab.None;
                    positionInPetEquipment = i;
                }
        }

        private void MouseManagementQuest()
        {

        }

        /// <summary>
        /// called when the left mouseButton is pressed
        /// </summary>
        /// <param name="mousePosition">the position of the mouse relative to the window</param>
        private void LeftMouseButtonPressed(Vector2 mousePosition)
        {
            if (activeTab != ETab.Quest && sortSelected)
                inventory.Sort(new SortByID());

            if (closeButtonSelected)
                OnOpen_Close();

            //inventar+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            if (MouseControler.MouseIn(new Rectangle(InventoryMatrixPosition, MatrixSize * FIELDSIZE)))
            {
                itemIsFollowingMouse = true;
                followingItem = inventory.At((int)cursorPositionInMatrix.Y * MatrixSizeX + (int)cursorPositionInMatrix.X);
                positionSelected = cursorPositionInMatrix;
            }

            //equipment++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //the bounding rectangle of the equipment slots a detailed test is not realy needed, so this is faster and easier to read
            Rectangle boundingRectangle = new Rectangle(EquipmentBase, EquipmentBase + EquipmentOffsets[5] + FIELDSIZE);

            if (cursorIn == ECursorIn.Equipment && MouseControler.MouseIn(boundingRectangle))
            {
                itemIsFollowingMouse = true;
                followingItem = equip.At(positionInEquipment);
                equip.RemoveAt(positionInEquipment);
                int? position = inventory.Add(followingItem);

                if(position != null)
                {
                    int _position = (int)position;
                    positionSelected = new Vector2(_position % MatrixSizeX, _position / MatrixSizeX);
                }
                //else
            }

            //pet
            if (cursorIn == ECursorIn.PetEquipment && MouseControler.MouseIn(boundingRectangle))
            {
                itemIsFollowingMouse = true;
                followingItem = petEquip.At(positionInPetEquipment);
                petEquip.RemoveAt(positionInPetEquipment);
                int? position = inventory.Add(followingItem);

                if (position != null)
                {
                    int _position = (int)position;
                    positionSelected = new Vector2(_position % MatrixSizeX, _position / MatrixSizeX);
                }
                //else
            }

            //Tabs+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            if (cursorIn == ECursorIn.Tabs && selectedTab != ETab.None)
            {
                activeTab = selectedTab;
                EnterExitQuest();

                switch (activeTab)
                {
                    case ETab.Character:
                        character_pet_questSprite.Texture = characterMenuTexture;
                        cursorIn = ECursorIn.Inventory;
                        break;
                    case ETab.Pet:
                        character_pet_questSprite.Texture = petMenuTexture;
                        cursorIn = ECursorIn.Inventory;
                        break;
                    case ETab.Quest:
                        character_pet_questSprite.Texture = questLogTexture;
                        cursorIn = ECursorIn.Quests;
                        break;
                }
            }

            
        }

        private void RightMouseButtonPressed(Vector2 mousePosition)
        {
            int index = (int)cursorPositionInMatrix.Y * MatrixSizeX + (int)cursorPositionInMatrix.X;

            if (cursorIn == ECursorIn.Inventory && inventory.At(index) != null)
            {
                inventory.At(index).OnUse(index);
            }

            //unequip equipment & petEquipment
            if (cursorIn == ECursorIn.Equipment && equip.At(positionInEquipment) != null)
            {
                inventory.Add(equip.At(positionInEquipment));
                equip.RemoveAt(positionInEquipment);
            }

            if (cursorIn == ECursorIn.PetEquipment && petEquip.At(positionInPetEquipment) != null)
            {
                inventory.Add(petEquip.At(positionInPetEquipment));
                petEquip.RemoveAt(positionInPetEquipment);
            }
        }

        /// <summary>
        /// is called when the left mouse button is pressed
        /// </summary>
        /// <param name="mousePosition">the position of the mouse relative to the window</param>
        private void LeftMouseButtonRelized(Vector2 mousePosition)
        {
            Vector2 directionToMouseFromMatrix = mousePosition - InventoryMatrixPosition;

            if (itemIsFollowingMouse)
            {
                itemIsFollowingMouse = false;

                //if in the matrix
                if (MouseControler.MouseIn(new Rectangle(InventoryMatrixPosition, MatrixSize * FIELDSIZE)))
                    inventory.Swap((int)positionSelected.Y * MatrixSizeX + (int)positionSelected.X, (int)cursorPositionInMatrix.Y * MatrixSizeX + (int)cursorPositionInMatrix.X);
                else
                    inventory.Swap((int)positionSelected.Y * MatrixSizeX + (int)positionSelected.X, (int)positionSelected.Y * MatrixSizeX + (int)positionSelected.X);

                if(cursorIn == ECursorIn.Equipment)
                {
                    try
                    {
                        Equipment eq = (Equipment)followingItem;

                        if(positionInEquipment == (int)eq.EquipmentType)
                        {
                            eq.OnUse((int)positionSelected.Y * MatrixSizeX + (int)positionSelected.X);
                        }
                    }
                    catch (InvalidCastException) { }
                }

                followingItem = null;
            }
        }



        //update*****************************************************************************************

        /// <summary>
        /// updates the texts
        /// </summary>
        private void TextUpdate()
        {
            attack.DisplayedString = "Attack: " + PlayerHandler.Player.Att;

            defense.DisplayedString = "Defense: " + PlayerHandler.Player.Def;

            exp.DisplayedString = "EXP: " + PlayerHandler.Player.CurrentEXP + "/" + PlayerHandler.Player.MaxEXP;

            lvl.DisplayedString = "Lvl: " + PlayerHandler.Player.Lvl;
            if (PlayerHandler.Player.Lvl == PlayerHandler.Player.MaxLvl)
                lvl.DisplayedString += "°";

            gold.DisplayedString = "Gold: " + PlayerHandler.Player.Gold;
            
            if(cursorIn == ECursorIn.Inventory)
            {
                if (inventory.At((int)cursorPositionInMatrix.Y * MatrixSizeX + (int)cursorPositionInMatrix.X) != null)
                    itemDescription.DisplayedString = inventory.At((int)cursorPositionInMatrix.Y * MatrixSizeX + (int)cursorPositionInMatrix.X).ItemDiscription;
                else
                    itemDescription.DisplayedString = "";
            }

        }

        /// <summary>
        /// updates the list and calls the navigation methods
        /// </summary>
        public void Update(GameTime gameTime)
        { 
            if (Keyboard.IsKeyPressed(Controls.OpenInventar) && !Game.isPressed)
            {
                OnOpen_Close();
                Game.isPressed = true;
            }
            if (IsOpen)
            {
                CursorManagement();
                MouseManagement();
                if (activeTab != ETab.Quest)
                    TextUpdate();
                inventory.Update(gameTime);
                equip.Update(gameTime);
                petEquip.Update(gameTime);
                try
                {
                    if (itemIsFollowingMouse)
                        followingItem.Position = new Vector2(Mouse.GetPosition(Game.window).X, Mouse.GetPosition(Game.window).Y);

                }
                catch (NullReferenceException)
                {

                }
            }
        }

        //draw************************************************************************************

        /// <summary>
        /// draws all sprites of this
        /// </summary>
        /// <param name="win"></param>
        public void Draw(RenderWindow win)
        {
            if (IsOpen)
            {
                //background
                win.Draw(character_pet_questSprite);

                //character tab
                if (selectedTab == ETab.Character && activeTab != selectedTab)
                    win.Draw(characterTab, SelectedTabState);
                else
                    win.Draw(characterTab);

                //pet tab
                if (selectedTab == ETab.Pet && activeTab != selectedTab)
                    win.Draw(petTab, SelectedTabState);
                else
                    win.Draw(petTab);

                //quest tab
                if (selectedTab == ETab.Quest && activeTab != selectedTab)
                    win.Draw(questTab, SelectedTabState);
                else
                    win.Draw(questTab);

                //close
                if (closeButtonSelected)
                    win.Draw(closeButton, SelectedCloseSortState);
                else
                    win.Draw(closeButton);

                //everything that must not be drawn if the quest tab is active
                if (activeTab != ETab.Quest)
                {
                    //sort
                    if (sortSelected)
                        win.Draw(sort, SelectedCloseSortState);
                    else
                        win.Draw(sort);

                    //the player
                    if (activeTab == ETab.Character)
                        win.Draw(displayedPlayer);

                    //the little gold icon
                    win.Draw(goldSprite);

                    //the texts
                    win.Draw(gold);
                    win.Draw(attack);
                    win.Draw(defense);
                    win.Draw(exp);
                    win.Draw(lvl);
                    win.Draw(itemDescription);

                    //the inventory items
                    inventory.Draw(win);

                    //cursor
                    if (cursorIn == ECursorIn.Inventory || cursorIn == ECursorIn.Equipment || cursorIn == ECursorIn.PetEquipment)
                    {
                        win.Draw(cursor);
                    }

                    //the selected indicator
                    if (isSeleced)
                    {
                        cursor.Position = InventoryMatrixPosition + positionSelected * FIELDSIZE;
                        win.Draw(cursor, SelectedCursorState);
                    }

                    if (activeTab == ETab.Character)
                        equip.Draw(win);
                }

                if(cursorIn == ECursorIn.Quests)
                {
                    win.Draw(cursor);
                }
            }
        }
    }
}
