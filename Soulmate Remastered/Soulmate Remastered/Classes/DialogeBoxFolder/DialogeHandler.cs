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
        public static List<DialogeBox> dialogeList { get; set; }

        public DialogeHandler()
        {
            dialogeList = new List<DialogeBox>();
        }

        public void draw(RenderWindow window)
        {
            foreach (DialogeBox dialoge in dialogeList)
            {
                dialoge.draw(window);
            }
        }
    }
}
