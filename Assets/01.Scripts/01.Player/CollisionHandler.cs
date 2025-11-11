using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    // 충돌 처리만 판단 : 땅, 기믹, 사다리 등등
    [Header("LayerMask")]
    [SerializeField] private LayerMask groundMask;      // Ground Layer
    [SerializeField] private LayerMask jumpPadMask;     // JumpPad Layer

    private bool isGrounded = false;

    public bool IsGrounded()
    {
        Ray ray = new Ray(transform.position, Vector2.down);
        return Physics.Raycast(ray, 1.5f, groundMask);
    }

    public JumpingPad IsJumpingPad()
    {
        Ray ray = new Ray(transform.position, Vector2.down);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 1.5f, jumpPadMask);
        if(hit.collider != null)
        {
            return hit.transform.GetComponent<JumpingPad>();
        }

        return null;
    }
}
