using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneMask : MonoBehaviour
{
    public GameObject clonePrefab;
    public GameObject[] cloneArr;
    public bool isCloneMaskObtained;
    public bool isCloneMaskActive;

    [Header("Player Reference")]
    public PlayerMovement playerMovement; // Player'ýn son yönünü almak için

    private void Awake()
    {
        isCloneMaskObtained = false;
        cloneArr = new GameObject[1];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isCloneMaskActive)
        {
            SpawnClone();
        }
        if (Input.GetKeyDown(KeyCode.C) && isCloneMaskActive)
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

        // Clone instantiate
        cloneArr[0] = Instantiate(clonePrefab, transform.position, Quaternion.identity);

        // Player'ýn son baktýðý yönü clone'a ilet
        PlayerClone cloneScript = cloneArr[0].GetComponent<PlayerClone>();
        if (cloneScript != null && playerMovement != null)
        {
            cloneScript.Initialize(playerMovement.GetLastMoveDir());
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
