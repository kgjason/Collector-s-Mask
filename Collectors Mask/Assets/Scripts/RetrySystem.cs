using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetrySystem : MonoBehaviour
{
    public int puzzleIndex = -2;
    public Vector3[] spawnPoint;
    public Vector3[] mirrorSpawnPoint;
    private int levelCount = 16;
    private int mirrorLevelCount = 4;

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
        mirrorSpawnPoint = new Vector3[mirrorLevelCount];
        //clone levels
        spawnPoint[0] = new Vector3(-10.51f, -0.5f, 0);
        spawnPoint[1] = new Vector3(-26.51f, -0.5f, 0);
        spawnPoint[2] = new Vector3(-41.51f, 4.5f, 0);
        spawnPoint[3] = new Vector3(-66.51f, 4.5f, 0);
        //time levels
        spawnPoint[4] = new Vector3(-0.3188838f, 0.3811346f, 0);//ana b lgeden sornaki yerdeki respawn noktas 
        spawnPoint[5] = new Vector3(14.49013f, 0.1809928f, 0);
        spawnPoint[6] = new Vector3(45.32136f, 0.1496218f, 0);
        spawnPoint[7] = new Vector3(70.68478f, 0.1277119f, 0);
        spawnPoint[8] = new Vector3(125.5103f, 0.2013657f, 0);
        spawnPoint[9] = new Vector3(169.1976f, 0.2164409f, 0);//mirror mask alma yeri. mirror mask oto main b lgeye    nl yor. Ana b lgede respawn noktas  yok. 
        //mirror levels real
        spawnPoint[10] = new Vector3(-5.394169f, 5.23184f, 0);//ana b lgeden sonraki yerdeki respawn noktas  -6.899.997
        spawnPoint[11] = new Vector3(-5.521762f, 18.69273f, 0);
        spawnPoint[12] = new Vector3(-5f, 35, 0);
        spawnPoint[13] = new Vector3(-5.502417f, 62.82272f, 0);
        spawnPoint[14] = new Vector3(-5.477401f, 86.57636f, 0);
        spawnPoint[15] = new Vector3(-5.576828f, 114f, 0);//collectors mask alma yeri. oyunun bitece i yer. buran n mirror hali yok.
        //mirror levels mirror spawns
        mirrorSpawnPoint[0] = new Vector3(-5.394169f + 6.899997f, 5.23184f, 0);
        mirrorSpawnPoint[1] = new Vector3(-5.521762f + 6.899997f, 18.69273f, 0);
        mirrorSpawnPoint[2] = new Vector3(-5.502417f + 6.899997f, 62.82272f, 0);
        mirrorSpawnPoint[3] = new Vector3(-5.477401f + 6.899997f, 86.57636f, 0);

        // Mirror objelerin orijinal pozisyonlar n  kaydet
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
        // Oyuncuyu spawn noktas na ta  
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
            // E er invisible klon varsa yok et
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
