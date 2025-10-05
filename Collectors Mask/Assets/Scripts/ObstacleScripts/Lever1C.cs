using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever1C : MonoBehaviour, IInteractable
{
    public Gate targetGate;
    public PressurePlateL activater;
    public bool isLightOn;
    public bool isActive;
    public float interactRange = 1.5f;
    public PlayerMovement player;
    private void Start()
    {
        isActive = false;
        isLightOn = false;
    }
    private void Update()
    {
        if (activater.isActive)
        {
            isLightOn = true;
        } else
        {
            isActive = false;
        }
        if (Vector3.Distance(player.transform.position, transform.position) <= interactRange && isLightOn)
        {

            if (Input.GetKeyDown(KeyCode.F))
            {
                isActive = !isActive;
                if (isActive)
                {
                    Activate();
                }
                else
                {
                    Deactivate();
                }
            }
        }
    }
    public void Activate()
    {
        //play lever turn on animasyonu
        if (targetGate != null)
        {
            targetGate.Activate();
        }
        Debug.Log("lever turned on");
    }
    public void Deactivate()
    {
        //play lever turn off animasyonu
        if (targetGate != null)
        {
            targetGate.Deactivate();
        }
        Debug.Log("lever turned off");
    }
}

