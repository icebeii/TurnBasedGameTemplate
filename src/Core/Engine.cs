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
        private readonly GameContext context;
        private readonly EncounterGenerator encounterGenerator;
        public Engine()
        {
            Player player = new Player();
            context = new GameContext(player);
            encounterGenerator = new EncounterGenerator();
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
            context.Player.PrintPlayerStats();
            bool first = true;

            while (context.Player.IsAlive)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    Output.Handler.Clear();
                }

                Output.Handler.WriteLine("Encounter #" + $"{context.EncounterCount + 1}");

                IEncounter encounter = encounterGenerator.Generate(context);
                encounter.Execute(context);
                context.EncounterCount++;

                Output.Handler.WriteLine("");
                Output.Handler.WriteLine("Press any key to continue.");
                Output.Handler.WaitForKey();
            }

            int score = context.EncounterCount;
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
            Output.Handler.WriteLine("Final level: " + $"{context.Player.Level}");
        }      
    }
}
