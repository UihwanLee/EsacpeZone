using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    private void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            //CheckInteractionObject();
        }
    }

    private void CheckInteractionObject()
    {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, maxCheckDist, interactablLayer))
        {
            if(hit.collider.gameObject != curInteractionObject)
            {
                curInteractionObject = hit.collider.gameObject;
                curInteractable = curInteractionObject.GetComponent<IInteractable>();
                interactTxt.text = curInteractable.GetInteractPrompt();
            }
        }
        else
        {
            curInteractionObject = null;
            curInteractable= null;
            interactTxt.text = string.Empty;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if(interactable != null)
        {
            curInteractable = interactable;
            curInteractable.OnInteract();
            interactTxt.text = curInteractable.GetInteractPrompt();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            curInteractable = interactable;
            curInteractable.OnInteract();
            interactTxt.text = string.Empty;
        }
    }
}
