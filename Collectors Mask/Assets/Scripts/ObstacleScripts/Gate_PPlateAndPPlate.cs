using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_PPlateAndPPlate : MonoBehaviour, IInteractable
{
    [Header("Baðlantýlar")]
    [SerializeField] private PlayerPressurePlate plateA;
    [SerializeField] private PlayerPressurePlate plateB;

    [Header("Kapý Ayarlarý")]
    public Vector3 xOffSet = new Vector3(3, 0, 0);
    public Vector3 yOffSet = new Vector3(0, 3, 0);
    private bool isMoved = false;

    private void Start()
    {
        if (plateA == null || plateB == null)
        {
            Debug.LogError("DualPlayerGate: Ýki PlayerPressurePlate atanmalý!");
            return;
        }
    }

    private void Update()
    {
        // Her frame kontrol et: iki plate de aktif mi?
        if (plateA.objectCount > 0 && plateB.objectCount > 0)
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
        // animasyon oynatýlabilir
    }

    public void Deactivate()
    {
        if (!isMoved) return;

        if (Mathf.Approximately(transform.eulerAngles.z, 90f))
            transform.position -= yOffSet;
        else
            transform.position -= xOffSet;

        isMoved = false;
        // animasyon oynatýlabilir
    }
}
