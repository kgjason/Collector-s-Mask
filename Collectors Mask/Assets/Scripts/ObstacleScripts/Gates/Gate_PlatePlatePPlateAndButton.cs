using UnityEngine;

public class Gate_PlatePlatePPlateAndButton : MonoBehaviour
{
    [Header("Gate Settings")]
    public Gate targetGate;

    [Header("Required Inputs")]
    public PressurePlate plate1;
    public PressurePlate plate2;
    public PlayerPressurePlate playerPlate;
    public Button button;

    private bool isActive = false;

    private void Update()
    {
        if (plate1 == null || plate2 == null || playerPlate == null || button == null || targetGate == null)
            return;

        bool allActive =
            plate1.isActive &&
            plate2.isActive &&
            playerPlate.objectCount > 0 &&
            button.isActive;

        // Durum deðiþtiðinde sadece o zaman tetikle
        if (allActive && !isActive)
        {
            isActive = true;
            Activate();
        }
        else if (!allActive && isActive)
        {
            isActive = false;
            Deactivate();
        }
    }

    private void Activate()
    {
        targetGate.Activate();
        Debug.Log("Gate (Plate+Plate+PlayerPlate+Button) opened!");
    }

    private void Deactivate()
    {
        targetGate.Deactivate();
        Debug.Log("Gate (Plate+Plate+PlayerPlate+Button) closed!");
    }
}
