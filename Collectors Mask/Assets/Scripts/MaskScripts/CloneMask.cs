using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneMask : MonoBehaviour
{
    public GameObject clonePrefab;
    public GameObject[] cloneArr;
    public bool isCloneMaskObtained;
    public bool isCloneMaskActive;
    private void Awake()
    {
        isCloneMaskObtained = false;
        cloneArr = new GameObject[1];
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isCloneMaskActive) 
        {
            SpawnClone();
        }
        if (Input.GetKeyDown(KeyCode.C) &&  isCloneMaskActive)
        {
            ChangePositions();
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
            Vector3 tempPos = cloneArr[0].transform.position;
            cloneArr[0].transform.position = transform.position;
            transform.position = tempPos;
        }     
    }
}
