using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.NPCFolder;
using Soulmate_Remastered.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.DialogeBoxFolder
{
    class DialogeBox
    {
        /// <summary>
        /// returns a bool value if the dialogue box is open right now or not
        /// </summary>
        public bool isOpen { get; protected set; }
        /// <summary>
        /// the position in world coordinates
        /// </summary>
        Vector2 position;
        /// <summary>
        /// the background Texture
        /// </summary>
        Texture background = new Texture("Pictures/Entities/DialogeBox/DialogeBoxBackground.png");
        /// <summary>
        /// the background sprite
        /// </summary>
        Sprite dialogeBox;
        /// <summary>
        /// the List wich contains all lines of the dialoge
        /// </summary>
        List<String> text;
        /// <summary>
        /// the shown Text on screen
        /// </summary>
        Text txt;
        /// <summary>
        /// the index of the next Line of the shown Text
        /// </summary>
        int index = 0;
        /// <summary>
        /// character size of the text
        /// </summary>
        readonly uint characterSize = 20;
        /// <summary>
        /// reference to the npc this dialoge box belongs to
        /// </summary>
        AbstractNPC npc;


        /// <summary>
        /// create a dialoge box at the position with that String
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="dialoge"></param>
        public DialogeBox(Vector2 pos, String dialoge, AbstractNPC _npc)
        {
            //initialize
            position = pos;
            dialogeBox = new Sprite(background);
            dialogeBox.Position = position;
            txt = new Text("", Game.font, characterSize);
            txt.Position = new Vector2f(dialogeBox.Position.X + 5, dialogeBox.Position.Y + 5);
            isOpen = true;
            npc = _npc;

            //create Dialoge and display it
            text = createDialoge(dialoge);
            setDisplayedString();
        }

        /// <summary>
        /// splits the String so that it can be shown properly
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private List<String> createDialoge(String str)
        {
            //get rid of all unneccessary linebreaks
            str = str.Replace("\n", "");

            //setup for the evaluation
            List<String> result = new List<string>();//the returned List
            String[] spliString = str.Split(' ');    //a String array that contains the single words, so we can determine rather they stay in this line or the next
            String oneLine = "";                     //a String that symbolize exactly one Line
            uint maxNumber = (background.Size.X - 10) / (uint)(characterSize / 2);//the max number of characters in one line

            //filling the result list
            foreach (String s in spliString)
            {
                /*
                 * if the number of character in one line plus the number of characters in this String are smaller than the maxNumber
                 * then this String stays in this Line
                 * else the Line is completed and we add it to the resulted list and start a new Line by setting oneLine = s + " "
                 */
                if (oneLine.Length + s.Length <= maxNumber)
                    oneLine += s + " ";
                else
                {
                    result.Add(oneLine);
                    oneLine = s + " ";
                }
            }

            //adding last Line
            result.Add(oneLine);
            oneLine = "";

            //return result
            return result;
        } 

        /// <summary>
        /// evaluates wich part of the text is on screen
        /// </summary>
        public void setDisplayedString()
        {
            //evaluate max number of lines we can show at the same time
            uint maxLineCount = (background.Size.Y-10) / characterSize;

            //we try to go through the whole string list
            for (int i = index; i < text.Count; i++)
            {
                /*
                 * if i-index < maxLineCount then the Line can be shown
                 * else we set index = i and break the loop.
                 */
                if (i - index < maxLineCount)
                {
                    txt.DisplayedString += text[i] + "\n";
                }
                else
                {
                    index = i;
                    break;
                }

                /*
                 * if it is the last itteration set index at a not defined number, it is now out of range and we will not get any new texts
                 * and now we can check if we have iterated all lines by checking index == text.count
                */
                if (i + 1 == text.Count)
                    index = i + 1;
            }
        }

        /// <summary>
        /// updates the dialoge box
        /// </summary>
        public void update()
        {
            //update the shown String if the interaction button is pressed
            if (Keyboard.IsKeyPressed(Controls.Interact) && !Game.isPressed)
            {
                Game.isPressed = true;

                //if there is nothing left to show, close the dialoge box
                if (index == text.Count)
                    npc.stopIteraction();//destroys this Instance

                //else set the diesplayed Sting new
                txt.DisplayedString = "";
                setDisplayedString();
            }
        }

        /// <summary>
        /// draws the dialoge box
        /// </summary>
        /// <param name="window"></param>
        public void draw(RenderWindow window)
        {
            //draw the background Sprite and the text
            if (isOpen)
            {
                window.Draw(dialogeBox);
                window.Draw(txt);
            }
        }
    }
}
