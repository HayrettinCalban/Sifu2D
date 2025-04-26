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
        FindFirstObjectByType<PlayerCombat>()?.AddKill();
        Destroy(gameObject);
    }
}