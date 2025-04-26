using UnityEngine;

public class LineDamageEnemy : MonoBehaviour
{
    public float lineLength = 5f;
    public float damageInterval = 1.0f;
    public string playerTag = "Player";
    public LayerMask obstacleLayer;

    private float nextDamageTime;
    private SimplePatrol2D patrolScript;
    private bool playerInFront = false;

    void Start()
    {
        patrolScript = GetComponent<SimplePatrol2D>();
    }

    void Update()
    {
        if (Time.time >= nextDamageTime)
        {
            DealDamageInLine();
            nextDamageTime = Time.time + damageInterval;
        }

        if (patrolScript != null)
        {
            patrolScript.enabled = !playerInFront;
        }
    }

    void DealDamageInLine()
    {
        Vector2 direction = transform.localScale.x >= 0 ? transform.right : -transform.right;
        Vector2 origin = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, lineLength, obstacleLayer);

        // Ray çizgisi debug için
        if (hit.collider != null)
        {
            Debug.DrawLine(origin, hit.point, Color.red, 0.2f);
            Debug.Log("Raycast çarptı: " + hit.collider.name + " | Layer: " + LayerMask.LayerToName(hit.collider.gameObject.layer));
        }
        else
        {
            Debug.DrawLine(origin, origin + direction * lineLength, Color.green, 0.2f);
            Debug.Log("Raycast hiçbir şeye çarpmadı.");
        }

        Animator anim = GetComponent<Animator>();

        if (hit.collider != null && hit.collider.CompareTag(playerTag))
        {
            playerInFront = true;
            if (anim != null)
                anim.SetBool("isAttacking", true);

            PlayerHealth playerHealth = hit.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
        }
        else
        {
            playerInFront = false;
            if (anim != null)
                anim.SetBool("isAttacking", false);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Editörde de doğru yönü göstermek için scale kontrolü yap
        Vector2 direction = transform.localScale.x >= 0 ? transform.right : -transform.right;
        Vector2 origin = transform.position;

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(origin, origin + direction * lineLength);
    }
}