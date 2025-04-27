using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    public Image healthBarFill; // Inspector'dan atayacağın world space health bar'ın fill objesi
    public float flashDuration = 0.15f; // Kırmızıya dönüş süresi
    public float knockbackForce = 2f; // Inspector'dan ayarla
    public GameObject healthPickupPrefab; // Inspector'dan ata

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(gameObject.name + " TakeDamage çağrıldı. Hasar: " + damage + " | Önceki Can: " + currentHealth); // DEBUG
        currentHealth -= damage;
        UpdateHealthBar();

        if (spriteRenderer != null)
            StartCoroutine(FlashRed());

        // Knockback uygula
        ApplyKnockback();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }

    void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " Die() metodu çağrıldı."); // DEBUG

        // Ölüm animasyonunu tetikle
        Animator anim = GetComponent<Animator>();
        if (anim != null)
            anim.SetTrigger("Dead"); // Animator'da "Dead" trigger'ı olmalı

        FindFirstObjectByType<PlayerCombat>()?.AddKill();

        // Health pickup bırak
        if (healthPickupPrefab != null)
            Instantiate(healthPickupPrefab, transform.position, Quaternion.identity);

        // Animasyonun bitmesini beklemeden hemen yok etmek yerine, animasyon süresi kadar bekle
        Destroy(gameObject, 1.0f); // 1 saniye sonra yok et (animasyon süresine göre ayarla)
    }

    void ApplyKnockback()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Oyuncunun pozisyonuna göre yön belirle
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Vector2 direction = (transform.position - player.transform.position).normalized;
                rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }

    void Update()
    {
        if (transform.position.y < -10f)
        {
            FindFirstObjectByType<PlayerCombat>()?.AddKill();
            Destroy(gameObject);
        }
    }
}