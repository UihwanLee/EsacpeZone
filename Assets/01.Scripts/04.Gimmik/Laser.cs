using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Laser 스크립트

    [SerializeField] private float defDistanceRay = 100;
    [SerializeField] private Transform laserFirePoint;
    [SerializeField] private LineRenderer lineRenderer;
    private Transform m_transform;

    private void Awake()
    {
        m_transform = GetComponent<Transform>();
    }

    private void Update()
    {
        ShootingLaser();
    }

    private void ShootingLaser()
    {
        if (Physics.Raycast(m_transform.position, transform.forward))
        {
            RaycastHit hit;
            Ray ray = new Ray(laserFirePoint.position, transform.forward);
            Physics.Raycast(ray, out hit);
            DrawRay(laserFirePoint.position, hit.point);
        }
        else
        {
            DrawRay(laserFirePoint.position, laserFirePoint.transform.forward * defDistanceRay);
        }
    }

    private void DrawRay(Vector3 startPos, Vector3 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}
