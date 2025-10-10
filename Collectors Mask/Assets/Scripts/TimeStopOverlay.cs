using UnityEngine;
using UnityEngine.UI;

public class TimeStopOverlay : MonoBehaviour
{
    [Header("Overlay Settings")]
    public Image overlayImage;
    public Color startColor = new Color(1f, 0.9f, 0f, 0.4f); // Sarý (RGBA)
    public Color endColor = new Color(1f, 1f, 1f, 0f);       // Beyaz, saydam

    private float totalDuration;
    private float remainingTime;
    private bool isActive = false;

    private void Update()
    {
        if (!isActive)
            return;

        remainingTime -= Time.unscaledDeltaTime;

        float t = Mathf.Clamp01(1 - (remainingTime / totalDuration));

        // Sarýdan beyaza doðru geçiþ
        overlayImage.color = Color.Lerp(startColor, endColor, t);

        // Bittiðinde kapat
        if (remainingTime <= 0f)
        {
            EndOverlay();
        }
    }

    public void StartOverlay(float duration)
    {
        totalDuration = duration;
        remainingTime = duration;
        isActive = true;

        overlayImage.enabled = true;
        overlayImage.color = startColor;
    }

    public void EndOverlay()
    {
        isActive = false;
        overlayImage.enabled = false;
    }
}
