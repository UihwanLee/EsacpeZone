using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    // 충돌 처리만 판단 : 땅, 기믹, 사다리 등등
    [Header("LayerMask")]
    [SerializeField] private LayerMask groundMask;      // Ground Layer
    [SerializeField] private LayerMask jumpPadMask;     // JumpPad Layer
    [SerializeField] private LayerMask buffMask;        // Buff Layer

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

    public Buff IsBuff()
    {
        Vector3 center = transform.position; 
        Vector3 halfExtents = new Vector3(0.5f, 0.5f, 0.5f);

        Vector3 direction = transform.forward; 
        Quaternion orientation = transform.rotation;
        float maxDistance = 1.5f; 

        RaycastHit hit;

        if (Physics.BoxCast(center, halfExtents, direction, out hit, orientation, maxDistance, buffMask))
        {
            return hit.transform.GetComponentInParent<Buff>();
        }

        // 충돌이 없으면 null 반환
        return null;
    }
}
