using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ladder : MonoBehaviour, IInteractable
{
    [Header("Transform")]
    public Transform startPosition;
    public Transform endPosition;

    [Header("Component")]
    [SerializeField] private GameObject interactUI;

    private Player player;
    public bool isStartPosition;

    private void Start()
    {
        player = CharacterManager.Instance.Player;
        interactUI.SetActive(false);
    }

    public string GetInteractPrompt()
    {
        return String.Empty;
    }

    public void ShowInteractUI()
    {
        if (player.controller.isClimbing) return;

        isStartPosition = IsStartPosition();

        Vector3 uiPos = interactUI.transform.localPosition;
        uiPos.y = (isStartPosition) ? startPosition.localPosition.y : endPosition.localPosition.y;
        interactUI.transform.localPosition = uiPos;
        interactUI.SetActive(true);
    }

    public void CloseInteractUI()
    {
        interactUI.SetActive(false);
    }

    public void OnInteract()
    {
        player.detachObject = this.gameObject;
        player.stateMachine.ChangeState(player.condition.ClimbState);
        player.controller.isClimbing = true;
        interactUI.SetActive(false);
    }

    private bool IsStartPosition()
    {
        return (Vector3.Distance(startPosition.position, player.transform.position) < Vector3.Distance(endPosition.position, player.transform.position));
    }
}
