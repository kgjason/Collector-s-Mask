using UnityEngine;

public class MaskChanger : MonoBehaviour
{
    [Header("Mask UI Slots")]
    public GameObject[] maskWheel = new GameObject[3];

    [Header("Mask Sprites")]
    public Sprite emptyMaskSprite;
    public Sprite cloneMaskSprite;
    public Sprite timeMaskSprite;
    public Sprite mirrorMaskSprite;

    [Header("Mask Components (Player üstünde)")]
    public TimeMask timeMask;
    public CloneMask cloneMask;
    public MirrorCloneMask mirrorMask;

    private int currentCloneIndex = 0;
    private int currentTimeIndex = 1;
    private int currentMirrorIndex = 2;

    private void Awake()
    {
        timeMask = GetComponent<TimeMask>();
        cloneMask = GetComponent<CloneMask>();
        mirrorMask = GetComponent<MirrorCloneMask>();
    }

    private void Start()
    {
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
        cloneMask.isCloneMaskActive = false;
        timeMask.isTimeMaskActive = false;
        mirrorMask.isMirrorMaskActive = false;

        if (currentCloneIndex == 1 && cloneMask.isCloneMaskObtained)
            cloneMask.isCloneMaskActive = true;
        else if (currentTimeIndex == 1 && timeMask.isTimeMaskObtained)
            timeMask.isTimeMaskActive = true;
        else if (currentMirrorIndex == 1 && mirrorMask.isMirrorMaskObtained)
            mirrorMask.isMirrorMaskActive = true;
    }

    public void UpdateUI()
    {
        maskWheel[currentCloneIndex].GetComponent<SpriteRenderer>().sprite =
            cloneMask.isCloneMaskObtained ? cloneMaskSprite : emptyMaskSprite;

        maskWheel[currentTimeIndex].GetComponent<SpriteRenderer>().sprite =
            timeMask.isTimeMaskObtained ? timeMaskSprite : emptyMaskSprite;

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
