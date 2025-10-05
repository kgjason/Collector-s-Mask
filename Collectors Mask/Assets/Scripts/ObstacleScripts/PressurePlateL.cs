using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateL : MonoBehaviour, IInteractable
{
    public bool isActive;
    public int objectCount = 0;
    private void Start()
    {
        isActive = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {       
        objectCount++;
        if (objectCount >= 1)
        {
            isActive = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {        
        objectCount--;
        if (objectCount <= 0)
        {
            isActive = false;
        }
    }

    public void Activate()
    {
        return;
    }
    public void Deactivate()
    {
        return;
    }
}
