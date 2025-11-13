using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    [Header("Setting Value")]
    public float checkRate = 0.05f;
    private float lastCheckTime;
    [SerializeField] private float maxCheckDist;
    [SerializeField] private LayerMask interactablLayer;

    [Header("Setting UI")]
    [SerializeField] private TextMeshProUGUI interactTxt;

    public GameObject curInteractionObject;
    private IInteractable curInteractable;

    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if(interactable != null)
        {
            curInteractable = interactable;
            curInteractable.ShowInteractUI();
            interactTxt.text = curInteractable.GetInteractPrompt();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            if(curInteractable!=null) curInteractable.CloseInteractUI();
            curInteractable = null;
            interactTxt.text = string.Empty;
        }
    }

    public void InputInteract(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();
        }
    }
}
