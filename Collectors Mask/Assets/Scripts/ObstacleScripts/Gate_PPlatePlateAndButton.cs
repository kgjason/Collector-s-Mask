using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_PPlatePlateAndButton : MonoBehaviour
{
    [Header("Connections")]
    public Gate targetGate;

    // �n �artlar
    public PressurePlate pressurePlate;           // herhangi bir obje koyuldu�unda aktif olan plate
    public PlayerPressurePlate playerPressure;    // oyuncu �zerinde ise objectCount > 0
    public Button button;                         // F ile aktif edilen buton (isActive)

    // �� durum takibi
    private bool gateIsOpen = false;

    private void Update()
    {
        // Referanslar�n ba�l� oldu�undan emin ol
        if (targetGate == null || pressurePlate == null || playerPressure == null || button == null)
            return;

        // Ko�ullar
        bool plateOk = pressurePlate.isActive;                 // PressurePlate aktif mi
        bool playerOk = playerPressure.objectCount > 0;        // PlayerPressurePlate �zerinde oyuncu var m�
        bool buttonOk = button.isActive;                       // Button aktif mi

        // T�m� true ise a�, de�ilse kapa
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
