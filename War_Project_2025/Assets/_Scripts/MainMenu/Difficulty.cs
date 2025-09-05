using UnityEngine;

public class Difficulty : MonoBehaviour
{
    [SerializeField] private GameObject EasyBorder;
    [SerializeField] private GameObject MediumBorder;
    [SerializeField] private GameObject HardBorder;

    // Called by UI Buttons
    public void SetEasy()
    {
        GameManager.Instance.difficulty = 1f;
        GameManager.Instance.runDifficulty = 1f;
        UpdateBorders(1f);
    }

    public void SetMedium()
    {
        GameManager.Instance.difficulty = 1.5f;
        GameManager.Instance.runDifficulty = 1.5f;
        UpdateBorders(1.5f);
    }

    public void SetHard()
    {
        GameManager.Instance.difficulty = 2f;
        GameManager.Instance.runDifficulty = 2f;
        UpdateBorders(2f);
    }

    private void OnEnable()
    {
        
        if (GameManager.Instance.difficulty <= 0f)
            GameManager.Instance.difficulty = 1f;

        UpdateBorders(GameManager.Instance.difficulty);
    }

    private void UpdateBorders(float difficulty)
    {
        EasyBorder.SetActive(difficulty == 1f);
        MediumBorder.SetActive(difficulty == 1.5f);
        HardBorder.SetActive(difficulty == 2f);
    }
}
