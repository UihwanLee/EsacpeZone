using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffHeal : Buff
{
    [Header("Heal Buff")]
    [SerializeField] private float healAmount;

    Coroutine healBuffCoroutine;
    public override void Active()
    {
        if (isActive == false) return;

        if (healBuffCoroutine != null)
        {
            StopCoroutine(healBuffCoroutine);
            healBuffCoroutine = StartCoroutine(HealBuffCoroutine());
        }
        else
        {
            healBuffCoroutine = StartCoroutine(HealBuffCoroutine());
        }
    }

    private IEnumerator HealBuffCoroutine()
    {
        PlayerCondition condition = CharacterManager.Instance.Player.condition;

        while (duration > 0.0f)
        {
            duration -= Time.deltaTime;

            condition.health.Add(condition.health.passiveValue * 2 * Time.deltaTime);

            yield return null;
        }

        BuffManager.Instance.RemoveBuff(this);
        SetActive(false);

        yield return null;
    }
}
