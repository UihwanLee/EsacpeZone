using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : IState
{
    private PlayerController controller;
    private PlayerCondition condition;
    private StateMachine stateMachine;

    public JumpState(PlayerController _controller, PlayerCondition _condition, StateMachine _stateMachine)
    {
        controller = _controller;
        condition = _condition;
        stateMachine = _stateMachine;
    }

    public void Enter()
    {
        // 점프
        controller._rb.AddForce(Vector2.up * Define.POWER_JUMP, ForceMode.Impulse);

        // 스태미나 소모
        condition.stamina.Subtract(condition.stamina.passiveValue * 4);
    }

    public void Do()
    {
        if (controller.isJumping) return;

        if(controller.IsGrounded())
        {
            stateMachine.ChangeState(condition.IdleState);
            controller.isJumping = false;
        }
    }

    public void Exit()
    {

    }
}
