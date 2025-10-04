using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMask : MonoBehaviour
{
    private bool isTimeStopped = false;
    public bool isTimeMaskActive;
    public bool isTimeMaskObtained;
    [SerializeField] private float freezeDuration = 3f;
    private List<ITimeFreezable> freezables = new List<ITimeFreezable>();
    private Coroutine freezeCoroutine;

    void Awake()
    {
        isTimeMaskObtained = true;
        MonoBehaviour[] allObjects = FindObjectsOfType<MonoBehaviour>();
        foreach (var obj in allObjects)
        {
            if (obj is ITimeFreezable freezable)
                freezables.Add(freezable);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isTimeMaskActive)
        {
            if (!isTimeStopped)
                StartFreezeTimer();
            else
                UnfreezeWorld();
        }
    }

    void StartFreezeTimer()
    {
        if (freezeCoroutine != null)
            StopCoroutine(freezeCoroutine);

        freezeCoroutine = StartCoroutine(FreezeForDuration());
    }

    IEnumerator FreezeForDuration()
    {
        FreezeWorld();
        yield return new WaitForSeconds(freezeDuration);
        UnfreezeWorld();
    }

    void FreezeWorld()
    {
        isTimeStopped = true;
        foreach (var f in freezables)
            f.FreezeTime();
        Debug.Log("Time Mask activated!");
    }

    void UnfreezeWorld()
    {
        isTimeStopped = false;
        foreach (var f in freezables)
            f.UnfreezeTime();
        Debug.Log("Time Mask deactivated!");
    }
}
