using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOnEnable : MonoBehaviour, IInteractable
{
    public Vector3 xOffSet = new Vector3(-3, 0, 0);
    public Vector3 yOffSet = new Vector3(0, 3, 0);
    public bool isMoved = false;

    public void Activate()
    {

        //play gate open animation
        if (isMoved != true)
        {
            if (Mathf.Approximately(transform.eulerAngles.z, 90f))
            {
                transform.position += yOffSet;
            }
            else
            {
                transform.position += xOffSet;
            }
        }
        isMoved = true;
    }
    public void Deactivate()
    {
        //play gate close animation
        if (isMoved == true)
        {
            if (Mathf.Approximately(transform.eulerAngles.z, 90f))
            {
                transform.position -= yOffSet;
            }
            else
            {
                transform.position -= xOffSet;
            }
        }
        isMoved = false;
    }
}
