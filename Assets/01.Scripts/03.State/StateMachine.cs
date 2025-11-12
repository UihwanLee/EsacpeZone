using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    // Player를 FSM을 관리하는 스크립트

    private IState currentState;        // 현재 상태
    public IState CurrentState { get { return currentState; } set { currentState = value; } }

    // 전환 규약을 나타내는 Dictionary
    private Dictionary<IState, List<IState>> allowedTransitions;

    private void Awake()
    {
        allowedTransitions = new Dictionary<IState, List<IState>>();
    }

    public void Init(IState initState)
    {
        // 초기 상태 결정
        currentState = initState;
        currentState.Enter();
    }

    public void MakeTransitionRule(IState fromState, IState toState)
    {
        if (!allowedTransitions.ContainsKey(fromState))
        {
            // 새로운 Transition이면 새로 생성
            allowedTransitions[fromState] = new List<IState>();
        }
        allowedTransitions[fromState].Add(toState);
    }


    public void ChangeState(IState newState)
    {
        // 상태 변경
        if (currentState != null &&
            allowedTransitions.ContainsKey(currentState) &&
            !allowedTransitions[currentState].Contains(newState))
        {
            Debug.Log($"규약 위반: {currentState}에서 {newState}으로 전환 불가");
            return; 
        }

        if (currentState != null)
        {
            // 이전 상태가 있다면 Exit 실행
            currentState.Exit();
        }

        // 새로운 상태 변경 및 Enter 호출
        currentState = newState;
        currentState.Enter();
    }

    public void Update()
    {
        // Update에서 Do 실행
        if(currentState != null)
            currentState.Do();
    }

    public void FixedUpdate()
    {
        // FixedUpdate에서 FixedDo 실행
        if (currentState != null)
            currentState.FixedDo();
    }
}
