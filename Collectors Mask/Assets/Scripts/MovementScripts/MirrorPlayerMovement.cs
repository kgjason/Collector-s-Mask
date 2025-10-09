using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MirrorPlayerMovement : MonoBehaviour, ITimeFreezable
{
    [Header("Movement Settings")]
    public float speed = 5f;

    [Header("Components")]
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public TimeMask timeMask;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lastMoveDir = Vector2.down;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (timeMask != null && timeMask.isTimeStopped)
        {
            FreezeTime();
            return;
        }
        else
        {
            UnfreezeTime();
        }

        // Input al (X tersine çevrildi)
        moveInput.x = Input.GetAxisRaw("Horizontal") * -1;
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (moveInput.sqrMagnitude > 1)
            moveInput.Normalize();

        bool isMoving = moveInput.sqrMagnitude > 0.01f;

        if (isMoving)
            lastMoveDir = moveInput;

        // Animator parametreleri
        animator.SetBool("isMoving", isMoving);
        animator.SetFloat("moveX", isMoving ? moveInput.x : lastMoveDir.x);
        animator.SetFloat("moveY", isMoving ? moveInput.y : lastMoveDir.y);

        // FlipX
        spriteRenderer.flipX = lastMoveDir.x < -0.1f;
    }

    private void FixedUpdate()
    {
        if (moveInput.sqrMagnitude < 0.01f)
            return;

        Vector2 targetPos = rb.position + moveInput * speed * Time.fixedDeltaTime;

        // Basit duvar çarpýþma kontrolü
        RaycastHit2D hit = Physics2D.CircleCast(rb.position, 0.1f, moveInput, speed * Time.fixedDeltaTime, LayerMask.GetMask("Wall"));
        if (hit.collider == null)
        {
            rb.MovePosition(targetPos);
        }
    }

    public void FreezeTime()
    {
        speed = 0;
        if (animator != null)
            animator.speed = 0f;  // Animasyonu durdur
    }

    public void UnfreezeTime()
    {
        speed = 5;
        if (animator != null)
            animator.speed = 1f;  // Animasyonu tekrar baþlat
    }
    public Vector2 GetLastMoveDir()
    {
        return lastMoveDir;
    }

}
