using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Door : MonoBehaviour, IInteractable
{
    public int currentIndex = 0;
    public Vector3[] nextAreaSpawnPos;
    public int nextAreaCount = 20;
    public float interactRange = 1.5f;
    public PlayerMovement player;
    public RetrySystem retrySystem;
    public bool isUsed = false;

    private void Awake()
    {
        nextAreaSpawnPos = new Vector3[nextAreaCount];
    }

    private void Start()
    {
        // Starting area
        nextAreaSpawnPos[0] = new Vector3(-5.51f, -3f, 0);

        // RetrySystem spawnPoint'lerini elle atama
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
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= interactRange)
        {
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

            // Player'ý taþý
            player.transform.position = nextAreaSpawnPos[currentIndex];
            retrySystem.LevelPassed();
            currentIndex++;

            // Yeni bölüme geçildiðinde aktif olan nesneleri sýfýrla
            ResetActiveObjectsInScene();
        }
        isUsed = true;
    }

    private void ResetActiveObjectsInScene()
    {
        // BreakableBlock reset
        BreakableBlock[] blocks = FindObjectsOfType<BreakableBlock>();
        foreach (var block in blocks)
        {
            block.gameObject.SetActive(true);
            var sr = block.GetComponent<SpriteRenderer>();
            if (sr != null) sr.color = Color.white;
            block.isBreaking = false;

            if (block.invisibleBlockPrefab != null)
            {
                // Eðer klon sahnede varsa yok et
                Transform clone = block.transform.Find(block.invisibleBlockPrefab.name + "(Clone)");
                if (clone != null)
                    Destroy(clone.gameObject);
            }
        }

        // Button reset
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (var btn in buttons)
        {
            btn.isActive = false;
            btn.Deactivate();
        }

        // Lever reset
        Lever[] levers = FindObjectsOfType<Lever>();
        foreach (var lever in levers)
        {
            lever.isActive = false;
            lever.Deactivate();
        }

        // TimeMask reset
        TimeMask timeMask = FindObjectOfType<TimeMask>();
        if (timeMask != null)
        {
            timeMask.enabled = false; // aktifse devre dýþý býrak
        }

        // Mirror / diðer özel objeler de buraya eklenebilir
    }

    public void Deactivate()
    {
        // Kapama iþlemi yok
    }
}
