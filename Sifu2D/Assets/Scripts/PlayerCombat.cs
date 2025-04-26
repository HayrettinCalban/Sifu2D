using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackCooldown = 0.5f; // Saldırı bekleme süresi
    public float attackRange = 1f; // Saldırı menzili
    public int attackDamage = 20; // Verilen hasar
    public LayerMask enemyLayers; // Düşmanların bulunduğu katman

    public Transform attackPoint; // Saldırı noktası
    public GameObject bulletPrefab;
    public Transform bulletPoint;
    public float bulletCooldown = 0.5f;
    private float nextBulletTime = 0f;

    public AudioClip katanaSound; // Katana sesi
    private AudioSource audioSource; // Ses kaynağı

    private bool isAttacking = false; // Saldırıyor mu?
    private Animator anim; // Animator referansı

    void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Saldırı
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartCoroutine(Attack());
        }

        // E tuşu ile mermi atma
        if (Input.GetKeyDown(KeyCode.E) && Time.time >= nextBulletTime)
        {
            Shoot();
            nextBulletTime = Time.time + bulletCooldown;
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;

        // Katana sesi çal (ses çalmıyorsa veya bitmişse)
        if (katanaSound != null && audioSource != null && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(katanaSound);
        }

        // Saldırı animasyonunu tetikle
        if (anim != null)
        {
            anim.SetTrigger("AttackTrigger");
            anim.SetBool("isAttacking", true); // Animator bool parametresi
        }

        // Saldırı yapılan düşmanları bul
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Düşmanlara hasar ver
            enemy.GetComponent<EnemyHealth>()?.TakeDamage(attackDamage);
        }

        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
        if (anim != null)
            anim.SetBool("isAttacking", false); // Saldırı bittiğinde kapat
    }

    void Shoot()
    {
        if (bulletPrefab != null && bulletPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletPoint.position, Quaternion.identity);
            Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            bullet.GetComponent<Bullet>().SetDirection(direction);
        }
    }

    // Saldırı menzilini sahnede göster
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
