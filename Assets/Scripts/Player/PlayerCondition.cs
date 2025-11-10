using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConditonState
{
    None,
    Run,
    Die
}

public class PlayerCondition : MonoBehaviour
{
    // Player가 Control할 Condition : 체력, 스태미나 

    public Condition health;
    public Condition stamina;

    private ConditonState state;

    private void Start()
    {
        state = ConditonState.None;
    }

    private void Update()
    {
        PassvieUpdateByState();
    }

    private void PassvieUpdateByState()
    {
        switch(state)
        {
            case ConditonState.None:
                stamina.Add(stamina.passiveValue * Time.deltaTime);
                break;
            case ConditonState.Run:
                stamina.Subtract(stamina.passiveValue * Time.deltaTime);
                break;
            case ConditonState.Die:
                break;
            default:
                break;
        }

        health.Subtract(health.passiveValue * Time.deltaTime);
    }

    public void Jump()
    {
        // 점프 시 Stamina 소모
        stamina.Subtract(stamina.passiveValue * 4);
    }

    public void ChangeState(ConditonState state)
    {
        this.state = state;
    }
}
