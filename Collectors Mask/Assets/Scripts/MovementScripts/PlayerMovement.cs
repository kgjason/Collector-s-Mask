using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Vector2 lastDirection = Vector2.down;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(x, y).normalized;

        bool moving = moveInput != Vector2.zero;
        animator.SetBool("isRunning", moving);

        if (moving)
        {
            lastDirection = moveInput;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                animator.SetBool("isRight", x > 0);
                animator.SetBool("isLeft", x < 0);
                animator.SetBool("isUp", false);
                animator.SetBool("isDown", false);

                spriteRenderer.flipX = animator.GetBool("isLeft");
            }
            else
            {
                animator.SetBool("isRight", false);
                animator.SetBool("isLeft", false);
                animator.SetBool("isUp", y > 0);
                animator.SetBool("isDown", y < 0);

                spriteRenderer.flipX = false;
            }
        }
        else
        {
            animator.SetBool("isRight", false);
            animator.SetBool("isLeft", false);
            animator.SetBool("isUp", false);
            animator.SetBool("isDown", false);

            spriteRenderer.flipX = lastDirection.x < 0;
        }
    }

    private void FixedUpdate()
    {
        Vector2 targetPos = (rb.position + moveInput * speed * Time.fixedDeltaTime);

        RaycastHit2D hit = Physics2D.CircleCast(rb.position, 0.1f, moveInput, speed * Time.fixedDeltaTime, LayerMask.GetMask("Wall"));
        if (hit.collider != null)
        {
            // Duvar varsa hareket etme
            return;
        }

        rb.MovePosition(targetPos);
    }
}
