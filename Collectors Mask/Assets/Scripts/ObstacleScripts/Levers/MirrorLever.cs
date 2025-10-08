using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorLever : MonoBehaviour, IInteractable
{
    public Gate targetGate;
    public bool isLightOn;
    public bool isActive;
    public float interactRange = 1.5f;
    public MirrorPlayerMovement player; // PlayerMovement yerine MirrorPlayerMovement

    private void Start()
    {
        isActive = false;
        isLightOn = true;
    }

    private void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("Lever: MirrorPlayerMovement bileþeni atanmamýþ!");
            return;
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
        // Play lever turn on animasyonu
        if (targetGate != null)
        {
            targetGate.Activate();
        }
        Debug.Log("Lever turned on");
    }

    public void Deactivate()
    {
        // Play lever turn off animasyonu
        if (targetGate != null)
        {
            targetGate.Deactivate();
        }
        Debug.Log("Lever turned off");
    }
}