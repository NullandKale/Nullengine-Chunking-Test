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

        Engine.Game g = new Engine.Game(new Vector2(xRes, yRes), ' ', 10);

        g.StartGame();

        Console.SetCursorPosition(xRes, 0);
        Console.WriteLine("Good Bye!");
    }
}