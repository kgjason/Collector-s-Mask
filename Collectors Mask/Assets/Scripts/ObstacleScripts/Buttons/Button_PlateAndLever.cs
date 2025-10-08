using System.Collections;
using UnityEngine;

public class Button_PlateAndLever : MonoBehaviour, IInteractable
{
    [Header("Connections")]
    public PlayerMovement player;
    public TimeMask timeMask;
    public bool isLightOn;
    public bool isActive;
    public float interactRange = 1.5f;

    private Coroutine buttonCoroutine;
    private float remainingTime = 0f;

    private void Start()
    {
        if (player == null)
            player = FindObjectOfType<PlayerMovement>();

        if (timeMask == null)
            timeMask = FindObjectOfType<TimeMask>();
    }

    private void Update()
    {
        // Oyuncu yak�nsa ve buton ����� yan�yorsa (kullan�labilir durumdaysa)
        if (Vector3.Distance(player.transform.position, transform.position) <= interactRange && isLightOn)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartButton(2f);
            }
        }
    }

    public void StartButton(float duration)
    {
        if (buttonCoroutine != null)
        {
            remainingTime += duration; // zaten aktifse s�re ekle
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
            if (!timeMask.isTimeStopped) // zaman durmad�ysa normal countdown
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
    }

    public void Deactivate()
    {
        isActive = false;
    }
}
