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
        List<String> text;
        Font font = new Font("FontFolder/arial_narrow_7.ttf");
        Text txt;
        Text testTxt = new Text("", Game.font, 20);
        int index = 0;
        bool isPressed = true;

        public DialogeBox(Vector2f pos, String dialoge)
        {
            position = pos;
            dialogeBox = new Sprite(background);
            dialogeBox.Position = position;
            txt = new Text("", font, 20);
            txt.Position = new Vector2f(dialogeBox.Position.X + 5, dialogeBox.Position.Y + 5);
            isOpen = true;
            testTxt.Position = txt.Position;
            testTxt = new Text(txt);
            Console.WriteLine("background.Size.X = " + background.Size.X);
            text = createDialoge(dialoge);
            setDisplayedString();
        }

        private List<String> createDialoge(String s)
        {
            List<String> result = new List<string>();
            Char[] spliString = s.ToCharArray();
            String oneLine = "";

            for (int i = 0; i < spliString.Length; i++)
            {
                testTxt.DisplayedString += spliString[i].ToString();
                if (testTxt.FindCharacterPos((uint)testTxt.DisplayedString.Length - 1).X < background.Size.X - 15 && !spliString[i].Equals('\n'))
                {
                    oneLine += spliString[i].ToString();
                }
                else
                {
                    result.Add(oneLine);
                    testTxt.DisplayedString = "";
                    oneLine = "";
                    if (!spliString[i].Equals('\n'))
                    {
                        i--;
                    }
                }
            }

            if (result.Count > 0 && !oneLine.Equals(result[result.Count - 1]))
            {
                result.Add(oneLine);
            }

            return result;
        } 

        public void setDisplayedString()
        {
            for (int i = index; i < text.Count; i++)
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
