using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// updates all open dialoge boxes
        /// </summary>
        public void Update()
        {
            //try catch because if a dialoge box is destroyed while updating it throws an exception but we can ignore that
            //it may be skips the update of all dialoge boxes after the destroyed one once
            try
            {
                foreach (DialogeBox dialoge in DialogeList)
                {
                    dialoge.Update();
                }
            }
            catch (InvalidOperationException) { }
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
