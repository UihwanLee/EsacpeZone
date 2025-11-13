using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MoveState : IState
{
    private Player player;
    private PlayerController controller;
    private PlayerCondition condition;
    private StateMachine stateMachine;

    public MoveState(Player _player)
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
        // Die 체크
        if (controller.isDie) stateMachine.ChangeState(condition.DieState);

        // 기본 상태에서는 체력이 서서히 감소함
        condition.health.Subtract(condition.health.passiveValue * Time.deltaTime);

        // 기본 상태에서는 스태미나가 기본적으로 참
        condition.stamina.Add(condition.stamina.passiveValue * Time.deltaTime);
    }

    public void FixedDo()
    {
        Move();
    }

    private void Move()
    {
        Vector3 dir = player.transform.forward * controller.CurrentMoveVector.y + player.transform.right * controller.CurrentMoveVector.x;
        dir *= controller.CurrentSpeed;
        dir.y = controller._rb.velocity.y;

        controller._rb.velocity = dir;
    }

    public void LatedDo()
    {
        CamerLook();
    }

    private void CamerLook()
    {
        // 마우스 회전에 따른 카메라 이동
        float curXLook = controller.CurrentXLook;
        curXLook += controller.MouseDelta.y * controller.LookIntensity;
        curXLook = Mathf.Clamp(curXLook, controller.MinXLook, controller.MaxXLook);

        Camera camera = controller.MainCamera;
        camera.transform.localEulerAngles = new Vector3(-curXLook, 0f, 0f);
        controller.MainCamera = camera;

        player.transform.eulerAngles += new Vector3(0f, controller.MouseDelta.x * controller.LookIntensity, 0f);
    }
    public void Exit()
    {
        
    }

    #region 입력처리

    public void HandleMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            // W, A, S, D키를 누르고 있는 상태라면 Move
            controller.CurrentMoveVector = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            controller.CurrentMoveVector = Vector2.zero;
            stateMachine.ChangeState(condition.IdleState);
        }
    }

    public void HandleRunInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            // Shift를 누르고 있을 시 달리기
            stateMachine.ChangeState(condition.RunState);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            stateMachine.ChangeState(condition.IdleState);
        }
    }

    public void HandleJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            // JumpState 변경
            stateMachine.ChangeState(condition.JumpState);
        }
    }

    public void HandleMouseInput(InputAction.CallbackContext context)
    {
        Vector2 mouseDelta = context.ReadValue<Vector2>();
        controller.ChangeMouseDelta(mouseDelta);
    }

    #endregion
}
