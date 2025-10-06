using System.Collections;
using UnityEngine;

public class Button : MonoBehaviour, IInteractable
{
    public Gate targetGate;
    public bool isLightOn;
    public bool isActive;
    public float interactRange = 1.5f;
    public PlayerMovement player;
    public TimeMask timeMask; // scene'deki TimeMask referansý

    private Coroutine buttonCoroutine;
    private float remainingTime = 0f;

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= interactRange && isLightOn)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartButton(3f);
            }
        }
    }

    public void StartButton(float duration)
    {
        if (buttonCoroutine != null)
        {
            remainingTime += duration; // zaten aktifse süre ekle
        }
        else
        {
            remainingTime = duration;
            buttonCoroutine = StartCoroutine(ButtonCoroutine());
        }
    }

    private IEnumerator ButtonCoroutine()
    {
        Activate();

        while (remainingTime > 0)
        {
            if (!timeMask.isTimeStopped) // zaman durmadýysa normal countdown
                remainingTime -= Time.deltaTime;

            yield return null;
        }

        Deactivate();
        buttonCoroutine = null;
    }

    public void ExtendDuration(float extraTime)
    {
        remainingTime += extraTime;
    }

    public void Activate()
    {
        isActive = true;
        targetGate?.Activate();
    }

    public void Deactivate()
    {
        isActive = false;
        targetGate?.Deactivate();
    }
}
