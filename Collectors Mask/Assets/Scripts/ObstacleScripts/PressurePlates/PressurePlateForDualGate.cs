using System.Collections;
using UnityEngine;

public class PressurePlateForDualGate : MonoBehaviour
{
    public int objectCount = 0;
    public bool isActive = false;
    [SerializeField] public TimeMask timeMask;
    private Coroutine delayedDeactivateCoroutine;

    private void Start()
    {
        if (timeMask == null)
        {
            timeMask = FindObjectOfType<TimeMask>();
            if (timeMask == null)
                Debug.LogWarning($"PressurePlateForDualGate ({gameObject.name}): timeMask atanmamýþ veya bulunamadý!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectCount++;
        Debug.Log($"PressurePlateForDualGate ({gameObject.name}): OnTriggerEnter2D, objectCount={objectCount}, isActive={isActive}, Collider={collision.gameObject.name}");
        if (objectCount >= 1)
        {
            if (delayedDeactivateCoroutine != null)
            {
                StopCoroutine(delayedDeactivateCoroutine);
                delayedDeactivateCoroutine = null;
            }
            isActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        objectCount--;
        bool isTimeStopped = (timeMask != null && timeMask.isTimeStopped) || Time.timeScale == 0f;
        Debug.Log($"PressurePlateForDualGate ({gameObject.name}): OnTriggerExit2D, objectCount={objectCount}, isTimeStopped={isTimeStopped}, Time.timeScale={Time.timeScale}, isActive={isActive}, Collider={collision.gameObject.name}");
        if (objectCount <= 0)
        {
            if (isTimeStopped)
            {
                delayedDeactivateCoroutine = StartCoroutine(DelayedDeactivate(3f));
            }
            else
            {
                isActive = false;
            }
        }
    }

    private IEnumerator DelayedDeactivate(float delay)
    {
        Debug.Log($"PressurePlateForDualGate ({gameObject.name}): DelayedDeactivate started, delay={delay}s");
        yield return new WaitForSeconds(delay);
        if (objectCount <= 0)
        {
            isActive = false;
            Debug.Log($"PressurePlateForDualGate ({gameObject.name}): DelayedDeactivate completed, isActive={isActive}");
        }
        delayedDeactivateCoroutine = null;
    }
}