using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMask : MonoBehaviour
{
    [Header("Mask Settings")]
    public bool isTimeStopped = false;
    public bool isTimeMaskActive;
    public bool isTimeMaskObtained;
    [SerializeField] private float freezeDuration = 3f;

    [Header("References")]
    private List<ITimeFreezable> freezables = new List<ITimeFreezable>();
    private Button[] allButtons;
    private Coroutine freezeCoroutine;
    private TimeStopOverlay overlay; // yeni eklendi

    void Awake()
    {
        isTimeMaskObtained = false;

        // Sahnedeki tüm ITimeFreezable objeleri bul
        MonoBehaviour[] allObjects = FindObjectsOfType<MonoBehaviour>();
        foreach (var obj in allObjects)
        {
            if (obj is ITimeFreezable freezable)
                freezables.Add(freezable);
        }

        // Sahnedeki tüm buttonlarý bir kez bul
        allButtons = FindObjectsOfType<Button>();

        // Overlay referansýný bul
        overlay = FindObjectOfType<TimeStopOverlay>();
        if (overlay == null)
        {
            Debug.LogWarning("TimeStopOverlay bulunamadý! (Canvas'a eklemeyi unutma)");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isTimeMaskActive && isTimeMaskObtained)
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

    private IEnumerator FreezeForDuration()
    {
        FreezeWorld();

        float elapsed = 0f;
        while (elapsed < freezeDuration)
        {
            // Button aktifse süresini uzat ve countdown duracak
            foreach (var b in allButtons)
            {
                if (b.isActive)
                    b.ExtendDuration(Time.unscaledDeltaTime); // ek süre ver
            }

            elapsed += Time.unscaledDeltaTime; // zaman durduðu için unscaled
            yield return null;
        }

        UnfreezeWorld();
    }

    public void FreezeWorld()
    {
        isTimeStopped = true;

        // Overlay efektini baþlat
        if (overlay != null)
            overlay.StartOverlay(freezeDuration);

        // Tüm ITimeFreezable objelerini durdur
        foreach (var f in freezables)
            f.FreezeTime();

        Debug.Log("Time Mask activated!");
    }

    public void UnfreezeWorld()
    {
        isTimeStopped = false;

        // Overlay efektini kapat
        if (overlay != null)
            overlay.EndOverlay();

        // Tüm ITimeFreezable objelerini tekrar hareket ettir
        foreach (var f in freezables)
            f.UnfreezeTime();

        Debug.Log("Time Mask deactivated!");
    }
}
