using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DieState : IState
{
    private Player player;
    private PlayerController controller;
    private PlayerCondition condition;
    private StateMachine stateMachine;

    private UIManager uiManager;

    public DieState(Player _player)
    {
        player = _player;
        controller = player.controller;
        condition = player.condition;
        stateMachine = player.stateMachine;

        Init();
    }

    private void Init()
    {
        uiManager = GameObject.FindObjectOfType<UIManager>();
        if (uiManager == null)
        {
            Debug.Log("UIManager 찾을 수 없음!");
        }
        else
        {
            uiManager.SetDieUI(false);
            Button btn_retry = uiManager.UI_Die.GetComponentInChildren<Button>();
            btn_retry.onClick.AddListener(ClickRetry);
        }
    }

    public void Enter()
    {
        // Player 제어
        controller._rb.useGravity = false;
        controller._rb.velocity = Vector3.zero;
        controller.isDie = true;

        if (uiManager != null) uiManager.SetDieUI(true);
    }

    public void Do()
    {

    }

    public void FixedDo()
    {
    }

    public void LatedDo()
    {
    }

    public void Exit()
    {
        controller._rb.useGravity = true;
        controller.isDie = false;

        if(uiManager != null)
        {
            uiManager.SetDieUI(false);
        }
    }

    private void ClickRetry()
    {
        stateMachine.ChangeState(condition.IdleState);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    #region 입력처리

    public void HandleMoveInput(InputAction.CallbackContext context)
    {

    }

    public void HandleRunInput(InputAction.CallbackContext context)
    {

    }

    public void HandleJumpInput(InputAction.CallbackContext context)
    {

    }

    public void HandleMouseInput(InputAction.CallbackContext context)
    {
    }

    #endregion
}
