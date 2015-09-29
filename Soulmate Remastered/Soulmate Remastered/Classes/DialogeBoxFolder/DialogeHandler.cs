using SFML.Graphics;
using System.Collections.Generic;

namespace Soulmate_Remastered.Classes.DialogeBoxFolder
{
    class DialogeHandler
    {
        /// <summary>
        /// the List of all open dialoge boxes
        /// </summary>
        public static List<DialogeBox> DialogeList { get; set; }

        /// <summary>
        /// initialize a dialoge handler, should only be called once
        /// </summary>
        public DialogeHandler()
        {
            DialogeList = new List<DialogeBox>();
        }

        public static void Clear()
        {
            for(int i = DialogeList.Count - 1; i >= 0; --i)
            {
                DialogeList[i].Deleate();
                DialogeList.RemoveAt(i);
            }
        }

        /// <summary>
        /// draws all open dialoge boxes
        /// </summary>
        /// <param name="window"></param>
        public void Draw(RenderWindow window)
        {
            foreach (DialogeBox dialoge in DialogeList)
            {
                dialoge.Draw(window);
            }
        }
    }
}
