using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RetrySystem : MonoBehaviour
{
    public int puzzleIndex = -2;
    public Vector3[] spawnPoint;
    public Vector3[] mirrorSpawnPoint;
    private int levelCount = 16;
    private int mirrorLevelCount = 5;

    [Header("References")]
    public PlayerMovement player;
    public MirrorPlayerMovement mirrorPlayer;

    [Header("Breakable Blocks")]
    public BreakableBlock[] breakableBlocks;

    [Header("Time Mask")]
    public TimeMask timeMask;

    [Header("Mirror Objects")]
    public Transform[] mirrorObjects;
    private Vector3[] originalMirrorPositions;

    public bool IsRetrying { get; set; } = false;
    public bool IsEnabled { get; set; } = true;

    void Awake()
    {
        spawnPoint = new Vector3[levelCount];
        mirrorSpawnPoint = new Vector3[mirrorLevelCount];

        // Spawn noktalarýný buraya ekle
        spawnPoint[0] = new Vector3(-10.51f, -0.5f, 0);
        spawnPoint[1] = new Vector3(-26.51f, -0.5f, 0);
        spawnPoint[2] = new Vector3(-41.51f, 4.5f, 0);
        spawnPoint[3] = new Vector3(-66.51f, 4.5f, 0);
        spawnPoint[4] = new Vector3(-0.3188838f, 0.3811346f, 0);
        spawnPoint[5] = new Vector3(14.49013f, 0.1809928f, 0);
        spawnPoint[6] = new Vector3(45.32136f, 0.1496218f, 0);
        spawnPoint[7] = new Vector3(70.68478f, 0.1277119f, 0);
        spawnPoint[8] = new Vector3(125.5103f, 0.2013657f, 0);
        spawnPoint[9] = new Vector3(169.1976f, 0.2164409f, 0);
        spawnPoint[10] = new Vector3(-5.394169f, 5.23184f, 0);
        spawnPoint[11] = new Vector3(-5.521762f, 18.69273f, 0);
        spawnPoint[12] = new Vector3(-5f, 35, 0);
        spawnPoint[13] = new Vector3(-5.502417f, 62.82272f, 0);
        spawnPoint[14] = new Vector3(-5.477401f, 86.57636f, 0);
        spawnPoint[15] = new Vector3(-5.576828f, 114f, 0);

        mirrorSpawnPoint[0] = new Vector3(-5.394169f + 6.899997f, 5.23184f, 0);
        mirrorSpawnPoint[1] = new Vector3(-5.521762f + 6.899997f, 18.69273f, 0);
        mirrorSpawnPoint[2] = new Vector3(-5f + 6.899997f, 35, 0);
        mirrorSpawnPoint[3] = new Vector3(-5.502417f + 6.899997f, 62.82272f, 0);
        mirrorSpawnPoint[4] = new Vector3(-5.477401f + 6.899997f, 86.57636f, 0);

        if (mirrorObjects != null && mirrorObjects.Length > 0)
        {
            originalMirrorPositions = new Vector3[mirrorObjects.Length];
            for (int i = 0; i < mirrorObjects.Length; i++)
                originalMirrorPositions[i] = mirrorObjects[i].position;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && puzzleIndex >= 0)
            RetryLevel();
    }

    public void RetryLevel()
    {
        if (!IsEnabled || IsRetrying) return;

        IsRetrying = true;

        int originalIndex = puzzleIndex;

        // Player pozisyonuna göre puzzleIndex ayarlamasý (opsiyonel)
        if (puzzleIndex > 0 && player != null)
        {
            float dist = Vector3.Distance(player.transform.position, spawnPoint[puzzleIndex]);
            if (dist > 10f)
                puzzleIndex--;
        }

        // Player reset
        if (player != null && spawnPoint != null && puzzleIndex >= 0 && puzzleIndex < spawnPoint.Length)
        {
            player.transform.position = spawnPoint[puzzleIndex];
            var rb = player.GetComponent<Rigidbody2D>();
            if (rb != null) rb.velocity = Vector2.zero;
        }

        // Mirror reset
        if (mirrorPlayer != null && puzzleIndex >= 10 && mirrorSpawnPoint != null)
        {
            int mirrorIndex = Mathf.Clamp(puzzleIndex - 10, 0, mirrorSpawnPoint.Length - 1);
            mirrorPlayer.transform.position = mirrorSpawnPoint[mirrorIndex];
            var rbm = mirrorPlayer.GetComponent<Rigidbody2D>();
            if (rbm != null) rbm.velocity = Vector2.zero;
        }

        // Breakables, masks, mirror objeleri ve klonlar
        if (breakableBlocks != null)
        {
            foreach (var block in breakableBlocks)
                block?.ResetBlock();
        }

        ResetInteractables();
        ResetTimeMask();
        ResetMirrorObjects();
        DestroyAllClones();

        puzzleIndex = originalIndex;

        IsRetrying = false;
    }

    public void ResetInteractables()
    {
        var interactables = FindObjectsOfType<MonoBehaviour>(true).OfType<IInteractable>().ToArray();
        foreach (var obj in interactables)
            obj?.Deactivate();
    }

    public void ResetTimeMask()
    {
        if (timeMask == null) return;
        if (timeMask.isTimeStopped) timeMask.UnfreezeWorld();
        if (!timeMask.enabled) timeMask.enabled = true;
    }

    public void ResetMirrorObjects()
    {
        if (mirrorObjects == null || originalMirrorPositions == null) return;
        for (int i = 0; i < mirrorObjects.Length; i++)
            if (mirrorObjects[i] != null)
                mirrorObjects[i].position = originalMirrorPositions[i];
    }

    public void DestroyAllClones()
    {
        var clones = GameObject.FindGameObjectsWithTag("PlayerClone");
        foreach (var c in clones) Destroy(c);
    }

    public void LevelPassed()
    {
        puzzleIndex++;
        if (puzzleIndex >= spawnPoint.Length)
            puzzleIndex = spawnPoint.Length - 1;
    }
}
