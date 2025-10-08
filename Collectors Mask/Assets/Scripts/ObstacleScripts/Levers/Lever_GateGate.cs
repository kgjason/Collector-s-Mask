using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever_GateGate : MonoBehaviour
{
    public Gate gateA;
    public Gate gateB;

    public bool isActive = false; // leverýn durumu
    public float interactRange = 1.5f;
    public PlayerMovement player;

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= interactRange)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isActive = !isActive;
                ToggleGates();
            }
        }
    }

    private void ToggleGates()
    {
        if (isActive)
        {
            if (gateA != null) gateA.Activate();
            if (gateB != null) gateB.Deactivate();
        }
        else
        {
            if (gateA != null) gateA.Deactivate();
            if (gateB != null) gateB.Activate();
        }
    }
}
