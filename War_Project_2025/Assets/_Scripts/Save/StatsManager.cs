using System.IO;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    private static string statsPath => Path.Combine(Application.persistentDataPath, "stats.json");
    private static string highscorePath => Path.Combine(Application.persistentDataPath, "highscore.json");

    // Save current run stats (call on player death)
    public static void SaveStats()
    {
        PlayerStats stats = new PlayerStats
        {
            totalKills = GameManager.Instance.totalKills,
            totalFlagsCollected = GameManager.Instance.totalFlagsCollected,
            maxWaveReached = GameManager.Instance.maxWaveReached,
            difficulty = GetDifficultyText(GameManager.Instance.runDifficulty)
        };

        File.WriteAllText(statsPath, JsonUtility.ToJson(stats, true));
        

        SaveHighScore(stats);
    }

    // Load current run stats
    public static PlayerStats LoadStats()
    {
        if (!File.Exists(statsPath)) return null;
        string json = File.ReadAllText(statsPath);
        return JsonUtility.FromJson<PlayerStats>(json);
    }

    // Save highscore if current run beats it
    private static void SaveHighScore(PlayerStats currentStats)
    {
        PlayerStats highscore = LoadHighScore();

        if (highscore == null || currentStats.maxWaveReached > highscore.maxWaveReached)
        {
            File.WriteAllText(highscorePath, JsonUtility.ToJson(currentStats, true));
           


        }
    }

    // Load highscore
    public static PlayerStats LoadHighScore()
    {
        if (!File.Exists(highscorePath)) return null;
        string json = File.ReadAllText(highscorePath);
        return JsonUtility.FromJson<PlayerStats>(json);
    }

    // Convert runDifficulty to readable text
    public static string GetDifficultyText(float runDifficulty)
    {
        if (Mathf.Approximately(runDifficulty, 1f)) return "Easy";
        if (Mathf.Approximately(runDifficulty, 1.5f)) return "Medium";
        if (Mathf.Approximately(runDifficulty, 2f)) return "Hard";
        return $"Custom ({runDifficulty:F1})";
    }
}
