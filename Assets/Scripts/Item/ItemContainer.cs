using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string GetInteractPrompt();
    public void OnInteract();
}

public class ItemContainer : MonoBehaviour, IInteractable
{
    public ItemData item;

    public string GetInteractPrompt()
    {
        string str = $"{item.itemName}\n{item.description}";
        return str;
    }

    public virtual void OnInteract()
    {
        
    }
}
