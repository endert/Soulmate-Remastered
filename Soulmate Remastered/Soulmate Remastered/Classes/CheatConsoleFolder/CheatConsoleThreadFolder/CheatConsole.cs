using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.CheatConsoleFolder.CheatConsoleThreadFolder
{
    class CheatConsole
    {
        /// <summary>
        /// the cursor that blinks has the size 5,10
        /// </summary>
        RectangleShape cursor = new RectangleShape(new Vector2f(5, 10));
        /// <summary>
        /// <para>Vector which contains width and heigth of the Console Window.</para>
        /// Standard 300,100
        /// </summary>
        public static Vector2u WindowSize { get; protected set; }
        /// <summary>
        /// Window of the Console Thread
        /// </summary>
        public RenderWindow Window { get; protected set; }
        /// <summary>
        /// other Windows for example a List with Cheats + Syntaxes etc.
        /// </summary>
        List<RenderWindow> otherWindows;
        /// <summary>
        /// the content of the other windows
        /// </summary>
        List<List<Drawable>> otherWindowsContent;
        Font font = new Font("FontFolder/arial_narrow_7.ttf");
        /// <summary>
        /// the entered Line in the Console
        /// </summary>
        public static Text Text_ { get; protected set; }
        /// <summary>
        /// the Text that says "close with Enter"
        /// </summary>
        Text closeWithEnter;
        /// <summary>
        /// Stopwatch for the blink time of the Cursor in the Console
        /// </summary>
        Stopwatch cursorBlickTime = new Stopwatch();
        /// <summary>
        /// 500 ms = 0,5s
        /// </summary>
        int cursorBlinkIntervallMilliseconds = 500;
        static readonly uint textSize = 20;
        
        public CheatConsole()
        {
            //set the standard of the windowSize
            WindowSize = new Vector2u(300, 100);

            //setting the cursor color to white
            cursor.FillColor = Color.White;

            //initializing the windows
            Window = new RenderWindow(new VideoMode(WindowSize.X, WindowSize.Y), "CheatConsole");
            Window.SetVisible(false);
            otherWindows = new List<RenderWindow>();
            otherWindowsContent = new List<List<Drawable>>();

            //initializing the texts
            closeWithEnter = new Text("close with Enter", font, 15);
            closeWithEnter.Position = new Vector2f((Window.Size.X / 2) - (42),
                                                  (Window.Size.Y / 2) - 30);
            Text_ = new Text("", font, 10);
            Text_.Position = new Vector2f(3, WindowSize.Y - 40);

            //start the blinkTime
            cursorBlickTime.Start();
        }

        /// <summary>
        /// <para>handle the current thread. When Return is pressed,</para> 
        /// <para>so the Console should be closed, start by settig visibility</para>
        /// <para>to false and interrupt the game Thread, so the sleep is</para> 
        /// <para>interrupted and the game thread can continue, also activate</para>
        /// <para>the Cheat that was entered.then set the Thread as paused</para>
        /// <para>until another Thread interrupts.</para>
        /// <para> </para>
        /// <para>If Return is not pressed set visibility of the window to true.</para>
        /// </summary>
        public void HandleThread()
        {
            if (Input.ReturnIsPressed())
            {
                Input.SetKeyPressed(Keyboard.Key.Return);
                if (Text_.DisplayedString.Equals("/" + Cheats.Cheat.ShowCheats.ToString()))
                {
                    Cheats.ActivateCheat(Text_.DisplayedString);
                    Text_.DisplayedString = "";
                    return ;
                }

                Window.SetVisible(false);

                CheatConsoleThreadStart.normalGameThread.Interrupt();
                Cheats.ActivateCheat(Text_.DisplayedString);
                Text_.DisplayedString = "";
                foreach (RenderWindow win in otherWindows)
                {
                    win.Close();
                }

                otherWindows = new List<RenderWindow>();
                otherWindowsContent = new List<List<Drawable>>();

                try
                {
                    Thread.Sleep(Timeout.Infinite);
                }
                catch (ThreadInterruptedException)
                {
                }
            }
            else
            {
                Window.SetVisible(true);
            }
        }

        /// <summary>
        /// the method to make the cursor blink in the Console
        /// </summary>
        private void CursorBlink()
        {
            //if timer.elapsed milliseconds > the time it should be shown, fill the cursor with transparents (disapear)
            if (cursorBlickTime.ElapsedMilliseconds > cursorBlinkIntervallMilliseconds)
                cursor.FillColor = Color.Transparent;
            //else set the color to white
            else
                cursor.FillColor = Color.White;

            //if the time ellapsed over the doubled time, so it was shown and transparent, restart the time
            if (cursorBlickTime.ElapsedMilliseconds > 2 * cursorBlinkIntervallMilliseconds)
                cursorBlickTime.Restart();
        }

        /// <summary>
        /// calls Input update, handle Thread also updates the window (Console)
        /// </summary>
        public void Update()
        {
            //call other "updates"
            HandleThread();
            Input.Update();

            //handle windows
            Window.Clear();
            Window.DispatchEvents();    //let the Window be moved by the cursor but not be closed
            Window.Size = WindowSize;   //stops resizing
            try
            {
                foreach (RenderWindow w in otherWindows)
                {
                    w.Clear();
                    w.DispatchEvents();
                }
            }
            catch (Exception e) { }


            //handle cursor
            cursor.Position = new Vector2f(Text_.FindCharacterPos((UInt32)Text_.DisplayedString.Length).X + cursor.Size.X, Text_.Position.Y);  //sets the cursor Position
            CursorBlink();

            //draw everything that must be drawn
            Window.Draw(closeWithEnter);
            Window.Draw(Text_);
            Window.Draw(cursor);
            for (int i = 0; i < otherWindows.Count; ++i)
            {
                for (int j = 0; j < otherWindowsContent[i].Count; ++j)
                {
                    otherWindows[i].Draw(otherWindowsContent[i][j]);
                }
            }

            //finaly display it
            Window.Display();
            foreach (RenderWindow w in otherWindows)
                w.Display();
        }

        /// <summary>
        /// Cheats that dont effect the Game directly
        /// </summary>
        /// <param name="p"></param>
        public delegate void Cheat(Params p);

        public void ActivateCheat(Params p, Cheat c)
        {
            c(p);
        }

        /// <summary>
        /// Open a new Window with all Cheats and Syntaxes
        /// </summary>
        /// <param name="p"></param>
        public void ShowCheats(Params p)
        {
            if (!p.IsEmpty)
                throw new FormatException();

            String shown = Cheats.CheatSyntax;
            Text t = new Text(shown, Game.font, textSize);
            t.Position = new Vector2(2, 2);

            Vector2 windowSize = new Vector2(t.FindCharacterPos((uint)t.DisplayedString.Length-1));

            List<Drawable> list = new List<Drawable>();
            list.Add(t);

            RenderWindow win = new RenderWindow(new VideoMode((uint)windowSize.X, (uint)windowSize.Y*2), "CheatList");
            win.Closed += DisposeContent;
            win.Closed += (sender, e) => ((RenderWindow)sender).Close();
            win.SetVisible(true);
           
            otherWindows.Add(win);
            otherWindowsContent.Add(list);
        }

        /// <summary>
        /// Only attach to closeevents of windows
        /// dispose all resources of the Content from that window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DisposeContent(Object sender, EventArgs e)
        {
            int index = 0;

            while (sender != otherWindows[index]) index++; //find the window in the List

            otherWindowsContent.RemoveAt(index);
            otherWindows.RemoveAt(index);
        }
    }
}
