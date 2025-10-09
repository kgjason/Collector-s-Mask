using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorPlayerCloneMask : MonoBehaviour
{
    public GameObject clonePrefab;
    public GameObject[] cloneArr;

    [Header("References")]
    public CloneMask playerCloneMask;    // Player'ýn mask durumu
    public TimeMask playerTimeMask;      // Player'ýn time stop durumu
    public MirrorPlayerMovement mirrorPlayer; // Mirror Player referansý

    private bool isCloneMaskActive;

    private void Awake()
    {
        isCloneMaskActive = false;
        cloneArr = new GameObject[1];
    }

    private void Update()
    {
        // Mask aktif mi?
        isCloneMaskActive = playerCloneMask != null && playerCloneMask.isCloneMaskActive;

        if (!isCloneMaskActive || (playerTimeMask != null && playerTimeMask.isTimeStopped))
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnClone();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangePositions();
        }
    }

    public void SpawnClone()
    {
        if (cloneArr[0] != null)
        {
            Destroy(cloneArr[0]);
        }

        cloneArr[0] = Instantiate(clonePrefab, transform.position, Quaternion.identity);

        // Mirror Player'ýn lastMoveDir bilgisini al
        Vector2 dir = mirrorPlayer != null ? mirrorPlayer.GetLastMoveDir() : Vector2.zero;

        // Eðer hareket yönü sýfýrsa default saða bak
        if (dir == Vector2.zero)
            dir = Vector2.right;

        // Clone scriptini al ve yönü ayarla
        MirrorPlayerClone cloneScript = cloneArr[0].GetComponent<MirrorPlayerClone>();
        if (cloneScript != null)
        {
            cloneScript.Initialize(dir);
        }
    }


    public void ChangePositions()
    {
        if (cloneArr[0] != null)
        {
            Vector3 clonePos = cloneArr[0].transform.position;
            Vector3 playerPos = transform.position;
            cloneArr[0].transform.position = playerPos;
            transform.position = clonePos;
        }
    }
}
