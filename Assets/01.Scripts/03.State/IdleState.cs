using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class IdleState : IState
{
    private Player player;
    private PlayerController controller;
    private PlayerCondition condition;
    private StateMachine stateMachine;

    public IdleState(Player _player)
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
    }

    public void Do()
    {
        // 기본 상태에서는 체력이 서서히 감소함
        condition.health.Subtract(condition.health.passiveValue * Time.deltaTime);

        // 기본 상태에서는 스태미나가 기본적으로 참
        condition.stamina.Add(condition.stamina.passiveValue * Time.deltaTime);
    }


    public void Exit()
    {
        
    }
}
