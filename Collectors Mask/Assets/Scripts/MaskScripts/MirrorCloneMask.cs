using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorCloneMask : MonoBehaviour
{
    public GameObject mirrorClonePrefab;
    public GameObject[] mirrorCloneArr;
    public bool isMirrorMaskObtained;
    public bool isMirrorMaskActive;
    private void Start()
    {       
        mirrorCloneArr = new GameObject[1];
        //isCloneMaskActive = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isMirrorMaskActive)
        {
            SpawnClone();
        }
        if (Input.GetKeyDown(KeyCode.C) && isMirrorMaskActive)
        {
            ChangePositions();
        }
    }
    public void SpawnClone()
    {
        if (mirrorCloneArr[0] != null)
        {
            Destroy(mirrorCloneArr[0]);
        }
        mirrorCloneArr[0] = Instantiate(mirrorClonePrefab, transform.position, Quaternion.identity);
    }
    public void ChangePositions()
    {
        if (mirrorCloneArr[0] != null)
        {
            Vector3 tempPos = mirrorCloneArr[0].transform.position;
            mirrorCloneArr[0].transform.position = transform.position;
            transform.position = tempPos;
        }
    }
}
