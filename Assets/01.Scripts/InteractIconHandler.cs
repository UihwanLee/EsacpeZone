using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractIconHandlewr : MonoBehaviour
{
    // Player를 바라보고 있는 방향으로 상호작용 아이콘 회전

    private Player player;

    private void Start()
    {
        player = CharacterManager.Instance.Player;
    }

    private void Update()
    {
        if(player == null) player = CharacterManager.Instance.Player;

        if (player == null)
        {
            Debug.Log("플레이어 못찾음");
            return;
        }

        Vector3 direction = player.transform.position - this.gameObject.transform.position;
        direction.y = 0;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
    }
}
