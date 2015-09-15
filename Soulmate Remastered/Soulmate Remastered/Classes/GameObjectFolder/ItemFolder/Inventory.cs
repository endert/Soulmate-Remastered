using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soulmate_Remastered.Core;
using SFML.Graphics;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;

namespace Soulmate_Remastered.Classes.ItemFolder
{
    /// <summary>
    /// base class for all inventorys, only contains the items
    /// </summary>
    class Inventory
    {
        //events********************************************************************

        /// <summary>
        /// event that is fired when the array changes
        /// </summary>
        public event EventHandler Changed;

        /// <summary>
        /// triggers the changed event
        /// </summary>
        private void OnChange()
        {
            EventHandler handler = Changed;
            if (handler != null)
                handler(this, null);
        }
        
        //**************************************************************************

        static char LineBreak { get { return ':'; } }

        ItemStack[] items;
        /// <summary>
        /// the number of items in this inventory
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// bool if the inventory is full
        /// </summary>
        public bool IsFull
        {
            get
            {
                if (Count >= items.Length)
                {
                    foreach (ItemStack i in items)
                        if (!i.IsFull)
                        {
                            return false;
                        }

                    return  true;
                }
                return false;
            }
        }
        /// <summary>
        /// the capacity of the inventory (how many stacks can be stored)
        /// </summary>
        public int Capacity { get { return items.Length; } }

        //Save/ Load***********************************************************************
        public string ToStringForSave()
        {
            string res = "";

            res += Capacity + LineBreak.ToString();

            foreach (ItemStack stack in items)
            {
                if (stack != null)
                    res += stack.ToStringForSave() + LineBreak.ToString();
                else
                    res += " " + LineBreak;
            }

            return res;
        }

        public void Load(string loadFrom)
        {
            string[] array = loadFrom.Split(LineBreak);

            items = new ItemStack[Convert.ToInt32(array[0])];

            for(int i = 1; i< array.Length;++i)
            {
                if(array[i] != " " && array[i] != "")
                {
                    items[i] = new ItemStack();
                    items[i].Load(array[i]);
                }
            }
        }

        //methodes*************************************************************************
        public Inventory() : this(1) { }

        public Inventory(int capacity)
        {
            Count = 0;
            items = new ItemStack[capacity];
        }

        /// <summary>
        /// returns the item at the given index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public AbstractItem At(int index)
        {
            if (items[index] != null)
                return items[index].Peek();

            return null;
        }

        /// <summary>
        /// adds the item
        /// </summary>
        /// <param name="item">added item</param>
        /// <returns>the index where it was inserted (needed for equip) null if couldnt be added</returns>
        public int? Add(AbstractItem item)
        {
            if (item == null)
                return null;

            int? index = null;

            if (!IsFull)
            {
                bool startedNewStack = false;

                for (int i = 0; i < items.Length; ++i)
                {
                    if (items[i] != null && !items[i].IsFull && items[i].Peek().GetType().Equals(item.GetType()))
                    {
                        items[i].Push(item);
                        index = i;
                        break;
                    }
                    else if (items[i] == null)
                    {
                        items[i] = new ItemStack(item);
                        startedNewStack = true;
                        index = i;
                        break;
                    }
                }

                if (startedNewStack)
                {
                    OnChange();

                    if (item != null)
                        Count++;
                }
            }

            return index;
        }

        /// <summary>
        /// removes the item at the given index
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            if (index >= 0 && index < items.Length && items[index] != null)
            {
                items[index].Pop();

                if (items[index].Count <= 0)
                {
                    items[index] = null;
                    Count--;
                    OnChange();
                }
            }
        }

        /// <summary>
        /// swaps the 2 items
        /// </summary>
        /// <param name="index1">index of the first item</param>
        /// <param name="index2">index of the second item</param>
        public void Swap(int index1, int index2)
        {
            ItemStack item1 = items[index1];
            items[index1] = items[index2];
            items[index2] = item1;

            OnChange();
        }

        /// <summary>
        /// sorts the inventory according the comparer
        /// </summary>
        /// <param name="comparer"></param>
        public void Sort(Comparer<ItemStack> comparer)
        {
            List<ItemStack> Litems = new List<ItemStack>();

            for (int i = 0; i < items.Length; ++i)
                if (items[i] != null)
                {
                    Litems.Add(items[i]);
                    items[i] = null;
                }


            Litems.Sort(comparer);

            for (int i = 0; i < Litems.Count; ++i)
                items[i] = Litems[i];

            OnChange();
        }

        /// <summary>
        /// updates all items and the list
        /// </summary>
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < items.Length; ++i)
                if (items[i] != null)
                {
                    items[i].Update(gameTime);
                    if (items[i].Count <= 0)
                        items[i] = null;
                }
        }

        public void Draw(RenderWindow win)
        {
            foreach (ItemStack item in items)
                if (item != null)
                    item.Draw(win);
        }
    }
}
