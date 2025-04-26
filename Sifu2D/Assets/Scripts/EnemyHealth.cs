using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(gameObject.name + " TakeDamage çağrıldı. Hasar: " + damage + " | Önceki Can: " + currentHealth); // DEBUG
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " Die() metodu çağrıldı."); // DEBUG
        Destroy(gameObject);
    }
}