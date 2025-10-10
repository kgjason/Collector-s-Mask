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

    [Header("Animation")]
    public Animator animator; //  Ekledik

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
            bool isTimeStopped = (timeMask != null && timeMask.isTimeStopped) || Time.timeScale == 0f;
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
        yield return new WaitForSeconds(delay);
        if (objectCount <= 0)
            Deactivate();
        delayedCoroutine = null;
    }

    public void Activate()
    {
        isActive = true;
        interactableTarget?.Activate();
        animator?.SetBool("isPressed", true); //  Press anim
    }

    public void Deactivate()
    {
        isActive = false;
        interactableTarget?.Deactivate();
        animator?.SetBool("isPressed", false); //  Release anim
    }
}
