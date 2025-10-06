using System.Collections;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    public float breakDelay = 0.5f;
    public bool isBreaking = false;

    [Header("Invisible Block")]
    public GameObject invisibleBlockPrefab; // sahnede prefab olarak ekle
    [HideInInspector] public GameObject invisibleBlockClone; // instantiate edilen blok referansý

    private SpriteRenderer sr;
    private Collider2D col;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
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
            // instantiate edilen klonu referans olarak sakla
            invisibleBlockClone = Instantiate(invisibleBlockPrefab, transform.position, transform.rotation);
        }
    }
}
