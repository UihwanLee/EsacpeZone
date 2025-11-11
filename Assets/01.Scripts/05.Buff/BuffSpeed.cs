using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpeed : Buff, IInteractable
{
    [Header("Speed Buff")]
    [SerializeField] private float accelearationSpeed;

    Coroutine speedBuffCoroutine;

    public override void Active()
    {
        if (isActive == false) return;

        if(speedBuffCoroutine != null)
        {
            StopCoroutine(speedBuffCoroutine);
            speedBuffCoroutine = StartCoroutine(SpeedBuffCoroutine());
        }
        else
        {
            speedBuffCoroutine = StartCoroutine(SpeedBuffCoroutine());
        }
    }

    private IEnumerator SpeedBuffCoroutine()
    {
        PlayerController controller = CharacterManager.Instance.Player.controller;

        while (duration > 0.0f)
        {
            duration -= Time.deltaTime;

            float curSpeed = Mathf.Lerp(Define.SPEED_MOVE, accelearationSpeed, (maxDuration - duration) / maxDuration);
            controller.ChangeSpeed(curSpeed);

            yield return null;
        }

        BuffManager.Instance.RemoveBuff(this);
        SetActive(false);

        yield return null;
    }
}
