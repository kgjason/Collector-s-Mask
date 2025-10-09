using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;

    [Header("Components")]
    public SpriteRenderer spriteRenderer; // Player sprite
    public Animator animator;             // Animator Controller

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lastMoveDir = Vector2.down;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Input al
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (moveInput.sqrMagnitude > 1)
            moveInput.Normalize();

        bool isMoving = moveInput.sqrMagnitude > 0.01f;

        // Son yönü güncelle
        if (isMoving)
            lastMoveDir = moveInput;

        // Animator parametrelerini güncelle
        animator.SetBool("isMoving", isMoving);
        animator.SetFloat("moveX", isMoving ? moveInput.x : lastMoveDir.x);
        animator.SetFloat("moveY", isMoving ? moveInput.y : lastMoveDir.y);

        // FlipX artýk lastMoveDir üzerinden
        spriteRenderer.flipX = lastMoveDir.x < -0.1f;
    }

    private void FixedUpdate()
    {
        if (moveInput.sqrMagnitude < 0.01f)
            return;

        Vector2 targetPos = rb.position + moveInput * speed * Time.fixedDeltaTime;

        // Duvar çarpýþma kontrolü
        RaycastHit2D hit = Physics2D.CircleCast(rb.position, 0.1f, moveInput, speed * Time.fixedDeltaTime, LayerMask.GetMask("Wall"));
        if (hit.collider == null)
        {
            rb.MovePosition(targetPos);
        }
    }
    public Vector2 GetLastMoveDir()
    {
        return lastMoveDir;
    }

}
