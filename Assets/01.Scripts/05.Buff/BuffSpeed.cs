using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpeed : Buff, IInteractable
{
    [Header("Speed Buff")]
    [SerializeField] private float accelearationSpeed;

    public override void Active()
    {

    }
}
