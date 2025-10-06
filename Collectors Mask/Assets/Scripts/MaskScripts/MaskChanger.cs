using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskChanger : MonoBehaviour
{
    public GameObject[] maskWheel = new GameObject[3];
    public int currentCloneIndex = 0;
    public int currentTimeIndex = 1;
    public int currentMirrorIndex = 2;
    public int currentIndex = 0;
    public Sprite emptyMaskSprite;
    public Sprite cloneMaskSprite;
    public Sprite timeMaskSprite;
    public Sprite mirrorMaskSprite;
    public TimeMask timeMask;
    public CloneMask cloneMask;
    public MirrorCloneMask mirrorMask;

    private void Start()
    {
        timeMask = GetComponent<TimeMask>();
        cloneMask = GetComponent<CloneMask>();
        // mirrorMask = GetComponent<MirrorMask>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TurnUILeft();
            UpdateUI();
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex += 3;
            }
            ChangeMask(currentIndex);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            TurnUIRight();
            UpdateUI();
            currentIndex++;
            currentIndex = currentIndex % 3;
            ChangeMask(currentIndex);
        }
    }

    public void ChangeMask(int index)
    {
        if (index == 0 && cloneMask.isCloneMaskObtained)
        {
            // mirror mask = false;
            timeMask.isTimeMaskActive = false;
            cloneMask.isCloneMaskActive = true;
        }
        else if (index == 1 && timeMask.isTimeMaskObtained)
        {
            // mirror mask = false;
            cloneMask.isCloneMaskActive = false;
            timeMask.isTimeMaskActive = true;
        }
        else if (index == 2)
        {
            timeMask.isTimeMaskActive = false;
            cloneMask.isCloneMaskActive = false;
            // mirror mask = true;
        }
    }
    public void UpdateUI()
    {
        if (cloneMask.isCloneMaskObtained == false)
        {
            maskWheel[currentCloneIndex].GetComponent<SpriteRenderer>().sprite = emptyMaskSprite;
        } else
        {
            maskWheel[currentCloneIndex].GetComponent<SpriteRenderer>().sprite = cloneMaskSprite;
        }
        if (timeMask.isTimeMaskObtained == false)
        {
            maskWheel[currentTimeIndex].GetComponent<SpriteRenderer>().sprite = emptyMaskSprite;
        }
        else
        {
            maskWheel[currentTimeIndex].GetComponent<SpriteRenderer>().sprite = timeMaskSprite;
        }
        if (mirrorMask.isMirrorMaskObtained == false)
        {
            maskWheel[currentMirrorIndex].GetComponent<SpriteRenderer>().sprite = emptyMaskSprite;
        }
        else
        {
            maskWheel[currentMirrorIndex].GetComponent<SpriteRenderer>().sprite = mirrorMaskSprite;
        }
    }
    public void TurnUIRight()
    {
        int tempIndex = currentCloneIndex;
        currentCloneIndex = currentTimeIndex;
        currentTimeIndex = currentMirrorIndex;
        currentMirrorIndex = tempIndex;
    }
    public void TurnUILeft()
    {
        int tempIndex = currentTimeIndex;
        currentTimeIndex = currentCloneIndex;
        currentCloneIndex = currentMirrorIndex;
        currentMirrorIndex = tempIndex;        
    }
}
