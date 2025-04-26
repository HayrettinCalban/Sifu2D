using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    [Header("Ground Check Settings")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;


    private Rigidbody2D rb;
    private Animator anim; // Animator referansı eklendi

    private float horizontalMove = 0f;
    private bool isGrounded = false;
    private bool isFacingRight = true;
    private bool isDead = false;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // Animator başlatıldı
    }

    void Update()
    {

        if (isDead)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        horizontalMove = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        if (anim != null)
        {
            anim.SetFloat("Speed", Mathf.Abs(horizontalMove));
            anim.SetBool("isGrounded", isGrounded);
        }

        Flip();
    }

    void FixedUpdate()
    {
        if (isDead) return;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        rb.linearVelocity = new Vector2(horizontalMove * moveSpeed, rb.linearVelocity.y);
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        if (anim != null)
            anim.SetTrigger("Jump"); // Zıplama animasyonunu tetikle
    }
    void Flip()
    {
        if ((horizontalMove > 0 && !isFacingRight) || (horizontalMove < 0 && isFacingRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
    }


    public void Die()
    {
        if (isDead) return;
        isDead = true;
        if (anim != null)
            anim.SetTrigger("Dead"); // Ölüm animasyonunu tetikle
        Debug.Log("Player Died!");

        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Collider2D>().enabled = false;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}