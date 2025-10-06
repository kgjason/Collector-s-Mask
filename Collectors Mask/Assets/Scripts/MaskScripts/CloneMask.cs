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
            // Player ve clone pozisyonlarýný swap et, clone spawn offset'ini dikkate al
            Vector3 clonePos = cloneArr[0].transform.position;
            Vector3 playerPos = transform.position;

            // Swap yaparken clone'un spawn offset'ini ekle
            cloneArr[0].transform.position = playerPos;
            transform.position = clonePos;
        }
    }
}
