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


    public Vector2 CurrentMoveVector { get; set; }          // 현재 움직임
    public float CurrentSpeed { get { return curSpeed; } }  // 현재 속도 프로퍼티

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

    // bool 값
    public bool isGrounded = false;
    public bool isMoving = false;
    public bool isClimbing = false;

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
        CheckBuff();
    }

    private void LateUpdate()
    {
        CamerLook();
    }

    #region 이동 처리
    public void InputMove(InputAction.CallbackContext context)
    {
        stateMachine.CurrentState.HandleMoveInput(context);
    }

    #endregion

    #region 달리기 처리

    public void InputRun(InputAction.CallbackContext context)
    {
        stateMachine.CurrentState.HandleRunInput(context);
    }

    #endregion

    #region 점프 처리

    public void InputJump(InputAction.CallbackContext context)
    {
        stateMachine.CurrentState.HandleJumpInput(context);
    }

    public void CheckJumping()
    {
        isGrounded = collisionHandler.IsGrounded();
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

    public void CheckBuff()
    {
        Buff buff = collisionHandler.IsBuff();
        if(buff != null)
        {
            buff.BuffOn();
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
