using System.Collections;
using UnityEngine;

public class PlayerPressurePlate_ForCombo : MonoBehaviour, IInteractable
{
    [SerializeField] private MonoBehaviour targetObject;
    private IInteractable interactableTarget;

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
        if (!collision.CompareTag("Player")) return;

        objectCount++;
        if (objectCount >= 1)
        {
            // Kapý kapanma coroutine’ini iptal et
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
            // Eðer zaten bekleyen coroutine varsa iptal et
            if (delayedCoroutine != null)
            {
                StopCoroutine(delayedCoroutine);
                delayedCoroutine = null;
            }

            // Coroutine baþlat
            delayedCoroutine = StartCoroutine(DelayedDeactivateCoroutine());
        }
    }

    private IEnumerator DelayedDeactivateCoroutine()
    {
        // Zaman duruyorsa bekle
        while (timeMask != null && timeMask.isTimeStopped)
            yield return null;

        // Zaman devam ettiðinde 3 saniye bekle
        yield return new WaitForSeconds(3f);

        // Eðer hala kimse yoksa kapat
        if (objectCount <= 0)
            Deactivate();

        delayedCoroutine = null;
    }

    public void Activate()
    {
        if (interactableTarget != null)
            interactableTarget.Activate();
    }

    public void Deactivate()
    {
        if (interactableTarget != null)
            interactableTarget.Deactivate();
    }
}
