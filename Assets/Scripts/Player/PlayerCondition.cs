using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    // Player가 Control할 Condition : 체력, 스태미나 

    public Condition health;
    public Condition stamina;

    private void Update()
    {
        // 피 감소
        health.Subtract(health.passiveValue * Time.deltaTime); 

        // 스태미나 증가
        stamina.Add(stamina.passiveValue * Time.deltaTime);
    }

    public void Jump()
    {
        // 점프 시 Stamina 소모
        stamina.Subtract(stamina.passiveValue * 4);
    }
}
