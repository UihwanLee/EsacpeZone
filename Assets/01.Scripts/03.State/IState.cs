using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState 
{
    // FSM에서 관리하려는 상태 인터페이스
    public void Enter();
    public void Do();
    public void Exit();
}
