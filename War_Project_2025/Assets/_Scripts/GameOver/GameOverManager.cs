using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statsText;
    public void MainMenu() 
    {
        SceneManager.LoadScene("Menu");
    }

    private void OnEnable()
    {
       
        // Load current run stats
        PlayerStats stats = StatsManager.LoadStats();

        if (stats == null)
        {
            statsText.text = "No stats available.";
            return;
        }

        // Format the stats for TMP
        statsText.text =
            $"Difficulty: {stats.difficulty}\n" +
            $"Max Wave Reached: {stats.maxWaveReached}\n" +
            $"Total Kills: {stats.totalKills}\n" +
            $"Flags Collected: {stats.totalFlagsCollected}";

    }
   
}
