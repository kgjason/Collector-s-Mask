using System.Collections;
using UnityEngine;

public class PlayerPressurePlate : MonoBehaviour, IInteractable
{
    [SerializeField] public MonoBehaviour targetObject;
    public bool isActive = false;
    public IInteractable interactableTarget;
    public int objectCount = 0;
    [SerializeField] public TimeMask timeMask;

    private void Start()
    {
        if (targetObject != null)
        {
            interactableTarget = targetObject as IInteractable;
            Debug.Log($"PlayerPressurePlate: targetObject set to {targetObject?.name}, interactableTarget={(interactableTarget != null ? interactableTarget.GetType().Name : "null")}");
        }
        else
        {
            Debug.LogWarning($"PlayerPressurePlate ({gameObject.name}): targetObject atanmamýþ!");
        }

        if (timeMask == null)
        {
            Debug.LogWarning($"PlayerPressurePlate ({gameObject.name}): timeMask atanmamýþ!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            objectCount++;
            Debug.Log($"PlayerPressurePlate ({gameObject.name}): OnTriggerEnter2D, objectCount={objectCount}, targetObject={targetObject?.name}");
            if (objectCount >= 1)
                Activate();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            objectCount--;
            bool isTimeStopped = (timeMask != null && timeMask.isTimeStopped) || Time.timeScale == 0f;
            Debug.Log($"PlayerPressurePlate ({gameObject.name}): OnTriggerExit2D, objectCount={objectCount}, isTimeStopped={isTimeStopped}, Time.timeScale={Time.timeScale}, targetObject={targetObject?.name}");
            if (objectCount <= 0)
            {
                if (isTimeStopped)
                {
                    StartCoroutine(DelayedDeactivate(3f));
                }
                else
                {
                    Deactivate();
                }
            }
        }
    }

    private IEnumerator DelayedDeactivate(float delay)
    {
        Debug.Log($"PlayerPressurePlate ({gameObject.name}): DelayedDeactivate started, delay={delay}s");
        yield return new WaitForSeconds(delay);
        if (objectCount <= 0)
        {
            Debug.Log($"PlayerPressurePlate ({gameObject.name}): DelayedDeactivate completed, deactivating");
            Deactivate();
        }
    }

    public void Activate()
    {
        isActive = true;
        interactableTarget?.Activate();
        Debug.Log($"PlayerPressurePlate ({gameObject.name}): Activate called, targetObject={targetObject?.name}");
    }

    public void Deactivate()
    {
        isActive = false;
        interactableTarget?.Deactivate();
        Debug.Log($"PlayerPressurePlate ({gameObject.name}): Deactivate called, targetObject={targetObject?.name}");
    }
}