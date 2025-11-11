using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffSlot : MonoBehaviour
{
    // 버프 슬롯 : 버프의 지속시간 등을 표시

    [Header("Buff")]
    [SerializeField] private Buff buff;

    [Header("UI Component")]
    [SerializeField] private Image buffIcon;
    [SerializeField] private GameObject remainObject;
    [SerializeField] private TextMeshProUGUI remainTimeTxt;

    // 버프 슬롯 상태
    public bool isActive;

    private float curDuration;

    public void Init()
    {
        ResetSlot();
    }

    private void Update()
    {
        if (isActive)
        {
            remainTimeTxt.text = buff.duration.ToString("F1");
        }
    }

    public void Set(Buff _buff)
    {
        this.buff = _buff;

        remainObject.SetActive(true);
        buffIcon.gameObject.SetActive(true);

        buffIcon.sprite = buff.icon;
        curDuration = buff.duration;
        remainTimeTxt.text = curDuration.ToString("F1");

        isActive = true;
    }

    public void ResetSlot()
    {
        remainTimeTxt.text = 0.0f.ToString("F1");
        curDuration = 0.0f;

        buffIcon.gameObject.SetActive(false);
        remainObject.SetActive(false);
        isActive = false;
    }

    public Buff Buff { get { return buff; } }
}
