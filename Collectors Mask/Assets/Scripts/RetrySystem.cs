using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetrySystem : MonoBehaviour
{
    public int puzzleIndex = 0;
    public Vector3[] spawnPoint;
    private int levelCount = 14;

    [Header("Breakable Blocks")]
    public BreakableBlock[] breakableBlocks;

    [Header("Interactables")]
    public Button[] buttons;
    public Lever[] levers;

    [Header("Time Mask")]
    public TimeMask timeMask;

    [Header("Mirror Objects")]
    public Transform[] mirrorObjects;
    private Vector3[] originalMirrorPositions;

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

        // Mirror objelerin orijinal pozisyonlarýný kaydet
        if (mirrorObjects != null && mirrorObjects.Length > 0)
        {
            originalMirrorPositions = new Vector3[mirrorObjects.Length];
            for (int i = 0; i < mirrorObjects.Length; i++)
                originalMirrorPositions[i] = mirrorObjects[i].position;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RetryLevel();
        }
    }

    public void RetryLevel()
    {
        // Oyuncuyu spawn noktasýna taþý
        if (spawnPoint != null && spawnPoint.Length > puzzleIndex)
            transform.position = spawnPoint[puzzleIndex];

        ResetBreakableBlocks();
        ResetInteractables();
        ResetTimeMask();
        ResetMirrorObjects();
    }

    private void ResetBreakableBlocks()
    {
        if (breakableBlocks == null) return;

        foreach (var block in breakableBlocks)
        {
            // Eðer invisible klon varsa yok et
            if (block.invisibleBlockClone != null)
            {
                Destroy(block.invisibleBlockClone);
                block.invisibleBlockClone = null;
            }

            block.gameObject.SetActive(true);

            // Sprite rengini resetle
            var sr = block.GetComponent<SpriteRenderer>();
            if (sr != null) sr.color = Color.white;

            block.isBreaking = false;
        }
    }

    private void ResetInteractables()
    {
        if (buttons != null)
        {
            foreach (var btn in buttons)
            {
                btn.StopAllCoroutines();
                btn.isActive = false;
                btn.Deactivate();
            }
        }

        if (levers != null)
        {
            foreach (var lever in levers)
            {
                lever.isActive = false;
                lever.Deactivate();
            }
        }
    }

    private void ResetTimeMask()
    {
        if (timeMask != null && timeMask.isTimeStopped)
        {
            timeMask.UnfreezeWorld();
        }
    }

    private void ResetMirrorObjects()
    {
        if (mirrorObjects == null || originalMirrorPositions == null) return;

        for (int i = 0; i < mirrorObjects.Length; i++)
        {
            if (mirrorObjects[i] != null)
                mirrorObjects[i].position = originalMirrorPositions[i];
        }
    }

    public void LevelPassed()
    {
        puzzleIndex++;
    }
}
