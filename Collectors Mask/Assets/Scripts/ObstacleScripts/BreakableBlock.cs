using System.Collections;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    public float breakDelay = 0.5f;
    public bool isBreaking = false;

    [Header("Invisible Block")]
    public GameObject invisibleBlockPrefab;
    [HideInInspector] public GameObject invisibleBlockClone;

    [Header("Time Control")]
    public TimeMask timeMask; // sahnedeki TimeMask referansý

    private SpriteRenderer sr;
    private Collider2D col;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        // Player objesini bul ve TimeMask componentini al
        if (timeMask == null)
        {
            PlayerMovement player = FindObjectOfType<PlayerMovement>();
            if (player != null)
                timeMask = player.GetComponent<TimeMask>();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Eðer zaman duruyorsa blok kýrýlmasýn
            if (timeMask != null && timeMask.isTimeStopped)
                return;

            if (!isBreaking)
                StartCoroutine(BreakBlock());
        }
    }

    private IEnumerator BreakBlock()
    {
        if (isBreaking)
            yield break;

        isBreaking = true;

        sr.color = Color.red;

        yield return new WaitForSeconds(breakDelay);

        gameObject.SetActive(false);

        if (invisibleBlockPrefab != null)
        {
            invisibleBlockClone = Instantiate(invisibleBlockPrefab, transform.position, transform.rotation);
        }
    }
}
