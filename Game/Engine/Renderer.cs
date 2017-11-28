using System;
using System.Collections.Generic;

namespace Engine
{
    public class Renderer
    {
        public static Renderer renderer;
        public static Vector3 windowOffset;
        public static Vector2 windowRes;
        public static Vector2 windowReserved;

        public static Vector2 windowBottom;

        public List<char[]> screenToWrite;
        public char[] clearLine;
        public Queue<string> messages;

        public char clear;

        public Renderer(Vector2 ScreenSize, char clearChar, Vector2 Reserve)
        {
            if (renderer == null)
            {
                renderer = this;
            }
            else
            {
                throw new Exception("Singleton Exception @ Renderer.cs");
            }


            Console.CursorVisible = false;
            clear = clearChar;
            windowReserved = Reserve;
            windowRes = new Vector2(ScreenSize.x - Reserve.x, ScreenSize.y - Reserve.y);
            windowOffset = new Vector3();
            windowBottom = new Vector2();
            updateWindowBottom();
            screenToWrite = new List<char[]>();
            clearLine = new char[windowRes.x];
            messages = new Queue<string>();
            initScreen();
        }

        private void updateWindowBottom()
        {
            windowBottom.x = windowOffset.x + windowRes.x;
            windowBottom.y = windowOffset.y + windowRes.y;
        }

        void initScreen()
        {
            for (int y = 0; y < windowRes.y; y++)
            {
                screenToWrite.Add(new char[windowRes.x]);
                for (int x = 0; x < windowRes.x; x++)
                {
                    screenToWrite[y][x] = clear;

                    if(y == 0)
                    {
                        clearLine[x] = clear;
                    }
                }
            }
        }


        public void doEarlyUpdate()
        {
            updateWindowBottom();
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
                clearLine.CopyTo(screenToWrite[y], 0);
            }
        }

        public void render(Vector2 ScreenPos, char tex)
        {
            if(ScreenPos != null)
            {
                if (utils.isInRange(0, windowRes.x - 1, ScreenPos.x) && (utils.isInRange(0, windowRes.y - 1, ScreenPos.y)))
                {
                    screenToWrite[ScreenPos.y][ScreenPos.x] = tex;
                }
            }
        }

        public void clearScreenAt(Vector2 ScreenPos)
        {
            if (ScreenPos != null)
            {
                if (utils.isInRange(0, windowRes.x - 1, ScreenPos.x) && (utils.isInRange(0, windowRes.y - 1, ScreenPos.y)))
                {
                    screenToWrite[ScreenPos.y][ScreenPos.x] = clear;
                }
            }
        }

        public static Vector2 worldPosToScreenPos(Vector2 worldPos)
        {
            if (utils.isInRange(windowOffset.x, windowBottom.x, worldPos.x) && (utils.isInRange(windowOffset.y, windowBottom.y, worldPos.y)))
            {
                return new Vector2(worldPos.x - windowOffset.x, worldPos.y - windowOffset.y);
            }
            else
            {
                return null;
            }
        }

        public static Vector2 worldPosToScreenPos(Vector3 worldPos)
        {
            if (utils.isInRange(windowOffset.x, windowBottom.x, worldPos.x) && (utils.isInRange(windowOffset.y, windowBottom.x, worldPos.y)) && worldPos.z == windowOffset.z)
            {
                return new Vector2(worldPos.x - windowOffset.x, worldPos.y - windowOffset.y);
            }
            else
            {
                return null;
            }
        }
    }
}
