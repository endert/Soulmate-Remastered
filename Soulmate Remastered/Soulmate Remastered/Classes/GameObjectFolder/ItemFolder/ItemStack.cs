using SFML.Graphics;
using Soulmate_Remastered.Core;
using System;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder
{
    class ItemStack
    {
        /// <summary>
        /// bool if the stack is full
        /// </summary>
        public bool IsFull { get { return Count >= stack.Length; } }

        static char LineBreak { get { return ','; } }

        /// <summary>
        /// the "stack"
        /// </summary>
        AbstractItem[] stack;

        /// <summary>
        /// the number of items in this "stack"
        /// </summary>
        public int Count { get; private set; }

        //Save/ Load***********************************************************************
        public string ToStringForSave()
        {
            return Count + LineBreak.ToString() + stack[0].ToStringForSave();
        }

        public void Load(string loadFrom)
        {
            string[] array = loadFrom.Split(LineBreak);

            Count = Convert.ToInt32(array[0]);

            string[] itemArray = array[1].Split(AbstractItem.LineBreak);

            AbstractItem item = (AbstractItem)Activator.CreateInstance(null, itemArray[0]).Unwrap();

            item.Load(array[1]);

            stack = new AbstractItem[item.MaxStackCount];
            
            for(int i = 0; i< Count; ++i)
            {
                stack[i] = item.Clone();
            }
            
            countText = new Text("", Game.font, 15);
            countText.Color = Color.Black;
            countText.Style = Text.Styles.Bold;
        }

        //methodes*************************************************************************
        public ItemStack() { stack = new AbstractItem[1]; }

        public ItemStack(AbstractItem item)
        {
            stack = new AbstractItem[item.MaxStackCount];
            stack[0] = item;
            Count = 1;
            countText = new Text("", Game.font, 15);
            countText.Color = Color.Black;
            countText.Style = Text.Styles.Bold;
        }

        Text countText;

        /// <summary>
        /// adds a item
        /// </summary>
        /// <param name="item"></param>
        public void Push(AbstractItem item)
        {
            stack[Count] = item;
            Count++;
        }

        /// <summary>
        /// returns the last added item
        /// </summary>
        /// <returns></returns>
        public AbstractItem Peek()
        {
            return stack[Count - 1];
        }

        /// <summary>
        /// returns the last added item and removes it
        /// </summary>
        /// <returns></returns>
        public AbstractItem Pop()
        {
            AbstractItem res = stack[Count - 1];
            stack[--Count] = null;
            if (Count > 0)
                stack[Count - 1].Position = res.Position;

            return res;
        }

        /// <summary>
        /// updates the stack
        /// </summary>
        public void Update(GameTime gameTime)
        {
            stack[Count - 1].Update(gameTime);

            if (Count > 1)
                countText.DisplayedString = Count.ToString();
            else
                countText.DisplayedString = "";

            countText.Position = (Vector2)stack[Count - 1].Position + (PlayerInventory.FIELDSIZE - 15);

            if (!stack[Count - 1].IsAlive)
                Pop();
        }

        /// <summary>
        /// draws the last added item plus the stack count
        /// </summary>
        /// <param name="win"></param>
        public void Draw(RenderWindow win)
        {
            stack[Count - 1].Draw(win);
            win.Draw(countText);
        }
    }
}
