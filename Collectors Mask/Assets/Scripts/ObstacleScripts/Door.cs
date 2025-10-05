using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public int currentIndex = 0;
    public Vector3[] nextAreaSpawnPos;
    public int nextAreaCount = 20;
    public float interactRange = 1.5f;
    public PlayerMovement player;
    public RetrySystem retrySystem;
    public bool isUsed = false;

    public Vector3 forward = Vector3.right;

    private void Awake()
    {
        nextAreaSpawnPos = new Vector3[nextAreaCount];
    }
    private void Start()
    {
        nextAreaSpawnPos[0] = new Vector3(-5.51f, -3f, 0);
        nextAreaSpawnPos[1] = retrySystem.spawnPoint[0];
        nextAreaSpawnPos[2] = retrySystem.spawnPoint[1];
        nextAreaSpawnPos[3] = retrySystem.spawnPoint[2];
        nextAreaSpawnPos[4] = retrySystem.spawnPoint[3];
        nextAreaSpawnPos[5] = retrySystem.spawnPoint[4];
        nextAreaSpawnPos[6] = retrySystem.spawnPoint[5];
        nextAreaSpawnPos[7] = retrySystem.spawnPoint[6];
        nextAreaSpawnPos[8] = retrySystem.spawnPoint[7];
        nextAreaSpawnPos[9] = retrySystem.spawnPoint[8];
        nextAreaSpawnPos[10] = retrySystem.spawnPoint[9];
        nextAreaSpawnPos[11] = retrySystem.spawnPoint[10];
        nextAreaSpawnPos[12] = retrySystem.spawnPoint[11];
        nextAreaSpawnPos[13] = retrySystem.spawnPoint[12];
        nextAreaSpawnPos[14] = retrySystem.spawnPoint[13];


        //starting area doors
        
        
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= interactRange)
        {
            // Press F UI gösterebilirsin
            if (Input.GetKeyDown(KeyCode.F))
            {
                Activate();
            }
        }
    }

    public void Activate()
    {
        if (!isUsed)
        {
            if (currentIndex >= nextAreaSpawnPos.Length) return;

            player.transform.position = nextAreaSpawnPos[currentIndex];
            retrySystem.levelPassed();
            currentIndex++;
        }     
        isUsed = true;
    }

    public void Deactivate()
    {
        return;
    }
}
