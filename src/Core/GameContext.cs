using src.Entities;
using System;
using System.Collections.Generic;

namespace src.Core
{
    /// <summary>
    /// Provides shared game state and services used across the entire game session
    /// </summary>
    public class GameContext
    {
        /// <summary>
        /// Gets the player controlled by the user
        /// </summary>
        public Player Player { get; }

        /// <summary>
        /// Gets the random number generator used for game logic
        /// </summary>
        public Random Random { get; }

        /// <summary>
        /// Gets or sets the number of encounters completed in the current run
        /// </summary>
        public int EncounterCount { get; set; }

        /// <summary>
        /// Gets the list of game logs generated during the session
        /// </summary>
        public List<IGameLog> Logs { get; } = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="GameContext"/> class
        /// </summary>
        /// <param name="player">The player character for this game session</param>
        public GameContext(Player player)
        {
            Player = player;
            Random = new Random();
        }

        /// <summary>
        /// Adds a log entry to the game log list and immediately displays it to the user
        /// </summary>
        /// <param name="log">The log entry to add and display.</param>
        public void AddLog(IGameLog log)
        {
            Logs.Add(log);
            Output.Handler.WriteLine("");
            Output.Handler.ShowLog(log);
        }
    }
}
