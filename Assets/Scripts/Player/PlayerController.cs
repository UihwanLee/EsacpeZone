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

    [Header("LayerMaske")]
    [SerializeField] private LayerMask groundMask;  // Ground Layer

    private Rigidbody _rb;
    private Camera camera;

    private PlayerCondition condition;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        camera = Camera.main;

        curSpeed = moveSpeed;
    }

    private void Start()
    {
        condition = CharacterManager.Instance.Player.condition;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CamerLook();
    }

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

    public void InputJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            // Space바 한번 누르면 점프
            Jump();
        }
    }

    public void InputRun(InputAction.CallbackContext context)
    {
        if (condition.state == ConditonState.Consume || condition.state == ConditonState.Die) return;

        if(context.phase == InputActionPhase.Performed)
        {
            // Shift를 누르고 있을 시 달리기
            ChangeSpeed(runSpeed);
            condition.ChangeState(ConditonState.Run);
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            ChangeSpeed(moveSpeed);
            condition.ChangeState(ConditonState.None);
        }
    }

    private void Jump()
    {
        if(IsGrounded())
        {
            _rb.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
            condition.Jump();
        }
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(transform.position, Vector2.down);
        return Physics.Raycast(ray, 1.5f, groundMask);
    }

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

    public void ChangeSpeed(float speed)
    {
        curSpeed = speed;
    }
}
