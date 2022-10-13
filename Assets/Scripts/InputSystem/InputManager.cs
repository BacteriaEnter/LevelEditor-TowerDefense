using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerContrl inputActions;
    [SerializeField] BattleManager battleManager;

    public bool confirmHold_input;
    public bool confirmTap_input;
    public bool cancel_Input;
    public bool rotateL_Input;
    public bool rotateR_Input;


    [SerializeField] LayerMask checkLayer;
    private void Awake()
    { 
    }

    private void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerContrl();
            inputActions.PlayerControl.Confirm.performed += i => confirmHold_input = true;
            inputActions.PlayerControl.Confirm.started += i => confirmTap_input = true;
            inputActions.PlayerControl.Confirm.canceled += i => confirmHold_input = false;
            inputActions.PlayerControl.Confirm.canceled += i => confirmTap_input = false;
            inputActions.PlayerControl.RotateLeft.started += i => rotateL_Input = true;
            inputActions.PlayerControl.RotateRight.started += i => rotateR_Input = true;
            inputActions.PlayerControl.Cancel.started += i => cancel_Input = true;
            inputActions.PlayerControl.Cancel.canceled += i => cancel_Input = false;
        }
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void TickInput()
    {
        HandleCancelInput();
        HandleConfirmTapInput();
    }

    private void Update()
    {
        if (GameManager.instance.sceneIndex==1)
        {
            if (confirmTap_input)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                if (Physics.Raycast(ray, out hit, checkLayer))
                {
                    if (hit.collider.CompareTag("LevelPoint"))
                    {
                        LevelPoint point = hit.collider.GetComponent<LevelPoint>();
                        if (point != null)
                        {
                            point.ToBattle();
                        }
                    }
                }
            }     
        }
    }

    void HandleCancelInput()
    {
        if (battleManager != null && cancel_Input == true)
        {
            battleManager.ActionBack();
        }
    }

    void HandleConfirmTapInput()
    {
        if (confirmTap_input)
        {
            battleManager.HandlePlayerSelectAction();
        }

    }

    public void LoadComponent()
    {
        battleManager = FindObjectOfType<BattleManager>();
    }
}
