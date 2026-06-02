using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Manages the game's high score by loading it from and saving it to a file
/// </summary>
public class HighScoreManager
{
    /// <summary>
    /// The file name used to store the high score
    /// </summary>
    private const string FileName = "highscore.txt";

    /// <summary>
    /// Gets the currently stored high score
    /// </summary>
    public int HighScore { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="HighScoreManager"/> class and loads the high score from storage
    /// </summary>
    public HighScoreManager()
    {
        Load();
    }

    /// <summary>
    /// Loads the high score from the file system
    /// If the file does not exist or contains invalid data, the score is set to 0
    /// </summary>
    private void Load()
    {
        if (!File.Exists(FileName))
        {
            HighScore = 0;
            return;
        }
        string text = File.ReadAllText(FileName);
        int.TryParse(text, out int score);
        HighScore = score;
    }

    /// <summary>
    /// Saves the current high score to the file
    /// </summary>
    private void Save()
    {
        File.WriteAllText(FileName, HighScore.ToString());
    }

    /// <summary>
    /// Updates the high score if the provided score is greater than the current value
    /// </summary>
    /// <param name="score">The score achieved in the current run</param>
    /// <returns>
    /// <c>true</c> if the score is a new high score; otherwise, <c>false</c>.
    /// </returns>
    public bool SetNewScore(int score)
    {
        if (score <= HighScore)
        {
            return false;
        }
        HighScore = score;
        Save();
        return true;
    }
}