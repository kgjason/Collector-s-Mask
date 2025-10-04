using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    public float breakDelay = 0.5f;
    private bool isBreaking = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y < -0.5f)
                {
                    StartCoroutine(BreakBlock());
                    break;
                }
            }
        }
    }

    private IEnumerator BreakBlock()
    {
        if (isBreaking) yield break;
        isBreaking = true;
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(breakDelay);

        gameObject.SetActive(false);
    }
}
