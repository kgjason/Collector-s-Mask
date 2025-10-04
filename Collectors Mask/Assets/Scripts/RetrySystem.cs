using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetrySystem : MonoBehaviour
{
    public int puzzleIndex = 0;
    public Vector3[] spawnPoint;
    public int levelCount;
    void Start()
    {
        spawnPoint = new Vector3[levelCount];
    }
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = spawnPoint[puzzleIndex];
        } 
    }
    public void levelPassed()
    {
        puzzleIndex++;
    }
}
