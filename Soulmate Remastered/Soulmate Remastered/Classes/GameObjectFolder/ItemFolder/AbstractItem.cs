using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.ItemFolder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder
{
    abstract class AbstractItem : GameObject, IComparable<AbstractItem>
    {
        public override String Type { get { return base.Type + ".Item"; } }
        public virtual float ID { get { return 1; } }
        public virtual bool Stackable { get { return true; } }
        public override bool Walkable { get { return true; } }
        public virtual bool sellable { get { return true; } }
        public virtual float SellPrize { get { return 0; } }
        protected float dropRate; // in percent
        public float DROPRATE { get { return dropRate; } }
        public bool onMap { get; set; }
        protected bool wasOnMap = false; //not longer on map but it was droped once
        public float pickUpRange { get { return 50f; } }
        protected Stopwatch decay = new Stopwatch();
        protected int decayingIn = 60000; //60sec
        protected float recoveryValue;
        protected int inventoryMatrixPositionX;
        protected int inventoryMatrixPositionY;

        public virtual String ItemDiscription 
        { 
            get 
            {
                String itemDiscription = "";

                return itemDiscription;
            } 
        }

        public virtual String ToStringForSave()
        {
            String itemForSave = "it" + LineBreak.ToString();

            itemForSave += Type.Split('.')[Type.Split('.').Length-1] + LineBreak.ToString();
            itemForSave += Position.X + LineBreak.ToString();
            itemForSave += Position.Y + LineBreak.ToString();

            return itemForSave;
        }

        public abstract AbstractItem clone();

        public void setPositionMatrix(int x, int y)
        {
            Position = new Vector2f(x * ItemHandler.playerInventory.FIELDSIZE + 1 + ItemHandler.playerInventory.inventoryMatrixPosition.X,
                y * ItemHandler.playerInventory.FIELDSIZE + 1 + ItemHandler.playerInventory.inventoryMatrixPosition.Y);
        }

        public void giveMatrixPosition(int i, int j)
        {
            inventoryMatrixPositionX = j;
            inventoryMatrixPositionY = i;
        }

        public virtual void pickUp()
        {
            if (Type.Equals("Object.Item.Gold"))
            {
                PlayerHandler.Player.Gold += 1;
                return;
            }
            for (int i = 0; i < ItemHandler.playerInventory.inventoryMatrix.GetLength(0); i++) //row -> x-coordinate
            {
                for (int j = 0; j < ItemHandler.playerInventory.inventoryMatrix.GetLength(1); j++) //collum -> y-coordinate
                {
                    if (ItemHandler.playerInventory.inventoryMatrix[i,j] == null)
                    {
                        ItemHandler.playerInventory.inventoryMatrix[i, j] = new Stack<AbstractItem>();
                        ItemHandler.playerInventory.inventoryMatrix[i, j].Push(this);
                        Position = new Vector2f((j * ItemHandler.playerInventory.FIELDSIZE + 1 + ItemHandler.playerInventory.inventoryMatrixPosition.X),
                                                (i * ItemHandler.playerInventory.FIELDSIZE + 1 + ItemHandler.playerInventory.inventoryMatrixPosition.Y));
                        onMap = false;
                        GameObjectHandler.removeAt(IndexObjectList);
                        return;

                    }
                    if (ItemHandler.playerInventory.inventoryMatrix[i, j].Count < Inventory.MaxStackCount &&
                       (ItemHandler.playerInventory.inventoryMatrix[i,j].Count == 0 || ItemHandler.playerInventory.inventoryMatrix[i, j].Peek() == null || ItemHandler.playerInventory.inventoryMatrix[i, j].Peek().CompareTo(this) == 0))
                    {
                        if (ItemHandler.playerInventory.inventoryMatrix[i,j].Count != 0 && ItemHandler.playerInventory.inventoryMatrix[i, j].Peek() != null)
                        {
                            ItemHandler.playerInventory.inventoryMatrix[i, j].Peek().setVisible(false);
                        }
                        ItemHandler.playerInventory.inventoryMatrix[i, j].Push(this);
                        Position = new Vector2f((j * ItemHandler.playerInventory.FIELDSIZE + 1 + ItemHandler.playerInventory.inventoryMatrixPosition.X),
                            (i * ItemHandler.playerInventory.FIELDSIZE + 1 + ItemHandler.playerInventory.inventoryMatrixPosition.Y));
                        onMap = false;
                        GameObjectHandler.removeAt(IndexObjectList);
                        return;
                    }
                }
            }
        }

        public virtual void Use() { } 

        public void drop(Vector2f dropPosition)
        {
            Position = dropPosition;
            Sprite.Position = Position;
            onMap = true;
        }

        public void setVisible(bool _visible)
        {
            IsVisible = _visible;
        }

        public void cloneAndDrop(Vector2f dropPosition)
        {
            AbstractItem dropedItem = this.clone();
            ItemHandler.add(dropedItem);
            dropedItem.drop(dropPosition);
        }

        public override void Update(GameTime gameTime)
        {
            HitBox.Position = Position;
            Sprite.Position = Position;
            if (onMap)
            {
                IsVisible = true;
                wasOnMap = true;
                decay.Start();
                if (decay.ElapsedMilliseconds >= decayingIn)
                {
                    IsAlive = false;
                }
            }
            if (wasOnMap && !onMap)
            {
                wasOnMap = false;
                onMap = false;
                decay.Reset();
            }
            if (!onMap)
            {
                IsVisible = false;
            }
        }

        public int CompareTo(AbstractItem other)
        {
            if (Stackable)
            {
                return (int)(ID - other.ID);
            }
            else
            {
                return -1;
            }
        }
    }
}
