using SFML.Graphics;
using Soulmate_Remastered.Core;
using System;
using System.Diagnostics;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder
{
    /// <summary>
    /// base classs for all items
    /// </summary>
    abstract class AbstractItem : GameObject
    {
        //Events*************************************************************************************

        /// <summary>
        /// the event that is triggered when this item is used
        /// </summary>
        public event EventHandler<UseEventArgs> UseEvent;

        /// <summary>
        /// triggers the use event
        /// </summary>
        /// <param name="index">the index in the inventory</param>
        public void OnUse(int index)
        {
            EventHandler<UseEventArgs> handler = UseEvent;
            if (handler != null)
                handler(this, new UseEventArgs(index));
        }

        //Constants**********************************************************************************

        /// <summary>
        /// the max count of a stack of this
        /// </summary>
        public virtual int MaxStackCount { get { return 99; } }
        /// <summary>
        /// the ID = 1x
        /// </summary>
        public virtual float ID { get { return 1; } }
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
        /// the distance this item can be picked up by the player, default = 50f
        /// </summary>
        public float PickUpRange { get { return 50f; } }
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

        //Properties*********************************************************************************

        /// <summary>
        /// drop rate of this in percent
        /// </summary>
        public float DropRate { get; protected set; }
        /// <summary>
        /// bool if this item is at the moment on the map or is hold by an entity
        /// </summary>
        public bool OnMap { get; set; }


        //Attributes*********************************************************************************

        /// <summary>
        /// bool if it was once on the map
        /// </summary>
        protected bool wasOnMap = false;
        /// <summary>
        /// bool if this was in the player Inventory
        /// </summary>
        bool wasInPlayerInventory = false;
        /// <summary>
        /// stopwatch for the decaying of this
        /// </summary>
        protected Stopwatch decay = new Stopwatch();
        /// <summary>
        /// the time in millisec this can lay on the map before it decays, default = 60 sec
        /// </summary>
        protected int decayingIn = 60000;


        //Methodes***********************************************************************************

        /// <summary>
        /// Abstract constructors are allways called
        /// </summary>
        public AbstractItem()
        {
            UseEvent += Use;
            OnMap = false;
            DropRate = 0;
            IsVisible = false;
        }

        /// <summary>
        /// saves this item, by creating a string out of this attributes
        /// </summary>
        /// <returns></returns>
        public virtual string ToStringForSave()
        {
            string itemForSave = "";

            itemForSave += GetType().FullName + LineBreak.ToString();
            itemForSave += Position.X + LineBreak.ToString();
            itemForSave += Position.Y + LineBreak.ToString();

            return itemForSave;
        }

        /// <summary>
        /// Load this from a string
        /// </summary>
        /// <param name="loadFrom"></param>
        public virtual void Load(string loadFrom)
        {
            string[] array = loadFrom.Split(LineBreak);

            Position = new Vector2(Convert.ToSingle(array[1]), Convert.ToSingle(array[2]));
        }

        /// <summary>
        /// clones this item
        /// </summary>
        /// <returns></returns>
        public abstract AbstractItem Clone();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        protected virtual void Use(object sender, UseEventArgs eventArgs)
        {
            IsAlive = false;
        }

        /// <summary>
        /// Update the attributes when it is picked up
        /// </summary>
        public void OnPickUp()
        {
            IsVisible = false;
            OnMap = false;
            wasInPlayerInventory = true;
            ItemHandler.ṔlayerInventory.Open_CloseInventory += SetVisible;
        }

        /// <summary>
        /// methode that sets the visibility when the inventory is Opened/ Closed
        /// </summary>
        void SetVisible(object sender, EventArgs e)
        {
            IsVisible = PlayerInventory.IsOpen;
        }

        /// <summary>
        /// drop this item
        /// </summary>
        /// <param name="dropPosition"></param>
        public void Drop(Vector2 dropPosition)
        {
            if (wasInPlayerInventory)
            {
                ItemHandler.ṔlayerInventory.Open_CloseInventory -= SetVisible;
            }

            Position = dropPosition;
            Sprite.Position = Position;
            OnMap = true;
        }

        /// <summary>
        /// clones this item and drop the clone
        /// </summary>
        /// <param name="dropPosition"></param>
        public void CloneAndDrop(Vector2 dropPosition)
        {
            AbstractItem dropedItem = this.Clone();
            ItemHandler.Add(dropedItem);
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
    }
}
