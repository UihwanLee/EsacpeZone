using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState
{
    private Player player;
    private PlayerController controller;
    private PlayerCondition condition;
    private StateMachine stateMachine;

    public RunState(Player player)
    {
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


    public void Exit()
    {

    }
}
