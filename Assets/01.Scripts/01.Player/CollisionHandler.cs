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

    private CapsuleCollider capsuleCollider;
    private float maxDistance = 1.3f;

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public bool IsGrounded()
    {
        bool isHit = false;

        Ray ray = new Ray(transform.position, Vector2.down);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, maxDistance, groundMask);

        DrawRay(hit, transform.position, Vector2.down * maxDistance);

        if (hit.collider != null)
        {
            isHit = true;
        }

        return isHit;
    }

    public JumpingPad IsJumpingPad()
    {
        Ray ray = new Ray(transform.position, Vector2.down);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 1.5f, jumpPadMask);

        DrawRay(hit, transform.position, Vector2.down * 1.5f);

        if (hit.collider != null)
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

    private void DrawRay(RaycastHit rayCastHit, Vector2 origin, Vector2 dir)
    {
        Color rayColor;
        if (rayCastHit.collider != null)
            rayColor = Color.green;
        else
            rayColor = Color.red;

        Debug.DrawRay(origin, dir, rayColor);
    }
}
