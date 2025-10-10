using System.Collections;
using UnityEngine;

public class PlayerPressurePlate : MonoBehaviour, IInteractable
{
    [SerializeField] public MonoBehaviour targetObject;
    public bool isActive = false;
    public IInteractable interactableTarget;
    public int objectCount = 0;
    [SerializeField] public TimeMask timeMask;

    [Header("Animation")]
    public Animator animator; //  Ekledik

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
            if (objectCount <= 0)
            {
                if (isTimeStopped)
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
        isActive = true;
        interactableTarget?.Activate();
        animator?.SetBool("isPressed", true); //  Basýldýðýnda animasyon
    }

    public void Deactivate()
    {
        isActive = false;
        interactableTarget?.Deactivate();
        animator?.SetBool("isPressed", false); //  Ayaðýný çektiðinde animasyon
    }
}
