using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpState : IState
{
    private Player player;
    private PlayerController controller;
    private PlayerCondition condition;
    private StateMachine stateMachine;

    private float jumpStartTime;

    public JumpState(Player _player)
    {
        player = _player;
        controller = player.controller;
        condition = player.condition;
        stateMachine = player.stateMachine;
    }

    public void Enter()
    {
        // 점프
        controller._rb.AddForce(Vector2.up * Define.POWER_JUMP, ForceMode.Impulse);
        jumpStartTime = Time.time;

        // 스태미나 소모
        condition.stamina.Subtract(condition.stamina.passiveValue * 4);
    }

    public void Do()
    {
        if (Time.time < jumpStartTime + 0.1f)
        {
            return; 
        }

        // 기본 상태에서는 체력이 서서히 감소함
        condition.health.Subtract(condition.health.passiveValue * Time.deltaTime);

        // 땅에 착지하면 다시 원래 상태로 돌아오기
        if (controller.collisionHandler.IsGrounded())
        {
            stateMachine.ChangeState(condition.IdleState);
        }
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

    }

    public void HandleJumpInput(InputAction.CallbackContext context)
    {

    }

    #endregion
}
