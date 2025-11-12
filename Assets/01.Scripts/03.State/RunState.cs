using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunState : IState
{
    private Player player;
    private PlayerController controller;
    private PlayerCondition condition;
    private StateMachine stateMachine;

    public RunState(Player _player)
    {
        player = _player;
        controller = player.controller;
        condition = player.condition;
        stateMachine = player.stateMachine;
    }

    public void Enter()
    {
        // Run 속도로 변경
        controller.ChangeSpeed(Define.SPEED_RUN);
    }

    public void Do()
    {
        // 기본 상태에서는 체력이 서서히 감소함
        condition.health.Subtract(condition.health.passiveValue * Time.deltaTime);

        // Run 상태에서는 스태미나가 지속적으로 감소함
        condition.stamina.Subtract(condition.stamina.passiveValue * Time.deltaTime);
    }

    public void FixedDo()
    {
        Move();
    }

    private void Move()
    {
        Vector3 dir = player.transform.forward * controller.CurrentMoveVector.y + player.transform.right * controller.CurrentMoveVector.x;
        dir *= controller.CurrentSpeed;
        dir.y = controller._rb.velocity.y;

        controller._rb.velocity = dir;
    }

    public void Exit()
    {

    }

    #region 입력처리

    public void HandleMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            // W, A, S, D키를 누르고 있는 상태라면 Move
            controller.CurrentMoveVector = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            controller.CurrentMoveVector = Vector2.zero;
        }
    }

    public void HandleRunInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            // Shift를 누르고 있을 시 달리기
            stateMachine.ChangeState(condition.RunState);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            stateMachine.ChangeState(condition.IdleState);
        }
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
