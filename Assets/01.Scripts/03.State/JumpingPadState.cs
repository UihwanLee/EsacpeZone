using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPadState : IState
{
    private Player player;
    private PlayerController controller;
    private PlayerCondition condition;
    private StateMachine stateMachine;

    private float jumpStartTime;

    public JumpingPadState(Player _player)
    {
        player = _player;
        controller = player.controller;
        condition = player.condition;
        stateMachine = player.stateMachine;
    }

    public void Enter()
    {
        // 기본 속도로 변경
        controller.ChangeSpeed(Define.SPEED_MOVE);

        if (player.jumpingPad == null)
        {
            Debug.LogError("JumpingPad가 없습니다.");
            return;
        }

        jumpStartTime = Time.time;

        // 방향대로 점프
        Vector3 jumpDir = player.jumpingPad.jumpingDirection.normalized;
        float jumpPower = player.jumpingPad.jumpPower;
        controller._rb.AddForce(jumpDir * jumpPower, ForceMode.Impulse);
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
            controller.isGrounded = false;
        }
    }


    public void Exit()
    {
        // JumpingPad Player에게서 지우기
        //player.jumpingPad = null;
    }
}
