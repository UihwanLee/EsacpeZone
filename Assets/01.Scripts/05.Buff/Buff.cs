using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Buff : MonoBehaviour, IInteractable
{
    [Header("BuffData")]
    [SerializeField] private BuffData buff;

    [Header("BuffInfo")]
    public string buffName;         // 버프 이름
    public string description;      // 버프 설명
    public BuffType type;           // 버프 타입
    public float duration;          // 버프 지속시간
    public Sprite icon;             // 버프 아이콘

    [Header("Component")]
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject interactUI;
    [SerializeField] private TextMeshProUGUI interactTitle;

    public bool isActive;

    protected float maxDuration;

    public Action eventBuffOff;     // 버프가 꺼질 때 이벤트 
    private void Awake()
    {
        if(buff == null)
        {
            Debug.Log("Buff를 설정해주십시오.");
            return;
        }

        Init();
    }

    private void Init()
    {
        buffName = buff.name;
        description = buff.description;
        type = buff.type;
        duration = buff.duration;
        maxDuration = buff.duration;
        icon = buff.icon;

        interactTitle.text = buff.name;
        interactUI.SetActive(false);
    }

    public string GetInteractPrompt()
    {
        string str = $"{buffName}\n{description}";
        return str;
    }

    public void OnInteract()
    {
    }

    public void ShowInteractUI()
    {
        if(isActive == false)
        {
            interactUI.SetActive(false);
            return;
        }

        interactUI.SetActive(!interactUI.activeSelf);
    }

    public virtual void BuffOn()
    {
        // 활성화 되어 있는 상태만 적용
        if (!model.gameObject.activeSelf) return;

        // 버프 창 넘기기
        BuffManager.Instance.AddBuff(this);
        model.SetActive(false);

        // Buff 적용은 각자 기능에 따라 구현
    }

    private void BuffOff()
    {
        // 버프가 꺼지면 event 발생
        eventBuffOff?.Invoke();
    }

    public void SetActive(bool active)
    {
        isActive = active;
    }

    public void ResetDuration()
    {
        // duration 초기화
        duration = maxDuration;
    }
}
