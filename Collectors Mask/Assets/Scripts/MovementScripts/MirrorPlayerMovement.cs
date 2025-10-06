using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorPlayerMovement : MonoBehaviour, ITimeFreezable
{
    public int speed = 5;
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal") * -1;
        float y = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(x, y, 0).normalized;
        transform.position += move * speed * Time.deltaTime;
    }
    public void FreezeTime()
    {
        speed = 0;
    }
    public void UnfreezeTime()
    {
        speed = 5;
    }
}
