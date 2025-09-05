
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerInputs;

    [Header("Action Map Names References")]
    [SerializeField] private string mapName = "Player";

    [Header("Action Names References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string run = "Run";
    [SerializeField] private string pauseMenu = "PauseMenu";
    [SerializeField] private string look = "Look";
    [SerializeField] private string attack = "Attack";
    [SerializeField] private string collect = "Collect";


    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction runAction;
    private InputAction pauseMenuAction;
    private InputAction lookAction;
    private InputAction attackAction;
    private InputAction collectAction;

    public static InputManager Instance { get; private set; }

    public InputAction MoveAction => moveAction;
    public InputAction JumpAction => jumpAction;
    public InputAction RunAction => runAction;
    public InputAction PauseMenuAction => pauseMenuAction;
    public InputAction AttackAction => attackAction;
    public InputAction LookAction => lookAction;
    public InputAction CollectAction => collectAction;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeInputActions();
        }
        else
        {
            Destroy(gameObject);
        }
    }



    private void InitializeInputActions()
    {
        moveAction = playerInputs.FindActionMap(mapName).FindAction(move);
        jumpAction = playerInputs.FindActionMap(mapName).FindAction(jump);
        runAction = playerInputs.FindActionMap(mapName).FindAction(run);
        pauseMenuAction = playerInputs.FindActionMap(mapName).FindAction(pauseMenu);
        lookAction = playerInputs.FindActionMap(mapName).FindAction(look);
        attackAction = playerInputs.FindActionMap(mapName).FindAction(attack);
        collectAction= playerInputs.FindActionMap(mapName).FindAction(collect);
       
        EnableInputActions();
    }

    public void EnableInputActions()
    {
        foreach (var action in playerInputs.FindActionMap(mapName).actions)
        {
            action?.Enable();
        }
    }



    private void OnDisable()
    {
        foreach (var action in playerInputs.FindActionMap(mapName).actions)
        {
            action?.Disable();
        }
    }

    public void DisableAllExceptPause()
    {
        foreach (var action in playerInputs.FindActionMap(mapName).actions)
        {
            if (action == pauseMenuAction) continue;
            action?.Disable();
        }
    }

 
    public void DisableAllInputs()
    {
        foreach (var action in playerInputs.FindActionMap(mapName).actions)
        {
            action?.Disable();
        }
    }

    public Dictionary<string, bool> GetInputsState()
    {
        var inputStates = new Dictionary<string, bool>();

        foreach (var action in playerInputs.FindActionMap(mapName).actions)
        {
            inputStates.Add(action.name, action.enabled);
        }

        return inputStates;
    }

    public void SetInputsState(Dictionary<string, bool> inputStates)
    {
        foreach (var entry in inputStates)
        {

            var action = playerInputs.FindActionMap(mapName).FindAction(entry.Key);

            if (action != null)
            {
                if (entry.Value)
                {
                    action.Enable();
                }
                else
                {
                    action.Disable();
                }
            }
        }
    }

}
