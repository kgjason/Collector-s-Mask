using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_PPlatePlateAndButton : MonoBehaviour
{
    [Header("Connections")]
    public Gate targetGate;

    // Ön þartlar
    public PressurePlate pressurePlate;           // herhangi bir obje koyulduðunda aktif olan plate
    public PlayerPressurePlate playerPressure;    // oyuncu üzerinde ise objectCount > 0
    public Button button;                         // F ile aktif edilen buton (isActive)

    // Ýç durum takibi
    private bool gateIsOpen = false;

    private void Update()
    {
        // Referanslarýn baðlý olduðundan emin ol
        if (targetGate == null || pressurePlate == null || playerPressure == null || button == null)
            return;

        // Koþullar
        bool plateOk = pressurePlate.isActive;                 // PressurePlate aktif mi
        bool playerOk = playerPressure.objectCount > 0;        // PlayerPressurePlate üzerinde oyuncu var mý
        bool buttonOk = button.isActive;                       // Button aktif mi

        // Tümü true ise aç, deðilse kapa
        if (plateOk && playerOk && buttonOk)
        {
            if (!gateIsOpen)
            {
                targetGate.Activate();
                gateIsOpen = true;
            }
        }
        else
        {
            if (gateIsOpen)
            {
                targetGate.Deactivate();
                gateIsOpen = false;
            }
        }
    }
}
