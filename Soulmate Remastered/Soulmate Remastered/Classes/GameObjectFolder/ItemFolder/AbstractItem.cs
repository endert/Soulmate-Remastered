using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.ItemFolder;
using Soulmate_Remastered.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder
{
    /// <summary>
    /// base classs for all items
    /// </summary>
    abstract class AbstractItem : GameObject, IComparable<AbstractItem>
    {
        /// <summary>
        /// the type of this instance
        /// </summary>
        public override String Type { get { return base.Type + ".Item"; } }
        /// <summary>
        /// the ID = 1x
        /// </summary>
        public virtual float ID { get { return 1; } }
        /// <summary>
        /// bool if it is stackable or not, default true
        /// </summary>
        public virtual bool Stackable { get { return true; } }
        /// <summary>
        /// bool if it triggers a collision or not, items do not trigger collisions
        /// </summary>
        public override bool Walkable { get { return true; } }
        /// <summary>
        /// bool if this item can be sold by a merchant
        /// </summary>
        public virtual bool Sellable { get { return true; } }
        /// <summary>
        /// the prize this item costs
        /// </summary>
        public virtual float SellPrize { get { return 0; } }
        /// <summary>
        /// drop rate of this in percent
        /// </summary>
        public float DropRate { get; protected set; }
        /// <summary>
        /// bool if this item is at the moment on the map or is hold by an entity
        /// </summary>
        public bool OnMap { get; set; }
        /// <summary>
        /// the distance this item can be picked up by the player, default = 50f
        /// </summary>
        public float PickUpRange { get { return 50f; } }

        /// <summary>
        /// bool if it was once on the map
        /// </summary>
        protected bool wasOnMap = false;
        /// <summary>
        /// stopwatch for the decaying of this
        /// </summary>
        protected Stopwatch decay = new Stopwatch();
        /// <summary>
        /// the time in millisec this can lay on the map before it decays, default = 60 sec
        /// </summary>
        protected int decayingIn = 60000;
        /// <summary>
        /// position in the inventory matrix
        /// </summary>
        protected Vector2 InventoryMatrixPosition { get; set; }
        /// <summary>
        /// the description of this item
        /// </summary>
        public virtual string ItemDiscription 
        { 
            get 
            {
                string itemDiscription = "";

                return itemDiscription;
            } 
        }

        /// <summary>
        /// saves this item, by creating a string out of this attributes
        /// </summary>
        /// <returns></returns>
        public virtual string ToStringForSave()
        {
            string itemForSave = "it" + LineBreak.ToString();

            itemForSave += Type.Split('.')[Type.Split('.').Length-1] + LineBreak.ToString();
            itemForSave += Position.X + LineBreak.ToString();
            itemForSave += Position.Y + LineBreak.ToString();

            return itemForSave;
        }

        /// <summary>
        /// clones this item
        /// </summary>
        /// <returns></returns>
        public abstract AbstractItem Clone();

        /// <summary>
        /// sets the item position according to the matrix position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPosition(int x, int y)
        {
            Position = new Vector2(x * ItemHandler.playerInventory.FIELDSIZE + 1 + ItemHandler.playerInventory.inventoryMatrixPosition.X,
                y * ItemHandler.playerInventory.FIELDSIZE + 1 + ItemHandler.playerInventory.inventoryMatrixPosition.Y);
        }

        /// <summary>
        /// set the matrix position
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public void SetMatrixPosition(int i, int j)
        {
            InventoryMatrixPosition = new Vector2(j, i);
        }

        /// <summary>
        /// method triggered by the pick up event
        /// </summary>
        public virtual void PickUp()
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
                        OnMap = false;
                        GameObjectHandler.removeAt(IndexObjectList);
                        return;

                    }
                    if (ItemHandler.playerInventory.inventoryMatrix[i, j].Count < Inventory.MaxStackCount &&
                       (ItemHandler.playerInventory.inventoryMatrix[i,j].Count == 0 || ItemHandler.playerInventory.inventoryMatrix[i, j].Peek() == null || ItemHandler.playerInventory.inventoryMatrix[i, j].Peek().CompareTo(this) == 0))
                    {
                        if (ItemHandler.playerInventory.inventoryMatrix[i,j].Count != 0 && ItemHandler.playerInventory.inventoryMatrix[i, j].Peek() != null)
                        {
                            ItemHandler.playerInventory.inventoryMatrix[i, j].Peek().SetVisible(false);
                        }
                        ItemHandler.playerInventory.inventoryMatrix[i, j].Push(this);
                        Position = new Vector2((j * ItemHandler.playerInventory.FIELDSIZE + 1 + ItemHandler.playerInventory.inventoryMatrixPosition.X),
                            (i * ItemHandler.playerInventory.FIELDSIZE + 1 + ItemHandler.playerInventory.inventoryMatrixPosition.Y));
                        OnMap = false;
                        GameObjectHandler.removeAt(IndexObjectList);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// use this item
        /// </summary>
        public virtual void Use() { } 

        /// <summary>
        /// drop this item
        /// </summary>
        /// <param name="dropPosition"></param>
        public void Drop(Vector2 dropPosition)
        {
            Position = dropPosition;
            Sprite.Position = Position;
            OnMap = true;
        }

        /// <summary>
        /// set visibility
        /// </summary>
        /// <param name="_visible"></param>
        public void SetVisible(bool _visible)
        {
            IsVisible = _visible;
        }

        /// <summary>
        /// clones this item and drop the clone
        /// </summary>
        /// <param name="dropPosition"></param>
        public void CloneAndDrop(Vector2 dropPosition)
        {
            AbstractItem dropedItem = this.Clone();
            ItemHandler.add(dropedItem);
            dropedItem.Drop(dropPosition);
        }

        public override void Update(GameTime gameTime)
        {
            HitBox.Position = Position;
            Sprite.Position = Position;
            if (OnMap)
            {
                IsVisible = true;
                wasOnMap = true;
                decay.Start();
                if (decay.ElapsedMilliseconds >= decayingIn)
                {
                    IsAlive = false;
                }
            }
            if (wasOnMap && !OnMap) //picked up
            {
                wasOnMap = false;
                OnMap = false;
                decay.Reset();
            }
            if (!OnMap)
            {
                IsVisible = false;
            }
        }

        /// <summary>
        /// compares this item with another item
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
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
