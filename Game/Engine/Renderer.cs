using System;
using System.Collections.Generic;

namespace Engine
{
    public class Renderer
    {
        public static Renderer renderer;
        public static Vector2 windowOffset;
        public static Vector2 windowRes;

        public List<char[]> screenToWrite;
        public Queue<String> messages;

        public char clear;

        public Renderer(Vector2 ScreenSize, char clearChar)
        {
            if (renderer == null)
            {
                renderer = this;
            }
            else
            {
                throw new Exception("Singleton Exception @ Renderer.cs");
            }

            clear = clearChar;
            windowRes = ScreenSize;
            windowOffset = new Vector2();
            screenToWrite = new List<char[]>();
            messages = new Queue<string>();
            initScreen();
        }

        void initScreen()
        {
            for (int y = 0; y < windowRes.y; y++)
            {
                screenToWrite.Add(new char[windowRes.x]);
                for (int x = 0; x < windowRes.x; x++)
                {
                    screenToWrite[y][x] = clear;
                }
            }
        }


        public void doUpdate()
        {
            int messageCount = messages.Count;
            for(int i = 0; i < messageCount; i++)
            {
                char[] temp = messages.Dequeue().ToCharArray();
                for(int j = 0; j < temp.Length; j++)
                {
                    screenToWrite[i][j] = temp[j];
                }
            }

            Console.SetCursorPosition(0, 0);
            for (int y = 0; y < windowRes.y; y++)
            {
                Console.WriteLine(screenToWrite[y]);
            }
        }

        public void render(Vector2 ScreenPos, char tex)
        {
            if(ScreenPos == null)
            {
                return;
            }
            else
            {
                screenToWrite[ScreenPos.y][ScreenPos.x] = tex;
            }
        }

        public static Vector2 worldPosToScreenPos(Vector2 worldPos)
        {
            if (utils.isInRange(windowOffset.x, windowRes.x + windowOffset.x, worldPos.x) && (utils.isInRange(windowOffset.y, windowRes.y + windowOffset.y, worldPos.y)))
            {
                return new Vector2(worldPos.x + windowOffset.x, worldPos.y + windowOffset.y);
            }
            else
            {
                return null;
            }
        }
    }
}
