using src.Core;

namespace src
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the game!");
            Console.WriteLine("1. Start new game");
            Console.WriteLine("2. Exit");

            string? input = Console.ReadLine();

            switch(input)
            {
                case "1":
                    Engine game = new();
                    game.Run();
                    break;

                case "2":
                    return;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}
