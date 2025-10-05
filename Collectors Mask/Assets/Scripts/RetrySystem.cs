using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetrySystem : MonoBehaviour
{
    public int puzzleIndex = 0;
    public Vector3[] spawnPoint;
    private int levelCount = 14;
    void Awake()
    {
        spawnPoint = new Vector3[levelCount];
        //clone levels
        spawnPoint[0] = new Vector3(-10.51f, -0.5f, 0);
        spawnPoint[1] = new Vector3(-26.51f, -0.5f, 0);
        spawnPoint[2] = new Vector3(-41.51f, 4.5f, 0);
        spawnPoint[3] = new Vector3(-66.51f, 4.5f, 0);
        //mirror levels
        spawnPoint[4] = new Vector3(-26.51f, -0.5f, 0);
        spawnPoint[5] = new Vector3(-26.51f, -0.5f, 0);
        spawnPoint[6] = new Vector3(-26.51f, -0.5f, 0);
        spawnPoint[7] = new Vector3(-26.51f, -0.5f, 0);
        spawnPoint[8] = new Vector3(-26.51f, -0.5f, 0);
        spawnPoint[9] = new Vector3(-26.51f, -0.5f, 0);
        spawnPoint[10] = new Vector3(-26.51f, -0.5f, 0);
        spawnPoint[11] = new Vector3(-26.51f, -0.5f, 0);
        spawnPoint[12] = new Vector3(-26.51f, -0.5f, 0);
        spawnPoint[13] = new Vector3(-26.51f, -0.5f, 0);

    }
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.R))
        {
            if (spawnPoint[puzzleIndex] != null)
            {
                transform.position = spawnPoint[puzzleIndex];
            }         
        } 
    }
    public void levelPassed()
    {
        puzzleIndex++;
    }
}
