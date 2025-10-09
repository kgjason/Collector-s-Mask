using UnityEngine;

public class PlayerClone : MonoBehaviour
{
    [Header("Components")]
    public SpriteRenderer spriteRenderer;

    [Header("Directional Sprites")]
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite rightSprite;

    /// <summary>
    /// Player�dan klon olu�turulurken �a�r�lacak
    /// </summary>
    /// <param name="lastDirection">Player'�n son bakt��� y�n</param>
    public void Initialize(Vector2 lastDirection)
    {
        if (Mathf.Abs(lastDirection.x) > Mathf.Abs(lastDirection.y))
        {
            // Sa� veya sol
            spriteRenderer.sprite = rightSprite;
            spriteRenderer.flipX = lastDirection.x < 0; // sola bak�yorsa flip
        }
        else
        {
            // Yukar� veya a�a��
            spriteRenderer.flipX = false; // yukar�/a�a��da flip yok
            if (lastDirection.y > 0)
                spriteRenderer.sprite = upSprite;
            else
                spriteRenderer.sprite = downSprite;
        }
    }
}
