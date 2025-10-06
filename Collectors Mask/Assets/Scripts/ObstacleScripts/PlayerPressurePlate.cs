using System.Collections;
using UnityEngine;

public class PlayerPressurePlate : MonoBehaviour, IInteractable
{
    [SerializeField] private MonoBehaviour targetObject;
    private IInteractable interactableTarget;
    public int objectCount = 0;
    public TimeMask timeMask;

    private void Start()
    {
        if (targetObject != null)
            interactableTarget = targetObject as IInteractable;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            objectCount++;
            if (objectCount >= 1)
                Activate();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            objectCount--;
            if (objectCount <= 0)
            {
                if (timeMask.isTimeStopped)
                    StartCoroutine(DelayedDeactivate(3f));
                else
                    Deactivate();
            }
        }
    }

    private IEnumerator DelayedDeactivate(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (objectCount <= 0)
            Deactivate();
    }

    public void Activate()
    {
        interactableTarget?.Activate();
    }

    public void Deactivate()
    {
        interactableTarget?.Deactivate();
    }
}
