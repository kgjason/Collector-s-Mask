using System.Collections;
using UnityEngine;

public class PressurePlateForDualGate : MonoBehaviour
{
    public int objectCount = 0;
    public bool isActive = false;
    [SerializeField] public TimeMask timeMask;
    private Coroutine delayedDeactivateCoroutine;

    [Header("Animation")]
    public Animator animator; //  Ekledik

    private void Start()
    {
        if (timeMask == null)
        {
            timeMask = FindObjectOfType<TimeMask>();
            if (timeMask == null)
                Debug.LogWarning($"PressurePlateForDualGate ({gameObject.name}): timeMask atanmamýþ!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectCount++;
        if (objectCount >= 1)
        {
            if (delayedDeactivateCoroutine != null)
            {
                StopCoroutine(delayedDeactivateCoroutine);
                delayedDeactivateCoroutine = null;
            }
            isActive = true;
            if (animator != null)
                animator.SetBool("isPressed", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        objectCount--;
        bool isTimeStopped = (timeMask != null && timeMask.isTimeStopped) || Time.timeScale == 0f;

        if (objectCount <= 0)
        {
            if (isTimeStopped)
            {
                delayedDeactivateCoroutine = StartCoroutine(DelayedDeactivate(3f));
            }
            else
            {
                isActive = false;
                if (animator != null)
                    animator.SetBool("isPressed", false);
            }
        }
    }

    private IEnumerator DelayedDeactivate(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (objectCount <= 0)
        {
            isActive = false;
            if (animator != null)
                animator.SetBool("isPressed", false);
        }
        delayedDeactivateCoroutine = null;
    }
}
