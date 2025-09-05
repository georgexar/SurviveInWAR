using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    public int totalKills;
    public int totalFlagsCollected;
    public int maxWaveReached;
    public float difficulty = 1f;
    public float runDifficulty = 1f;
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void IncreaseDifficulty()
    {
        difficulty *= 1.1f;
        
    }
}
