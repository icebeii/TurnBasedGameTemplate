using src.Core;

namespace src
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Output.Handler = new ConsoleOutputHandler();

            Output.Handler.WriteLine("Welcome to the game!");
            Output.Handler.WriteLine("1. Start new game");
            Output.Handler.WriteLine("2. Exit");

            int choice = InputHandler.GetChoiceFromTheList(1, 2);

            switch(choice)
            {
                case 1:
                    Engine game = new();
                    game.Run();
                    break;

                case 2:
                    return;
            }
        }
    }
}
