using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.DialogeBoxFolder
{
    class DialogeBox
    {
        bool isOpen;
        Vector2f position;
        Texture background = new Texture("Pictures/DialogeBox/DialogeBoxBackground.png");
        Sprite dialogeBox;
        String theWholeDialoge;
        Font font = new Font("FontFolder/arial_narrow_7.ttf");
        Text txt;

        public DialogeBox(Vector2f pos, String dialoge)
        {
            position = pos;
            dialogeBox = new Sprite(background);
            dialogeBox.Position = position;
            theWholeDialoge = dialoge;
            txt = new Text(dialoge, font, 20);
            txt.Position = new Vector2f(dialogeBox.Position.X + 5, dialogeBox.Position.Y + 5);
            isOpen = true;
            DialogeHandler.dialogeList.Add(this);
        }

        public void draw(RenderWindow window)
        {
            if (isOpen)
            {
                window.Draw(dialogeBox);
                window.Draw(txt);
            }
        }
    }
}
