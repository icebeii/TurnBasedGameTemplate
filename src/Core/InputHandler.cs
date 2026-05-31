using src.Core;
using System;
using System.Collections.Generic;

public static class InputHandler
{
    public static int GetChoiceFromTheList(int minBound, int maxBound)
    {
        while (true)
        {
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int choice))
            {
                if (choice >= minBound && choice <= maxBound)
                {
                    return choice;
                }
                else
                {
                    Output.Handler.WriteLine("Invalid choice.");
                }
            }
        }
    }
}