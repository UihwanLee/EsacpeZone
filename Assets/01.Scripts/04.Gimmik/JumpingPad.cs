using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPad : MonoBehaviour, IGimmik
{
    [Header("JumpingInfo")]
    public Vector3 jumpingDirection = new Vector3(0f, 10f, 0f);
    public float jumpPower = 100f;

    public void Activate()
    {
        // JumpingPad는 Player가 점프할 방향을 전달해줌
        Player player = CharacterManager.Instance.Player;

        // 현재 JumpingPad 알려주고 상태 변경
        player.jumpingPad = this;
        player.stateMachine.ChangeState(player.condition.JumpingPadState);
    }
}
