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
        public bool isOpen { get; set; }
        Vector2f position;
        Texture background = new Texture("Pictures/Entities/DialogeBox/DialogeBoxBackground.png");
        Sprite dialogeBox;
        String theWholeDialoge;
        String oneLine;
        Font font = new Font("FontFolder/arial_narrow_7.ttf");
        Text txt;
        int index = 0;
        String[] text;
        int numberOfLines = 0;
        float bvft = 1.7f; //stupid Variable For Texts
        bool isPressed = true;

        public DialogeBox(Vector2f pos, String dialoge)
        {
            position = pos;
            dialogeBox = new Sprite(background);
            dialogeBox.Position = position;
            theWholeDialoge = dialoge;
            txt = new Text("", font, 20);
            oneLine = "";
            txt.Position = new Vector2f(dialogeBox.Position.X + 5, dialogeBox.Position.Y + 5);
            setDisplayedString();
            isOpen = true;
        }

        public void setDisplayedString()
        {
            text = theWholeDialoge.Split();
            for (int i = index; i < text.Length; i++)
            {
                if ((oneLine.Length + text[i].Length) * txt.CharacterSize / bvft < background.Size.X)
                {
                    txt.DisplayedString += text[i] + " ";
                    oneLine += text[i] + " ";
                }
                else if (numberOfLines++ * txt.CharacterSize * bvft < background.Size.Y)
                {
                    txt.DisplayedString += "\n" + text[i] + " ";
                    oneLine = "";
                }
                else
                {
                    index = i;
                    break;
                }
            }
        }

        public void update()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.P) && !isPressed)
            {
                txt.DisplayedString = "";
                oneLine = "";
                numberOfLines = 0;
                isPressed = true;
                setDisplayedString();
            }

            if (!Keyboard.IsKeyPressed(Keyboard.Key.P) && isPressed)
            {
                isPressed = false;
            }
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
