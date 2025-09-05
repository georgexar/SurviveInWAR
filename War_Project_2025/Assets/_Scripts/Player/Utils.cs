using UnityEngine;

public class Utils : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private void Update()
    {
        if (InputManager.Instance.PauseMenuAction.triggered) 
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }
    }
}
