using System.Collections;
using UnityEngine;

public class CollectMask : MonoBehaviour
{
    public float interactRange = 1.5f;
    public MaskChanger maskChanger;
    public PlayerMovement player;
    public CloneMask cloneMask;
    public TimeMask timeMask;
    public MirrorCloneMask mirrorMask;

    [SerializeField] private GameObject cloneMaskSprite;
    [SerializeField] private GameObject timeMaskSprite;
    [SerializeField] private GameObject mirrorMaskSprite;

    public Vector3 teleportPoint = new Vector3(-5.51f, -0.5f, 0);

    private RetrySystem retrySystem;

    private void Awake()
    {
        player = GetComponent<PlayerMovement>();
        maskChanger = GetComponent<MaskChanger>();
        cloneMask = GetComponent<CloneMask>();
        timeMask = GetComponent<TimeMask>();
        mirrorMask = GetComponent<MirrorCloneMask>();

        retrySystem = FindObjectOfType<RetrySystem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRange);
            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Player")) continue;
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
        string name = maskObject.name.ToLower();

        if (name.Contains("clone"))
        {
            cloneMask.isCloneMaskObtained = true;
            cloneMask.isCloneMaskActive = true;
            Destroy(cloneMaskSprite);
        }
        else if (name.Contains("time"))
        {
            timeMask.isTimeMaskObtained = true;
            Destroy(timeMaskSprite);

            // Retry devre dýþý býrak
            if (retrySystem != null)
                retrySystem.IsEnabled = false;

            transform.position = teleportPoint;
        }
        else if (name.Contains("mirror"))
        {
            mirrorMask.isMirrorMaskObtained = true;
            Destroy(mirrorMaskSprite);

            // Retry devre dýþý býrak
            if (retrySystem != null)
                retrySystem.IsEnabled = false;

            transform.position = teleportPoint;
        }
        else
        {
            Destroy(maskObject);
            // game ending screen
        }

        maskChanger.UpdateUI();
        maskChanger.UpdateActiveMask();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
