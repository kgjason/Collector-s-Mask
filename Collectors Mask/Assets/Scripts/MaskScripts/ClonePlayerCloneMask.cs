using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonePlayerCloneMask : MonoBehaviour
{
    public GameObject clonePrefab;
    public GameObject[] cloneArr;
    public CloneMask playerCloneMask;
    public TimeMask playerTimeMask;
    private bool isCloneMaskObtained = true;
    private bool isCloneMaskActive;
    private void Awake()
    {
        isCloneMaskActive = false;
        cloneArr = new GameObject[1];
    }
    private void Update()
    {
        if (playerCloneMask.isCloneMaskActive == true)
        {
            isCloneMaskActive = true;
        } else
        {
            isCloneMaskActive = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isCloneMaskActive)
        {
            if (playerTimeMask.isTimeStopped == true)
            {
                return;
            }else
            {
                SpawnClone();
            }               
        }
        if (Input.GetKeyDown(KeyCode.C) && isCloneMaskActive)
        {
            if (playerTimeMask.isTimeStopped == true)
            {
                return;
            }
            else
            {
                ChangePositions();
            }
            
        }
    }
    public void SpawnClone()
    {
        if (cloneArr[0] != null)
        {
            Destroy(cloneArr[0]);
        }
        cloneArr[0] = Instantiate(clonePrefab, transform.position, Quaternion.identity);
    }
    public void ChangePositions()
    {
        if (cloneArr[0] != null)
        {
            // Player ve clone pozisyonlarýný swap et, clone spawn offset'ini dikkate al
            Vector3 clonePos = cloneArr[0].transform.position;
            Vector3 playerPos = transform.position;
            cloneArr[0].transform.position = playerPos;
            transform.position = clonePos;
        }
    }
}
