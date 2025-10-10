using System.Collections;
using UnityEngine;

public class PressurePlate : MonoBehaviour, IInteractable
{
    [SerializeField] private MonoBehaviour targetObject;
    private IInteractable interactableTarget;
    public bool isActive;
    public int objectCount = 0;
    public TimeMask timeMask;

    [Header("Animation")]
    public Animator animator; //  Ekledik

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
            if (timeMask != null && timeMask.isTimeStopped)
                StartCoroutine(DelayedDeactivate(3f));
            else
                Deactivate();
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
        isActive = true;
        interactableTarget?.Activate();
        if (animator != null)
            animator.SetBool("isPressed", true); //  Animasyon tetiklenir
    }

    public void Deactivate()
    {
        isActive = false;
        interactableTarget?.Deactivate();
        if (animator != null)
            animator.SetBool("isPressed", false); //  Geri animasyon
    }
}
