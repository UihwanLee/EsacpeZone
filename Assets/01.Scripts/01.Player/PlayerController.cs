using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float curSpeed;        // 현재 속도
    [SerializeField] private float moveSpeed;       // 이동 속도
    [SerializeField] private float runSpeed;        // 달리기 속도
    [SerializeField] private float jumpPower;       // 점프 파워
    private Vector2 curMoveVector;                  // 현재 움직임

    [Header("CameraLook")]
    [SerializeField] private float minXLook;            // 최소 시야
    [SerializeField] private float maxXLook;            // 최대 시야
    [SerializeField] private float lookSensitivity;     // 회전 속도

    private float curXLook;
    private Vector2 mouseDelta;

    public Rigidbody _rb;
    private Camera camera;

    // 클래스 참조
    private PlayerCondition condition;
    private StateMachine stateMachine;
    public CollisionHandler collisionHandler;

    // Jump bool 값
    public bool isJumping = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        camera = Camera.main;
    }

    private void Start()
    {
        condition = GetComponent<PlayerCondition>();
        stateMachine = GetComponent<StateMachine>();
        collisionHandler = GetComponent<CollisionHandler>();
    }

    private void Update()
    {
        CheckJumping();
        CheckJumpingPad();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CamerLook();
    }

    #region 이동 처리
    public void InputMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            // W, A, S, D키를 누르고 있는 상태라면 Move
            curMoveVector = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            curMoveVector = Vector2.zero;
        }
    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMoveVector.y + transform.right * curMoveVector.x;
        dir *= curSpeed;
        dir.y = _rb.velocity.y;

        _rb.velocity = dir;
    }
    #endregion

    #region 달리기 처리

    public void InputRun(InputAction.CallbackContext context)
    {
        // 특정 상태일 때는 Input 안받게

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

    #endregion

    #region 점프 처리

    public void InputJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            // JumpState 변경
            stateMachine.ChangeState(condition.JumpState);
        }
    }

    public void CheckJumping()
    {
        isJumping = collisionHandler.IsGrounded();
    }

    #endregion

    #region 기믹 처리

    public void CheckJumpingPad()
    {
        JumpingPad pad = collisionHandler.IsJumpingPad();
        if(pad != null)
        {
            pad.Activate();
        }
    }

    #endregion

    #region 시선 처리

    public void InputMouseDelta(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    private void CamerLook()
    {
        // 마우스 회전에 따른 카메라 이동
        curXLook += mouseDelta.y * lookSensitivity;
        curXLook = Mathf.Clamp(curXLook, minXLook, maxXLook);
        camera.transform.localEulerAngles = new Vector3(-curXLook, 0f, 0f);

        transform.eulerAngles += new Vector3(0f, mouseDelta.x * lookSensitivity, 0f);
    }

    #endregion

    #region 상태 처리

    public void ChangeSpeed(float speed)
    {
        curSpeed = speed;
    }

    #endregion
}
