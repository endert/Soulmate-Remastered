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
        public static List<DialogeBox> dialogeList { get; set; }

        /// <summary>
        /// initialize a dialoge handler, should only be called once
        /// </summary>
        public DialogeHandler()
        {
            dialogeList = new List<DialogeBox>();
        }

        /// <summary>
        /// updates all open dialoge boxes
        /// </summary>
        public void update()
        {
            //try catch because if a dialoge box is destroyed while updating it throws an exception but we can ignore that
            //it may be skips the update of all dialoge boxes after the destroyed one once
            try
            {
                foreach (DialogeBox dialoge in dialogeList)
                {
                    dialoge.update();
                }
            }
            catch (InvalidOperationException) { }
        }

        /// <summary>
        /// draws all open dialoge boxes
        /// </summary>
        /// <param name="window"></param>
        public void draw(RenderWindow window)
        {
            foreach (DialogeBox dialoge in dialogeList)
            {
                dialoge.draw(window);
            }
        }
    }
}
