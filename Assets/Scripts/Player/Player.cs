using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condtion;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        condtion = GetComponent<PlayerCondition>();
    }
}
