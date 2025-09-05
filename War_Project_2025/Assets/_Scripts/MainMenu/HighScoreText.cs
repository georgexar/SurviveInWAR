using TMPro;
using UnityEngine;

public class HighScoreText : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI highscoreText;

    private void OnEnable()
    {
        // Load highscore
        PlayerStats highscore = StatsManager.LoadHighScore();

        if (highscore == null)
        {
            highscoreText.text = "No highscore yet.";
            return;
        }

        // Format stats for TMP
        highscoreText.text =
            $"Highscore Run\n" +
            $"Difficulty: {highscore.difficulty}\n" +
            $"Max Wave: {highscore.maxWaveReached}\n" +
            $"Total Kills: {highscore.totalKills}\n" +
            $"Flags Collected: {highscore.totalFlagsCollected}";
    }
}
