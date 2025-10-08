using System.Collections;
using UnityEngine;

public class TimeDoor : MonoBehaviour
{
    public TimeMask timeMask;
    public RetrySystem retrySystem;
    public float interactRange = 1.5f;
    public PlayerMovement player;
    public bool isUsed = false;

    private void Update()
    {
        if (player == null || timeMask == null || retrySystem == null) return;

        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= interactRange && Input.GetKeyDown(KeyCode.F) && timeMask.isTimeMaskObtained)
        {
            Activate();
        }
    }

    public void Activate()
    {
        if (isUsed) return;

        // Player'ý maske odasýndan sonraki baþlangýç noktasýna ýþýnla
        player.transform.position = new Vector3(-0.3188838f, 0.3811346f, 0);

        // Puzzle indexi bir sonraki level olarak güncelle
        retrySystem.LevelPassed();

        // Scene reset
        ResetSceneViaRetrySystem();

        // Artýk retry sistemi aktif
        retrySystem.IsEnabled = true;

        isUsed = true;
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
