using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpingPadState : IState
{
    private Player player;
    private PlayerController controller;
    private PlayerCondition condition;
    private StateMachine stateMachine;

    private float jumpStartTime;

    public JumpingPadState(Player _player)
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

        if (player.jumpingPad == null)
        {
            Debug.LogError("JumpingPad가 없습니다.");
            return;
        }

        jumpStartTime = Time.time;

        // 방향대로 점프
        Vector3 jumpDir = player.jumpingPad.jumpingDirection.normalized;
        float jumpPower = player.jumpingPad.jumpPower;
        controller._rb.AddForce(jumpDir * jumpPower, ForceMode.Impulse);
    }

    public void Do()
    {
        if (Time.time < jumpStartTime + 0.1f)
        {
            return;
        }

        // 기본 상태에서는 체력이 서서히 감소함
        condition.health.Subtract(condition.health.passiveValue * Time.deltaTime);

        // 땅에 착지하면 다시 원래 상태로 돌아오기
        if (controller.collisionHandler.IsGrounded())
        {
            stateMachine.ChangeState(condition.IdleState);
            controller.isGrounded = false;
        }
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
        // JumpingPad Player에게서 지우기
        //player.jumpingPad = null;
    }

    #region 입력처리

    public void HandleMoveInput(InputAction.CallbackContext context)
    {

    }

    public void HandleRunInput(InputAction.CallbackContext context)
    {

    }

    public void HandleJumpInput(InputAction.CallbackContext context)
    {

    }

    public void HandleMouseInput(InputAction.CallbackContext context)
    {
        Vector2 mouseDelta = context.ReadValue<Vector2>();
        controller.ChangeMouseDelta(mouseDelta);
    }

    #endregion
}
