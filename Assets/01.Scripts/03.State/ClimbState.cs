using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClimbState : IState
{
    private Player player;
    private PlayerController controller;
    private PlayerCondition condition;
    private StateMachine stateMachine;

    private Ladder ladder;
    private float startOffsetThresHold = 0.1f;          // 사다리 시작 Offset 한계점
    private float endOffsetThresHold = 1.4f;            // 사다리 끝 Offset 한계점

    public ClimbState(Player _player)
    {
        player = _player;
        controller = player.controller;
        condition = player.condition;
        stateMachine = player.stateMachine;
    }

    public void Enter()
    {
        // Player 제어
        controller._rb.useGravity = false;
        controller._rb.velocity = Vector3.zero;

        // 사다리에 달라붙기
        if(player.detachObject != null)
        {
            ladder = player.detachObject.GetComponent<Ladder>();
            if(ladder != null )
            {
                player.transform.position = (ladder.isStartPosition) ? ladder.startPosition.position : ladder.endPosition.position;
            }
        }
    }

    public void Do()
    {
        // 기본 상태에서는 체력이 서서히 감소함
        condition.health.Subtract(condition.health.passiveValue * Time.deltaTime);

        // 기본 상태에서는 스태미나가 기본적으로 참
        condition.stamina.Add(condition.stamina.passiveValue * Time.deltaTime);

        if(ladder == null) { Debug.Log("붙은 사다리가 없음!"); return; }

        // 사다리 아래쪽과 끝쪽에 다다르면 StateChange
        Vector3 playerPos = player.transform.position;
        if (ladder.startPosition.position.y - playerPos.y > startOffsetThresHold ||
            playerPos.y - ladder.endPosition.position.y > endOffsetThresHold)
        {
            controller.isClimbing = false;
            stateMachine.ChangeState(condition.IdleState);
        }
    }

    public void FixedDo()
    {
        Climb();
    }

    private void Climb()
    {
        Vector3 dir = player.transform.up * controller.CurrentMoveVector.y;
        dir *= controller.CurrentSpeed;

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
        controller._rb.useGravity = true;
    }

    #region 입력처리

    public void HandleMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            controller.CurrentMoveVector = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            controller.CurrentMoveVector = Vector2.zero;
        }
    }

    public void HandleRunInput(InputAction.CallbackContext context)
    {

    }

    public void HandleJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            // JumpState 변경
            controller.isClimbing = false;
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
