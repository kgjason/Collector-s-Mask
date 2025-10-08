using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorDoor : MonoBehaviour
{
    public MirrorCloneMask mirrorMask;
    public RetrySystem retrySystem;
    public float interactRange = 1.5f;
    public MirrorPlayerMovement mirrorPlayer;
    public GameObject[] invisibleBlock;
    public PlayerMovement player;
    public bool isUsed = false;

    private void Update()
    {
        if (player == null || mirrorMask == null || retrySystem == null) return;

        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= interactRange && Input.GetKeyDown(KeyCode.F) && mirrorMask.isMirrorMaskObtained)
        {
            Activate();
        }
    }

    public void Activate()
    {
        if (isUsed) return;

        Vector3 playerPos = new Vector3(-5.394169f, 5.23184f, 0);
        Vector3 mirrorPos = new Vector3(-5.394169f + 6.899997f, 5.23184f, 0);

        if (player != null) player.transform.position = playerPos;
        if (mirrorPlayer != null) mirrorPlayer.transform.position = mirrorPos;

        if (invisibleBlock != null)
        {
            for (int i = 0; i < invisibleBlock.Length; i++)
            {
                if (invisibleBlock[i] != null)
                    Destroy(invisibleBlock[i]);
            }
        }

        retrySystem.LevelPassed();

        // Scene reset
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

        // Artýk retry sistemi aktif
        retrySystem.IsEnabled = true;

        isUsed = true;
    }
}
