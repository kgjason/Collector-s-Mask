using System.Collections;
using UnityEngine;

public class PressurePlate_ForCombo : MonoBehaviour, IInteractable
{
    [SerializeField] private MonoBehaviour targetObject;
    private IInteractable interactableTarget;

    public bool isActive = false;
    public int objectCount = 0;
    public TimeMask timeMask;

    private Coroutine delayedCoroutine;

    private void Start()
    {
        if (targetObject != null)
            interactableTarget = targetObject as IInteractable;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectCount++;
        if (objectCount >= 1)
        {
            if (delayedCoroutine != null)
            {
                StopCoroutine(delayedCoroutine);
                delayedCoroutine = null;
            }
            Activate();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        objectCount--;
        if (objectCount <= 0)
        {
            if (timeMask != null && timeMask.isTimeStopped)
                delayedCoroutine = StartCoroutine(DelayedDeactivate(3f));
            else
                Deactivate();
        }
    }

    private IEnumerator DelayedDeactivate(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (objectCount <= 0)
            Deactivate();
        delayedCoroutine = null;
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
