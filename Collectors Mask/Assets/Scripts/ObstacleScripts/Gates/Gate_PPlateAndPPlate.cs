using UnityEngine;

public class Gate_PPlateAndPPlate : MonoBehaviour
{
    [Header("Baðlantýlar")]
    [SerializeField] private DPlayerPressurePlate plateA;
    [SerializeField] private DPlayerPressurePlate plateB;

    [Header("Kapý Ayarlarý")]
    [SerializeField] private Vector3 xOffSet = new Vector3(3, 0, 0);
    [SerializeField] private Vector3 yOffSet = new Vector3(0, 3, 0);
    private bool isMoved = false;

    private void Start()
    {
        if (plateA == null || plateB == null)
        {
            Debug.LogError($"Gate ({gameObject.name}): PlateA veya PlateB atanmamýþ!");
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        if (plateA == null || plateB == null) return;

        bool bothPlatesActive = plateA.isActive && plateB.isActive;

        if (bothPlatesActive && !isMoved)
        {
            Activate();
        }
        else if (!bothPlatesActive && isMoved)
        {
            Deactivate();
        }
    }

    private void Activate()
    {
        if (isMoved) return;

        if (Mathf.Approximately(transform.eulerAngles.z, 90f))
            transform.position += yOffSet;
        else
            transform.position += xOffSet;

        isMoved = true;
        Debug.Log($"Gate ({gameObject.name}): Gate opened!");
    }

    private void Deactivate()
    {
        if (!isMoved) return;

        if (Mathf.Approximately(transform.eulerAngles.z, 90f))
            transform.position -= yOffSet;
        else
            transform.position -= xOffSet;

        isMoved = false;
        Debug.Log($"Gate ({gameObject.name}): Gate closed!");
    }
}
