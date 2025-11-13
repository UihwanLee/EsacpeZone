using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public StateMachine stateMachine;
    public PlayerController controller;
    public PlayerCondition condition;

    public JumpingPad jumpingPad;
    public GameObject detachObject;

    public GameObject UI_Die;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        stateMachine = GetComponent<StateMachine>();
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }
}
