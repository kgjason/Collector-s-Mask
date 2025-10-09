using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MirrorCloneMask : MonoBehaviour
{
    public bool isMirrorMaskObtained;
    public bool isMirrorMaskActive;
    public bool isMirrorWorldActive;
    public GameObject mirrorWorld;
    public GameObject mirrorWorldWall;
    public GameObject mirrorPlayer;
    public GameObject mirrorLevels;
    private void Start()
    {
        isMirrorWorldActive = false;
        DeactivateMirrorWorld();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isMirrorMaskActive)
        {
            isMirrorWorldActive = !isMirrorWorldActive;
            if (isMirrorWorldActive)
            {
                ActivateMirrorWorld();
            } else
            {
                DeactivateMirrorWorld();
            }
        }
    }
    public void ActivateMirrorWorld()
    {
        mirrorPlayer.SetActive(true);
        mirrorLevels.SetActive(true);
        mirrorWorld.SetActive(true);
        mirrorWorldWall.SetActive(true);
    }
    public void DeactivateMirrorWorld()
    {
        mirrorPlayer.SetActive(false);
        mirrorLevels.SetActive(false);
        mirrorWorld.SetActive(false);
        mirrorWorldWall.SetActive(false);
    }
}
