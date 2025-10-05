using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public int currentIndex = 0;
    public Vector3[] nextAreaSpawnPos;
    public int areaCount;
    public bool isActive;
    public float interactRange = 1.5f;
    public PlayerMovement player;
    private void Start()
    {
        nextAreaSpawnPos = new Vector3[areaCount];
        isActive = false;
    }
    private void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= interactRange)
        {
            //press f UI ý belirecek
            isActive = true;
            if (Input.GetKeyDown(KeyCode.F))
            {
                Activate();
            }           
        }
    }
    public void Activate()
    {
        player.transform.position = nextAreaSpawnPos[currentIndex];
        currentIndex++;
    }
    public void Deactivate()
    {
        return;
    }
}
