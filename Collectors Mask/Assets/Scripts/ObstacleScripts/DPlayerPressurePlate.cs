using System.Collections;
using UnityEngine;

public class DPlayerPressurePlate : MonoBehaviour
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
                Debug.LogWarning($"PlayerPressurePlateForDualGate ({gameObject.name}): timeMask atanmamýþ veya bulunamadý!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            objectCount++;
            Debug.Log($"PlayerPressurePlateForDualGate ({gameObject.name}): OnTriggerEnter2D, objectCount={objectCount}, isActive={isActive}");
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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            objectCount--;
            bool isTimeStopped = (timeMask != null && timeMask.isTimeStopped) || Time.timeScale == 0f;
            Debug.Log($"PlayerPressurePlateForDualGate ({gameObject.name}): OnTriggerExit2D, objectCount={objectCount}, isTimeStopped={isTimeStopped}, Time.timeScale={Time.timeScale}, isActive={isActive}");
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
    }

    private IEnumerator DelayedDeactivate(float delay)
    {
        Debug.Log($"PlayerPressurePlateForDualGate ({gameObject.name}): DelayedDeactivate started, delay={delay}s");
        yield return new WaitForSeconds(delay);
        if (objectCount <= 0)
        {
            isActive = false;
            Debug.Log($"PlayerPressurePlateForDualGate ({gameObject.name}): DelayedDeactivate completed, isActive={isActive}");
        }
        delayedDeactivateCoroutine = null;
    }
}