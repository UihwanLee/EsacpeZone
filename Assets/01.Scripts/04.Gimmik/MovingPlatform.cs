using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float movingSpeed = 3.0f;
    [SerializeField] private float movingDistance = 5.0f;

    private float movingDir = 1.0f;
    private float distanceThreadHold = 0.1f;

    private Vector3 targetPosition;
    private Vector3 originPosition;

    private GameObject target;
    private Vector3 offset;

    private void Awake()
    {
        target = null;
        originPosition = transform.position;
        targetPosition = originPosition + new Vector3(movingDir * movingDistance, 0f, 0f);
    }

    private void Update()
    {
        Moving();
    }

    private void Moving()
    {
        // 좌, 우로 움직임
        Vector3 curPosition = transform.position;
        curPosition.x += movingDir * movingSpeed * Time.deltaTime;
        transform.position = curPosition;

        if(Vector3.Distance(targetPosition, curPosition) < distanceThreadHold) 
        {
            movingDir *= -1.0f;
            targetPosition = originPosition + new Vector3(movingDir * movingDistance, 0f, 0f);
        }
    }

    private void LateUpdate()
    {
        // Player와 Platform 간 offset 줄이기
        if (target != null)
        {
            target.transform.position = transform.position + offset;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        Player player = collision.transform.GetComponent<Player>();
        if(player != null)
        {
            // 현재 Player 이동 중인지 체크
            if (player.controller.isMoving)
            {
                target = null;
            }

            // player 전달
            player.detachPlaform = this.gameObject;

            // target 지정
            target = player.transform.gameObject;
            offset = player.transform.position - transform.position;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Player player = collision.transform.GetComponent<Player>();
        if (player != null)
        {
            target = null;
            offset = Vector3.zero;
            player.detachPlaform = null;
        }
    }
}
