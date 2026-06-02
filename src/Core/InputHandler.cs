using src.Core;
using System;
using System.Collections.Generic;

/// <summary>
/// Provides helper methods for validating and reading user input from the console
/// </summary>
public static class InputHandler
{
    /// <summary>
    /// Reads an integer input from the console and ensures it is within the specified range
    /// The method blocks until the user provides a valid value
    /// </summary>
    /// <param name="minBound">The minimum allowed value (inclusive)</param>
    /// <param name="maxBound">The maximum allowed value (inclusive)</param>
    /// <returns>
    /// A valid integer selected by the user within the specified range
    /// </returns>
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