using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string GetInteractPrompt();
    public void ShowInteractUI();
    public void CloseInteractUI();
    public void OnInteract();
}
