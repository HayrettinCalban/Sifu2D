using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public float attackRange = 5f;
    public float attackInterval = 1.0f;
    public int attackDamage = 1;
    public string playerTag = "Player";

    [Header("References")]
    public LayerMask attackLayers; // Katmanları belirlemek için
    private Animator anim;
    private float nextAttackTime;

    [Header("Debug")]
    public bool showDebugRay = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            TryAttack();
            nextAttackTime = Time.time + attackInterval;
        }
    }

    private void TryAttack()
    {
        // Yönü belirle (karakterin scale'ine göre)
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Vector2 origin = transform.position;

        // Raycast ile hedefi kontrol et
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, attackRange, attackLayers);

        if (showDebugRay)
        {
            Debug.DrawLine(origin, hit.collider != null ? hit.point : origin + direction * attackRange, hit.collider != null ? Color.red : Color.green, 0.2f);
        }

        // Eğer bir hedef varsa
        if (hit.collider != null && hit.collider.CompareTag(playerTag))
        {
            Attack(hit.collider);
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }
    }

    private void Attack(Collider2D target)
    {
        // Animasyonu oynat
        anim.SetBool("isAttacking", true);

        // Hasar ver
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    public void EndAttackAnim()
    {
        if (anim != null)
            anim.SetBool("isAttacking", false);
    }

    private void OnDrawGizmosSelected()
    {
        if (!showDebugRay) return;

        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Vector2 origin = transform.position;

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(origin, origin + direction * attackRange);
    }
}
