using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string GetInteractPrompt();
    public void ShowInteractUI();
    public void OnInteract();
}

public class ItemContainer : MonoBehaviour, IInteractable
{
    public ItemData item;
    public GameObject interactUI;

    [SerializeField] private float accelearationSpeed;
    [SerializeField] private float duration;

    private void Start()
    {
        interactUI.SetActive(false);
    }

    public string GetInteractPrompt()
    {
        string str = $"{item.itemName}\n{item.description}";
        return str;
    }

    public void OnInteract()
    {
        CharacterManager.Instance.Player.condition.ChangeState(ConditonState.Consume);
        StartCoroutine(AccelearationCoroutine());
    }

    public void ShowInteractUI()
    {
        interactUI.SetActive(!interactUI.activeSelf);
    }

    private IEnumerator AccelearationCoroutine()
    {
        float time = 0.0f;

        while(time < duration)
        {
            time += Time.deltaTime;

            float curSpeed = Mathf.Lerp(5.0f, accelearationSpeed,time / duration);
            CharacterManager.Instance.Player.controller.ChangeSpeed(curSpeed);

            yield return null;
        }

        CharacterManager.Instance.Player.condition.ChangeState(ConditonState.None);
        CharacterManager.Instance.Player.controller.ChangeSpeed(5.0f);
        yield return null;
    }
}
