using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject Default;
    [SerializeField] private GameObject InformationDialog;
   

    private Dictionary<string, bool> savedInputStates;
    void OnEnable()
    {
       
        Time.timeScale = 0;
        savedInputStates = InputManager.Instance.GetInputsState();
        InputManager.Instance.DisableAllExceptPause();

    }

    void OnDisable()
    {
       
        PauseReset();

    }

    private void PauseReset()
    {
        Time.timeScale = 1;
        Default.SetActive(true);
        InformationDialog.SetActive(false);
       
       
        InputManager.Instance.SetInputsState(savedInputStates);
    }

    public void MainMenu()
    {

        PauseReset();
      
        SceneManager.LoadScene("Menu");
    }

    public void BtnSound() { }
}
