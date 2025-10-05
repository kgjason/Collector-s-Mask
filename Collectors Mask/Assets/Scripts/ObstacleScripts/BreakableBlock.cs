using System.Collections;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    public float breakDelay = 0.5f;
    private bool isBreaking = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(BreakBlock());
        }
    }

    private IEnumerator BreakBlock()
    {
        if (isBreaking) yield break;
        isBreaking = true;

        // Görsel feedback
        GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(breakDelay);

        // Bloku kaldýr
        gameObject.SetActive(false);
    }
}
