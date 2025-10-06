using System.Collections;
using System.Collections.Generic;
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

    private void Update()
    {
        // Tüm ön þartlar aktif mi?
        bool allActive = plate1 != null && plate1.isActive &&
                         plate2 != null && plate2.isActive &&
                         playerPlate != null && playerPlate.objectCount > 0 &&
                         button != null && button.isActive;

        if (allActive)
        {
            Activate();
        }
        else
        {
            Deactivate();
        }
    }

    public void Activate()
    {
        if (targetGate != null)
            targetGate.Activate();
    }

    public void Deactivate()
    {
        if (targetGate != null)
            targetGate.Deactivate();
    }
}
