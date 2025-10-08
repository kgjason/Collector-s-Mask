using System.Collections;
using UnityEngine;

public class PlayerPressurePlate_ForCombo : MonoBehaviour, IInteractable
{
    [SerializeField] private MonoBehaviour targetObject;
    private IInteractable interactableTarget;

    [SerializeField] private TimeMask timeMask;

    public bool isActive = false;
    public int objectCount = 0;
    private Coroutine delayedCoroutine;

    private void Start()
    {
        if (targetObject != null)
            interactableTarget = targetObject as IInteractable;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

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
        if (!collision.CompareTag("Player")) return;

        objectCount--;
        if (objectCount <= 0)
        {
            // Zaman durdurulmuþsa (timeMask.isTimeStopped veya Time.timeScale kontrolü)
            bool isTimeStopped = (timeMask != null && timeMask.isTimeStopped) || Time.timeScale == 0f;
            Debug.Log($"OnTriggerExit2D: objectCount={objectCount}, isTimeStopped={isTimeStopped}, Time.timeScale={Time.timeScale}, isActive={isActive}");

            if (isTimeStopped)
            {
                if (delayedCoroutine != null)
                {
                    StopCoroutine(delayedCoroutine);
                    delayedCoroutine = null;
                }
                delayedCoroutine = StartCoroutine(DelayedDeactivate(3f));
            }
            else
            {
                Deactivate();
            }
        }
    }

    private IEnumerator DelayedDeactivate(float delay)
    {
        Debug.Log($"DelayedDeactivate started: delay={delay}s, isActive={isActive}");
        yield return new WaitForSeconds(delay);
        if (objectCount <= 0)
        {
            Debug.Log("DelayedDeactivate: Deactivating plate");
            Deactivate();
        }
        delayedCoroutine = null;
    }

    public void Activate()
    {
        isActive = true;
        interactableTarget?.Activate();
        Debug.Log("Plate Activated");
    }

    public void Deactivate()
    {
        isActive = false;
        interactableTarget?.Deactivate();
        Debug.Log("Plate Deactivated");
    }
}