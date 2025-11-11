using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    // Player가 Control할 Condition : 체력, 스태미나 

    public Condition health;
    public Condition stamina;

    // State 객체
    public IState IdleState { get; private set; }
    public IState MoveState { get; private set; }
    public IState RunState { get; private set; }
    public IState JumpState { get; private set; }

    // 클래스 참조
    private PlayerController controller;
    private StateMachine stateMachine;

    [Header("State UI")]
    [SerializeField] private TextMeshProUGUI stateTxt; // 테스트 용 state Txt

    private void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        stateMachine = CharacterManager.Instance.Player.stateMachine;

        InitState();
    }

    private void InitState()
    {
        IdleState = new IdleState(controller, this, stateMachine);
        MoveState = new MoveState(controller, this, stateMachine);
        RunState = new RunState(controller, this, stateMachine);
        JumpState = new JumpState(controller, this, stateMachine);

        // 초기 상태 IdleState로 변경
        stateMachine.Init(IdleState);
    }

    private void Update()
    {
        stateTxt.text = $"현재 상태: {stateMachine.CurState}";        
    }
}
