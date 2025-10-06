using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_PPlatePlateAndPlate : MonoBehaviour
{
    public Gate targetGate;

    [Header("Plates Required")]
    public PressurePlate plate1;
    public PressurePlate plate2;
    public PlayerPressurePlate playerPlate;

    private void Update()
    {
        // Tüm üç plate aktif mi?
        if (plate1 != null && plate2 != null && playerPlate != null)
        {
            if (plate1.isActive && plate2.isActive && playerPlate.objectCount > 0)
            {
                targetGate?.Activate();
            }
            else
            {
                targetGate?.Deactivate();
            }
        }
    }
}
