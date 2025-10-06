using System.Collections;
using UnityEngine;

public class PressurePlate : MonoBehaviour, IInteractable
{
    [SerializeField] private MonoBehaviour targetObject;
    private IInteractable interactableTarget;
    public bool isActive;
    public int objectCount = 0;
    public TimeMask timeMask; // scene'deki TimeMask referansý

    private void Start()
    {
        if (targetObject != null)
            interactableTarget = targetObject as IInteractable;
        else
            Debug.Log("Target object not assigned!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectCount++;
        if (objectCount >= 1)
            Activate();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        objectCount--;
        if (objectCount <= 0)
        {
            if (timeMask.isTimeStopped)
                StartCoroutine(DelayedDeactivate(3f)); // zaman durduysa bekle
            else
                Deactivate();
        }
    }

    private IEnumerator DelayedDeactivate(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (objectCount <= 0) // baþka biri basmamýþsa
            Deactivate();
    }

    public void Activate()
    {
        isActive = true;
        interactableTarget?.Activate();
    }

    public void Deactivate()
    {
        isActive = false;
        interactableTarget?.Deactivate();
    }
}
