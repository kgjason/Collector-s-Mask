using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour, IInteractable
{
    [SerializeField] private MonoBehaviour targetObject;
    private IInteractable interactableTarget;
    public bool isActive;
    public int objectCount = 0;
    private void Start()
    {
        if (targetObject != null)
            interactableTarget = targetObject as IInteractable;
        else
            Debug.Log("Target object not assigned!");
        isActive = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isActive = true;
        objectCount++;
        if (objectCount >= 1)
        {
            Activate();
        }     
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        objectCount--;
        if (objectCount <= 0)
        {
            Deactivate();
        }     
    }

    public void Activate()
    {
        //animasyon
        interactableTarget?.Activate();
    }
    public void Deactivate()
    {
        //animasyon
        interactableTarget?.Deactivate();
    }
}
