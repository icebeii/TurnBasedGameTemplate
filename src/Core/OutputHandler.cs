using System;
using System.Collections.Generic;

namespace src.Core
{
    public static class Output
    {
        public static IOutputHandler Handler { get; set; }
    }

    public interface IOutputHandler
    {
        void WriteLine(string message);
        void ShowLog(IGameLog log);
        void Clear();
        void WaitForKey();
    }

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
