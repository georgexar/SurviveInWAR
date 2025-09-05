using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class MenuManager : MonoBehaviour
{
  
  
    public void NewGameBtn()
    {
        InputManager.Instance.EnableInputActions();
        SceneManager.LoadScene("Game");
    }

    public void ExitButton()
    {

        Application.Quit();
    }
}
