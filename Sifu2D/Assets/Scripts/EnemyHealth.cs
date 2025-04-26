using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    public Image healthBarFill; // Inspector'dan atayacağın world space health bar'ın fill objesi

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(gameObject.name + " TakeDamage çağrıldı. Hasar: " + damage + " | Önceki Can: " + currentHealth); // DEBUG
        currentHealth -= damage;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
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

        // Animasyonun bitmesini beklemeden hemen yok etmek yerine, animasyon süresi kadar bekle
        Destroy(gameObject, 1.0f); // 1 saniye sonra yok et (animasyon süresine göre ayarla)
    }
}