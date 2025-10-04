using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour, IInteractable
{
    public Vector3 xOffSet = new Vector3(3, 0, 0);
    public Vector3 yOffSet = new Vector3(0, 3, 0);

    public void Activate()
    {
        //play gate open animation
        if (transform.rotation.z == 90)
        {
            transform.position += yOffSet;
        } else
        {
            transform.position -= xOffSet;
        }
    }
    public void Deactivate()
    {
        //play gate close animation
        if (transform.rotation.z == 90)
        {
            transform.position = -yOffSet;
        } else
        {
            transform.position -= xOffSet;
        }
    }
}
