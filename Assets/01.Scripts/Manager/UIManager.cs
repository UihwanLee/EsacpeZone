using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject ui_Die;

    public void SetDieUI(bool active)
    {
        ui_Die.SetActive(active);
    }

    public GameObject UI_Die { get { return ui_Die; } set { ui_Die = value; } }
}
