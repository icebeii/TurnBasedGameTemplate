using src.Encounters;
using src.Entities;
using System;
using System.Collections.Generic;

namespace src.Core
{
    /// <summary>
    /// Represents the main game engine responsible for running the game loop
    /// </summary>
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

        /// <summary>
        /// Starts and runs the main game loop
        /// The loop generates encounters and executes them until the player dies
        /// After the game ends, the final score and high score status are displayed
        /// </summary>
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

            int score = _context.EncounterCount;
            HighScoreManager highScoreManager = new HighScoreManager();
            bool newHighScore = highScoreManager.SetNewScore(score);

            Output.Handler.WriteLine("");
            Output.Handler.WriteLine("Game over!");
            if (newHighScore)
            {
                Output.Handler.WriteLine("");
                Output.Handler.WriteLine("New high score!");
            }
            Output.Handler.WriteLine("Encounters completed: " + $"{score}");
            Output.Handler.WriteLine("Final level: " + $"{_context.Player.Level}");
        }      
    }
}
