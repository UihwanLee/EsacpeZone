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
        controller.Jump();

        // 스태미나 소모
        condition.stamina.Subtract(condition.stamina.passiveValue * 4);
    }

    public void Do()
    {
        stateMachine.CurState = this;
        if(controller.IsGrounded())
        {
            Debug.Log("변경");
            // 땅에 닿으면 State 변경
            stateMachine.ChangeState(condition.IdleState);
        }
    }

    public void Exit()
    {

    }
}
