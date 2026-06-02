using System;
using System.Collections.Generic;

namespace src.Core
{
    /// <summary>
    /// Provides a global access point for the current output handler used to display game information
    /// </summary>
    public static class Output
    {
        /// <summary>
        /// Gets or sets the active output handler implementation.
        /// </summary>
        public static IOutputHandler Handler { get; set; }
    }

    /// <summary>
    /// Defines a contract for handling game output operations
    /// </summary>
    public interface IOutputHandler
    {
        /// <summary>
        /// Writes a line of text to the output
        /// </summary>
        void WriteLine(string message);

        /// <summary>
        /// Displays a formatted game log entry
        /// </summary>
        void ShowLog(IGameLog log);

        /// <summary>
        /// Clears the output screen
        /// </summary>
        void Clear();

        /// <summary>
        /// Waits for a user key press before continuing execution
        /// </summary>
        void WaitForKey();
    }

    /// <summary>
    /// Default console-based implementation of <see cref="IOutputHandler"/>
    /// Uses standard console input/output for displaying game information
    /// </summary>
    public class ConsoleOutputHandler : IOutputHandler
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void ShowLog(IGameLog log)
        {
            Console.WriteLine(log.GetMessage());
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void WaitForKey()
        {
            Console.ReadKey();
        }
    }
}
