using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskChanger : MonoBehaviour
{
    public int currentIndex = 0;
    public TimeMask timeMask;
    public CloneMask cloneMask;
    //public MirrorMask mirrorMask;
    private void Start()
    {
        timeMask = GetComponent<TimeMask>();
        cloneMask = GetComponent<CloneMask>();
        cloneMask.isCloneMaskActive = true;
        //mirrorMask = GetComponent<MirrorMask>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentIndex--;
            if (currentIndex < 0 )
            {
                currentIndex = currentIndex + 3;
            }
            ChangeMask(currentIndex);
        } else if (Input.GetKeyDown(KeyCode.E))
        {
            currentIndex++;
            currentIndex = currentIndex % 3;
            ChangeMask(currentIndex);
        }
    }
    public void ChangeMask(int index)
    {
        if (index == 0 && cloneMask.isCloneMaskObtained)
        {
            //mirror mask = false;
            timeMask.isTimeMaskActive = false;
            cloneMask.isCloneMaskActive = true;
        } else if (index == 1 && timeMask.isTimeMaskObtained)
        {
            //mirror mask = false;
            cloneMask.isCloneMaskActive = false;
            timeMask.isTimeMaskActive = true;
        } else if (index == 2)
        {
            timeMask.isTimeMaskActive = false;
            cloneMask.isCloneMaskActive = false;
            //mirror mask = true;
        }
    }
}
