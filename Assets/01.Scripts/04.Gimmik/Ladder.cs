using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if(player != null )
        {
            if (player.controller.isClimbing) return;

            player.detachPlaform = this.gameObject;
            player.stateMachine.ChangeState(player.condition.ClimbState);
            player.controller.isClimbing = true;
        }
    }
}
