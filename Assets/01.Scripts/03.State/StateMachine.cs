using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    // Player를 FSM을 관리하는 스크립트

    private IState curState;        // 현재 상태
    public IState CurState { get { return curState; } set { curState = value; } }

    public void Init(IState initState)
    {
        // 초기 상태 결정
        curState = initState;
        curState.Enter();
    }

    public void ChangeState(IState newState)
    {
        // 상태 변경

        if(curState != null)
        {
            // 이전 상태가 있다면 Exit 실행
            curState.Exit();
        }

        // 새로운 상태 변경 및 Enter 호출
        curState = newState;
        curState.Enter();
    }

    public void Update()
    {
        // Update에서 Do 실행
        curState.Do();
    }
}
