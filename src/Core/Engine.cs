using src.Encounters;
using src.Entities;
using System;
using System.Collections.Generic;

namespace src.Core
{
    public class Engine
    {
        private readonly GameContext _context;
        private readonly EncounterGenerator _encounterGenerator;
        public Engine()
        {
            Player player = new Player();
            _context = new GameContext(player);
            _encounterGenerator = new EncounterGenerator();
        }

        public void Run()
        {
            Output.Handler.Clear();
            Output.Handler.WriteLine("Game started!");
            _context.Player.PrintPlayerStats();
            bool first = true;

            while (_context.Player.IsAlive)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    Output.Handler.Clear();
                }

                Output.Handler.WriteLine("Encounter #" + $"{_context.EncounterCount + 1}");

                IEncounter encounter = _encounterGenerator.Generate(_context);
                encounter.Execute(_context);
                _context.EncounterCount++;

                Output.Handler.WriteLine("");
                Output.Handler.WriteLine("Press any key to continue.");
                Output.Handler.WaitForKey();
            }

            Output.Handler.WriteLine("");
            Output.Handler.WriteLine("Game over!");
            Output.Handler.WriteLine("Encounters completed: " + $"{_context.EncounterCount}");
            Output.Handler.WriteLine("Final level: " + $"{_context.Player.Level}");
        }      
    }
}
