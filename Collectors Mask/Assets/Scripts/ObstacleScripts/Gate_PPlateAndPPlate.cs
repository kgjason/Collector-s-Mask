using UnityEngine;

public class Gate_PPlateAndPPlate : MonoBehaviour
{
    [Header("Baðlantýlar")]
    [SerializeField] private DPlayerPressurePlate plateA;
    [SerializeField] private DPlayerPressurePlate plateB;
    [SerializeField] private RetrySystem retrySystem;

    [Header("Kapý Ayarlarý")]
    [SerializeField] private Vector3 xOffSet = new Vector3(3, 0, 0);
    [SerializeField] private Vector3 yOffSet = new Vector3(0, 3, 0);
    private bool isMoved = false;

    private void Start()
    {
        // Null kontrolleri
        if (plateA == null || plateB == null)
        {
            Debug.LogError($"Gate_PPlateAndPPlate ({gameObject.name}): PlateA veya PlateB atanmamýþ! PlateA={plateA}, PlateB={plateB}");
            enabled = false;
            return;
        }
        if (retrySystem == null)
        {
            retrySystem = FindObjectOfType<RetrySystem>();
            if (retrySystem == null)
                Debug.LogError($"Gate_PPlateAndPPlate ({gameObject.name}): RetrySystem bulunamadý!");
        }
    }

    private void Update()
    {
        if (plateA == null || plateB == null)
        {
            Debug.LogWarning($"Gate_PPlateAndPPlate ({gameObject.name}): PlateA veya PlateB eksik!");
            return;
        }

        bool bothPlatesActive = plateA.isActive && plateB.isActive;
        Debug.Log($"[Gate_PPlateAndPPlate] ({gameObject.name}) PlateA: {plateA.isActive}, PlateB: {plateB.isActive}, BothActive: {bothPlatesActive}, isMoved: {isMoved}, TimeStopped: {(plateA.timeMask != null ? plateA.timeMask.isTimeStopped : false)}");

        if (bothPlatesActive && !isMoved)
        {
            Activate();
        }
        else if (!bothPlatesActive && isMoved)
        {
            Deactivate();
        }
    }

    public void Activate()
    {
        if (isMoved) return;

        if (Mathf.Approximately(transform.eulerAngles.z, 90f))
            transform.position += yOffSet;
        else
            transform.position += xOffSet;

        isMoved = true;
        Debug.Log($"Gate_PPlateAndPPlate ({gameObject.name}): Gate opened!");

        if (retrySystem != null)
        {
            retrySystem.LevelPassed();
            Debug.Log($"Gate_PPlateAndPPlate ({gameObject.name}): LevelPassed called, new puzzleIndex: {retrySystem.puzzleIndex}");
        }
    }

    public void Deactivate()
    {
        if (!isMoved) return;

        if (Mathf.Approximately(transform.eulerAngles.z, 90f))
            transform.position -= yOffSet;
        else
            transform.position -= xOffSet;

        isMoved = false;
        Debug.Log($"Gate_PPlateAndPPlate ({gameObject.name}): Gate closed!");
    }
}