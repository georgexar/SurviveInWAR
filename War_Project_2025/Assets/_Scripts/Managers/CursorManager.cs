using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance { get; private set; }

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
        }
    }
    private void Update()
    {
        HandleCursor();
    }

    private void HandleCursor()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "MainMenu")
        {
            ShowCursor();
            return;
        }

        if (sceneName == "Game")
        {
          
            GameObject pauseMenu = GameObject.Find("PauseMenu");
            GameObject gameOverPanel = GameObject.Find("GameOver");

            bool showCursor = false;

            if (pauseMenu != null && pauseMenu.activeInHierarchy)
                showCursor = true;

            if (gameOverPanel != null && gameOverPanel.activeInHierarchy)
                showCursor = true;

            if (showCursor)
                ShowCursor();
            else
                HideCursor();
        }
    }
    // Show cursor (for menus)
    public void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Hide and lock cursor (for gameplay)
    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
