using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    // 상태를 나타내는 클래스

    [SerializeField] private float curValue = 0.0f;
    [SerializeField] private float maxValue = 100.0f;

    public float passiveValue = 5.0f;

    [SerializeField] private Image uiBar;

    private void Awake()
    {
        uiBar = GetComponent<Image>();
    }

    private void Start()
    {
        curValue = maxValue;
    }

    private void Update()
    {
        uiBar.fillAmount = GetPersentage();
    }

    private float GetPersentage()
    {
        return curValue / maxValue;
    }

    public void Add(float value)
    {
        curValue = Mathf.Min(curValue + value, maxValue);
    }

    public void Subtract(float value)
    {
        curValue = Mathf.Max(curValue - value, 0f);
    }

    public float CurValue { get { return curValue; } }
}
