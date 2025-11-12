using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClimbState : IState
{
    private Player player;
    private PlayerController controller;
    private PlayerCondition condition;
    private StateMachine stateMachine;

    public ClimbState(Player _player)
    {
        player = _player;
        controller = player.controller;
        condition = player.condition;
        stateMachine = player.stateMachine;
    }

    public void Enter()
    {
        // Player 제어
        controller._rb.useGravity = false;
        controller._rb.velocity = Vector3.zero;
    }

    public void Do()
    {
        // 기본 상태에서는 체력이 서서히 감소함
        condition.health.Subtract(condition.health.passiveValue * Time.deltaTime);

        // 기본 상태에서는 스태미나가 기본적으로 참
        condition.stamina.Add(condition.stamina.passiveValue * Time.deltaTime);
    }

    public void FixedDo()
    {
        Climb();
    }

    private void Climb()
    {
        Vector3 dir = player.transform.up * controller.CurrentMoveVector.y;
        dir *= controller.CurrentSpeed;

        controller._rb.velocity = dir;
    }

    public void Exit()
    {
        controller._rb.useGravity = true;
    }

    #region 입력처리

    public void HandleMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            controller.CurrentMoveVector = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            controller.CurrentMoveVector = Vector2.zero;
        }
    }

    public void HandleRunInput(InputAction.CallbackContext context)
    {

    }

    public void HandleJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            // JumpState 변경
            stateMachine.ChangeState(condition.JumpState);
        }
    }

    #endregion
}
