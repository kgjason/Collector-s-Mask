using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskChanger : MonoBehaviour
{
    public GameObject[] maskWheel = new GameObject[3]; // UI pozisyonlarý
    public int currentCloneIndex = 0;
    public int currentTimeIndex = 1;
    public int currentMirrorIndex = 2;

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
        mirrorMask = GetComponent<MirrorCloneMask>();
        UpdateUI();
        UpdateActiveMask();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TurnUILeft();
            UpdateUI();
            UpdateActiveMask();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            TurnUIRight();
            UpdateUI();
            UpdateActiveMask();
        }
    }

    public void UpdateActiveMask()
    {
        // Önce tüm maskeleri pasif yap
        cloneMask.isCloneMaskActive = false;
        timeMask.isTimeMaskActive = false;
        mirrorMask.isMirrorMaskActive = false;

        // UI'daki 1. pozisyondaki mask aktif olsun
        if (currentCloneIndex == 1 && cloneMask.isCloneMaskObtained)
            cloneMask.isCloneMaskActive = true;
        else if (currentTimeIndex == 1 && timeMask.isTimeMaskObtained)
            timeMask.isTimeMaskActive = true;
        else if (currentMirrorIndex == 1 && mirrorMask.isMirrorMaskObtained)
            mirrorMask.isMirrorMaskActive = true;
    }

    public void UpdateUI()
    {
        // Clone mask
        maskWheel[currentCloneIndex].GetComponent<SpriteRenderer>().sprite =
            cloneMask.isCloneMaskObtained ? cloneMaskSprite : emptyMaskSprite;

        // Time mask
        maskWheel[currentTimeIndex].GetComponent<SpriteRenderer>().sprite =
            timeMask.isTimeMaskObtained ? timeMaskSprite : emptyMaskSprite;

        // Mirror mask
        maskWheel[currentMirrorIndex].GetComponent<SpriteRenderer>().sprite =
            mirrorMask.isMirrorMaskObtained ? mirrorMaskSprite : emptyMaskSprite;
    }

    public void TurnUIRight()
    {
        int temp = currentCloneIndex;
        currentCloneIndex = currentMirrorIndex;
        currentMirrorIndex = currentTimeIndex;
        currentTimeIndex = temp;
    }

    public void TurnUILeft()
    {
        int temp = currentCloneIndex;
        currentCloneIndex = currentTimeIndex;
        currentTimeIndex = currentMirrorIndex;
        currentMirrorIndex = temp;
    }
}
