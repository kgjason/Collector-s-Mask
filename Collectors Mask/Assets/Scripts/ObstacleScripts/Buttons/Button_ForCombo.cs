using System.Collections;
using UnityEngine;

public class Button_ForCombo : MonoBehaviour, IInteractable
{
    public bool isLightOn = true;           // Inspector'dan kontrol edilebilir
    public bool isActive = false;
    public float interactRange = 1.5f;
    public PlayerMovement player;
    public MirrorPlayerMovement mirrorPlayer;
    public TimeMask timeMask;

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
        if (player == null) return;

        if ((Vector3.Distance(player.transform.position, transform.position) <= interactRange && isLightOn) || (Vector3.Distance(mirrorPlayer.transform.position, transform.position) <= interactRange && isLightOn))
        {
            if (Input.GetKeyDown(KeyCode.F))
                StartButton(2f);
        }
    }

    public void StartButton(float duration)
    {
        if (buttonCoroutine != null)
            remainingTime += duration;
        else
        {
            remainingTime = duration;
            buttonCoroutine = StartCoroutine(ButtonCoroutine());
        }
    }

    private IEnumerator ButtonCoroutine()
    {
        Activate();

        while (remainingTime > 0f)
        {
            if (timeMask == null || !timeMask.isTimeStopped)
                remainingTime -= Time.deltaTime;

            yield return null;
        }

        Deactivate();
        buttonCoroutine = null;
    }

    public void Activate() => isActive = true;

    public void Deactivate() => isActive = false;
}
