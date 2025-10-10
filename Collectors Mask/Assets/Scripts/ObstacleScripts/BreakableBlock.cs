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
    public TimeMask timeMask;

    private SpriteRenderer sr;
    private Collider2D col;
    private Animator animator;
    private Coroutine breakCoroutine;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        if (timeMask == null)
        {
            PlayerMovement player = FindObjectOfType<PlayerMovement>();
            if (player != null)
                timeMask = player.GetComponent<TimeMask>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (timeMask != null && timeMask.isTimeStopped)
                return;

            if (!isBreaking && breakCoroutine == null)
                breakCoroutine = StartCoroutine(BreakBlock());
        }
    }

    private IEnumerator BreakBlock()
    {
        isBreaking = true;

        // Animasyon baþlasýn
        if (animator != null)
            animator.SetBool("isBreaking", true);

        yield return new WaitForSeconds(breakDelay);

        // Retry sýrasýnda kýrýlmayý iptal et
        RetrySystem retrySystem = FindObjectOfType<RetrySystem>();
        if (retrySystem != null && retrySystem.IsRetrying)
        {
            isBreaking = false;
            if (animator != null)
                animator.SetBool("isBreaking", false);
            breakCoroutine = null;
            yield break;
        }

        gameObject.SetActive(false);

        if (invisibleBlockPrefab != null)
        {
            invisibleBlockClone = Instantiate(invisibleBlockPrefab, transform.position, transform.rotation);
        }

        breakCoroutine = null;
    }

    public void ResetBlock()
    {
        if (breakCoroutine != null)
        {
            StopCoroutine(breakCoroutine);
            breakCoroutine = null;
        }

        if (invisibleBlockClone != null)
        {
            Destroy(invisibleBlockClone);
            invisibleBlockClone = null;
        }

        isBreaking = false;

        if (animator != null)
            animator.SetBool("isBreaking", false);

        gameObject.SetActive(true);
    }
}
