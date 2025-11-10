using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;       // 이동 속도
    [SerializeField] private float jumpPower;       // 점프 파워
    private Vector2 curMoveVector;                  // 현재 움직임

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
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
        dir *= moveSpeed;
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

    private void Jump()
    {
        _rb.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
    }
}
