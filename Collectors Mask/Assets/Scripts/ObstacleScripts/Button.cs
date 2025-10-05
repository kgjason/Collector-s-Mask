using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour, IInteractable
{
    public Gate targetGate;
    public bool isLightOn;
    public bool isActive;
    public float interactRange = 1.5f;
    public PlayerMovement player;
    private void Start()
    {
        isActive = false;
        isLightOn = true;
    }
    private void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= interactRange && isLightOn && !isActive)
        {

            if (Input.GetKeyDown(KeyCode.F))
            {
                isActive = true;
                StartCoroutine("ButtonCoroutine");
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
        Debug.Log("button turned on");
    }
    public void Deactivate()
    {
        //play lever turn off animasyonu
        if (targetGate != null)
        {
            targetGate.Deactivate();
        }
        Debug.Log("button turned off");
    }
    public IEnumerator ButtonCoroutine()
    {
        Activate();
        yield return new WaitForSeconds(3);
        isActive = false;
        Deactivate();
    }
}
