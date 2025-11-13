using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IState 
{
    // FSM에서 관리하려는 상태 인터페이스
    public void Enter();
    public void Do();
    public void FixedDo();
    public void LatedDo();
    public void Exit();

    // State에서 Input 관련 처리를 함
    public void HandleMoveInput(InputAction.CallbackContext context);
    public void HandleRunInput(InputAction.CallbackContext context);
    public void HandleJumpInput(InputAction.CallbackContext context);
    public void HandleMouseInput(InputAction.CallbackContext context);
}
