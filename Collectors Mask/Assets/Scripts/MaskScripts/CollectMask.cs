using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMask : MonoBehaviour
{
    public float interactRange = 1.5f;
    private int maskIndex;
    public PlayerMovement player;
    public CloneMask cloneMask;
    public TimeMask timeMask;
    [SerializeField] private GameObject cloneMaskSprite;
    [SerializeField] private GameObject timeMaskSprite;
    public Vector3 teleportPoint;

    void Awake()
    {
        maskIndex = 0;
        player = GetComponent<PlayerMovement>();
        cloneMask = GetComponent<CloneMask>();
        timeMask = GetComponent<TimeMask>();
        teleportPoint = new Vector3(-5.51f, -0.5f, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(player.transform.position, interactRange);

            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Mask"))
                {
                    MaskCollected(hit.gameObject);
                    break;
                }
            }
        }
    }

    public void MaskCollected(GameObject maskObject)
    {
        if (maskIndex == 0)
        {
            cloneMask.isCloneMaskObtained = true;
            Destroy(cloneMaskSprite);
        }
        else if (maskIndex == 1)
        {
            timeMask.isTimeMaskObtained = true;
            Destroy(timeMaskSprite);
            transform.position = teleportPoint;
        }

        maskIndex++;

        Destroy(maskObject); // maskeyi ortadan kald�rmak istersen
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (player != null)
            Gizmos.DrawWireSphere(player.transform.position, interactRange);
    }
}
