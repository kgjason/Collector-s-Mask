using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneDoor : MonoBehaviour
{
    public CloneMask cloneMask;
    public RetrySystem retrySystem;
    public float interactRange = 1.5f;
    public PlayerMovement player;
    public bool isUsed = false;

    private void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= interactRange && Input.GetKeyDown(KeyCode.F) && cloneMask.isCloneMaskObtained)
        {
            Activate();
        }
    }

    public void Activate()
    {
        if (!isUsed)
        {
            // Oyuncuyu spawn noktasýna taþý
            player.transform.position = new Vector3(-5.51f, -3f, 0);

            // Level progression
            retrySystem.LevelPassed();

            // Sahneyi resetle
            ResetSceneViaRetrySystem();

            isUsed = true;
        }
    }

    private void ResetSceneViaRetrySystem()
    {
        if (retrySystem == null) return;

        retrySystem.IsRetrying = true;

        if (retrySystem.breakableBlocks != null)
        {
            foreach (var block in retrySystem.breakableBlocks)
            {
                if (block != null)
                    block.ResetBlock();
            }
        }

        retrySystem.ResetInteractables();
        retrySystem.ResetTimeMask();
        retrySystem.ResetMirrorObjects();
        retrySystem.DestroyAllClones();

        retrySystem.IsRetrying = false;
    }
}
