using System;

class Program
{
    public static OpenTK.Input.Key moveUp = OpenTK.Input.Key.W;
    public static OpenTK.Input.Key moveDown = OpenTK.Input.Key.S;
    public static OpenTK.Input.Key moveleft = OpenTK.Input.Key.A;
    public static OpenTK.Input.Key moveRight = OpenTK.Input.Key.D;


    public static void Main(string[] args)
    {
        int xRes = Console.WindowWidth - 1;
        int yRes = Console.WindowHeight - 1;

        for (int x = 0; x < xRes; x++)
        {
            for (int y = 0; y < yRes; y++)
            {
                Console.SetCursorPosition(x, y);
                Console.Write('X');
            }
        }

        Console.SetCursorPosition(0, 0);
        Console.WriteLine("Hello World!");
        Console.WriteLine("The Console Resolution: xRes: " + xRes + " yRes: " + yRes);

        Engine.Game g = new Engine.Game(new Vector2(xRes, yRes), ' ');

        g.StartGame();

        Console.SetCursorPosition(0, 0);
        Console.WriteLine("Good Bye!");
    }
}