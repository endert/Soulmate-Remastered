﻿using SFML.Graphics;
using SFML.Window;
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
        Texture cursorTexture = new Texture("Pictures/CheatConsole/Cursor.png");
        Sprite cursor;

        public static Stopwatch watch = new Stopwatch();
        public static uint windowSizeX = 300;
        public static uint windowSizeY = 100;
        RenderWindow window;
        public static bool isPressed;
        Font font = new Font("FontFolder/arial_narrow_7.ttf");
        public static Text text;
        Text closeWithEnter;

        public CheatConsole()
        {
            cursor = new Sprite(cursorTexture);
            window = new RenderWindow(new VideoMode(windowSizeX, windowSizeY), "CheatConsole");
            window.Closed += window_Close;
            window.SetVisible(false);
            closeWithEnter = new Text("close with Enter", font, 15);
            closeWithEnter.Position = new Vector2f((window.Size.X / 2) - (42),
                                                  (window.Size.Y / 2) - 30);
            text = new Text("", font, 10);
            text.Position = new Vector2f(3, windowSizeY - 40);
        }

        public void handleThread()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Return) && !CheatConsole.isPressed)
            {
                window.SetVisible(false);

                CheatConsoleThread.normalGameThread.Interrupt();
                Cheats.activateCheat(text.DisplayedString);
                text.DisplayedString = "";

                try
                {
                    Thread.Sleep(Timeout.Infinite);
                }
                catch (ThreadInterruptedException)
                {
                    CheatConsole.isPressed = true;
                    watch.Restart();
                }
            }
            else
            {
                window.SetVisible(true);
            }
        }

        public void update()
        {
            if (!isPressed)
            {
                watch.Start();
            }
            handleThread();

            window.Clear();
            //window.DispatchEvents();

            Input.InputHandle();
            cursor.Position = new Vector2f(text.FindCharacterPos((UInt32)text.DisplayedString.Length).X + cursor.Texture.Size.X, text.Position.Y);

            if (!NavigationHelp.isAnyKeyPressed() || watch.ElapsedMilliseconds > 150)
            {
                isPressed = false;
            }

            window.Draw(closeWithEnter);
            window.Draw(text);
            window.Draw(cursor);
            window.Display();
        }

        private void window_Close(Object sender, EventArgs e)
        {
            ((RenderWindow)sender).Close();
        }
    }
}