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
        String[] text;
        Font font = new Font("FontFolder/arial_narrow_7.ttf");
        Text txt;
        Text testTxt = new Text("", Game.font, 20);
        int index = 0;
        int numberOfLines = 0;
        float bvft = 1.7f; //stupid Variable For Texts
        bool isPressed = true;

        public DialogeBox(Vector2f pos, String[] dialoge)
        {
            position = pos;
            dialogeBox = new Sprite(background);
            dialogeBox.Position = position;
            text = dialoge;
            txt = new Text("", font, 20);
            txt.Position = new Vector2f(dialogeBox.Position.X + 5, dialogeBox.Position.Y + 5);
            isOpen = true;
            testTxt.Position = txt.Position;
            Console.WriteLine("background.Size.X = " + background.Size.X);
            setDisplayedString();
        }

        public void setDisplayedString()
        {
            for (int i = index; i < text.Length; i++)
            {
                if (i - index < 4)
                {
                    txt.DisplayedString += text[i] + "\n";
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
                testTxt.DisplayedString = "";
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
