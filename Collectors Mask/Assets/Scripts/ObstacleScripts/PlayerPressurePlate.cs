using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPressurePlate : MonoBehaviour, IInteractable
{
    [SerializeField] private MonoBehaviour targetObject;
    private IInteractable interactableTarget;
    public int objectCount = 0;
    private void Start()
    {
        if (targetObject != null)
            interactableTarget = targetObject as IInteractable;
        else
            Debug.Log("Target object not assigned!");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            objectCount++;
            if (objectCount >= 1)
            {
                Activate();
            }
        }       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            objectCount--;
            if (objectCount <= 0)
            {
                Deactivate();
            }
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
