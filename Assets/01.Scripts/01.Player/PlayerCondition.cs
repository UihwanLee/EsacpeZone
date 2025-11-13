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
    public IState JumpingPadState { get; private set; }
    public IState ClimbState { get; private set; }
    public IState DieState { get; private set; }

    // 클래스 참조
    private Player player;
    private PlayerController controller;
    private StateMachine stateMachine;

    [Header("State UI")]
    [SerializeField] private TextMeshProUGUI stateTxt; // 테스트 용 state Txt

    private void Start()
    {
        player = GetComponent<Player>();
        controller = GetComponent<PlayerController>();
        stateMachine = GetComponent<StateMachine>();

        InitState();
    }

    private void InitState()
    {
        IdleState = new IdleState(player);
        MoveState = new MoveState(player);
        RunState = new RunState(player);
        JumpState = new JumpState(player);
        JumpingPadState = new JumpingPadState(player);
        ClimbState = new ClimbState(player);
        DieState = new DieState(player);

        SetUpTransition();

        // 초기 상태 IdleState로 변경
        stateMachine.Init(IdleState);
    }

    private void SetUpTransition()
    {
        // Idle
        stateMachine.MakeTransitionRule(IdleState, MoveState);
        stateMachine.MakeTransitionRule(IdleState, RunState);
        stateMachine.MakeTransitionRule(IdleState, JumpState);
        stateMachine.MakeTransitionRule(IdleState, JumpingPadState);
        stateMachine.MakeTransitionRule(IdleState, ClimbState);
        stateMachine.MakeTransitionRule(IdleState, DieState);

        // Move
        stateMachine.MakeTransitionRule(MoveState, IdleState);
        stateMachine.MakeTransitionRule(MoveState, RunState);
        stateMachine.MakeTransitionRule(MoveState, JumpState);
        stateMachine.MakeTransitionRule(MoveState, JumpingPadState);
        stateMachine.MakeTransitionRule(MoveState, ClimbState);
        stateMachine.MakeTransitionRule(MoveState, DieState);

        // Run
        stateMachine.MakeTransitionRule(RunState, IdleState);
        stateMachine.MakeTransitionRule(RunState, MoveState);
        stateMachine.MakeTransitionRule(RunState, JumpState);
        stateMachine.MakeTransitionRule(RunState, JumpingPadState);
        stateMachine.MakeTransitionRule(RunState, ClimbState);
        stateMachine.MakeTransitionRule(RunState, DieState);

        // Jump
        stateMachine.MakeTransitionRule(JumpState, IdleState);
        stateMachine.MakeTransitionRule(JumpState, JumpingPadState);
        stateMachine.MakeTransitionRule(JumpState, DieState);

        // JumpingPad
        stateMachine.MakeTransitionRule(JumpingPadState, IdleState);
        stateMachine.MakeTransitionRule(JumpingPadState, JumpingPadState);
        stateMachine.MakeTransitionRule(JumpingPadState, DieState);

        // Climb
        stateMachine.MakeTransitionRule(ClimbState, IdleState);
        stateMachine.MakeTransitionRule(ClimbState, JumpState);
        stateMachine.MakeTransitionRule(ClimbState, DieState);

        // Die
        stateMachine.MakeTransitionRule(DieState, IdleState);
    }

    private void Update()
    {
        stateTxt.text = $"현재 상태: {stateMachine.CurrentState}";        
    }
}
