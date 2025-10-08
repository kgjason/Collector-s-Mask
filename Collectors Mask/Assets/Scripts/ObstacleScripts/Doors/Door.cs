using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public int currentIndex = 0;
    public Vector3[] nextAreaSpawnPos;
    public int nextAreaCount = 20;
    public float interactRange = 1.5f;

    [Header("References")]
    public MirrorPlayerMovement mirrorPlayer;
    public PlayerMovement player;
    public RetrySystem retrySystem;

    public bool isUsed = false;

    private void Awake()
    {
        nextAreaSpawnPos = new Vector3[nextAreaCount];
    }

    private void Start()
    {
        // starting area
        nextAreaSpawnPos[0] = new Vector3(-5.51f, -3f, 0);

        if (retrySystem == null)
        {
            Debug.LogError("Door: retrySystem reference missing!");
            return;
        }

        int max = Mathf.Min(retrySystem.spawnPoint.Length, nextAreaCount - 1);
        for (int i = 0; i < max; i++)
            nextAreaSpawnPos[i + 1] = retrySystem.spawnPoint[i];
    }

    private void Update()
    {
        if (player == null) return;
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= interactRange && Input.GetKeyDown(KeyCode.F))
        {
            Activate();
        }
    }

    public void Activate()
{
    if (!isUsed)
    {
        // Oyuncuyu spawn noktasýna taþý
        player.transform.position = nextAreaSpawnPos[currentIndex];

        // Mirror player
        if (mirrorPlayer != null && retrySystem != null)
        {
            int spawnPointIndex = (currentIndex == 0) ? -1 : currentIndex - 1;
            if (spawnPointIndex >= 10 && retrySystem.mirrorSpawnPoint != null)
            {
                int mirrorIndex = Mathf.Clamp(spawnPointIndex - 10, 0, retrySystem.mirrorSpawnPoint.Length - 1);
                mirrorPlayer.transform.position = retrySystem.mirrorSpawnPoint[mirrorIndex];
            }
        }

        // Level progression: puzzleIndex’i kapýnýn currentIndex’i ile eþitle
        if (retrySystem != null)
            retrySystem.puzzleIndex = Mathf.Max(retrySystem.puzzleIndex, currentIndex - 1); // kapý geçiþine göre güncelle

        currentIndex++;

        // Scene reset
        ResetSceneViaRetrySystem();

        isUsed = true;
    }
}

    private IEnumerator AdvanceLevelAfterDelay()
    {
        yield return new WaitForEndOfFrame();
        if (retrySystem != null) retrySystem.LevelPassed();
        currentIndex++;
    }

    private void ResetSceneViaRetrySystem()
    {
        if (retrySystem == null) return;
        retrySystem.IsRetrying = true;

        if (retrySystem.breakableBlocks != null)
        {
            foreach (var block in retrySystem.breakableBlocks)
                block?.ResetBlock();
        }

        retrySystem.ResetInteractables();
        retrySystem.ResetTimeMask();
        retrySystem.ResetMirrorObjects();
        retrySystem.DestroyAllClones();

        retrySystem.IsRetrying = false;
    }

    public void Deactivate() { }
}
