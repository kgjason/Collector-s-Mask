using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_PlateAndPlate : MonoBehaviour
{
    [Header("Ba�lant�lar")]
    [SerializeField] private PressurePlate plateA;
    [SerializeField] private PressurePlate plateB;

    [Header("Kap� Ayarlar�")]
    public Vector3 xOffSet = new Vector3(3, 0, 0);
    public Vector3 yOffSet = new Vector3(0, 3, 0);
    private bool isMoved = false;

    private void Start()
    {
        if (plateA == null || plateB == null)
        {
            Debug.LogError("DualPressureGate: �ki PressurePlate atanmal�!");
        }
    }

    private void Update()
    {
        // �ki plaka da aktifse kap� a��l�r, biri pasifse kapan�r
        if (plateA.isActive && plateB.isActive)
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
        if (isMoved) return;

        if (Mathf.Approximately(transform.eulerAngles.z, 90f))
            transform.position += yOffSet;
        else
            transform.position += xOffSet;

        isMoved = true;
        Debug.Log("Gate opened!");
    }

    public void Deactivate()
    {
        if (!isMoved) return;

        if (Mathf.Approximately(transform.eulerAngles.z, 90f))
            transform.position -= yOffSet;
        else
            transform.position -= xOffSet;

        isMoved = false;
        Debug.Log("Gate closed!");
    }
}
