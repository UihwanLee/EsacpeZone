using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Laser : MonoBehaviour
{
    // Laser 스크립트

    [Header("Value")]
    [SerializeField] private float maxLaserDistance = 100;

    [Header("Component")]
    [SerializeField] private Transform laserFirePoint;
    [SerializeField] private LineRenderer lineRenderer;

    [Header("LayerMask")]
    [SerializeField] private LayerMask targetLayer;

    private void Update()
    {
        ShootingLaser();
    }

    private void ShootingLaser()
    {
        RaycastHit hit;
        Vector3 rayStartPos = laserFirePoint.position;

        bool isHit = Physics.Raycast(rayStartPos, transform.forward, out hit, maxLaserDistance);

        if(isHit)
        {
            DrawRay(rayStartPos, hit.point);

            // Player가 맞는다면
            if (hit.collider.gameObject == CharacterManager.Instance.Player.gameObject)
            {
                Player player = CharacterManager.Instance.Player;
                player.stateMachine.ChangeState(player.condition.DieState);
            }
        }
        else
        {
            DrawRay(laserFirePoint.position, laserFirePoint.transform.forward * maxLaserDistance);
        }
    }

    private void DrawRay(Vector3 startPos, Vector3 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}
