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
    /// Player’dan klon oluþturulurken çaðrýlacak
    /// </summary>
    /// <param name="lastDirection">Player'ýn son baktýðý yön</param>
    public void Initialize(Vector2 lastDirection)
    {
        if (Mathf.Abs(lastDirection.x) > Mathf.Abs(lastDirection.y))
        {
            // Sað veya sol
            spriteRenderer.sprite = rightSprite;
            spriteRenderer.flipX = lastDirection.x < 0; // sola bakýyorsa flip
        }
        else
        {
            // Yukarý veya aþaðý
            spriteRenderer.flipX = false; // yukarý/aþaðýda flip yok
            if (lastDirection.y > 0)
                spriteRenderer.sprite = upSprite;
            else
                spriteRenderer.sprite = downSprite;
        }
    }
}
